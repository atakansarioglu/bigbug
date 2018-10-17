namespace BigBug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonNewProject = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.buttonPortOpenClose = new System.Windows.Forms.Button();
            this.comboPorts = new System.Windows.Forms.ComboBox();
            this.timerSerialPortListUpdate = new System.Windows.Forms.Timer(this.components);
            this.textBaud = new System.Windows.Forms.TextBox();
            this.browserNewProject = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.timerSerialCommListener = new System.Windows.Forms.Timer(this.components);
            this.sp = new System.IO.Ports.SerialPort(this.components);
            this.labelProjectName = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.textFilter = new System.Windows.Forms.TextBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonRefreshProject = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.labelClearFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewProject
            // 
            this.buttonNewProject.Location = new System.Drawing.Point(16, 15);
            this.buttonNewProject.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNewProject.Name = "buttonNewProject";
            this.buttonNewProject.Size = new System.Drawing.Size(100, 28);
            this.buttonNewProject.TabIndex = 0;
            this.buttonNewProject.Text = "&New Project";
            this.buttonNewProject.UseVisualStyleBackColor = true;
            this.buttonNewProject.Click += new System.EventHandler(this.buttonNewProject_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFile";
            this.openFile.Multiselect = true;
            // 
            // buttonPortOpenClose
            // 
            this.buttonPortOpenClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPortOpenClose.Location = new System.Drawing.Point(716, 15);
            this.buttonPortOpenClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPortOpenClose.Name = "buttonPortOpenClose";
            this.buttonPortOpenClose.Size = new System.Drawing.Size(100, 28);
            this.buttonPortOpenClose.TabIndex = 1;
            this.buttonPortOpenClose.Text = "&Port Open";
            this.buttonPortOpenClose.UseVisualStyleBackColor = true;
            this.buttonPortOpenClose.Click += new System.EventHandler(this.buttonPortOpenClose_Click);
            // 
            // comboPorts
            // 
            this.comboPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPorts.FormattingEnabled = true;
            this.comboPorts.Location = new System.Drawing.Point(464, 16);
            this.comboPorts.Margin = new System.Windows.Forms.Padding(4);
            this.comboPorts.Name = "comboPorts";
            this.comboPorts.Size = new System.Drawing.Size(160, 24);
            this.comboPorts.TabIndex = 2;
            this.comboPorts.SelectedIndexChanged += new System.EventHandler(this.comboPorts_SelectedIndexChanged);
            // 
            // timerSerialPortListUpdate
            // 
            this.timerSerialPortListUpdate.Enabled = true;
            this.timerSerialPortListUpdate.Interval = 1;
            this.timerSerialPortListUpdate.Tick += new System.EventHandler(this.timerSerialPortListUpdate_Tick);
            // 
            // textBaud
            // 
            this.textBaud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBaud.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBaud.Location = new System.Drawing.Point(633, 17);
            this.textBaud.Margin = new System.Windows.Forms.Padding(4);
            this.textBaud.Name = "textBaud";
            this.textBaud.Size = new System.Drawing.Size(73, 23);
            this.textBaud.TabIndex = 4;
            this.textBaud.Text = "9600";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(716, 508);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 28);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(608, 508);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 28);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clea&r";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // timerSerialCommListener
            // 
            this.timerSerialCommListener.Interval = 333;
            this.timerSerialCommListener.Tick += new System.EventHandler(this.serialCommListenerTimer_Tick);
            // 
            // labelProjectName
            // 
            this.labelProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(232, 21);
            this.labelProjectName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(0, 17);
            this.labelProjectName.TabIndex = 9;
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(16, 514);
            this.labelAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(172, 17);
            this.labelAuthor.TabIndex = 10;
            this.labelAuthor.Text = "Atakan SARIOGLU - 2016";
            // 
            // saveFile
            // 
            this.saveFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // textFilter
            // 
            this.textFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilter.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textFilter.Location = new System.Drawing.Point(363, 511);
            this.textFilter.Margin = new System.Windows.Forms.Padding(4);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(212, 23);
            this.textFilter.TabIndex = 13;
            this.textFilter.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            // 
            // labelFilter
            // 
            this.labelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(316, 514);
            this.labelFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(39, 17);
            this.labelFilter.TabIndex = 14;
            this.labelFilter.Text = "Filter";
            // 
            // buttonRefreshProject
            // 
            this.buttonRefreshProject.Location = new System.Drawing.Point(124, 15);
            this.buttonRefreshProject.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRefreshProject.Name = "buttonRefreshProject";
            this.buttonRefreshProject.Size = new System.Drawing.Size(100, 28);
            this.buttonRefreshProject.TabIndex = 15;
            this.buttonRefreshProject.Text = "Refresh";
            this.buttonRefreshProject.UseVisualStyleBackColor = true;
            this.buttonRefreshProject.Visible = false;
            this.buttonRefreshProject.Click += new System.EventHandler(this.buttonRefreshProject_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid.CausesValidation = false;
            this.dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid.EnableHeadersVisualStyles = false;
            this.dataGrid.Location = new System.Drawing.Point(0, 53);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dataGrid.RowTemplate.Height = 18;
            this.dataGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(831, 446);
            this.dataGrid.TabIndex = 16;
            // 
            // labelClearFilter
            // 
            this.labelClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelClearFilter.AutoSize = true;
            this.labelClearFilter.Location = new System.Drawing.Point(581, 514);
            this.labelClearFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelClearFilter.Name = "labelClearFilter";
            this.labelClearFilter.Size = new System.Drawing.Size(17, 17);
            this.labelClearFilter.TabIndex = 18;
            this.labelClearFilter.Text = "X";
            this.labelClearFilter.Visible = false;
            this.labelClearFilter.Click += new System.EventHandler(this.labelClearFilter_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 544);
            this.Controls.Add(this.labelClearFilter);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.buttonRefreshProject);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelProjectName);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBaud);
            this.Controls.Add(this.comboPorts);
            this.Controls.Add(this.buttonPortOpenClose);
            this.Controls.Add(this.buttonNewProject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(847, 580);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BigBug v3.0.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNewProject;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button buttonPortOpenClose;
        private System.Windows.Forms.ComboBox comboPorts;
        private System.Windows.Forms.Timer timerSerialPortListUpdate;
        private System.Windows.Forms.TextBox textBaud;
        private System.Windows.Forms.FolderBrowserDialog browserNewProject;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Timer timerSerialCommListener;
        private System.IO.Ports.SerialPort sp;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.TextBox textFilter;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Button buttonRefreshProject;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label labelClearFilter;
    }
}

