namespace zap
{
    partial class SaBan
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
            this.XaBanpanel = new System.Windows.Forms.Panel();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.PlayPauseBtn = new System.Windows.Forms.Button();
            this.SeekTrackBar = new System.Windows.Forms.TrackBar();
            this.TimeTotal = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openVideoFile = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.openXaBanImg = new System.Windows.Forms.OpenFileDialog();
            this.lampList = new System.Windows.Forms.ListBox();
            this.PutOut = new System.Windows.Forms.Button();
            this.NumberID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.PutOutOne = new System.Windows.Forms.Button();
            this.OnOffAll = new System.Windows.Forms.Button();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.MyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.UpdateStatus = new System.Windows.Forms.Button();
            this.ConnectTimeBarAndSrc = new System.Windows.Forms.CheckBox();
            this.MinusTimebar = new System.Windows.Forms.Button();
            this.PlusTimebar = new System.Windows.Forms.Button();
            this.ledTurnOn = new System.Windows.Forms.CheckBox();
            this.sourceScript = new zap.DataGridViewEx();
            this.Time1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SeekTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceScript)).BeginInit();
            this.SuspendLayout();
            // 
            // XaBanpanel
            // 
            this.XaBanpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XaBanpanel.Location = new System.Drawing.Point(2, 0);
            this.XaBanpanel.Name = "XaBanpanel";
            this.XaBanpanel.Size = new System.Drawing.Size(687, 535);
            this.XaBanpanel.TabIndex = 0;
            this.MyToolTip.SetToolTip(this.XaBanpanel, "Sa bàn");
            // 
            // VideoPanel
            // 
            this.VideoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoPanel.Location = new System.Drawing.Point(690, 3);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(173, 107);
            this.VideoPanel.TabIndex = 1;
            this.MyToolTip.SetToolTip(this.VideoPanel, "Clip");
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(768, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Mở clip";
            this.MyToolTip.SetToolTip(this.button1, "Chọn file clip");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PlayPauseBtn
            // 
            this.PlayPauseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayPauseBtn.Location = new System.Drawing.Point(823, 158);
            this.PlayPauseBtn.Name = "PlayPauseBtn";
            this.PlayPauseBtn.Size = new System.Drawing.Size(34, 23);
            this.PlayPauseBtn.TabIndex = 3;
            this.PlayPauseBtn.Text = "▶";
            this.MyToolTip.SetToolTip(this.PlayPauseBtn, "Play/Pause");
            this.PlayPauseBtn.UseVisualStyleBackColor = true;
            this.PlayPauseBtn.Click += new System.EventHandler(this.PlayPauseBtn_Click);
            // 
            // SeekTrackBar
            // 
            this.SeekTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SeekTrackBar.Location = new System.Drawing.Point(696, 107);
            this.SeekTrackBar.Maximum = 100;
            this.SeekTrackBar.Name = "SeekTrackBar";
            this.SeekTrackBar.Size = new System.Drawing.Size(158, 45);
            this.SeekTrackBar.TabIndex = 4;
            this.SeekTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.MyToolTip.SetToolTip(this.SeekTrackBar, "Thanh trượt clip");
            this.SeekTrackBar.Scroll += new System.EventHandler(this.SeekTrackBar_Scroll);
            // 
            // TimeTotal
            // 
            this.TimeTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeTotal.AutoSize = true;
            this.TimeTotal.Location = new System.Drawing.Point(800, 141);
            this.TimeTotal.Name = "TimeTotal";
            this.TimeTotal.Size = new System.Drawing.Size(57, 13);
            this.TimeTotal.TabIndex = 5;
            this.TimeTotal.Text = "/0:00:00.0";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(818, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Ghi";
            this.MyToolTip.SetToolTip(this.button2, "Ghi ra file .src");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(693, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 7;
            // 
            // openVideoFile
            // 
            this.openVideoFile.FileName = "SaBan";
            this.openVideoFile.InitialDirectory = "d:\\film";
            this.openVideoFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(692, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Mở sa bàn";
            this.MyToolTip.SetToolTip(this.button3, "Chọn file sa bàn");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.openXaBanImg_Click);
            // 
            // openXaBanImg
            // 
            this.openXaBanImg.FileName = "SaBan";
            this.openXaBanImg.InitialDirectory = "d:\\film";
            this.openXaBanImg.FileOk += new System.ComponentModel.CancelEventHandler(this.openXaBanImg_FileOk);
            // 
            // lampList
            // 
            this.lampList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lampList.FormattingEnabled = true;
            this.lampList.Location = new System.Drawing.Point(758, 214);
            this.lampList.Name = "lampList";
            this.lampList.Size = new System.Drawing.Size(61, 17);
            this.lampList.TabIndex = 12;
            // 
            // PutOut
            // 
            this.PutOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PutOut.Location = new System.Drawing.Point(692, 212);
            this.PutOut.Name = "PutOut";
            this.PutOut.Size = new System.Drawing.Size(31, 23);
            this.PutOut.TabIndex = 13;
            this.PutOut.Text = "<<";
            this.MyToolTip.SetToolTip(this.PutOut, "Kéo toàn bộ Led");
            this.PutOut.UseVisualStyleBackColor = true;
            this.PutOut.Click += new System.EventHandler(this.PutOut_Click);
            // 
            // NumberID
            // 
            this.NumberID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberID.Location = new System.Drawing.Point(778, 187);
            this.NumberID.Name = "NumberID";
            this.NumberID.Size = new System.Drawing.Size(41, 20);
            this.NumberID.TabIndex = 15;
            this.NumberID.Text = "1,2,3,4";
            this.MyToolTip.SetToolTip(this.NumberID, "Các ID sử dụng");
            this.NumberID.TextChanged += new System.EventHandler(this.NumberID_textChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(751, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "ID:";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(692, 185);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(54, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "Làm mới";
            this.MyToolTip.SetToolTip(this.button5, "Làm lại");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // PutOutOne
            // 
            this.PutOutOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PutOutOne.Location = new System.Drawing.Point(725, 212);
            this.PutOutOne.Name = "PutOutOne";
            this.PutOutOne.Size = new System.Drawing.Size(29, 23);
            this.PutOutOne.TabIndex = 17;
            this.PutOutOne.Text = "<";
            this.MyToolTip.SetToolTip(this.PutOutOne, "Kéo Led lựa chọn");
            this.PutOutOne.UseVisualStyleBackColor = true;
            this.PutOutOne.Click += new System.EventHandler(this.button6_Click);
            // 
            // OnOffAll
            // 
            this.OnOffAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OnOffAll.Location = new System.Drawing.Point(825, 208);
            this.OnOffAll.Name = "OnOffAll";
            this.OnOffAll.Size = new System.Drawing.Size(29, 23);
            this.OnOffAll.TabIndex = 18;
            this.OnOffAll.Text = "On";
            this.MyToolTip.SetToolTip(this.OnOffAll, "Đặt tất cả thành sáng/tắt");
            this.OnOffAll.UseVisualStyleBackColor = true;
            this.OnOffAll.Click += new System.EventHandler(this.OnOffAll_Click);
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeTextBox.Location = new System.Drawing.Point(743, 137);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.Size = new System.Drawing.Size(60, 20);
            this.TimeTextBox.TabIndex = 19;
            this.TimeTextBox.Text = "00:00:00.0";
            this.TimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MyToolTip.SetToolTip(this.TimeTextBox, "Thời gian hiện tại của clip");
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(723, 238);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(36, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "▼";
            this.MyToolTip.SetToolTip(this.button4, "Sinh bản ghi");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(761, 238);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(27, 23);
            this.button6.TabIndex = 22;
            this.button6.Text = "-";
            this.MyToolTip.SetToolTip(this.button6, "Xoá bản ghi. Chọn bản ghi để xoá.");
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(791, 238);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(27, 23);
            this.button7.TabIndex = 23;
            this.button7.Text = "+";
            this.MyToolTip.SetToolTip(this.button7, "Copy bản ghi. Chọn bản ghi để Copy.");
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // UpdateStatus
            // 
            this.UpdateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateStatus.Location = new System.Drawing.Point(691, 238);
            this.UpdateStatus.Name = "UpdateStatus";
            this.UpdateStatus.Size = new System.Drawing.Size(27, 23);
            this.UpdateStatus.TabIndex = 25;
            this.UpdateStatus.Text = ">";
            this.MyToolTip.SetToolTip(this.UpdateStatus, "Lấy trạng thái đèn Led hiện thời vào trường ID Text");
            this.UpdateStatus.UseVisualStyleBackColor = true;
            this.UpdateStatus.Click += new System.EventHandler(this.UpdateStatus_Click);
            // 
            // ConnectTimeBarAndSrc
            // 
            this.ConnectTimeBarAndSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectTimeBarAndSrc.AutoSize = true;
            this.ConnectTimeBarAndSrc.Location = new System.Drawing.Point(704, 139);
            this.ConnectTimeBarAndSrc.Name = "ConnectTimeBarAndSrc";
            this.ConnectTimeBarAndSrc.Size = new System.Drawing.Size(42, 17);
            this.ConnectTimeBarAndSrc.TabIndex = 26;
            this.ConnectTimeBarAndSrc.Text = "Src";
            this.MyToolTip.SetToolTip(this.ConnectTimeBarAndSrc, "Cho phép gắn kết giữa Source và TimeBar");
            this.ConnectTimeBarAndSrc.UseVisualStyleBackColor = true;
            // 
            // MinusTimebar
            // 
            this.MinusTimebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinusTimebar.Location = new System.Drawing.Point(690, 116);
            this.MinusTimebar.Name = "MinusTimebar";
            this.MinusTimebar.Size = new System.Drawing.Size(12, 23);
            this.MinusTimebar.TabIndex = 27;
            this.MinusTimebar.Text = "-";
            this.MyToolTip.SetToolTip(this.MinusTimebar, "Giảm thời gian");
            this.MinusTimebar.UseVisualStyleBackColor = true;
            this.MinusTimebar.Click += new System.EventHandler(this.MinusTimebar_Click);
            // 
            // PlusTimebar
            // 
            this.PlusTimebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlusTimebar.Location = new System.Drawing.Point(851, 115);
            this.PlusTimebar.Name = "PlusTimebar";
            this.PlusTimebar.Size = new System.Drawing.Size(12, 23);
            this.PlusTimebar.TabIndex = 28;
            this.PlusTimebar.Text = "+";
            this.MyToolTip.SetToolTip(this.PlusTimebar, "Tăng thời gian");
            this.PlusTimebar.UseVisualStyleBackColor = true;
            this.PlusTimebar.Click += new System.EventHandler(this.PlusTimebar_Click);
            // 
            // ledTurnOn
            // 
            this.ledTurnOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ledTurnOn.AutoSize = true;
            this.ledTurnOn.Location = new System.Drawing.Point(823, 189);
            this.ledTurnOn.Name = "ledTurnOn";
            this.ledTurnOn.Size = new System.Drawing.Size(44, 17);
            this.ledTurnOn.TabIndex = 29;
            this.ledTurnOn.Text = "Led";
            this.MyToolTip.SetToolTip(this.ledTurnOn, "Cho phép kết nối hệ thống sa bàn để hiển thị");
            this.ledTurnOn.UseVisualStyleBackColor = true;
            this.ledTurnOn.CheckedChanged += new System.EventHandler(this.ledTurnOn_CheckedChanged);
            // 
            // sourceScript
            // 
            this.sourceScript.AllowDrop = true;
            this.sourceScript.AllowUserToAddRows = false;
            this.sourceScript.AllowUserToOrderColumns = true;
            this.sourceScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceScript.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sourceScript.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time1,
            this.IDText});
            this.sourceScript.Location = new System.Drawing.Point(690, 268);
            this.sourceScript.Name = "sourceScript";
            this.sourceScript.Size = new System.Drawing.Size(173, 267);
            this.sourceScript.TabIndex = 24;
            this.sourceScript.SelectionChanged += new System.EventHandler(this.sourceScript_SelectionChanged);
            // 
            // Time1
            // 
            this.Time1.HeaderText = "Time";
            this.Time1.Name = "Time1";
            this.Time1.Width = 60;
            // 
            // IDText
            // 
            this.IDText.HeaderText = "IDText";
            this.IDText.Name = "IDText";
            this.IDText.Width = 200;
            // 
            // SaBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 535);
            this.Controls.Add(this.ledTurnOn);
            this.Controls.Add(this.PlusTimebar);
            this.Controls.Add(this.MinusTimebar);
            this.Controls.Add(this.ConnectTimeBarAndSrc);
            this.Controls.Add(this.UpdateStatus);
            this.Controls.Add(this.sourceScript);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.OnOffAll);
            this.Controls.Add(this.PutOutOne);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NumberID);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.PutOut);
            this.Controls.Add(this.lampList);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TimeTotal);
            this.Controls.Add(this.SeekTrackBar);
            this.Controls.Add(this.PlayPauseBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.XaBanpanel);
            this.Name = "SaBan";
            this.Text = "Sa bàn";
            this.AutoSizeChanged += new System.EventHandler(this.resizeForm);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Saban_closing);
            this.Load += new System.EventHandler(this.XaBan_Load);
            this.ClientSizeChanged += new System.EventHandler(this.resizeForm);
            this.Resize += new System.EventHandler(this.resizeForm);
            ((System.ComponentModel.ISupportInitialize)(this.SeekTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceScript)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel XaBanpanel;
        private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button PlayPauseBtn;
        private System.Windows.Forms.TrackBar SeekTrackBar;
        private System.Windows.Forms.Label TimeTotal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openVideoFile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openXaBanImg;
        private System.Windows.Forms.ListBox lampList;
        private System.Windows.Forms.Button PutOut;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox NumberID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button PutOutOne;
        private System.Windows.Forms.Button OnOffAll;
        private System.Windows.Forms.TextBox TimeTextBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ToolTip MyToolTip;
        private DataGridViewEx sourceScript;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDText;
        private System.Windows.Forms.Button UpdateStatus;
        private System.Windows.Forms.CheckBox ConnectTimeBarAndSrc;
        private System.Windows.Forms.Button MinusTimebar;
        private System.Windows.Forms.Button PlusTimebar;
        private System.Windows.Forms.CheckBox ledTurnOn;
    }
}