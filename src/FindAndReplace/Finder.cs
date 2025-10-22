using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FindAndReplace
{
	public delegate void FileProcessedEventHandler(object sender, ProcessorEventArgs<Finder.FindResultItem> e);

	public class Finder : ProcessorBase<Finder.FindResultItem>
	{
		public class FindResultItem : ResultItem { }

		public class FindResult
		{
			public List<FindResultItem> Items { get; set; }
			public Stats Stats { get; set; }

			public List<FindResultItem> ItemsWithMatches
			{
				get { return Items.Where(r => r.NumMatches > 0).ToList(); }
			}

		}

		public Finder() { }

		public FindResult Find()
		{
			var resultItems = Process();
			return new FindResult { Items = resultItems, Stats = Stats };
		}

		protected override void PerformOperation(FindResultItem resultItem, string fileContent, RegexOptions regexOptions)
		{
			// No operation needed for finding, matches are already counted in base class
		}

        public event FileProcessedEventHandler FileProcessed;

		protected override void OnFileProcessed(FindResultItem resultItem)
		{
			FileProcessed?.Invoke(this, new ProcessorEventArgs<FindResultItem>(resultItem, Stats, Status, IsSilent));
		}

		public string GenCommandLine(bool showEncoding)
		{
			return CommandLineUtils.GenerateCommandLine(Dir, FileMask, ExcludeFileMask, ExcludeDir, IncludeSubDirectories, IsCaseSensitive,
			                                            FindTextHasRegEx, SkipBinaryFileDetection, showEncoding,
			                                            IncludeFilesWithoutMatches, UseEscapeChars, AlwaysUseEncoding, FindText,
			                                            null, IsKeepModifiedDate);
		}
	}
}
