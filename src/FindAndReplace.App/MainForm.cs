using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Linq;


namespace FindAndReplace.App
{

	public partial class MainForm : Form
	{
		//public const int ExtraWidthWhenResults = 335;

		private Finder _finder;
		private Replacer _replacer;
		private Thread _currentThread;

		public bool _isFindOnly;
		private FormData _lastOperationFormData;


		private delegate void SetFindResultCallback(Finder.FindResultItem resultItem, Stats stats, Status status);

		private delegate void SetReplaceResultCallback(Replacer.ReplaceResultItem resultItem, Stats stats, Status status);

		public MainForm()
		{
			InitializeComponent();
		    Text = $"{Text} (v{Application.ProductVersion})";
            gvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvResults.MultiSelect = true;
            gvResults.MouseDown += Results_MouseDown;
            gvResults.MouseMove += Results_MouseMove;
        }


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			//Fix from: http://stackoverflow.com/questions/3421453/c-why-is-text-in-textbox-highlighted-selected-when-form-is-displayed
			txtDir.SelectionStart = txtDir.Text.Length;
			txtDir.DeselectAll();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_currentThread != null && _currentThread.IsAlive)
				_currentThread.Abort();
		}

		private void btnFindOnly_Click(object sender, EventArgs e)
		{
			_isFindOnly = true;

			if (!ValidateForm())
				return;

			PrepareFinderGrid();

			lblStats.Text = "";
			lblStatus.Text = "Getting file list...";

			var finder = GetFinder();

			CreateListener(finder);

			ShowResultPanel();
		    txtMatchesPreview.Clear();
            SaveToRegistry();

			_currentThread = new Thread(DoFindWork);
			_currentThread.IsBackground = true;

			_currentThread.Start();
		}

		private void SaveToRegistry()
		{
			var data = new FormData
			{
				IsFindOnly = _isFindOnly,
				Dir = txtDir.Text,
				IncludeSubDirectories = chkIncludeSubDirectories.Checked,
				FileMask = txtFileMask.Text,
				ExcludeFileMask = txtExcludeFileMask.Text,
				FindText = CleanRichBoxText(txtFind.Text),
				IsCaseSensitive = chkIsCaseSensitive.Checked,
				IsRegEx = chkIsRegEx.Checked,
				SkipBinaryFileDetection = chkSkipBinaryFileDetection.Checked,
				IncludeFilesWithoutMatches = chkIncludeFilesWithoutMatches.Checked,
				ShowEncoding = chkShowEncoding.Visible && chkShowEncoding.Checked,
				ReplaceText = CleanRichBoxText(txtReplace.Text),
				UseEscapeChars = chkUseEscapeChars.Checked,
				FileEncoding = cmbEncoding.Text,
				ExcludeDir = txtExcludeDir.Text,
				IsKeepModifiedDate = chkKeepModifiedDate.Checked
			};

			_lastOperationFormData = data;

			try
			{
				data.SaveSetting();
			}
			catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
			{
				MessageBox.Show(this,
					"Could not save settings. Another instance may still be writing the configuration file." + Environment.NewLine + ex.Message,
					"Save Settings",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}
		}



		private void PrepareFinderGrid()
		{
			gvResults.DataSource = null;

			gvResults.Rows.Clear();
			gvResults.Columns.Clear();

			AddResultsColumn("Filename", "Filename", 250);
			AddResultsColumn("Path", "Path", 450);

			if (chkShowEncoding.Visible && chkShowEncoding.Checked)
				AddResultsColumn("FileEncoding", "Encoding", 100);

			AddResultsColumn("NumMatches", "Matches", 55);
			AddResultsColumn("ErrorMessage", "Error", 150);
            gvResults.Columns[gvResults.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            gvResults.Columns.Add("MatchesPreview", "");
			gvResults.Columns[gvResults.ColumnCount - 1].Visible = false;

			HideMatchesPreviewPanel();

			progressBar.Value = 0;
		}

		private void AddResultsColumn(string dataPropertyName, string headerText, int width)
		{
			gvResults.Columns.Add(new DataGridViewColumn()
				{
					DataPropertyName = dataPropertyName,
					HeaderText = headerText,
					CellTemplate = new DataGridViewTextBoxCell(),
					Width = width,
					SortMode = DataGridViewColumnSortMode.Automatic,
				});
		}

		private void CreateListener(Finder finder)
		{
			_finder = finder;
			_finder.FileProcessed += OnFinderFileProcessed;
		}

		private void OnFinderFileProcessed(object sender, ProcessorEventArgs<Finder.FindResultItem> e)
		{
			if (!gvResults.InvokeRequired)
			{
				ShowFindResult(e.ResultItem, e.Stats, e.Status);
			}
			else
			{
				SetFindResultCallback findResultCallback = ShowFindResult;
				Invoke(findResultCallback, new object[] {e.ResultItem, e.Stats, e.Status});
			}
		}

		private void ShowFindResult(Finder.FindResultItem findResultItem, Stats stats, Status status)
		{
			if (stats.Files.Total != 0)
			{
				if (findResultItem.IncludeInResultsList)
				{
					gvResults.Rows.Add();

					int currentRow = gvResults.Rows.Count - 1;

					gvResults.Rows[currentRow].ContextMenuStrip = CreateContextMenu(currentRow);

					int columnIndex = 0;
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = findResultItem.FileName;
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = findResultItem.FileRelativePath;

					if (_lastOperationFormData.ShowEncoding)
						gvResults.Rows[currentRow].Cells[columnIndex++].Value = findResultItem.FileEncoding != null
							                                                        ? findResultItem.FileEncoding.WebName
							                                                        : String.Empty;

					gvResults.Rows[currentRow].Cells[columnIndex++].Value = findResultItem.NumMatches;
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = findResultItem.ErrorMessage;

					gvResults.Rows[currentRow].Resizable = DataGridViewTriState.False;

					if (findResultItem.IsSuccess && findResultItem.NumMatches > 0) //Account for errors and IncludeFilesWithoutMatches
					{
						string fileContent = string.Empty;

						using (var sr = new StreamReader(findResultItem.FilePath, findResultItem.FileEncoding))
						{
							fileContent = sr.ReadToEnd();
						}


						List<MatchPreviewLineNumber> lineNumbers = Utils.GetLineNumbersForMatchesPreview(fileContent,
						                                                                                 findResultItem.Matches);
						gvResults.Rows[currentRow].Cells[columnIndex].Value = GenerateMatchesPreviewText(fileContent,
						                                                                                 lineNumbers.Select(
							                                                                                 ln => ln.LineNumber).ToList());
					}

					//Grid likes to select the first row for some reason
					if (gvResults.Rows.Count == 1)
						gvResults.ClearSelection();

				}

				progressBar.Maximum = stats.Files.Total;
				progressBar.Value = stats.Files.Processed;

				lblStatus.Text = "Processing " + stats.Files.Processed + " of " + stats.Files.Total + " files.  Last file: " +
				                 findResultItem.FileRelativePath;

				ShowStats(stats);
			}
			else
			{
				HideResultPanel();
				HideStats();
			}



			//When last file - enable buttons back
			if (status == Status.Completed || status == Status.Cancelled)
			{
				if (status == Status.Completed)
					lblStatus.Text = "Processed " + stats.Files.Processed + " files.";

				if (status == Status.Cancelled)
					lblStatus.Text = "Operation was cancelled.";

				EnableButtons();
			}

		}

		private void DisableButtons()
		{
			//this.Cursor = Cursors.WaitCursor;

			UpdateButtons(false);
		}

		private void EnableButtons()
		{
			UpdateButtons(true);

			//this.Cursor = Cursors.Arrow;
		}

		private void UpdateButtons(bool enabled)
		{
			btnFindOnly.Enabled = enabled;
			btnReplace.Enabled = enabled;
			btnGenReplaceCommandLine.Enabled = enabled;
			btnCancel.Enabled = !enabled;
		}

		private void DoFindWork()
		{
		    try
		    {
		        _finder.Find();
		    }
		    catch (Exception e)
		    {
		        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		        OnFinderFileProcessed(this, new ProcessorEventArgs<Finder.FindResultItem>(new Finder.FindResultItem(), new Stats(), Status.Cancelled, _finder.IsSilent));
		    }
		}

		private void ShowResultPanel()
		{
			DisableButtons();
			HideMatchesPreviewPanel();

			if (!pnlGridResults.Visible)
			{
				pnlGridResults.Visible = true;

				//if (pnlCommandLine.Visible)
				//{
				//	this.Height -= pnlCommandLine.Height + 10;
				//	pnlCommandLine.Visible = false;
				//}

				//this.Height += pnlGridResults.Height + 10;
				//this.Width += ExtraWidthWhenResults;
			}
		}

		private void HideResultPanel()
		{
			if (pnlGridResults.Visible)
			{
				pnlGridResults.Visible = false;
			}
		}

		private void ShowMatchesPreviewPanel()
		{
			if (!txtMatchesPreview.Enabled)
			{
				txtMatchesPreview.Enabled = true;
			}
		}

		private void HideMatchesPreviewPanel()
		{
			if (txtMatchesPreview.Enabled)
			{
				txtMatchesPreview.Enabled = false;
			}
		}


		private bool _isFormValid = true;
		private Control _firstInvalidControl = null;

		private bool ValidateForm()
		{
			_isFormValid = true;
			_firstInvalidControl = null;

			ValidateControls(Controls);

			//Focus on first invalid control
			_firstInvalidControl?.Focus();

			if (!_isFormValid && AutoValidate == AutoValidate.Disable)
				AutoValidate = AutoValidate.EnablePreventFocusChange; //Revalidate on focus change

			return _isFormValid;
		}

		private void ValidateControls(Control.ControlCollection controls)
		{
			foreach (Control control in controls)
			{
				//Eric - Not needed for now
				//if (control is Panel && !control.CausesValidation)  //handle pnlFind which causes validation
				//{
				//	ValidateControls(control.Controls);
				//	continue;
				//}

				if (!control.CausesValidation)
					continue;

				control.Focus();

				if (!Validate() || errorProvider1.GetError(control) != "")
				{
					if (_isFormValid)
						_firstInvalidControl = control;

					_isFormValid = false;
				}
				else
				{
					errorProvider1.SetError(control, "");
				}
			}
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			_isFindOnly = false;

			if (!ValidateForm())
				return;

			if (txtReplace.Text == "")
			{
				DialogResult dlgResult = MessageBox.Show(this, "Are you sure you want to replace with an empty string?", "Replace Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.No)
					return;
			}

			ShowResultPanel();

			lblStats.Text = "";
			lblStatus.Text = "Getting file list...";

			PrepareReplacerGrid();
			txtMatchesPreview.Clear();

			var replacer = GetReplacer();

			CreateListener(replacer);

			SaveToRegistry();

            _currentThread = new Thread(DoReplaceWork) { IsBackground = true };
            _currentThread.Start();
		}

		private void CreateListener(Replacer replacer)
		{
			_replacer = replacer;
			_replacer.FileProcessed += ReplaceFileProceed;
		}

		private void PrepareReplacerGrid()
		{
			gvResults.DataSource = null;

			gvResults.Rows.Clear();
			gvResults.Columns.Clear();

			AddResultsColumn("Filename", "Filename", 250);
			AddResultsColumn("Path", "Path", 400);

			if (chkShowEncoding.Visible && chkShowEncoding.Checked)
				AddResultsColumn("FileEncoding", "Encoding", 100);

			AddResultsColumn("NumMatches", "Matches", 50);
			AddResultsColumn("IsSuccess", "Replaced", 60);
			AddResultsColumn("ErrorMessage", "Error", 150);
		    gvResults.Columns[gvResults.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            gvResults.Columns.Add("MatchesPreview", "");
			gvResults.Columns[gvResults.ColumnCount - 1].Visible = false;

			HideMatchesPreviewPanel();
			progressBar.Value = 0;
		}

		private void DoReplaceWork()
		{
		    try
		    {
		        _replacer.Replace();
            }
		    catch (Exception e)
		    {
		        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		        ReplaceFileProceed(this, new ProcessorEventArgs<Replacer.ReplaceResultItem>(new Replacer.ReplaceResultItem(), new Stats(), Status.Cancelled, _replacer.IsSilent));
		    }
        }

		private void ShowReplaceResult(Replacer.ReplaceResultItem replaceResultItem, Stats stats, Status status)
		{
			if (stats.Files.Total > 0)
			{
				if (replaceResultItem.IncludeInResultsList)
				{
					gvResults.Rows.Add();

					int currentRow = gvResults.Rows.Count - 1;

					gvResults.Rows[currentRow].ContextMenuStrip = CreateContextMenu(currentRow);

					int columnIndex = 0;
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = replaceResultItem.FileName;
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = replaceResultItem.FileRelativePath;

					if (_lastOperationFormData.ShowEncoding)
						gvResults.Rows[currentRow].Cells[columnIndex++].Value = replaceResultItem.FileEncoding != null
							                                                        ? replaceResultItem.FileEncoding.WebName
							                                                        : String.Empty;

					gvResults.Rows[currentRow].Cells[columnIndex++].Value = replaceResultItem.NumMatches;
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = replaceResultItem.IsReplaced ? "Yes" : "No";
					gvResults.Rows[currentRow].Cells[columnIndex++].Value = replaceResultItem.ErrorMessage;

					gvResults.Rows[currentRow].Resizable = DataGridViewTriState.False;

					if (replaceResultItem.IsSuccess && replaceResultItem.NumMatches > 0)
						//Account for errors and IncludeFilesWithoutMatches
					{
						string fileContent = string.Empty;


                        using (var sr = new StreamReader(replaceResultItem.FilePath, replaceResultItem.FileEncoding))
						{
							fileContent = sr.ReadToEnd();
						}

						List<MatchPreviewLineNumber> lineNumbers = Utils.GetLineNumbersForMatchesPreview(fileContent,
						                                                                                 replaceResultItem.Matches,
						                                                                                 _lastOperationFormData
							                                                                                 .ReplaceText.Length, true);
						gvResults.Rows[currentRow].Cells[columnIndex].Value = GenerateMatchesPreviewText(fileContent,
						                                                                                 lineNumbers.Select(
							                                                                                 ln => ln.LineNumber).ToList());
					}

					//Grid likes to select the first row for some reason
					if (gvResults.Rows.Count == 1)
						gvResults.ClearSelection();
				}

				progressBar.Maximum = stats.Files.Total;
				progressBar.Value = stats.Files.Processed;

				lblStatus.Text = "Processing " + stats.Files.Processed + " of " + stats.Files.Total + " files.  Last file: " +
				                 replaceResultItem.FileRelativePath;
			 

				ShowStats(stats, true);
			}
			else
			{
				HideResultPanel();
				HideStats();
			}


			//When last file - enable buttons back
			if (status == Status.Completed || status == Status.Cancelled)
			{
				if (status == Status.Completed)
					lblStatus.Text = "Processed " + stats.Files.Processed + " files.";

				if (status == Status.Cancelled)
					lblStatus.Text = "Operation was cancelled.";

				EnableButtons();
			}
		}

		private void ReplaceFileProceed(object sender, ProcessorEventArgs<Replacer.ReplaceResultItem> e)
		{
			if (!gvResults.InvokeRequired)
			{
				ShowReplaceResult(e.ResultItem, e.Stats, e.Status);
			}
			else
			{
				var replaceResultCallback = new SetReplaceResultCallback(ShowReplaceResult);
				Invoke(replaceResultCallback, new object[] {e.ResultItem, e.Stats, e.Status});
			}
		}

		private void btnGenReplaceCommandLine_Click(object sender, EventArgs e)
		{
			if (!ValidateForm())
				return;

			lblStats.Text = "";

			var replacer = GetReplacer();
            Clipboard.SetText($"\"{Application.ExecutablePath}\" {replacer.GenCommandLine(chkShowEncoding.Visible && chkShowEncoding.Checked)}");
        }

		private void txtDir_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var validationResult = ValidationUtils.IsDirValid(txtDir.Text, "Dir");

			if (!validationResult.IsSuccess)
			{
				errorProvider1.SetError(txtDir, validationResult.ErrorMessage);
				return;
			}

			errorProvider1.SetError(txtDir, "");
		}

		private void txtFileMask_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var validationResult = ValidationUtils.IsNotEmpty(txtFileMask.Text, "FileMask");

			if (!validationResult.IsSuccess)
			{
				errorProvider1.SetError(txtFileMask, validationResult.ErrorMessage);
				return;
			}

			errorProvider1.SetError(txtFileMask, "");
		}

		private void pnlFind_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var validationResult = ValidationUtils.IsNotEmpty(txtFind.Text, "Find");

			if (!validationResult.IsSuccess)
			{
				errorProvider1.SetError(txtFind, validationResult.ErrorMessage);
				return;
			}

			
			if (chkIsRegEx.Checked)
			{
				validationResult = ValidationUtils.IsValidRegExp(txtFind.Text, "Find");

				if (!validationResult.IsSuccess)
				{
					errorProvider1.SetError(txtFind, validationResult.ErrorMessage);
					return;
				}
			}

			if (chkUseEscapeChars.Checked)
			{
				validationResult = ValidationUtils.IsValidEscapeSequence(txtFind.Text, "Find");

				if (!validationResult.IsSuccess)
				{
					errorProvider1.SetError(txtFind, validationResult.ErrorMessage);
					return;
				}
			}

			errorProvider1.SetError(txtFind, "");
		}

		private void pnlReplace_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (chkUseEscapeChars.Checked)
			{
				var validationResult = ValidationUtils.IsValidEscapeSequence(txtReplace.Text, "Replace");

				if (!validationResult.IsSuccess)
				{
					errorProvider1.SetError(txtReplace, validationResult.ErrorMessage);
					return;
				}
			}

			errorProvider1.SetError(txtReplace, "");
		}

		private void gvResults_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1) //heading
				return;

			int matchedPreviewColIndex = gvResults.ColumnCount - 1; //Always last column

			if (gvResults.Rows[e.RowIndex].Cells[matchedPreviewColIndex].Value == null)
			{
				HideMatchesPreviewPanel();
				return;
			}

			ShowMatchesPreviewPanel();

			var matchesPreviewText = gvResults.Rows[e.RowIndex].Cells[matchedPreviewColIndex].Value.ToString();

			txtMatchesPreview.SelectionLength = 0;
			txtMatchesPreview.Clear();

			txtMatchesPreview.Text = matchesPreviewText;

			var font = new Font(txtMatchesPreview.Font.Name, txtMatchesPreview.Font.Size, FontStyle.Bold);

			//Use _lastOperation form data since user may change it before clicking on preview
			var findText = _lastOperationFormData.IsFindOnly
				               ? _lastOperationFormData.FindText
				               : _lastOperationFormData.ReplaceText;
			findText = findText.Replace("\r\n", "\n");

			findText = ((_lastOperationFormData.IsRegEx || _lastOperationFormData.UseEscapeChars) && _lastOperationFormData.IsFindOnly) ? findText : Regex.Escape(findText);
			var mathches = Regex.Matches(txtMatchesPreview.Text, findText,
			                             Utils.GetRegExOptions(_lastOperationFormData.IsCaseSensitive));

			int count = 0;
			int maxCount = 1000;

			foreach (Match match in mathches)
			{
				txtMatchesPreview.SelectionStart = match.Index;

				txtMatchesPreview.SelectionLength = match.Length;

				txtMatchesPreview.SelectionFont = font;

				txtMatchesPreview.SelectionColor = Color.LightGreen;

				//Limit highlighted matches, otherwise may lock up the app .  Happened with 65K+
				count++;
				if (count > maxCount)
					break;
			}

			txtMatchesPreview.SelectionLength = 0;
		}

		private string GenerateMatchesPreviewText(string content, List<int> rowNumbers)
		{
			var separator = Environment.NewLine;

			var lines = content.Split(new string[] {separator}, StringSplitOptions.None);

			var stringBuilder = new StringBuilder();

			rowNumbers = rowNumbers.Distinct().OrderBy(r => r).ToList();
			var prevLineIndex = 0;
			string lineSeparator = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";


            foreach (var rowNumber in rowNumbers)
			{
				if (rowNumber - prevLineIndex > 1 && prevLineIndex != 0)
				{
					stringBuilder.AppendLine("");
					stringBuilder.AppendLine(lineSeparator);
					stringBuilder.AppendLine("");
				}
				stringBuilder.AppendLine(lines[rowNumber]);
				prevLineIndex = rowNumber;
			}

			return stringBuilder.ToString();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			cmbEncoding.Items.AddRange(GetEncodings());
			cmbEncoding.SelectedIndex = 0;
			InitWithRegistryData();
		    txtDir.Focus();
		}

		//from http://stackoverflow.com/questions/334630/c-open-folder-and-select-the-file
		private void gvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1) //heading
				return;

			OpenFileUsingExternalApp(e.RowIndex);
		}

		private ContextMenuStrip CreateContextMenu(int rowNumber)
		{
			var contextMenu = new ContextMenuStrip();
			contextMenu.ShowImageMargin = false;
            var eventArgs = new GVResultEventArgs { cellRow = rowNumber };

            var openMenuItem = new ToolStripMenuItem("Open");
            openMenuItem.Click += (s, e) => OpenFileUsingExternalApp(rowNumber);
			contextMenu.Items.Add(openMenuItem);

            var openFolderMenuItem = new ToolStripMenuItem("Open Containing Folder");
			openFolderMenuItem.Click += (s, e) => OpenFileFolder(rowNumber);
            contextMenu.Items.Add(openFolderMenuItem);

            var copyNameItem = new ToolStripMenuItem("Copy File Name");
            copyNameItem.Click += (s, e) => Clipboard.SetText($"{gvResults.Rows[rowNumber].Cells[0]}");
			contextMenu.Items.Add(copyNameItem);

			var copyNameNoExtItem = new ToolStripMenuItem("Copy File Name Without Extension");
			copyNameNoExtItem.Click += (s, e) => Clipboard.SetText(Path.GetFileNameWithoutExtension($"{gvResults.Rows[rowNumber].Cells[0].Value.ToString()}"));
			contextMenu.Items.Add(copyNameNoExtItem);

			var copyPathItem = new ToolStripMenuItem("Copy Full Path");
			copyPathItem.Click += (s, e) => Clipboard.SetText(GetFullPath(rowNumber));
			contextMenu.Items.Add(copyPathItem);

            return contextMenu;
		}

		public string GetFullPath(int rowIndex) => txtDir.Text.Replace('/', '\\').TrimEnd('\\') + '\\' + gvResults.Rows[rowIndex].Cells[1].Value.ToString().TrimStart('.');

        private void OpenFileUsingExternalApp(int rowIndex)
        {
            var filePath = gvResults.Rows[rowIndex].Cells[1].Value.ToString();

            string file = txtDir.Text + filePath.TrimStart('.');
            Process.Start(file);
        }

        private void OpenFileFolder(int cellRow)
		{
			var filePath = gvResults.Rows[cellRow].Cells[1].Value.ToString();
			string argument = @"/select, " + txtDir.Text + filePath.TrimStart('.');
			Process.Start("explorer.exe", argument);
		}

		private void ShowStats(Stats stats, bool showReplaceStats = false)
		{
			var lines = new List<string> {
				$"- Processed {stats.Files.Processed}/{stats.Files.Total}{(stats.Files.Binary > 0 ? $" (Skipped {stats.Files.Binary} binary files)" : "")}{(stats.Files.FailedToRead > 0 ? $" (Failed to read {stats.Files.FailedToRead} files)" : "")}",
				$"- With matches: {stats.Files.WithMatches}, without matches: {stats.Files.WithoutMatches}",
			};

            if (showReplaceStats)
                lines.Add($"- Replaced {stats.Matches.Replaced}{(stats.Files.FailedToWrite > 0 ? $", failed to write {stats.Files.FailedToWrite}" : "")}");

			var passedSec = stats.Time.Passed.TotalSeconds;
			var remainSec = stats.Time.Remaining.TotalSeconds;

            if (passedSec > 0.1d)
                lines.Add($"- Time: {Utils.FormatTimeSpan(passedSec)}{(passedSec > 2d && remainSec > 1d ? $", ETA: {Utils.FormatTimeSpan(remainSec)}" : "")}");

			lblStats.Text = string.Join("\n", lines);
		}


		private void HideStats()
		{
			lblStats.Text = String.Empty;
		}


		public class GVResultEventArgs : EventArgs
		{
			public int cellRow { get; set; }
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (_currentThread.IsAlive)
			{
				if (_isFindOnly)
					_finder.Cancel();
				else
					_replacer.Cancel();
			}
		}

		private void InitWithRegistryData()
		{
			var data = new FormData();

			data.LoadSetting();

		    if (data.IsFirstTime)
		    {
                data.IsFirstTime = false;
                return;
		    }

			txtDir.Text = data.Dir;
			chkIncludeSubDirectories.Checked = data.IncludeSubDirectories;
			txtFileMask.Text = data.FileMask;
            txtExcludeFileMask.Text = data.ExcludeFileMask;
		    txtExcludeDir.Text = data.ExcludeDir;
            txtFind.Text = data.FindText;
			chkIsCaseSensitive.Checked = data.IsCaseSensitive;
			chkIsRegEx.Checked = data.IsRegEx;
			chkSkipBinaryFileDetection.Checked = data.SkipBinaryFileDetection;
			chkIncludeFilesWithoutMatches.Checked = data.IncludeFilesWithoutMatches;
			chkShowEncoding.Checked = data.ShowEncoding;
			txtReplace.Text = data.ReplaceText;
			chkUseEscapeChars.Checked = data.UseEscapeChars;
		    chkKeepModifiedDate.Checked = data.IsKeepModifiedDate;

            if (!string.IsNullOrEmpty(data.FileEncoding))
				cmbEncoding.SelectedIndex = cmbEncoding.Items.IndexOf(data.FileEncoding);
		}

		private void btnSelectDir_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.SelectedPath = txtDir.Text;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				txtDir.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void txtReplace_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A))
			{
				txtReplace.SelectAll();
				e.SuppressKeyPress = true;
				e.Handled = true;
			}
		}

		private void txtFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A))
			{
				txtFind.SelectAll();
				e.SuppressKeyPress = true;
				e.Handled = true;
			}
		}

		private void btnSwap_Click(object sender, EventArgs e)
		{
			string findText = txtFind.Text;

			txtFind.Text = txtReplace.Text;
			txtReplace.Text = findText;
		}

		private string[] GetEncodings()
		{
			var encs = new List<string> { "Auto Detect" };
			encs.AddRange(Encoding.GetEncodings().OrderBy(ei => ei.Name).Select(ei => ei.Name));
			return encs.ToArray();
		}

		private Finder GetFinder()
		{
			var finder = new Finder();
			finder.Dir = txtDir.Text;

			finder.IncludeSubDirectories = chkIncludeSubDirectories.Checked;
			finder.FileMask = txtFileMask.Text;
			finder.FindTextHasRegEx = chkIsRegEx.Checked;
			finder.FindText = CleanRichBoxText(txtFind.Text);
			finder.IsCaseSensitive = chkIsCaseSensitive.Checked;
			finder.SkipBinaryFileDetection = chkSkipBinaryFileDetection.Checked;
			finder.IncludeFilesWithoutMatches = chkIncludeFilesWithoutMatches.Checked;
			finder.ExcludeFileMask = txtExcludeFileMask.Text;
		    finder.ExcludeDir = txtExcludeDir.Text;

            finder.UseEscapeChars = chkUseEscapeChars.Checked;

			if (cmbEncoding.SelectedIndex > 0)
				finder.AlwaysUseEncoding = Utils.GetEncodingByName(cmbEncoding.Text);

			return finder;
		}



		private string CleanRichBoxText(string text)
		{
			return text.Replace("\n", Environment.NewLine);
		}


		private Replacer GetReplacer()
		{
			var replacer = new Replacer();

			replacer.Dir = txtDir.Text;
			replacer.IncludeSubDirectories = chkIncludeSubDirectories.Checked;

			replacer.FileMask = txtFileMask.Text;
			replacer.ExcludeFileMask = txtExcludeFileMask.Text;
		    replacer.ExcludeDir = txtExcludeDir.Text;
            replacer.FindText = CleanRichBoxText(txtFind.Text);
			replacer.IsCaseSensitive = chkIsCaseSensitive.Checked;
			replacer.FindTextHasRegEx = chkIsRegEx.Checked;
			replacer.SkipBinaryFileDetection = chkSkipBinaryFileDetection.Checked;
			replacer.IncludeFilesWithoutMatches = chkIncludeFilesWithoutMatches.Checked;
			replacer.ReplaceText =  CleanRichBoxText(txtReplace.Text);
			replacer.UseEscapeChars = chkUseEscapeChars.Checked;
		    replacer.IsKeepModifiedDate = chkKeepModifiedDate.Checked;


            if (cmbEncoding.SelectedIndex > 0)
				replacer.AlwaysUseEncoding = Utils.GetEncodingByName(cmbEncoding.Text);

			return replacer;
		}

        private void chkIncludeSubDirectories_CheckedChanged(object sender, EventArgs e)
        {
            txtExcludeDir.Enabled = chkIncludeSubDirectories.Checked;
        }

        #region Drag-n-Drop

        private Rectangle _dragBox = Rectangle.Empty;
        private int _mouseDownRowIndex = -1;

        private void Results_MouseDown(object sender, MouseEventArgs e)
        {
            var ht = gvResults.HitTest(e.X, e.Y);
            _mouseDownRowIndex = ht.RowIndex;

            if (_mouseDownRowIndex >= 0 && e.Button == MouseButtons.Left)
            {
                // create the drag box for threshold
                Size dragSize = SystemInformation.DragSize;
                _dragBox = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                _dragBox = Rectangle.Empty;
            }
        }

        private void Results_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _dragBox == Rectangle.Empty || _dragBox.Contains(e.Location))
                return;

            // If user hasn't selected the row yet, select it now.
            if (_mouseDownRowIndex >= 0 && !gvResults.Rows[_mouseDownRowIndex].Selected)
            {
                gvResults.ClearSelection();
                gvResults.Rows[_mouseDownRowIndex].Selected = true;
            }

            // Collect file paths from selected rows
            var paths = gvResults.SelectedRows.Cast<DataGridViewRow>().Select(r => Convert.ToString(r.Cells[1].Value)).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            if (paths.Length == 0)
				return;

			// Reconstruct full path
			for(int i = 0; i < paths.Length; i++)
			{
				paths[i] = txtDir.Text.Replace('/', '\\').TrimEnd('\\') + '\\' + paths[i].TrimStart('.');
            }

            paths = paths.Where(File.Exists).Where(Path.IsPathRooted).Distinct().ToArray();
            if (paths.Length == 0)
				return;

            var dataObject = new DataObject(DataFormats.FileDrop, paths);
            DoDragDrop(dataObject, DragDropEffects.Copy);
        }

        #endregion
    }
}
 
