using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FindAndReplace
{
    public class ReplacerEventArgs : EventArgs
    {
        public Replacer.ReplaceResultItem ResultItem { get; set; }
        public Stats Stats { get; set; }
        public Status Status { get; set; }
        public bool IsSilent { get; set; }

        public ReplacerEventArgs(Replacer.ReplaceResultItem resultItem, Stats stats, Status status, bool isSilent = false)
        {
            ResultItem = resultItem;
            Stats = stats;
            Status = status;
            IsSilent = isSilent;
        }
    }

    public delegate void ReplaceFileProcessedEventHandler(object sender, ReplacerEventArgs e);

    public class Replacer : ProcessorBase<Replacer.ReplaceResultItem>
    {
        public string ReplaceText { get; set; }

        public class ReplaceResultItem : ResultItem
        {
            public bool FailedToWrite { get; set; }
        }

        public class ReplaceResult
        {
            public List<ReplaceResultItem> ResultItems { get; set; }

            public Stats Stats { get; set; }
        }

        public ReplaceResult Replace()
        {
            Verify.Argument.IsNotNull(ReplaceText, "ReplaceText");
            var resultItems = Process();
            return new ReplaceResult { ResultItems = resultItems, Stats = Stats };
        }

        protected override void PerformOperation(ReplaceResultItem resultItem, string fileContent, RegexOptions regexOptions)
        {
            string escapedFindText = FindText;
            if (!FindTextHasRegEx && !UseEscapeChars)
                escapedFindText = Regex.Escape(FindText);

            string newContent = Regex.Replace(fileContent, escapedFindText, UseEscapeChars ? Regex.Unescape(ReplaceText) : ReplaceText, regexOptions);

            DateTime dt = DateTime.Now;

            try
            {
                if (IsKeepModifiedDate)
                {
                    dt = File.GetLastWriteTime(resultItem.FilePath);
                }

                using (var sw = new StreamWriter(resultItem.FilePath, false, resultItem.FileEncoding))
                {
                    sw.Write(newContent);
                }

                if (IsKeepModifiedDate)
                {
                    File.SetLastWriteTime(resultItem.FilePath, dt);
                }
            }
            catch (Exception ex)
            {
                resultItem.IsSuccess = false;
                resultItem.FailedToWrite = true;
                resultItem.ErrorMessage = ex.Message;
            }
        }

        protected override void UpdateStats(ReplaceResultItem resultItem)
        {
            base.UpdateStats(resultItem);

            if (resultItem.IsSuccess && resultItem.NumMatches > 0)
            {
                Stats.Matches.Replaced += resultItem.NumMatches;
            }
            else if (!resultItem.IsSuccess)
            {
                if (resultItem.FailedToWrite)
                    Stats.Files.FailedToWrite++;
            }
        }

        public event ReplaceFileProcessedEventHandler FileProcessed;

        protected override void OnFileProcessed(ReplaceResultItem resultItem)
        {
            FileProcessed?.Invoke(this, new ReplacerEventArgs(resultItem, Stats, Status, IsSilent));
        }

        public string GenCommandLine(bool showEncoding)
        {
            return CommandLineUtils.GenerateCommandLine(Dir, FileMask, ExcludeFileMask, ExcludeDir, IncludeSubDirectories, IsCaseSensitive,
                                                        FindTextHasRegEx, SkipBinaryFileDetection, showEncoding,
                                                        IncludeFilesWithoutMatches, UseEscapeChars, AlwaysUseEncoding, FindText,
                                                        ReplaceText, IsKeepModifiedDate);
        }
    }
}
