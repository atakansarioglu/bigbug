namespace WindowsFormsApplication1
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
            this.connect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.baud = new System.Windows.Forms.TextBox();
            this.text = new System.Windows.Forms.TextBox();
            this.parity = new System.Windows.Forms.CheckBox();
            this.sp = new System.IO.Ports.SerialPort(this.components);
            this.clear = new System.Windows.Forms.Button();
            this.count = new System.Windows.Forms.NumericUpDown();
            this.repeat = new System.Windows.Forms.Button();
            this.auto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.count)).BeginInit();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connect.AutoSize = true;
            this.connect.Location = new System.Drawing.Point(488, 12);
            this.connect.Name = "connect";
            this.connect.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.connect.Size = new System.Drawing.Size(82, 21);
            this.connect.TabIndex = 0;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.CheckedChanged += new System.EventHandler(this.connect_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(53, 10);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(100, 22);
            this.port.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baud";
            // 
            // baud
            // 
            this.baud.Location = new System.Drawing.Point(222, 10);
            this.baud.Name = "baud";
            this.baud.Size = new System.Drawing.Size(100, 22);
            this.baud.TabIndex = 4;
            this.baud.Text = "9600";
            // 
            // text
            // 
            this.text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text.Enabled = false;
            this.text.Location = new System.Drawing.Point(12, 48);
            this.text.Multiline = true;
            this.text.Name = "text";
            this.text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text.Size = new System.Drawing.Size(558, 165);
            this.text.TabIndex = 5;
            this.text.Text = "HE\r\nmX-1.250\r\nmX32.768\r\nTM12 34 1 5\r\nTM0 34 0 5\r\nDsBigBug\\ remote\\ printf\r\n";
            this.text.TextChanged += new System.EventHandler(this.text_TextChanged);
            this.text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_KeyDown);
            // 
            // parity
            // 
            this.parity.AutoSize = true;
            this.parity.Location = new System.Drawing.Point(344, 12);
            this.parity.Name = "parity";
            this.parity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.parity.Size = new System.Drawing.Size(66, 21);
            this.parity.TabIndex = 6;
            this.parity.Text = "Parity";
            this.parity.ThreeState = true;
            this.parity.UseVisualStyleBackColor = true;
            // 
            // sp
            // 
            this.sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sp_DataReceived);
            // 
            // clear
            // 
            this.clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clear.Location = new System.Drawing.Point(495, 218);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 7;
            this.clear.Text = "clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // count
            // 
            this.count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.count.Location = new System.Drawing.Point(76, 219);
            this.count.Maximum = new decimal(new int[] {
            1073741824,
            0,
            0,
            0});
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(120, 22);
            this.count.TabIndex = 8;
            this.count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // repeat
            // 
            this.repeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.repeat.Enabled = false;
            this.repeat.Location = new System.Drawing.Point(202, 218);
            this.repeat.Name = "repeat";
            this.repeat.Size = new System.Drawing.Size(75, 23);
            this.repeat.TabIndex = 9;
            this.repeat.Text = "send";
            this.repeat.UseVisualStyleBackColor = true;
            this.repeat.Click += new System.EventHandler(this.repeat_Click);
            // 
            // auto
            // 
            this.auto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.auto.AutoSize = true;
            this.auto.Checked = true;
            this.auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.auto.Location = new System.Drawing.Point(12, 220);
            this.auto.Name = "auto";
            this.auto.Size = new System.Drawing.Size(58, 21);
            this.auto.TabIndex = 10;
            this.auto.Text = "auto";
            this.auto.UseVisualStyleBackColor = true;
            this.auto.CheckedChanged += new System.EventHandler(this.auto_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 253);
            this.Controls.Add(this.auto);
            this.Controls.Add(this.repeat);
            this.Controls.Add(this.count);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.parity);
            this.Controls.Add(this.text);
            this.Controls.Add(this.baud);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connect);
            this.Name = "MainForm";
            this.Text = "BigBugTester";
            ((System.ComponentModel.ISupportInitialize)(this.count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox connect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox baud;
        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.CheckBox parity;
        private System.IO.Ports.SerialPort sp;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.NumericUpDown count;
        private System.Windows.Forms.Button repeat;
        private System.Windows.Forms.CheckBox auto;
    }
}

