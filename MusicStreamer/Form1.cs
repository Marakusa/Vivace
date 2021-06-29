using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Vivace
{
    public partial class Form1 : Form
    {
        public string version = "0.2.1";

        public string source = "";

        MediaPlayer wplayer;

        public List<string> songs = new List<string>();
        public List<string> searchList = new List<string>();
        public List<string> searched = new List<string>();

        public bool paused = false;
        public bool update = false;


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 5; // you can rename this variable if you like

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        PlaylistImport playlistImport;

        //public Spotify spotify;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            Width = Properties.Settings.Default.windowwidth;
            Height = Properties.Settings.Default.windowheight;

            if (Properties.Settings.Default.windowmaximized)
            {
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                MaximizedBounds = new Rectangle(MaximizedBounds.X - 5, MaximizedBounds.Y - 5, MaximizedBounds.Width + 10, MaximizedBounds.Height + 10);
                WindowState = FormWindowState.Maximized;

                window_maximize.Text = "❐";
            }

            wplayer = new MediaPlayer();
            wplayer.Volume = (double)(Properties.Settings.Default.volume / 100.0);
            volume.Value = Properties.Settings.Default.volume;
            shuffle.Checked = Properties.Settings.Default.shuffle;

            WebRequest webRequest = WebRequest.Create("https://aquaticstudios.org/gamedownloads/Vivace.ver");
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            if (responseFromServer != version)
            {
                DialogResult res = MessageBox.Show($"Update available!\nCurrent: {version}\nNew: {responseFromServer}", "Update\nDo you want to update now?", MessageBoxButtons.YesNo);
                
                if (res == DialogResult.Yes)
                {
                    Process.Start("https://aquaticstudios.org/Vivace.exe");
                    update = true;
                }
            }

            //try
            //{
            //    spotify = new Spotify();
            //    spotify.Initialize(this);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            playlistImport = new PlaylistImport();
            playlistImport.form = this;

            LoadList(Properties.Settings.Default.lastlist);
        }
        public void LoadList(string _name)
        {
            paused = true;

            play.Text = "▶";

            wplayer.Stop();

            nowPlaying.Text = "";
            timestamp.Text = "";

            source = @"C:\Vivace\" + _name + @"\";

            Properties.Settings.Default.lastlist = _name;
            Properties.Settings.Default.Save();

            if (!Directory.Exists(source))
                Directory.CreateDirectory(source);

            albumlist.Items.Clear();
            songs.Clear();

            foreach (var f in Directory.GetFiles(source))
            {
                albumlist.Items.Add(Path.GetFileNameWithoutExtension(f));
                songs.Add(f);
            }

            if (songs.Count > 0)
            {
                next_Click(null, null);
            }
        }

        bool searching = false;

        private void search_Click(object sender, EventArgs e)
        {
            if (!searching)
                SearchAsync(searchText.Text);
        }
        private void searchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r" && !searching)
            {
                SearchAsync(searchText.Text);
            }
        }

        string current = "";

        public async Task SearchAsync(string search)
        {
            lastSearchIndex = -1;

            this.setName.Enabled = false;
            this.searchlist.Enabled = false;
            this.search.Enabled = false;
            this.searchText.Enabled = false;
            searching = true;

            try
            {
                var youtube = YouTube.Default;
                youtube.GetVideo(search);

                await Task.Run(() => ConvertVideo(search));
            }
            catch
            {
                searchlist.Items.Clear();
                searchList.Clear();
                searched.Clear();

                var url = "https://www.youtube.com/results?search_query=" + search;

                WebRequest request = WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                var youtube = YouTube.Default;

                var linkParser = new Regex(@"(?:\/watch\?v=)\S{11}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                int i = 0;

                foreach (Match m in linkParser.Matches(responseFromServer))
                {
                    if (i < 5)
                    {
                        var video = youtube.GetVideo(m.Value);

                        string min = Math.Floor((double)(video.Info.LengthSeconds / 60)).ToString("0");
                        string sec = ((double)video.Info.LengthSeconds - Math.Floor((double)(video.Info.LengthSeconds / 60)) * 60).ToString("00");
                        string len = min + ":" + sec;

                        searched.Add(video.Title);
                        searchlist.Items.Add(video.Title + " " + len);
                        searchList.Add(m.Value);
                    }
                    else
                    {
                        break;
                    }

                    i++;
                }

                this.setName.Text = "";
                this.setName.Enabled = true;
                this.searchlist.Enabled = true;
                this.search.Enabled = true;
                this.searchText.Enabled = true;
                searching = false;
            }
        }

        Task<Stream> input;
        public async void ConvertVideo(string url)
        {
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(url);
            var client = new HttpClient();
            long? totalByte = 0;

            if (!Directory.Exists(source))
            {
                Directory.CreateDirectory(source);
            }

            string title = setName.Text;

            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");

            string p = source + title + ".wav";

            using (Stream output = File.OpenWrite(p))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Head, video.Uri))
                {
                    await Task.Run(() => totalByte = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result.Content.Headers.ContentLength);
                }

                using (await Task.Run(() => input = client.GetStreamAsync(video.Uri)))
                {
                    byte[] buffer = new byte[16 * 1024];
                    int read = 0;
                    int totalRead = 0;

                    Console.WriteLine("Download Started");

                    while (await Task.Run(() => (read = input.Result.Read(buffer, 0, buffer.Length)) > 0))
                    {
                        await Task.Run(() => output.Write(buffer, 0, read));
                        totalRead += read;

                        progressBar1.Invoke(new Action(() => progressBar1.Maximum = 100));
                        progressBar1.Invoke(new Action(() => progressBar1.Value = (int)Math.Round((double)(totalRead / totalByte * 100.0))));
                    }

                    Console.WriteLine("Download Complete");
                }
            }

            albumlist.Invoke(new Action(() => albumlist.Items.Add(Path.GetFileNameWithoutExtension(p))));
            songs.Add(p);

            this.setName.Invoke(new Action(() => this.setName.Text = ""));
            this.setName.Invoke(new Action(() => this.setName.Enabled = true));
            this.searchlist.Invoke(new Action(() => this.searchlist.Enabled = true));
            this.search.Invoke(new Action(() => this.search.Enabled = true));
            this.searchText.Invoke(new Action(() => this.searchText.Enabled = true));
            searching = false;
        }
        public async void ConvertVideo(string url, string songFileName)
        {
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(url);
            var client = new HttpClient();
            long? totalByte = 0;

            if (!Directory.Exists(source))
            {
                Directory.CreateDirectory(source);
            }

            string title = songFileName;

            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");

            string p = source + title + ".wav";

            using (Stream output = File.OpenWrite(p))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Head, video.Uri))
                {
                    await Task.Run(() => totalByte = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result.Content.Headers.ContentLength);
                }

                using (await Task.Run(() => input = client.GetStreamAsync(video.Uri)))
                {
                    byte[] buffer = new byte[16 * 1024];
                    int read = 0;
                    int totalRead = 0;

                    Console.WriteLine("Download Started");

                    while (await Task.Run(() => (read = input.Result.Read(buffer, 0, buffer.Length)) > 0))
                    {
                        await Task.Run(() => output.Write(buffer, 0, read));
                        totalRead += read;

                        progressBar1.Invoke(new Action(() => progressBar1.Maximum = 100));
                        progressBar1.Invoke(new Action(() => progressBar1.Value = (int)Math.Round((double)(totalRead / totalByte * 100.0))));
                    }

                    Console.WriteLine("Download Complete");
                }
            }

            albumlist.Invoke(new Action(() => albumlist.Items.Add(Path.GetFileNameWithoutExtension(p))));
            songs.Add(p);

            this.setName.Invoke(new Action(() => this.setName.Text = ""));
            this.setName.Invoke(new Action(() => this.setName.Enabled = true));
            this.searchlist.Invoke(new Action(() => this.searchlist.Enabled = true));
            this.search.Invoke(new Action(() => this.search.Enabled = true));
            this.searchText.Invoke(new Action(() => this.searchText.Enabled = true));
            searching = false;
        }
        public void PlaySong(string path)
        {
            if (current != path)
            {
                paused = false;
                play.Text = "⏸";

                Console.WriteLine("Song changed");

                wplayer.Open(new Uri(path));
                wplayer.Play();

                nowPlaying.Text = Path.GetFileNameWithoutExtension(path);
                this.Text = Path.GetFileNameWithoutExtension(path);

                timeline.Maximum = 0;
                timeline.Value = 0;

                current = path;

                Console.WriteLine("Song playing");
            }
        }

        private void play_Click(object sender, EventArgs e)
        {
            if (songs.Count > 0)
            {
                if (wplayer.Source != null)
                {
                    if (!paused)
                    {
                        paused = true;
                        wplayer.Pause();
                        play.Text = "▶";
                    }
                    else
                    {
                        paused = false;
                        wplayer.Play();
                        play.Text = "⏸";
                    }
                }
                else
                {
                    paused = false;
                    albumlist.SelectedIndex = 0;
                    play.Text = "⏸";
                    next_Click(null, null);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (update)
            {
                Application.Exit();
                return;
            }

            if (wplayer.IsBuffering)
            {
                this.Text = "Vivace - Buffering...";
                timestamp.Text = "Buffering...";
            }
            else
            {
                if (wplayer.HasAudio)
                {
                    if (!paused)
                    {
                        this.Text = Path.GetFileNameWithoutExtension(wplayer.Source.LocalPath);
                    }
                    else
                        this.Text = "Vivace";

                    try
                    {
                        if (wplayer.Position.TotalSeconds >= wplayer.NaturalDuration.TimeSpan.TotalSeconds)
                        {
                            if (shuffle.Checked)
                            {
                                albumlist.SelectedIndex = new Random().Next(0, albumlist.Items.Count);
                            }
                            else
                            {
                                if (albumlist.SelectedIndex + 1 < albumlist.Items.Count)
                                    albumlist.SelectedIndex++;
                                else
                                    albumlist.SelectedIndex = 0;
                            }

                            PlaySong(songs[albumlist.SelectedIndex]);
                        }
                        else
                        {
                            int time = (int)Math.Round(wplayer.Position.TotalSeconds);

                            if (time <= timeline.Maximum && time >= timeline.Minimum)
                            {
                                double m = Math.Floor((double)(wplayer.Position.TotalSeconds / 60));
                                double s = ((double)wplayer.Position.TotalSeconds - Math.Floor((double)(wplayer.Position.TotalSeconds / 60)) * 60);

                                if (s.ToString("00") == "60")
                                {
                                    m = Math.Floor((double)(wplayer.Position.TotalSeconds / 60)) + 1;
                                    s = 0;
                                }

                                double ml = Math.Floor((double)(wplayer.NaturalDuration.TimeSpan.TotalSeconds / 60));
                                double sl = ((double)wplayer.NaturalDuration.TimeSpan.TotalSeconds - Math.Floor((double)(wplayer.NaturalDuration.TimeSpan.TotalSeconds / 60)) * 60);

                                if (sl.ToString("00") == "60")
                                {
                                    ml = Math.Floor((double)(wplayer.NaturalDuration.TimeSpan.TotalSeconds / 60)) + 1;
                                    sl = 0;
                                }

                                timestamp.Text = m.ToString("0") + ":" + s.ToString("00") + " / " + ml.ToString("0") + ":" + sl.ToString("00");

                                timeline.Maximum = (int)wplayer.NaturalDuration.TimeSpan.TotalSeconds;
                                timeline.Value = (int)wplayer.Position.TotalSeconds;
                            }
                            else
                            {
                                timeline.Value = 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    this.Text = "Vivace";
                    timeline.Value = 0;
                }
            }
        }

        public double Duration(string file)
        {
            WMPLib.WindowsMediaPlayer wmp = new WMPLib.WindowsMediaPlayer();
            WMPLib.IWMPMedia mediainfo = wmp.newMedia(file);
            return mediainfo.duration;
        }

        private void volume_Scroll(object sender, EventArgs e)
        {
            wplayer.Volume = volume.Value / 100.0;
            Properties.Settings.Default.volume = volume.Value;
            Properties.Settings.Default.Save();
        }

        private void timeline_Scroll(object sender, EventArgs e)
        {
            TimeSpan t = new TimeSpan(timeline.Value * 10000000);
            wplayer.Position = t;
        }

        int lastIndex = -1;
        int lastSearchIndex = -1;

        private void albumlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (albumlist.SelectedIndex == lastIndex && albumlist.SelectedIndex >= 0 && albumlist.SelectedIndex < albumlist.Items.Count)
            {
                PlaySong(songs[albumlist.SelectedIndex]);
            }

            lastIndex = albumlist.SelectedIndex;
        }

        private async void window_close_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.windowwidth = Width;
            Properties.Settings.Default.windowheight = Height;
            Properties.Settings.Default.windowmaximized = WindowState == FormWindowState.Maximized;
            await Task.Run(() => Properties.Settings.Default.Save());

            Application.Exit();
        }
        private void window_maximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                MaximizedBounds = new Rectangle(MaximizedBounds.X - 5, MaximizedBounds.Y - 5, MaximizedBounds.Width + 10, MaximizedBounds.Height + 10);
                WindowState = FormWindowState.Maximized;

                window_maximize.Text = "❐";
            }
            else
            {
                WindowState = FormWindowState.Normal;

                window_maximize.Text = "⬜";
            }
        }
        private void window_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void import_Click(object sender, EventArgs e)
        {
            playlistImport.ShowDialog();
        }

        private void searchlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchlist.SelectedIndex == lastSearchIndex && searchlist.SelectedIndex >= 0 && searchlist.SelectedIndex < searchlist.Items.Count)
            {
                SearchAsync(searchList[searchlist.SelectedIndex]);
            }
            else
            {
                if (searchlist.SelectedIndex >= 0 && searchlist.SelectedIndex < searchlist.Items.Count)
                    setName.Text = searched[searchlist.SelectedIndex];
            }

            lastSearchIndex = searchlist.SelectedIndex;
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (songs.Count > 0)
            {
                if (shuffle.Checked)
                {
                    albumlist.SelectedIndex = new Random().Next(0, albumlist.Items.Count);
                }
                else
                {
                    if (albumlist.SelectedIndex + 1 < albumlist.Items.Count)
                        albumlist.SelectedIndex++;
                    else
                        albumlist.SelectedIndex = 0;
                }

                PlaySong(songs[albumlist.SelectedIndex]);
            }
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (songs.Count > 0)
            {
                if (albumlist.SelectedIndex - 1 >= 0)
                    albumlist.SelectedIndex--;
                else
                    albumlist.SelectedIndex = albumlist.Items.Count - 1;

                PlaySong(songs[albumlist.SelectedIndex]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumList albumList = new AlbumList();
            albumList.form = this;
            albumList.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadList(Properties.Settings.Default.lastlist);
        }

        private void shuffle_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.shuffle = shuffle.Checked;
            Properties.Settings.Default.Save();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.MediaPlayPause)
            {
                if (!paused)
                {
                    wplayer.Play();
                }
                else
                {
                    wplayer.Pause();
                }
            }
            if (e.KeyCode == Keys.Play)
            {
                paused = false;
                wplayer.Play();
            }
            if (e.KeyCode == Keys.Pause)
            {
                paused = true;
                wplayer.Pause();
            }
            if (e.KeyCode == Keys.MediaNextTrack)
            {
                next_Click(null, null);
            }
            if (e.KeyCode == Keys.MediaPreviousTrack)
            {
                prev_Click(null, null);
            }
        }
    }
}
