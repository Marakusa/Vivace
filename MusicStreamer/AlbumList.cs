using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Vivace
{
    public partial class AlbumList : Form
    {
        public List<string> lists = new List<string>();

        public AlbumList()
        {
            InitializeComponent();

            foreach (var f in Directory.GetDirectories(@"C:\Vivace\"))
            {
                lists.Add(Path.GetFileName(f));
                listBox1.Items.Add(Path.GetFileName(f));
            }
        }

        public Form1 form;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = textBox1.Text;

                listBox1.Items.Add(newName);
                lists.Add(newName);

                textBox1.Text = "";
            }
        }

        int lastIndex = -1;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == lastIndex && listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < listBox1.Items.Count)
            {
                form.LoadList(lists[listBox1.SelectedIndex]);
                this.Close();
            }

            lastIndex = listBox1.SelectedIndex;
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lists.Count > 1)
            {
                foreach (var f in Directory.GetFiles(@"C:\Vivace\" + lists[listBox1.SelectedIndex]))
                    File.Delete(f);

                if (Directory.Exists(@"C:\Vivace\" + lists[listBox1.SelectedIndex]))
                    Directory.Delete(@"C:\Vivace\" + lists[listBox1.SelectedIndex]);

                lists.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                form.LoadList(lists[0]);
            }
        }
    }
}
