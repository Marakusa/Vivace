
namespace Vivace
{
    partial class LyricsReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LyricsReader));
            this.lyrics = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lyrics
            // 
            this.lyrics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lyrics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lyrics.DetectUrls = false;
            this.lyrics.Location = new System.Drawing.Point(12, 12);
            this.lyrics.Name = "lyrics";
            this.lyrics.ReadOnly = true;
            this.lyrics.Size = new System.Drawing.Size(360, 429);
            this.lyrics.TabIndex = 0;
            this.lyrics.Text = "";
            // 
            // LyricsReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.lyrics);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LyricsReader";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lyrics Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LyricsReader_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox lyrics;
    }
}