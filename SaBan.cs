using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Threading;

namespace zap
{

    public partial class SaBan : Form
    {
        MyModbus mb = new MyModbus();
        public char[] delimiterChars = { ',', ';' };
        public Video MyVideo;
        public int PanelWidth, PanelHeight;
        public string videofilename;
        public string PlayingPosition, Duration;
        public Image imgXaBan;
        Regex regex = new Regex(@"(\d+):(\d{2}):(\d{2})\.(\d+)");
        Regex regex01 = new Regex(@"\[(\d+:\d{2}:\d{2}\.\d+)\]([\[\[ID:\d+\][\[\d+\]]+\]]+)");
        Regex regex1 = new Regex(@"\[\[ID:(\d+)\]([\[\d+\]]+)\]");
        Regex regex2 = new Regex(@"\[(\d+)\]");
        private int stepTime = 10; // 10=.1s ==100~.01s  ==1000~.001s
        string CurFolder = Application.StartupPath + @"\data\";
        //LedLamp ledLamp;
        public SaBan()
        {
            InitializeComponent();
        }
        public string CalculateTime(double Time)
        {
            string mm, hh, ss, CalculatedTime;
            int h, m, s, T, ms;

            double t = Math.Floor(Time);
            ms = Convert.ToInt32(Math.Round((Time - t) * stepTime));
            T = Convert.ToInt32(t);

            h = (T / 3600);
            T = T % 3600;
            m = (T / 60);
            s = T % 60;
            //ms = T - m * 60 - s;
            if (h < 10)
                hh = string.Format("0{0}", h);
            else
                hh = h.ToString();
            if (m < 10)
                mm = string.Format("0{0}", m);
            else
                mm = m.ToString();
            if (s < 10)
                ss = string.Format("0{0}", s);
            else
                ss = s.ToString();

            CalculatedTime = string.Format("{0}:{1}:{2}.{3}", hh, mm, ss, ms);

            return CalculatedTime;
        }
        public double RevertCalculateTime(string Time)
        {
            double CalculatedTime=0.0;
            //Regex regex = new Regex(@"(\d+):(\d{2}):(\d{2})\.(\d+)");
            Match match = regex.Match(Time);
            if (match.Success)
            {
                int len =  match.Groups[4].Value.ToString().Length;
                CalculatedTime = Convert.ToDouble(match.Groups[4].Value.ToString());
                for (int i = 0; i < len; i++)
                    CalculatedTime /= stepTime;
                CalculatedTime += Convert.ToDouble(match.Groups[3].Value.ToString());
                CalculatedTime += 60 * Convert.ToDouble(match.Groups[2].Value.ToString());
                CalculatedTime += 60 * 60 * Convert.ToDouble(match.Groups[1].Value.ToString());
            }

            return CalculatedTime;

        } 
        public int GetRowMaxLessThanOrEqual(string Time)
        {
            int index = 0;
            var compareDate = RevertCalculateTime(Time); //or whatever date you want
            //sourceScript.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            //sourceScript.s
            //sourceScript.Sort(sourceScript.Columns[1], ListSortDirection.Ascending);
            foreach (DataGridViewRow r in sourceScript.Rows)
            {
                var cellDate = RevertCalculateTime(r.Cells[0].Value.ToString());
                if (cellDate <= compareDate)
                    index = r.Index;
                else
                    break;

            }

            return index;
        }
        public int CalculateVolume()
        {
            //Maximum Volume/Loudest value = 0, Minimum Volume/Mute value = -10000

            return -500;

        }
        public void SaveLocToFile()
        {
            var children = XaBanpanel.Controls.OfType<LedLamp>();
            List<LedLampObj> list = new List<LedLampObj>();
            foreach (LedLamp c in children)
            {
                LedLampObj led = new LedLampObj();
                led.Name = c.Name;
                led.Text = c.Name;
                led.Status = c.Status;
                led.X = c.Location.X;
                led.Y = c.Location.Y;
                list.Add(led);
            }
            Locations.WriteToXmlFile<List<LedLampObj>>(CurFolder + Properties.Settings.Default.SabanFileLoc, list);
        }
        public void LoadLocFromFile()
        {
            List<LedLampObj> list = new List<LedLampObj>();
            list = Locations.ReadFromXmlFile<List<LedLampObj>>(CurFolder + Properties.Settings.Default.SabanFileLoc);
            if(list!=null)
            foreach (LedLampObj c in list)
            {
                LedLamp ledLamp = new LedLamp();
                // ledLamp1
                // 
                ledLamp.Location = new System.Drawing.Point(c.X, c.Y);
                //ledLamp.Size = new System.Drawing.Size(200, 200);
                ledLamp.Text = c.Name;
                ledLamp.Name = c.Name;
                ledLamp.Status = c.Status;
                ledLamp.MouseDown += new MouseEventHandler(ledLamp_MouseDown);
                ledLamp.MouseMove += new MouseEventHandler(ledLamp_MouseMove);
                ledLamp.MouseUp += new MouseEventHandler(ledLamp_MouseUp);
                XaBanpanel.Controls.Add(ledLamp);
            }
            
        }
        public void LoadSrc()
        {
            string fileName = CurFolder + Properties.Settings.Default.SrcFile;
            if (!File.Exists(fileName)) return;
            String fileText = null;
            try
            {
                fileText = File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
                //Handle exception here
            }
            using (StringReader sr = new StringReader(fileText))
            {
                string line;
                //Regex regex = new Regex(@"(\d+):(\d{2}):(\d{2})\.(\d+)");
                //Regex regex1 = new Regex(@"\[\[ID:(\d+)\]([\[\d+\]]+)\]");
                //Regex regex2 = new Regex(@"\[(\d+)\]");
                sourceScript.Rows.Clear();
                sourceScript.Refresh();
                while ((line = sr.ReadLine()) != null)
                {

                    //int l;
                    line = line.Trim();
                    foreach (Match match in regex01.Matches(line))
                    {
                        if (match.Groups.Count > 1)
                        {
                            string[] row = { match.Groups[1].Value.ToString(), match.Groups[2].Value.ToString() };
                            sourceScript.Rows.Add(row);
                        }
                    }
                }

            }
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            PlayPauseBtn.Enabled = true;
            PlayPauseBtn.Text = "\u23F8";
            StartUp(openVideoFile.FileName);
        }
        private void SeekTrackBar_Scroll(object sender, EventArgs e)
        {
            if (MyVideo != null)
            {
                MyVideo.CurrentPosition = SeekTrackBar.Value / (stepTime * 1.0);
                TimeTextBox.Text = CalculateTime(MyVideo.CurrentPosition);
                TimeTotal.Text =  "/" + CalculateTime(MyVideo.Duration);
                if  (ConnectTimeBarAndSrc.Checked == true)
                {
                    int r = GetRowMaxLessThanOrEqual(TimeTextBox.Text);
                    sourceScript.ClearSelection();
                    sourceScript.CurrentCell = sourceScript.Rows[r].Cells[0];
                    sourceScript.Rows[r].Selected = true;
                }                
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
                    PlayPauseBtn.Text = "\u25B6";
                }
                else
                {
                    MyVideo.Play();
                    timer1.Start();
                    PlayPauseBtn.Text = "\u23F8";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeTextBox.Text = CalculateTime(MyVideo.CurrentPosition);
            TimeTotal.Text = "/" + CalculateTime(MyVideo.Duration);
            SeekTrackBar.Value = Convert.ToInt32(MyVideo.CurrentPosition * stepTime);
        }
        public void StartUp(string videofile)
        {
            if (MyVideo != null)
            {
                MyVideo.Dispose();
            }

            Text = openVideoFile.FileName;
            PanelWidth = VideoPanel.Width;
            PanelHeight = VideoPanel.Height;

            MyVideo = new Video(videofile);
            videofilename = videofile;
            MyVideo.Owner = VideoPanel;
            VideoPanel.Width = PanelWidth;
            VideoPanel.Height = PanelHeight;

            SeekTrackBar.Minimum = Convert.ToInt32(MyVideo.CurrentPosition);
            SeekTrackBar.Maximum = Convert.ToInt32(MyVideo.Duration * stepTime);
            SeekTrackBar.Value = 0;
            PlayPauseBtn.Text = "\u25B6";

            Duration = CalculateTime(MyVideo.Duration);
            PlayingPosition = "0:00:00.0";
            //TimeTotal.Text = PlayingPosition + "/" + Duration;
            TimeTotal.Text = "/" + Duration;
            //Show the cursor...
            MyVideo.ShowCursor();

            MyVideo.Audio.Volume = CalculateVolume();

            //To show the first frame of the video to recognize it
            MyVideo.Play();
            MyVideo.Pause();

        }
        /// Resize image with a directory as source
        /// </summary>
        /// <param name="OriginalFileLocation">Image location</param>
        /// <param name="heigth">new height</param>
        /// <param name="width">new width</param>
        /// <param name="keepAspectRatio">keep the aspect ratio</param>
        /// <param name="getCenter">return the center bit of the image</param>
        /// <returns>image with new dimentions</returns>
        public Image resizeImageFromFile(Image OriginalImg, int heigth, int width, Boolean keepAspectRatio, Boolean getCenter)
        {
            int newheigth = heigth;
            System.Drawing.Image FullsizeImage = OriginalImg;

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (keepAspectRatio || getCenter)
            {
                int bmpY = 0;
                double resize = (double)FullsizeImage.Width / (double)width;//get the resize vector
                if (getCenter)
                {
                    bmpY = (int)((FullsizeImage.Height - (heigth * resize)) / 2);// gives the Y value of the part that will be cut off, to show only the part in the center
                    Rectangle section = new Rectangle(new Point(0, bmpY), new Size(FullsizeImage.Width, (int)(heigth * resize)));// create the section to cut of the original image
                                                                                                                                 //System.Console.WriteLine("the section that will be cut off: " + section.Size.ToString() + " the Y value is minimized by: " + bmpY);
                    Bitmap orImg = new Bitmap((Bitmap)FullsizeImage);//for the correct effect convert image to bitmap.
                    FullsizeImage.Dispose();//clear the original image
                    using (Bitmap tempImg = new Bitmap(section.Width, section.Height))
                    {
                        Graphics cutImg = Graphics.FromImage(tempImg);//              set the file to save the new image to.
                        cutImg.DrawImage(orImg, 0, 0, section, GraphicsUnit.Pixel);// cut the image and save it to tempImg
                        FullsizeImage = tempImg;//save the tempImg as FullsizeImage for resizing later
                        orImg.Dispose();
                        cutImg.Dispose();
                        return FullsizeImage.GetThumbnailImage(width, heigth, null, IntPtr.Zero);
                    }
                }
                else newheigth = (int)(FullsizeImage.Height / resize);//  set the new heigth of the current image
            }//return the image resized to the given heigth and width
            return FullsizeImage.GetThumbnailImage(width, newheigth, null, IntPtr.Zero);
        }
        private void openXaBanImg_FileOk(object sender, CancelEventArgs e)
        {
            imgXaBan= Image.FromFile
                   (openXaBanImg.FileName);
            Image img = resizeImageFromFile(imgXaBan, XaBanpanel.Height, XaBanpanel.Width, true, false);
            XaBanpanel.BackgroundImage = img;
        }

        private void resizeForm(object sender, EventArgs e)
        {
            if (imgXaBan != null)
            {
                Image img = resizeImageFromFile(imgXaBan, XaBanpanel.Height, XaBanpanel.Width, true, false);
                XaBanpanel.BackgroundImage = img;
            }
        }

        private Control activeControl;
        private Point previousLocation;

        void ledLamp_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as Control;
            previousLocation = e.Location;
            Cursor = Cursors.Hand;
        }
        
        void ledLamp_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl != sender)
                return;

            var location = activeControl.Location;
            location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            activeControl.Location = location;
        }

        void ledLamp_MouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openVideoFile.Title = "Select video file..";
            //openFileDialog1.InitialDirectory = Application.StartupPath;
            openVideoFile.DefaultExt = ".avi";
            openVideoFile.Filter = "AVI files|*.avi|DAT files|*.dat|All files|*.*";
            openVideoFile.ShowDialog();
        }
        private void openXaBanImg_Click(object sender, EventArgs e)
        {
            openXaBanImg.Title = "Select video file..";
            //openXaBanImg.InitialDirectory = Application.StartupPath;
            openXaBanImg.DefaultExt = ".jpg";
            openXaBanImg.Filter = "jpg files|*.jpg|png files|*.png|png files|*.gif|All files|*.*";
            openXaBanImg.ShowDialog();
        }
       

        private void button5_Click(object sender, EventArgs e)
        {
            XaBanGenerate();
            var children = XaBanpanel.Controls.OfType<LedLamp>();
            XaBanpanel.Controls.Clear();
            foreach (LedLamp c in children)
                c.Dispose();
        }

        private void PutOut_Click(object sender, EventArgs e)
        {
            int x=20, y=30;
            int i = 0;
            while (lampList.Items.Count>0)
            {
                LedLamp ledLamp = new LedLamp();
                // ledLamp1
                // 
                ledLamp.Location = new System.Drawing.Point(x, y);
                //ledLamp.Size = new System.Drawing.Size(200, 200);
                ledLamp.Text = lampList.Items[lampList.TopIndex].ToString();
                ledLamp.Name = lampList.Items[lampList.TopIndex].ToString();
                ledLamp.MouseDown += new MouseEventHandler(ledLamp_MouseDown);
                ledLamp.MouseMove += new MouseEventHandler(ledLamp_MouseMove);
                ledLamp.MouseUp += new MouseEventHandler(ledLamp_MouseUp);
                XaBanpanel.Controls.Add(ledLamp);
                lampList.Items.RemoveAt(lampList.TopIndex);
                x += 40;
                //y += 10;
                i++;
                if (i == 8)
                {
                    x = 20;
                    y += 20;
                    i = 0;
                }
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lampList.Items.Count == 0) return;
            LedLamp ledLamp = new LedLamp();
            // ledLamp1
            // 
            if (lampList.SelectedIndex == -1)
                lampList.SelectedIndex = lampList.TopIndex;
            ledLamp.Location = new System.Drawing.Point(100, 150);
            //ledLamp.Size = new System.Drawing.Size(200, 200);
            ledLamp.Text = lampList.Items[lampList.SelectedIndex].ToString();
            ledLamp.Name= lampList.Items[lampList.SelectedIndex].ToString();
            ledLamp.MouseDown += new MouseEventHandler(ledLamp_MouseDown);
            ledLamp.MouseMove += new MouseEventHandler(ledLamp_MouseMove);
            ledLamp.MouseUp += new MouseEventHandler(ledLamp_MouseUp);
            XaBanpanel.Controls.Add(ledLamp);
            lampList.Items.RemoveAt(lampList.SelectedIndex);
        }
        private bool _IsOn = true;

        public bool IsOn
        {
            get
            {
                return _IsOn;
            }
            set
            {
                _IsOn = value;
                OnOffAll.Text = _IsOn ? "On" : "Off";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string l;
            //int n = Convert.ToInt32(NumberID.Text);
            string s1 = TimeTextBox.Text ;
            
            string s2 = GetStringStatus();

            
            string[] row = { s1, s2 };
            //var datagridViewItem = new DataGridViewRow();
            //datagridViewItem.
            sourceScript.Rows.Add(row);
            //sourceScript.Items.Add(s);
            
        }
        private string GetStringStatus()
        {
            string s2 = "";
            string l;
            int n  = Properties.Settings.Default.numberByte;
            char[] delimiterChars = { ',', ';' };
            string[] numbers = NumberID.Text.Split(delimiterChars);
            //int n = Convert.ToInt32(NumberID.Text);
            foreach (string s in numbers)
            {
                int i = Convert.ToInt32(s.Trim());
                s2 += @"[[ID:" + i.ToString() + "]";
                for (int i1 = 0; i1 < n; i1++)
                {
                    s2 += @"[";
                    for (int i2 = 0; i2 < 8; i2++)
                    {

                        l = i.ToString() + "." + i1.ToString() + "." + i2.ToString();
                        Control[] controls = XaBanpanel.Controls.Find(l, true);
                        if (controls.Length > 0)
                        {
                            LedLamp ctr = (LedLamp)controls[0];
                            s2 += ctr.Status.ToString();
                        }
                        else {
                            // Do logic when there are no picture boxes
                        }
                    }
                    s2 += "]";
                }
                s2 += "]";
            }
            return s2;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in sourceScript.SelectedRows)
                sourceScript.Rows.Remove(item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string extension = System.IO.Path.GetExtension(videofilename);
            string result = videofilename.Substring(0, videofilename.Length - extension.Length);
            result += ".src";
            sourceScript.Sort(sourceScript.Columns[0], ListSortDirection.Ascending);
            sourceScript.Columns[0].HeaderCell.SortGlyphDirection =
                SortOrder.Ascending ;

            using (var tw = new StreamWriter(result))
            {
                foreach (DataGridViewRow item in sourceScript.Rows)
                {
                    tw.WriteLine("[" + item.Cells[0].Value+ "]" + item.Cells[1].Value);
                }
            }
            MessageBox.Show("Đã ghi vào file: "+ result);
            //int rc = sourceScript.CurrentCell.RowIndex;
            //MessageBox.Show(rc.ToString());
        }

        private void OnOffAll_Click(object sender, EventArgs e)
        {
            foreach (var pb in XaBanpanel.Controls.OfType<LedLamp>())
            {
                if (IsOn)
                    pb.Status = 0;
                else
                    pb.Status = 1;
                //MessageBox.Show("ok");
            }
            IsOn = !IsOn;
            
        }

        private void Saban_closing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            SaveLocToFile();
            mb.Dispose();
            Application.Exit();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in sourceScript.SelectedRows)
            {
                //DataGridViewRow it = new DataGridViewRow();
                DataGridViewRow clonedRow = (DataGridViewRow)item.Clone();
                for (Int32 index = 0; index < item.Cells.Count; index++)
                {
                    clonedRow.Cells[index].Value = item.Cells[index].Value;
                }

                sourceScript.Rows.Add(clonedRow);
            }
        }

        private void NumberID_textChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IDs = NumberID.Text;
            //Properties.Settings.Default.Save();
        }

        private void sourceScript_SelectionChanged(object sender, EventArgs e)
        {
            if (sourceScript.CurrentCell == null) return;
            int cr = sourceScript.CurrentCell.RowIndex;
            if (ConnectTimeBarAndSrc.Checked==false)
            {
                TimeTextBox.Text = sourceScript.Rows[cr].Cells[0].Value.ToString();
                if (MyVideo != null)
                {
                    double t = RevertCalculateTime(TimeTextBox.Text);
                    MyVideo.CurrentPosition = t;
                    SeekTrackBar.Value = Convert.ToInt32(MyVideo.CurrentPosition * stepTime);
                }
            }
            try
            {
                LedLampTurn(sourceScript.Rows[cr].Cells[1].Value.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void XaBan_Load(object sender, EventArgs e)
        {
            
            //openXaBanImg.FileName = Properties.Settings.Default.SabanPicture;
            NumberID.Text = Properties.Settings.Default.IDs.ToString();
            //videofilename = Properties.Settings.Default.VideoFile;
            PlayPauseBtn.Enabled = true;
            PlayPauseBtn.Text = "\u23F8";
            StartUp(CurFolder + Properties.Settings.Default.VideoFile);
            imgXaBan = Image.FromFile
                   (CurFolder + Properties.Settings.Default.SabanPicture);
            Image img = resizeImageFromFile(imgXaBan, XaBanpanel.Height, XaBanpanel.Width, true, false);
            XaBanpanel.BackgroundImage = img;
            XaBanGenerate();
            LoadLocFromFile();
            LoadSrc();
            
            
            //Properties.Settings.Default.Save();
        }

        private void UpdateStatus_Click(object sender, EventArgs e)
        {
            if(sourceScript.CurrentCell!=null)
            {
                int ri = sourceScript.CurrentCell.RowIndex;
                string s = GetStringStatus();
                sourceScript.Rows[ri].Cells[1].Value = s;
            }
        }

        private void PlusTimebar_Click(object sender, EventArgs e)
        {
            if(SeekTrackBar.Value<SeekTrackBar.Maximum)
            {
                SeekTrackBar.Value++;
                MyVideo.CurrentPosition = SeekTrackBar.Value / (stepTime * 1.0);
                TimeTextBox.Text = CalculateTime(MyVideo.CurrentPosition);
            }
            
        }

        private void MinusTimebar_Click(object sender, EventArgs e)
        {
            if (SeekTrackBar.Value > SeekTrackBar.Minimum)
            {
                SeekTrackBar.Value--;
                MyVideo.CurrentPosition = SeekTrackBar.Value / (stepTime * 1.0);
                TimeTextBox.Text = CalculateTime(MyVideo.CurrentPosition);
            }
        }

        private void ledTurnOn_CheckedChanged(object sender, EventArgs e)
        {
            if (ledTurnOn.Checked)
            {
                string s = "";
                try
                {
                    s = mb.CheckId();
                }
                catch { }
                if(s!="")
                    MessageBox.Show("Các ID không sẵn sàng: " + s);
            }
        }

        private void LedLampTurn(string text)
        {
            //Regex regex1 = new Regex(@"\[\[ID:(\d+)\]([\[\d+\]]+)\]");
            //Regex regex2 = new Regex(@"\[(\d+)\]");
            //int l;
            //Push to Mosbus RTU
            if (ledTurnOn.Checked)
                mb.LedTurnOn(text);
            //LED lamp turn on/off
            foreach (Match match in regex1.Matches(text))
            {
                if (match.Groups.Count > 1)
                {
                    int id = Convert.ToInt32(match.Groups[1].Value);
                    MatchCollection m = regex2.Matches(match.Groups[2].Value);
                    for (int i = 0; i < m.Count; i++)
                    {
                        if (m[i].Groups.Count > 0)
                        {
                            string str = m[i].Groups[1].Value;
                            string name = id.ToString()+"."+i.ToString()+".";
                            for (int j = 0; j < str.Length; j++)
                            {
                                Control[] controls = XaBanpanel.Controls.Find(name + j.ToString(), true);
                                if (controls.Length > 0)
                                {
                                    LedLamp led = (LedLamp)controls[0];
                                    //l= Convert.ToInt32(str[j].ToString());
                                    led.Status = Convert.ToInt32(str[j].ToString());
                                }
                            }
                        }

                    }
                }
            }
        }
       
        private void XaBanGenerate()
        {
            
            string[] numbers = NumberID.Text.Split(delimiterChars);
            int n = Properties.Settings.Default.numberByte;
            foreach (string s in numbers)
            {
                int i = Convert.ToInt32(s.Trim());
                for (int i1 = 0; i1 < n; i1++)
                {
                    for (int i2 = 0; i2 < 8; i2++)
                    {
                        lampList.Items.Add(i.ToString() + "." + i1.ToString() + "." + i2.ToString());
                    }
                }
            }
        }

    }
}
