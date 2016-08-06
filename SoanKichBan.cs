using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Text.RegularExpressions;
namespace zap
{
    public partial class SoanKichBan : Form
    {
        public Video MyVideo;
        public int PanelWidth, PanelHeight;
        public string PlayingPosition, Duration;
        public SoanKichBan()
        {
            InitializeComponent();
        }
        public string CalculateTime(double Time)
        {
            string mm, ss, CalculatedTime;
            int h, m, s, T, ms;

            double t = Math.Floor(Time);
            ms = Convert.ToInt32(Math.Round((Time - t) * 10));
            T = Convert.ToInt32(t);

            h = (T / 3600);
            T = T % 3600;
            m = (T / 60);
            s = T % 60;
            //ms = T - m * 60 - s;
            if (m < 10)
                mm = string.Format("0{0}", m);
            else
                mm = m.ToString();
            if (s < 10)
                ss = string.Format("0{0}", s);
            else
                ss = s.ToString();

            CalculatedTime = string.Format("{0}:{1}:{2}.{3}", h, mm, ss, ms);

            return CalculatedTime;
        }

        public int CalculateVolume()
        {
            //Maximum Volume/Loudest value = 0, Minimum Volume/Mute value = -10000
            
                    return -500;
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            PlayPauseBtn.Enabled = true;
            PlayPauseBtn.Text = "Pause \u23F8";
            StartUp();
        }
        private void SeekTrackBar_Scroll(object sender, EventArgs e)
        {
            if (MyVideo != null)
            {
                MyVideo.CurrentPosition = SeekTrackBar.Value/10.0;
                TimeTextBox.Text = CalculateTime(MyVideo.CurrentPosition) + "/" + CalculateTime(MyVideo.Duration);
                string lastLine = "";
                if (sourceScript.Lines.Any())
                {
                    lastLine = sourceScript.Lines[sourceScript.Lines.Length - 1].Trim();
                }
                var regex = new Regex(@"^\[\d+:\d{2}:\d{2}.\d\]$");
                var match = regex.Match(lastLine);
                if (match.Success)
                    sourceScript.Text = sourceScript.Text.Remove(sourceScript.Text.LastIndexOf(Environment.NewLine));
                sourceScript.Text += "\r\n" + "[" + CalculateTime(MyVideo.CurrentPosition) + "]";
            }
            else
            {
                SeekTrackBar.Value = 0;
            }
        }
        private void PlayPauseBtn_Click(object sender, EventArgs e)
        {
            if (MyVideo != null)
            {
                if (MyVideo.Playing)
                {
                    MyVideo.Pause();
                    timer1.Stop();
                    PlayPauseBtn.Text = "Play \u25B6";
                }
                else
                {
                    MyVideo.Play();
                    timer1.Start();
                    PlayPauseBtn.Text = "Pause \u23F8";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeTextBox.Text = CalculateTime(MyVideo.CurrentPosition) + "/" + CalculateTime(MyVideo.Duration);
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            //TimeTextBox.Text = TimeTextBox.Tag.ToString();
            sourceScript.Clear();
            destinationScript.Clear();

        }

        private void translateBtn_Click(object sender, EventArgs e)
        {
            int chuky = 1000;
            int nhapnhay = 500;
            logText.Clear();
            destinationScript.Clear();
            string[] lines = sourceScript.Lines;
            int len = lines.Length;
            bool logicError = false;
            if (len<2)
            {
                logText.Text += "\r\n" + "Chỉ có 2 dòng";
                logicError = true;
            }

            
            var regex = new Regex(@"^\[(\d+)\]");
            var match = regex.Match(lines[0]);
            if (logicError==false)
            {
                if (!match.Success)
                {
                    logicError = true;
                    logText.Text += "\r\n" + "Dòng 1 có lỗi";
                }
                else
                {
                    if (match.Groups.Count == 0)
                    {
                        logicError = true;
                        logText.Text += "\r\n" + "Dòng 1 có lỗi";
                    }
                    else
                    {
                        chuky = Convert.ToInt32(match.Groups[1].Value);
                    }
                }
            }
            if (logicError == false)
            {
                match = regex.Match(lines[1]);
                if (!match.Success)
                {
                    logicError = true;
                    logText.Text += "\r\n" + "Dòng 2 có lỗi";
                }
                else
                {
                    if (match.Groups.Count == 0)
                    {
                        logicError = true;
                        logText.Text += "\r\n" + "Dòng 2 có lỗi";
                    }
                    else
                    {
                        nhapnhay = Convert.ToInt32(match.Groups[1].Value);
                    }
                }
            }
            regex = new Regex(
                //\[(time)\]\[(ID)\]\[(Data1)\]\[(Data2)\]\[(Data3)\]\[(Data4)\]
                @"^\[(\d+:\d{2}:\d{2}\.\d)\]\[(\d+)\]\[([01\*]+)\]\[([01\*]+)\]\[([01\*]+)\]\[([01\*]+)\]$");
            //Duyệt cú pháp
            if (logicError == false)
            {
                for(int i = 2;i < len; i++)
                {
                    match = regex.Match(lines[i]);
                    if (!match.Success)
                    {
                        logicError = true;
                        logText.Text += "\r\n" + "Dòng " + (i+1).ToString() + " có lỗi cú pháp";
                        break;
                    }
                    else
                    {
                        if (match.Groups.Count<=6)
                        {
                            logicError = true;
                            logText.Text += "\r\n" + "Dòng " + (i + 1).ToString() + " có lỗi, không đủ nhóm";
                            break;
                        }
                    }
                }
            }
            //Duyệt logic thời gian

            if (logicError == false)
            {
                double start = -1.0;
                double next = 0.0;
                var r1 = new Regex(@"(\d+):(\d{2}):(\d{2})\.(\d)");
                string v = string.Empty;
                for (int i = 2; i < len; i++)
                {
                    match = regex.Match(lines[i]);
                    v = match.Groups[1].Value;
                    
                    var m= r1.Match(v);
                    if (!m.Success)
                    {
                        logicError = true;
                        logText.Text += "\r\n" + "Dòng " + (i + 1).ToString() + " có lỗi cú pháp thời gian";
                        break;
                    }
                    else
                    {
                        if (m.Groups.Count <= 4)
                        {
                            logicError = true;
                            logText.Text += "\r\n" + "Dòng " + (i + 1).ToString() + " có lỗi cú pháp thời gian";
                            break;
                        }
                        else
                        {
                            next = Convert.ToInt32(m.Groups[1].Value) * 3600 +
                                    Convert.ToInt32(m.Groups[2].Value) * 60 +
                                    Convert.ToInt32(m.Groups[3].Value) +
                                    Convert.ToInt32(m.Groups[4].Value) * 0.1;
                            if (next<start)
                            {
                                logicError = true;
                                logText.Text += "\r\n" + "Dòng " + (i + 1).ToString() + " có lỗi logic thời gian";
                                break;
                            }
                            else
                            {
                                start = next; 
                            }
                        }
                    }
                }
            }
            //Dịch nếu không có lỗi
            if (logicError == false)
            {
                logText.Text += "OK";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form2 class
            SaBan settingsForm = new SaBan();

            // Show the settings form
            settingsForm.Show();
        }

        public void StartUp()
        {
            if (MyVideo != null)
            {
                MyVideo.Dispose();
            }

            Text = openFileDialog1.SafeFileName;
            PanelWidth = VideoPanel.Width;
            PanelHeight = VideoPanel.Height;

            MyVideo = new Video(openFileDialog1.FileName);
            MyVideo.Owner = VideoPanel;
            VideoPanel.Width = PanelWidth;
            VideoPanel.Height = PanelHeight;

            SeekTrackBar.Minimum = Convert.ToInt32(MyVideo.CurrentPosition);
            SeekTrackBar.Maximum = Convert.ToInt32(MyVideo.Duration * 10);
            SeekTrackBar.Value = 0;
            PlayPauseBtn.Text = "Pause \u23F8";

            Duration = CalculateTime(MyVideo.Duration);
            PlayingPosition = "0:00:00.0";
            TimeTextBox.Text = PlayingPosition + "/" + Duration;
            //Show the cursor...
            MyVideo.ShowCursor();

            MyVideo.Audio.Volume = CalculateVolume();

            //To show the first frame of the video to recognize it
            MyVideo.Play();
            MyVideo.Pause();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select video file..";
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.DefaultExt = ".avi";
            openFileDialog1.Filter = "AVI files|*.avi|DAT files|*.dat|All files|*.*";
            openFileDialog1.ShowDialog();
        }
    }
}
