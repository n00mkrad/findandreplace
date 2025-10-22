using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FindAndReplace
{
    public class ProcessorEventArgs<TResultItem> : EventArgs where TResultItem : ResultItem
    {
        public TResultItem ResultItem { get; set; }
        public Stats Stats { get; set; }
        public Status Status { get; set; }
        public bool IsSilent { get; set; }

        public ProcessorEventArgs(TResultItem resultItem, Stats stats, Status status, bool isSilent = false)
        {
            ResultItem = resultItem;
            Stats = stats;
            Status = status;
            IsSilent = isSilent;
        }
    }

    public abstract class ProcessorBase<TResultItem> where TResultItem : ResultItem, new()
    {
        public string Dir { get; set; }
        public bool IncludeSubDirectories { get; set; }
        public string FileMask { get; set; }
        public string ExcludeFileMask { get; set; }
        public string ExcludeDir { get; set; }
        public string FindText { get; set; }
        public bool IsCaseSensitive { get; set; }
        public bool FindTextHasRegEx { get; set; }
        public bool SkipBinaryFileDetection { get; set; }
        public bool UseEscapeChars { get; set; }
        public Encoding AlwaysUseEncoding { get; set; }
        public Encoding DefaultEncodingIfNotDetected { get; set; }
        public bool IncludeFilesWithoutMatches { get; set; }
        public bool IsSilent { get; set; }
        public bool IsCancelRequested { get; set; }
        public bool IsKeepModifiedDate { get; set; }

        protected Stats Stats { get; private set; }
        protected Status Status { get; private set; }

        public void Cancel()
        {
            IsCancelRequested = true;
        }

        protected List<TResultItem> Process()
        {
            Verify.Argument.IsNotEmpty(Dir, "Dir");
            Verify.Argument.IsNotEmpty(FileMask, "FileMask");
            Verify.Argument.IsNotEmpty(FindText, "FindText");

            Status = Status.Processing;
            var startTime = DateTime.Now;

            string[] filesInDirectory = Utils.GetFilesInDirectory(Dir, FileMask, IncludeSubDirectories, ExcludeFileMask, ExcludeDir);

            var resultItems = new List<TResultItem>();
            Stats = new Stats();
            Stats.Files.Total = filesInDirectory.Length;

            var startTimeProcessingFiles = DateTime.Now;

            foreach (string filePath in filesInDirectory)
            {
                var resultItem = ProcessFile(filePath);
                Stats.Files.Processed++;

                UpdateStats(resultItem);

                if (resultItem.IncludeInResultsList)
                    resultItems.Add(resultItem);

                Stats.UpdateTime(startTime, startTimeProcessingFiles);

                if (IsCancelRequested)
                    Status = Status.Cancelled;

                if (Stats.Files.Total == Stats.Files.Processed)
                    Status = Status.Completed;

                OnFileProcessed(resultItem);

                if (Status == Status.Cancelled)
                    break;
            }

            if (filesInDirectory.Length == 0)
            {
                Status = Status.Completed;
                OnFileProcessed(new TResultItem());
            }

            return resultItems;
        }

        protected virtual void UpdateStats(TResultItem resultItem)
        {
            if (resultItem.IsSuccess)
            {
                Stats.Matches.Found += resultItem.NumMatches;

                if (resultItem.NumMatches > 0)
                    Stats.Files.WithMatches++;
                else
                    Stats.Files.WithoutMatches++;
            }
            else
            {
                if (resultItem.FailedToReadWrite)
                    Stats.Files.FailedToRead++;

                if (resultItem.IsBinaryFile)
                    Stats.Files.Binary++;
            }
        }

        protected TResultItem ProcessFile(string filePath)
        {
            var resultItem = new TResultItem
            {
                IsSuccess = true,
                IncludeFilesWithoutMatches = IncludeFilesWithoutMatches,
                FileName = Path.GetFileName(filePath),
                FilePath = filePath,
                FileRelativePath = "." + filePath.Substring(Dir.Length)
            };

            byte[] sampleBytes;

            try
            {
                sampleBytes = Utils.ReadFileContentSample(filePath);
            }
            catch (Exception exception)
            {
                resultItem.IsSuccess = false;
                resultItem.FailedToReadWrite = true;
                resultItem.ErrorMessage = exception.Message;
                return resultItem;
            }

            if (!SkipBinaryFileDetection && Utils.IsBinaryFile(sampleBytes))
            {
                resultItem.IsSuccess = false;
                resultItem.IsBinaryFile = true;
                return resultItem;
            }

            Encoding encoding = DetectEncoding(sampleBytes);
            if (encoding == null)
            {
                resultItem.IsSuccess = false;
                resultItem.FailedToReadWrite = true;
                resultItem.ErrorMessage = "Could not detect file encoding.";
                return resultItem;
            }
            resultItem.FileEncoding = encoding;

            string fileContent;
            using (var sr = new StreamReader(filePath, encoding))
            {
                fileContent = sr.ReadToEnd();
            }

            RegexOptions regexOptions = Utils.GetRegExOptions(IsCaseSensitive);
            var matches = Utils.FindMatches(fileContent, FindText, FindTextHasRegEx, UseEscapeChars, regexOptions);

            resultItem.NumMatches = matches.Count;
            resultItem.Matches = matches;

            if (matches.Count > 0)
            {
                PerformOperation(resultItem, fileContent, regexOptions);
            }

            return resultItem;
        }

        protected abstract void PerformOperation(TResultItem resultItem, string fileContent, RegexOptions regexOptions);

        protected abstract void OnFileProcessed(TResultItem resultItem);

        private Encoding DetectEncoding(byte[] sampleBytes)
        {
            if (AlwaysUseEncoding != null)
                return AlwaysUseEncoding;

            return EncodingDetector.Detect(sampleBytes, defaultEncoding: DefaultEncodingIfNotDetected);
        }
    }
}