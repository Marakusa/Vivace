using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vivace
{
    public partial class LyricsReader : Form
    {
        public LyricsReader()
        {
            InitializeComponent();
        }

        public void SetLyrics(string text, string title)
        {
            lyrics.Text = text;
            Text = $"Lyrics | {title}";
        }

        private void LyricsReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
