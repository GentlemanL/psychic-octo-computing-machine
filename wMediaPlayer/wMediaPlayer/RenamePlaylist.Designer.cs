namespace wMediaPlayer
{
    partial class RenamePlaylist
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_playlists = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox_newPlaylistName = new System.Windows.Forms.TextBox();
            this.btn_rename = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selected playlist";
            // 
            // comboBox_playlists
            // 
            this.comboBox_playlists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_playlists.FormattingEnabled = true;
            this.comboBox_playlists.Location = new System.Drawing.Point(13, 26);
            this.comboBox_playlists.Name = "comboBox_playlists";
            this.comboBox_playlists.Size = new System.Drawing.Size(259, 21);
            this.comboBox_playlists.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New name";
            // 
            // txtBox_newPlaylistName
            // 
            this.txtBox_newPlaylistName.Location = new System.Drawing.Point(13, 84);
            this.txtBox_newPlaylistName.Name = "txtBox_newPlaylistName";
            this.txtBox_newPlaylistName.Size = new System.Drawing.Size(259, 20);
            this.txtBox_newPlaylistName.TabIndex = 3;
            // 
            // btn_rename
            // 
            this.btn_rename.Location = new System.Drawing.Point(15, 110);
            this.btn_rename.Name = "btn_rename";
            this.btn_rename.Size = new System.Drawing.Size(127, 23);
            this.btn_rename.TabIndex = 4;
            this.btn_rename.Text = "Rename";
            this.btn_rename.UseVisualStyleBackColor = true;
            this.btn_rename.Click += new System.EventHandler(this.btn_rename_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(148, 110);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(127, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // RenamePlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 142);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_rename);
            this.Controls.Add(this.txtBox_newPlaylistName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_playlists);
            this.Controls.Add(this.label1);
            this.Name = "RenamePlaylist";
            this.Text = "RenamePlaylist";
            this.Load += new System.EventHandler(this.RenamePlaylist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_playlists;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBox_newPlaylistName;
        private System.Windows.Forms.Button btn_rename;
        private System.Windows.Forms.Button btn_cancel;
    }
}