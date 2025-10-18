using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace FindAndReplace.App
{
    public class FormData
    {
        private const string ConfigFileName = "findandreplace.settings.json";
        private const int SaveRetryCount = 10;
        private const int SaveRetryDelayMilliseconds = 200;

        private static string GetConfigFilePath()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
            return Path.Combine(baseDirectory, ConfigFileName);
        }

        private static JavaScriptSerializer CreateSerializer()
        {
            return new JavaScriptSerializer();
        }

        public bool IsFindOnly { get; set; }
        public string Dir { get; set; } = string.Empty;
        public bool IncludeSubDirectories { get; set; }
        public string FileMask { get; set; } = string.Empty;
        public string ExcludeFileMask { get; set; } = string.Empty;
        public string ExcludeDir { get; set; } = string.Empty;
        public string FindText { get; set; } = string.Empty;
        public bool IsCaseSensitive { get; set; }
        public bool IsRegEx { get; set; }
        public bool SkipBinaryFileDetection { get; set; }
        public bool ShowEncoding { get; set; }
        public bool IncludeFilesWithoutMatches { get; set; }
        public string ReplaceText { get; set; } = string.Empty;
        public bool UseEscapeChars { get; set; }
        public string FileEncoding { get; set; } = string.Empty;
        public bool IsKeepModifiedDate { get; set; }
        public bool IsFirstTime { get; set; } = true;

        public void SaveSetting()
        {
            var path = GetConfigFilePath();
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var serializer = CreateSerializer();
            var payload = serializer.Serialize(this);
            Exception lastError = null;

            for (var attempt = 0; attempt < SaveRetryCount; attempt++)
            {
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                    {
                        writer.Write(payload);
                    }

                    lastError = null;
                    break;
                }
                catch (IOException ex)
                {
                    lastError = ex;
                    Thread.Sleep(SaveRetryDelayMilliseconds);
                }
                catch (UnauthorizedAccessException ex)
                {
                    lastError = ex;
                    Thread.Sleep(SaveRetryDelayMilliseconds);
                }
            }

            if (lastError != null)
            {
                throw new IOException($"Failed to write settings file '{path}' after {SaveRetryCount} attempts.", lastError);
            }
        }

        public void LoadSetting()
        {
            var path = GetConfigFilePath();
            if (!File.Exists(path))
            {
                IsFirstTime = true;
                return;
            }

            try
            {
                IsFirstTime = false;
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var json = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        IsFirstTime = true;
                        return;
                    }

                    var serializer = CreateSerializer();
                    var data = serializer.Deserialize<FormData>(json);
                    if (data == null)
                    {
                        IsFirstTime = true;
                        return;
                    }

                    CopyFrom(data);
                }
            }
            catch (InvalidOperationException)
            {
                IsFirstTime = true;
            }
            catch (IOException)
            {
                IsFirstTime = true;
            }
            catch (UnauthorizedAccessException)
            {
                IsFirstTime = true;
            }
        }

        private void CopyFrom(FormData data)
        {
            IsFindOnly = data.IsFindOnly;
            Dir = data.Dir ?? string.Empty;
            IncludeSubDirectories = data.IncludeSubDirectories;
            FileMask = data.FileMask ?? string.Empty;
            ExcludeFileMask = data.ExcludeFileMask ?? string.Empty;
            ExcludeDir = data.ExcludeDir ?? string.Empty;
            FindText = data.FindText ?? string.Empty;
            IsCaseSensitive = data.IsCaseSensitive;
            IsRegEx = data.IsRegEx;
            SkipBinaryFileDetection = data.SkipBinaryFileDetection;
            ShowEncoding = data.ShowEncoding;
            IncludeFilesWithoutMatches = data.IncludeFilesWithoutMatches;
            ReplaceText = data.ReplaceText ?? string.Empty;
            UseEscapeChars = data.UseEscapeChars;
            FileEncoding = data.FileEncoding ?? string.Empty;
            IsKeepModifiedDate = data.IsKeepModifiedDate;
        }
    }
}
