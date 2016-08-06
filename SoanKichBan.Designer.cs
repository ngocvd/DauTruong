namespace zap
{
    partial class SoanKichBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoanKichBan));
            this.sourceScript = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.destinationScript = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.translateBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.newBtn = new System.Windows.Forms.Button();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.SeekTrackBar = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TimeTextBox = new System.Windows.Forms.Label();
            this.PlayPauseBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.logText = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SeekTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // sourceScript
            // 
            this.sourceScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceScript.Location = new System.Drawing.Point(13, 25);
            this.sourceScript.Multiline = true;
            this.sourceScript.Name = "sourceScript";
            this.sourceScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourceScript.Size = new System.Drawing.Size(387, 378);
            this.sourceScript.TabIndex = 0;
            this.sourceScript.Text = "[100]; Tốc độ đọc (ms)\r\n[500]; Tốc độ nhấp nháy (ms)\r\n[00:00:00.0][ID][00001111][" +
    "00001111][00001111][0000111*]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kịch bản nguồn:";
            // 
            // destinationScript
            // 
            this.destinationScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationScript.Location = new System.Drawing.Point(409, 263);
            this.destinationScript.Multiline = true;
            this.destinationScript.Name = "destinationScript";
            this.destinationScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.destinationScript.Size = new System.Drawing.Size(292, 140);
            this.destinationScript.TabIndex = 2;
            this.destinationScript.Text = resources.GetString("destinationScript.Text");
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kịch bản đích:";
            // 
            // translateBtn
            // 
            this.translateBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.translateBtn.Location = new System.Drawing.Point(321, 406);
            this.translateBtn.Name = "translateBtn";
            this.translateBtn.Size = new System.Drawing.Size(79, 23);
            this.translateBtn.TabIndex = 4;
            this.translateBtn.Text = "Dịch ==>";
            this.translateBtn.UseVisualStyleBackColor = true;
            this.translateBtn.Click += new System.EventHandler(this.translateBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(615, 406);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(59, 23);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Ghi";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // newBtn
            // 
            this.newBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newBtn.Location = new System.Drawing.Point(210, 406);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(59, 23);
            this.newBtn.TabIndex = 6;
            this.newBtn.Text = "Làm mới";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // VideoPanel
            // 
            this.VideoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoPanel.Location = new System.Drawing.Point(409, 9);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(289, 171);
            this.VideoPanel.TabIndex = 7;
            // 
            // SeekTrackBar
            // 
            this.SeekTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SeekTrackBar.Location = new System.Drawing.Point(481, 197);
            this.SeekTrackBar.Name = "SeekTrackBar";
            this.SeekTrackBar.Size = new System.Drawing.Size(220, 45);
            this.SeekTrackBar.TabIndex = 8;
            this.SeekTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.SeekTrackBar.Scroll += new System.EventHandler(this.SeekTrackBar_Scroll);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(409, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Mở Clip";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeTextBox.AutoSize = true;
            this.TimeTextBox.Location = new System.Drawing.Point(584, 246);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.Size = new System.Drawing.Size(34, 13);
            this.TimeTextBox.TabIndex = 10;
            this.TimeTextBox.Text = "00:00";
            // 
            // PlayPauseBtn
            // 
            this.PlayPauseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PlayPauseBtn.Enabled = false;
            this.PlayPauseBtn.Location = new System.Drawing.Point(409, 217);
            this.PlayPauseBtn.Name = "PlayPauseBtn";
            this.PlayPauseBtn.Size = new System.Drawing.Size(59, 23);
            this.PlayPauseBtn.TabIndex = 11;
            this.PlayPauseBtn.Text = "Pause \\u23F8";
            this.PlayPauseBtn.UseVisualStyleBackColor = true;
            this.PlayPauseBtn.Click += new System.EventHandler(this.PlayPauseBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 418);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Log:";
            // 
            // logText
            // 
            this.logText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logText.Location = new System.Drawing.Point(15, 435);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logText.Size = new System.Drawing.Size(695, 59);
            this.logText.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(129, 406);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Sa bàn";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SoanKichBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 494);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PlayPauseBtn);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SeekTrackBar);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.translateBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.destinationScript);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceScript);
            this.Name = "SoanKichBan";
            this.Text = "Soạn kịch bản";
            ((System.ComponentModel.ISupportInitialize)(this.SeekTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox destinationScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button translateBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.TrackBar SeekTrackBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label TimeTextBox;
        private System.Windows.Forms.Button PlayPauseBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logText;
        private System.Windows.Forms.Button button2;
    }
}