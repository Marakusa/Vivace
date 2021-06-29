using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace Vivace
{
    public partial class PlaylistImport : Form
    {
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


        public Form1 form;
        public List<string> lists = new List<string>();

        public PlaylistImport()
        {
            InitializeComponent();

            import.Enabled = false;
        }

        private void window_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlaylistImport_Shown(object sender, EventArgs e)
        {
            import.Enabled = false;
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            listBox1.Items.Clear();
            lists.Clear();

            foreach (var f in Directory.GetDirectories(@"C:\Vivace\"))
            {
                lists.Add(Path.GetFileName(f));
                listBox1.Items.Add(Path.GetFileName(f));
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = textBox2.Text;

                listBox1.Items.Add(newName);
                lists.Add(newName);

                textBox2.Text = "";
            }
        }

        int importingIndex = 0;
        string playlist = "";

        public void ImportList(List<string> list, List<string> trackNames)
        {
            importingIndex = 0;
            playlist = listBox1.Items[listBox1.SelectedIndex].ToString();
            ImportNext(list, trackNames);
        }
        private async void ImportNext(List<string> list, List<string> trackNames)
        {
            if (importingIndex < list.Count)
            {
                await Task.Run(() => ConvertVideo(list[importingIndex], trackNames[importingIndex], playlist, list, trackNames));
                importingIndex++;
            }
            else
            {
                window_close.Enabled = true;
                import.Enabled = true;
                Console.WriteLine("Done!");
            }
        }

        Task<Stream> input;
        public async void ConvertVideo(string url, string songFileName, string playlist, List<string> list, List<string> trackNames)
        {
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(url);
            var client = new HttpClient();
            long? totalByte = 0;

            if (!Directory.Exists(@"C:\Vivace\" + playlist + @"\"))
            {
                Directory.CreateDirectory(@"C:\Vivace\" + playlist + @"\");
            }

            string title = songFileName;

            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");

            string p = @"C:\Vivace\" + playlist + @"\" + title + ".wav";

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
                    }

                    Console.WriteLine("Download Complete");
                }
            }

            ImportNext(list, trackNames);
        }

        private void import_Click(object sender, EventArgs e)
        {
            window_close.Enabled = false;
            import.Enabled = false;

            //form.spotify.GetPlaylist(textBox1.Text, this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && listBox1.SelectedIndex < listBox1.Items.Count && listBox1.SelectedIndex >= 0)
            {
                import.Enabled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && listBox1.SelectedIndex < listBox1.Items.Count && listBox1.SelectedIndex >= 0)
            {
                import.Enabled = true;
            }
        }
    }
}
