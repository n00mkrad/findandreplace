using System.Drawing;

namespace FindAndReplace.App
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtDir = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip_btnSwap = new System.Windows.Forms.ToolTip(this.components);
            this.btnSwap = new System.Windows.Forms.Button();
            this.toolTip_chkIncludeFilesWithoutMatches = new System.Windows.Forms.ToolTip(this.components);
            this.chkIncludeFilesWithoutMatches = new System.Windows.Forms.CheckBox();
            this.toolTip_chkSkipBinaryFileDetection = new System.Windows.Forms.ToolTip(this.components);
            this.chkSkipBinaryFileDetection = new System.Windows.Forms.CheckBox();
            this.toolTip_chkShowEncoding = new System.Windows.Forms.ToolTip(this.components);
            this.chkShowEncoding = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExcludeDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkIncludeSubDirectories = new System.Windows.Forms.CheckBox();
            this.lblStats = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnFindOnly = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnGenReplaceCommandLine = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkIsCaseSensitive = new System.Windows.Forms.CheckBox();
            this.chkIsRegEx = new System.Windows.Forms.CheckBox();
            this.chkKeepModifiedDate = new System.Windows.Forms.CheckBox();
            this.chkUseEscapeChars = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtExcludeFileMask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFileMask = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pnlGridResults = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.txtMatchesPreview = new System.Windows.Forms.RichTextBox();
            this.gvResults = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlGridResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // txtDir
            // 
            this.txtDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDir.ForeColor = System.Drawing.Color.White;
            this.errorProvider1.SetIconPadding(this.txtDir, 30);
            this.txtDir.Location = new System.Drawing.Point(77, 2);
            this.txtDir.Margin = new System.Windows.Forms.Padding(0);
            this.txtDir.MinimumSize = new System.Drawing.Size(539, 20);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(871, 20);
            this.txtDir.TabIndex = 0;
            this.txtDir.Validating += new System.ComponentModel.CancelEventHandler(this.txtDir_Validating);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select folder with files to find and replace.";
            // 
            // btnSwap
            // 
            this.btnSwap.AccessibleDescription = "";
            this.btnSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnSwap.CausesValidation = false;
            this.btnSwap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnSwap.ForeColor = System.Drawing.Color.White;
            this.btnSwap.Location = new System.Drawing.Point(4, 32);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(67, 25);
            this.btnSwap.TabIndex = 1;
            this.btnSwap.Text = "↑ ↓";
            this.toolTip_btnSwap.SetToolTip(this.btnSwap, "Swap find text and replace text");
            this.btnSwap.UseVisualStyleBackColor = false;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // chkIncludeFilesWithoutMatches
            // 
            this.chkIncludeFilesWithoutMatches.AutoSize = true;
            this.chkIncludeFilesWithoutMatches.Location = new System.Drawing.Point(3, 118);
            this.chkIncludeFilesWithoutMatches.Name = "chkIncludeFilesWithoutMatches";
            this.chkIncludeFilesWithoutMatches.Size = new System.Drawing.Size(169, 17);
            this.chkIncludeFilesWithoutMatches.TabIndex = 5;
            this.chkIncludeFilesWithoutMatches.Text = "Include Files Without Matches";
            this.toolTip_chkIncludeFilesWithoutMatches.SetToolTip(this.chkIncludeFilesWithoutMatches, "Show files without matches in results.");
            this.chkIncludeFilesWithoutMatches.UseVisualStyleBackColor = true;
            // 
            // chkSkipBinaryFileDetection
            // 
            this.chkSkipBinaryFileDetection.AutoSize = true;
            this.chkSkipBinaryFileDetection.Location = new System.Drawing.Point(3, 49);
            this.chkSkipBinaryFileDetection.Name = "chkSkipBinaryFileDetection";
            this.chkSkipBinaryFileDetection.Size = new System.Drawing.Size(147, 17);
            this.chkSkipBinaryFileDetection.TabIndex = 2;
            this.chkSkipBinaryFileDetection.Text = "Skip Binary File Detection";
            this.toolTip_chkSkipBinaryFileDetection.SetToolTip(this.chkSkipBinaryFileDetection, "Include binary files when searching for the string in \'Find\'.");
            this.chkSkipBinaryFileDetection.UseVisualStyleBackColor = true;
            // 
            // chkShowEncoding
            // 
            this.chkShowEncoding.AutoSize = true;
            this.chkShowEncoding.Location = new System.Drawing.Point(3, 95);
            this.chkShowEncoding.Name = "chkShowEncoding";
            this.chkShowEncoding.Size = new System.Drawing.Size(120, 17);
            this.chkShowEncoding.TabIndex = 4;
            this.chkShowEncoding.Text = "Show File Encoding";
            this.toolTip_chkShowEncoding.SetToolTip(this.chkShowEncoding, "Indicate encoding detected for each file");
            this.chkShowEncoding.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 24);
            this.panel1.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnSelectDir);
            this.panel2.Controls.Add(this.txtDir);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(979, 24);
            this.panel2.TabIndex = 0;
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnSelectDir.CausesValidation = false;
            this.btnSelectDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectDir.Location = new System.Drawing.Point(948, 2);
            this.btnSelectDir.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnSelectDir.MaximumSize = new System.Drawing.Size(25, 20);
            this.btnSelectDir.MinimumSize = new System.Drawing.Size(27, 20);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(27, 20);
            this.btnSelectDir.TabIndex = 1;
            this.btnSelectDir.Text = "···";
            this.btnSelectDir.UseVisualStyleBackColor = false;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Root Dir:";
            // 
            // txtExcludeDir
            // 
            this.txtExcludeDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcludeDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtExcludeDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcludeDir.ForeColor = System.Drawing.Color.White;
            this.txtExcludeDir.Location = new System.Drawing.Point(97, 0);
            this.txtExcludeDir.MinimumSize = new System.Drawing.Size(50, 20);
            this.txtExcludeDir.Name = "txtExcludeDir";
            this.txtExcludeDir.Size = new System.Drawing.Size(382, 20);
            this.txtExcludeDir.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Exclude Dirs:";
            // 
            // chkIncludeSubDirectories
            // 
            this.chkIncludeSubDirectories.AutoSize = true;
            this.chkIncludeSubDirectories.Checked = true;
            this.chkIncludeSubDirectories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSubDirectories.Location = new System.Drawing.Point(83, 5);
            this.chkIncludeSubDirectories.Name = "chkIncludeSubDirectories";
            this.chkIncludeSubDirectories.Size = new System.Drawing.Size(74, 17);
            this.chkIncludeSubDirectories.TabIndex = 1;
            this.chkIncludeSubDirectories.Text = "Recursive";
            this.chkIncludeSubDirectories.UseVisualStyleBackColor = true;
            this.chkIncludeSubDirectories.CheckedChanged += new System.EventHandler(this.chkIncludeSubDirectories_CheckedChanged);
            // 
            // lblStats
            // 
            this.lblStats.Location = new System.Drawing.Point(-2, 3);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(439, 59);
            this.lblStats.TabIndex = 51;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.btnSwap);
            this.panel5.Controls.Add(this.txtFind);
            this.panel5.Controls.Add(this.txtReplace);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.flowLayoutPanel1);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(3, 66);
            this.panel5.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(981, 203);
            this.panel5.TabIndex = 2;
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.ForeColor = System.Drawing.Color.White;
            this.txtFind.Location = new System.Drawing.Point(77, 0);
            this.txtFind.Multiline = true;
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(684, 57);
            this.txtFind.TabIndex = 0;
            this.txtFind.WordWrap = false;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // txtReplace
            // 
            this.txtReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtReplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReplace.CausesValidation = false;
            this.txtReplace.ForeColor = System.Drawing.Color.White;
            this.txtReplace.Location = new System.Drawing.Point(77, 63);
            this.txtReplace.Multiline = true;
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(685, 58);
            this.txtReplace.TabIndex = 0;
            this.txtReplace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReplace_KeyDown);
            this.txtReplace.Validating += new System.ComponentModel.CancelEventHandler(this.pnlReplace_Validating);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.Controls.Add(this.btnFindOnly);
            this.panel9.Controls.Add(this.btnReplace);
            this.panel9.Controls.Add(this.lblStats);
            this.panel9.Controls.Add(this.btnGenReplaceCommandLine);
            this.panel9.Location = new System.Drawing.Point(76, 124);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(685, 72);
            this.panel9.TabIndex = 1;
            // 
            // btnFindOnly
            // 
            this.btnFindOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnFindOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindOnly.ForeColor = System.Drawing.Color.White;
            this.btnFindOnly.Location = new System.Drawing.Point(526, 3);
            this.btnFindOnly.Name = "btnFindOnly";
            this.btnFindOnly.Size = new System.Drawing.Size(75, 25);
            this.btnFindOnly.TabIndex = 0;
            this.btnFindOnly.Text = "Find Only";
            this.btnFindOnly.UseVisualStyleBackColor = false;
            this.btnFindOnly.Click += new System.EventHandler(this.btnFindOnly_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplace.ForeColor = System.Drawing.Color.White;
            this.btnReplace.Location = new System.Drawing.Point(607, 3);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 25);
            this.btnReplace.TabIndex = 0;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnGenReplaceCommandLine
            // 
            this.btnGenReplaceCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenReplaceCommandLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnGenReplaceCommandLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenReplaceCommandLine.ForeColor = System.Drawing.Color.White;
            this.btnGenReplaceCommandLine.Location = new System.Drawing.Point(445, 3);
            this.btnGenReplaceCommandLine.Name = "btnGenReplaceCommandLine";
            this.btnGenReplaceCommandLine.Size = new System.Drawing.Size(75, 25);
            this.btnGenReplaceCommandLine.TabIndex = 1;
            this.btnGenReplaceCommandLine.Text = "Get Cmd";
            this.btnGenReplaceCommandLine.UseVisualStyleBackColor = false;
            this.btnGenReplaceCommandLine.Visible = false;
            this.btnGenReplaceCommandLine.Click += new System.EventHandler(this.btnGenReplaceCommandLine_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Replace:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.chkIsCaseSensitive);
            this.flowLayoutPanel1.Controls.Add(this.chkIsRegEx);
            this.flowLayoutPanel1.Controls.Add(this.chkSkipBinaryFileDetection);
            this.flowLayoutPanel1.Controls.Add(this.chkKeepModifiedDate);
            this.flowLayoutPanel1.Controls.Add(this.chkShowEncoding);
            this.flowLayoutPanel1.Controls.Add(this.chkIncludeFilesWithoutMatches);
            this.flowLayoutPanel1.Controls.Add(this.chkUseEscapeChars);
            this.flowLayoutPanel1.Controls.Add(this.panel8);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(767, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(210, 195);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // chkIsCaseSensitive
            // 
            this.chkIsCaseSensitive.AutoSize = true;
            this.chkIsCaseSensitive.Location = new System.Drawing.Point(3, 3);
            this.chkIsCaseSensitive.Name = "chkIsCaseSensitive";
            this.chkIsCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.chkIsCaseSensitive.TabIndex = 0;
            this.chkIsCaseSensitive.Text = "Case-sensitive";
            this.chkIsCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkIsRegEx
            // 
            this.chkIsRegEx.AutoSize = true;
            this.chkIsRegEx.Location = new System.Drawing.Point(3, 26);
            this.chkIsRegEx.Name = "chkIsRegEx";
            this.chkIsRegEx.Size = new System.Drawing.Size(94, 17);
            this.chkIsRegEx.TabIndex = 1;
            this.chkIsRegEx.Text = "Enable RegEx";
            this.chkIsRegEx.UseVisualStyleBackColor = true;
            // 
            // chkKeepModifiedDate
            // 
            this.chkKeepModifiedDate.AutoSize = true;
            this.chkKeepModifiedDate.Location = new System.Drawing.Point(3, 72);
            this.chkKeepModifiedDate.Name = "chkKeepModifiedDate";
            this.chkKeepModifiedDate.Size = new System.Drawing.Size(120, 17);
            this.chkKeepModifiedDate.TabIndex = 3;
            this.chkKeepModifiedDate.Text = "Keep Modified Date";
            this.chkKeepModifiedDate.UseVisualStyleBackColor = true;
            // 
            // chkUseEscapeChars
            // 
            this.chkUseEscapeChars.AutoSize = true;
            this.chkUseEscapeChars.Location = new System.Drawing.Point(3, 141);
            this.chkUseEscapeChars.Name = "chkUseEscapeChars";
            this.chkUseEscapeChars.Size = new System.Drawing.Size(138, 17);
            this.chkUseEscapeChars.TabIndex = 6;
            this.chkUseEscapeChars.Text = "Use Escape Characters";
            this.chkUseEscapeChars.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lblEncoding);
            this.panel8.Controls.Add(this.cmbEncoding);
            this.panel8.Location = new System.Drawing.Point(0, 161);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(201, 24);
            this.panel8.TabIndex = 9;
            // 
            // lblEncoding
            // 
            this.lblEncoding.Location = new System.Drawing.Point(0, 0);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(55, 24);
            this.lblEncoding.TabIndex = 7;
            this.lblEncoding.Text = "Encoding:";
            this.lblEncoding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.ForeColor = System.Drawing.Color.White;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Location = new System.Drawing.Point(61, 3);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(137, 21);
            this.cmbEncoding.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find:";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.txtExcludeFileMask);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(499, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(480, 34);
            this.panel4.TabIndex = 0;
            // 
            // txtExcludeFileMask
            // 
            this.txtExcludeFileMask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcludeFileMask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtExcludeFileMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcludeFileMask.ForeColor = System.Drawing.Color.White;
            this.txtExcludeFileMask.Location = new System.Drawing.Point(97, 3);
            this.txtExcludeFileMask.Name = "txtExcludeFileMask";
            this.txtExcludeFileMask.Size = new System.Drawing.Size(382, 20);
            this.txtExcludeFileMask.TabIndex = 1;
            this.txtExcludeFileMask.Text = "*.dll, *.exe";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Exclude Files:";
            // 
            // txtFileMask
            // 
            this.txtFileMask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileMask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtFileMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileMask.ForeColor = System.Drawing.Color.White;
            this.txtFileMask.Location = new System.Drawing.Point(77, 3);
            this.txtFileMask.Name = "txtFileMask";
            this.txtFileMask.Size = new System.Drawing.Size(420, 20);
            this.txtFileMask.TabIndex = 0;
            this.txtFileMask.Text = "*.*";
            this.txtFileMask.Validating += new System.ComponentModel.CancelEventHandler(this.txtFileMask_Validating);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "File Mask:";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.txtFileMask);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Location = new System.Drawing.Point(3, 28);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(497, 34);
            this.panel6.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.pnlGridResults);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(0, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(982, 654);
            this.panel3.TabIndex = 42;
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.Controls.Add(this.chkIncludeSubDirectories);
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(500, 26);
            this.panel10.TabIndex = 53;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.txtExcludeDir);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Location = new System.Drawing.Point(499, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(480, 26);
            this.panel7.TabIndex = 52;
            // 
            // pnlGridResults
            // 
            this.pnlGridResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridResults.Controls.Add(this.label9);
            this.pnlGridResults.Controls.Add(this.lblResults);
            this.pnlGridResults.Controls.Add(this.txtMatchesPreview);
            this.pnlGridResults.Controls.Add(this.gvResults);
            this.pnlGridResults.Controls.Add(this.btnCancel);
            this.pnlGridResults.Controls.Add(this.lblStatus);
            this.pnlGridResults.Controls.Add(this.progressBar);
            this.pnlGridResults.Location = new System.Drawing.Point(3, 268);
            this.pnlGridResults.Name = "pnlGridResults";
            this.pnlGridResults.Size = new System.Drawing.Size(975, 386);
            this.pnlGridResults.TabIndex = 51;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Preview:";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(1, 4);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(45, 13);
            this.lblResults.TabIndex = 52;
            this.lblResults.Text = "Results:";
            // 
            // txtMatchesPreview
            // 
            this.txtMatchesPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMatchesPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtMatchesPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatchesPreview.DetectUrls = false;
            this.txtMatchesPreview.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatchesPreview.ForeColor = System.Drawing.Color.White;
            this.txtMatchesPreview.Location = new System.Drawing.Point(77, 233);
            this.txtMatchesPreview.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtMatchesPreview.Name = "txtMatchesPreview";
            this.txtMatchesPreview.ReadOnly = true;
            this.txtMatchesPreview.Size = new System.Drawing.Size(899, 150);
            this.txtMatchesPreview.TabIndex = 57;
            this.txtMatchesPreview.Text = "";
            // 
            // gvResults
            // 
            this.gvResults.AllowUserToAddRows = false;
            this.gvResults.AllowUserToDeleteRows = false;
            this.gvResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvResults.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvResults.Location = new System.Drawing.Point(77, 4);
            this.gvResults.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.gvResults.MultiSelect = false;
            this.gvResults.Name = "gvResults";
            this.gvResults.ReadOnly = true;
            this.gvResults.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.gvResults.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvResults.Size = new System.Drawing.Size(899, 180);
            this.gvResults.TabIndex = 51;
            this.gvResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvResults_CellClick);
            this.gvResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvResults_CellDoubleClick);
            this.gvResults.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvResults_CellClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(900, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Stop";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(75, 187);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.Text = "...";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(77, 203);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(817, 24);
            this.progressBar.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(984, 681);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 690);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find and Replace";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlGridResults.ResumeLayout(false);
            this.pnlGridResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResults)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ToolTip toolTip_btnSwap;
		private System.Windows.Forms.ToolTip toolTip_chkIncludeFilesWithoutMatches;
		private System.Windows.Forms.ToolTip toolTip_chkSkipBinaryFileDetection;
		private System.Windows.Forms.ToolTip toolTip_chkShowEncoding;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtExcludeDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkIncludeSubDirectories;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtFileMask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindOnly;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkIsCaseSensitive;
        private System.Windows.Forms.CheckBox chkIsRegEx;
        private System.Windows.Forms.CheckBox chkSkipBinaryFileDetection;
        private System.Windows.Forms.CheckBox chkKeepModifiedDate;
        private System.Windows.Forms.CheckBox chkShowEncoding;
        private System.Windows.Forms.CheckBox chkIncludeFilesWithoutMatches;
        private System.Windows.Forms.CheckBox chkUseEscapeChars;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenReplaceCommandLine;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Panel pnlGridResults;
        private System.Windows.Forms.Label lblResults;
        public System.Windows.Forms.DataGridView gvResults;
        private System.Windows.Forms.TextBox txtExcludeFileMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtMatchesPreview;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel7;
    }
}

