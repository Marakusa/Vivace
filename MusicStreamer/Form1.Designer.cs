
namespace Vivace
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.search = new System.Windows.Forms.Button();
            this.searchText = new System.Windows.Forms.TextBox();
            this.albumlist = new System.Windows.Forms.ListBox();
            this.nowPlaying = new System.Windows.Forms.Label();
            this.play = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.prev = new System.Windows.Forms.Button();
            this.volume = new System.Windows.Forms.TrackBar();
            this.timeline = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.searchlist = new System.Windows.Forms.ListBox();
            this.setName = new System.Windows.Forms.TextBox();
            this.timestamp = new System.Windows.Forms.Label();
            this.shuffle = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.search.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search.Location = new System.Drawing.Point(678, 64);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(94, 22);
            this.search.TabIndex = 0;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // searchText
            // 
            this.searchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchText.Location = new System.Drawing.Point(12, 64);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(660, 22);
            this.searchText.TabIndex = 1;
            this.searchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchText_KeyPress);
            // 
            // albumlist
            // 
            this.albumlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.albumlist.FormattingEnabled = true;
            this.albumlist.IntegralHeight = false;
            this.albumlist.Location = new System.Drawing.Point(12, 92);
            this.albumlist.Name = "albumlist";
            this.albumlist.Size = new System.Drawing.Size(414, 283);
            this.albumlist.TabIndex = 4;
            this.albumlist.SelectedIndexChanged += new System.EventHandler(this.albumlist_SelectedIndexChanged);
            // 
            // nowPlaying
            // 
            this.nowPlaying.AutoSize = true;
            this.nowPlaying.Location = new System.Drawing.Point(150, 12);
            this.nowPlaying.Name = "nowPlaying";
            this.nowPlaying.Size = new System.Drawing.Size(35, 13);
            this.nowPlaying.TabIndex = 7;
            this.nowPlaying.Text = "label1";
            // 
            // play
            // 
            this.play.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.play.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.play.Location = new System.Drawing.Point(377, 419);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(30, 30);
            this.play.TabIndex = 8;
            this.play.Text = "▶";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // next
            // 
            this.next.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.next.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.next.Location = new System.Drawing.Point(407, 419);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(30, 30);
            this.next.TabIndex = 9;
            this.next.Text = "⏭";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // prev
            // 
            this.prev.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.prev.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prev.Location = new System.Drawing.Point(347, 419);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(30, 30);
            this.prev.TabIndex = 10;
            this.prev.Text = "⏮";
            this.prev.UseVisualStyleBackColor = true;
            this.prev.Click += new System.EventHandler(this.prev_Click);
            // 
            // volume
            // 
            this.volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.volume.AutoSize = false;
            this.volume.Location = new System.Drawing.Point(653, 426);
            this.volume.Maximum = 100;
            this.volume.Name = "volume";
            this.volume.Size = new System.Drawing.Size(125, 23);
            this.volume.TabIndex = 11;
            this.volume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volume.Scroll += new System.EventHandler(this.volume_Scroll);
            // 
            // timeline
            // 
            this.timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeline.AutoSize = false;
            this.timeline.LargeChange = 10;
            this.timeline.Location = new System.Drawing.Point(3, 397);
            this.timeline.Maximum = 100;
            this.timeline.Name = "timeline";
            this.timeline.Size = new System.Drawing.Size(775, 23);
            this.timeline.SmallChange = 5;
            this.timeline.TabIndex = 12;
            this.timeline.TickStyle = System.Windows.Forms.TickStyle.None;
            this.timeline.Scroll += new System.EventHandler(this.timeline_Scroll);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // searchlist
            // 
            this.searchlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchlist.FormattingEnabled = true;
            this.searchlist.IntegralHeight = false;
            this.searchlist.Location = new System.Drawing.Point(432, 92);
            this.searchlist.Name = "searchlist";
            this.searchlist.Size = new System.Drawing.Size(340, 255);
            this.searchlist.TabIndex = 13;
            this.searchlist.SelectedIndexChanged += new System.EventHandler(this.searchlist_SelectedIndexChanged);
            // 
            // setName
            // 
            this.setName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.setName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.setName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setName.Location = new System.Drawing.Point(432, 353);
            this.setName.Name = "setName";
            this.setName.Size = new System.Drawing.Size(340, 22);
            this.setName.TabIndex = 14;
            // 
            // timestamp
            // 
            this.timestamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timestamp.AutoSize = true;
            this.timestamp.Location = new System.Drawing.Point(9, 381);
            this.timestamp.Name = "timestamp";
            this.timestamp.Size = new System.Drawing.Size(35, 13);
            this.timestamp.TabIndex = 15;
            this.timestamp.Text = "label1";
            // 
            // shuffle
            // 
            this.shuffle.AutoSize = true;
            this.shuffle.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shuffle.Location = new System.Drawing.Point(441, 423);
            this.shuffle.Name = "shuffle";
            this.shuffle.Size = new System.Drawing.Size(45, 25);
            this.shuffle.TabIndex = 16;
            this.shuffle.Text = "🔀";
            this.shuffle.UseVisualStyleBackColor = true;
            this.shuffle.CheckedChanged += new System.EventHandler(this.shuffle_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vivace.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(678, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 22);
            this.button1.TabIndex = 18;
            this.button1.Text = "Playlists";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(678, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 22);
            this.button2.TabIndex = 19;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.shuffle);
            this.Controls.Add(this.timestamp);
            this.Controls.Add(this.setName);
            this.Controls.Add(this.searchlist);
            this.Controls.Add(this.timeline);
            this.Controls.Add(this.volume);
            this.Controls.Add(this.prev);
            this.Controls.Add(this.next);
            this.Controls.Add(this.play);
            this.Controls.Add(this.nowPlaying);
            this.Controls.Add(this.albumlist);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.search);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vivace";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button search;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.ListBox albumlist;
        private System.Windows.Forms.Label nowPlaying;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.TrackBar volume;
        private System.Windows.Forms.TrackBar timeline;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox searchlist;
        private System.Windows.Forms.TextBox setName;
        private System.Windows.Forms.Label timestamp;
        private System.Windows.Forms.CheckBox shuffle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

