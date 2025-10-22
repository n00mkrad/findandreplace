using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FindAndReplace
{
	public delegate void ReplacerFileProcessedEventHandler(object sender, ProcessorEventArgs<Replacer.ReplaceResultItem> e);

	public class Replacer : ProcessorBase<Replacer.ReplaceResultItem>
	{
		public class ReplaceResultItem : ResultItem
		{
			public int NumReplaces { get; set; }
			public bool IsReplaced { get; set; }
		}

		public class ReplaceResult
		{
			public List<ReplaceResultItem> ResultItems { get; set; }
			public Stats Stats { get; set; }
		}

		public string ReplaceText { get; set; }

		public ReplaceResult Replace()
		{
			Verify.Argument.IsNotNull(ReplaceText, "ReplaceText");

			var resultItems = Process();
			return new ReplaceResult { ResultItems = resultItems, Stats = Stats };
		}

		protected override void PerformOperation(ReplaceResultItem resultItem, string fileContent, RegexOptions regexOptions)
		{
			// string newFileContent;

            // if (FindTextHasRegEx)
            // {
            // 	string replacement = UseEscapeChars ? Regex.Unescape(ReplaceText) : ReplaceText;
            // 	newFileContent = Regex.Replace(fileContent, FindText, replacement, regexOptions);
            // }
            // else
            // {
            // 	newFileContent = fileContent.Replace(FindText, ReplaceText,
            // 	                                     IsCaseSensitive
            // 		                                     ? StringComparison.InvariantCulture
            // 		                                     : StringComparison.InvariantCultureIgnoreCase);
            // }

            string escapedFindText = FindText;

            if (!FindTextHasRegEx && !UseEscapeChars)
                escapedFindText = Regex.Escape(FindText);

            string newFileContent = Regex.Replace(fileContent, escapedFindText, UseEscapeChars ? Regex.Unescape(ReplaceText) : ReplaceText, regexOptions);

            if (newFileContent != fileContent)
			{
				try
				{
					WriteFile(resultItem.FilePath, newFileContent, resultItem.FileEncoding);
					resultItem.IsReplaced = true;
				}
				catch (Exception e)
				{
					resultItem.IsSuccess = false;
					resultItem.ErrorMessage = e.Message;
				}
			}

			resultItem.NumReplaces = resultItem.NumMatches; //In this version of app number of replaces is the same as number of matches
		}

		private void WriteFile(string filePath, string content, Encoding encoding)
		{
			if (IsKeepModifiedDate)
			{
				var lastWriteTime = File.GetLastWriteTime(filePath);
				File.WriteAllText(filePath, content, encoding);
				File.SetLastWriteTime(filePath, lastWriteTime);
			}
			else
			{
				File.WriteAllText(filePath, content, encoding);
			}
		}

		protected override void UpdateStats(ReplaceResultItem resultItem)
		{
			base.UpdateStats(resultItem);

			if (resultItem.IsSuccess)
			{
				if (resultItem.IsReplaced)
					Stats.Matches.Replaced += resultItem.NumReplaces;
			}
			else
			{
				if (!resultItem.FailedToReadWrite && !resultItem.IsBinaryFile) //If we failed to open or it is a binary file, it is already counted
					Stats.Files.FailedToWrite++;
			}
		}

		public event ReplacerFileProcessedEventHandler FileProcessed;

		protected override void OnFileProcessed(ReplaceResultItem resultItem)
		{
			FileProcessed?.Invoke(this, new ProcessorEventArgs<ReplaceResultItem>(resultItem, Stats, Status, IsSilent));
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
