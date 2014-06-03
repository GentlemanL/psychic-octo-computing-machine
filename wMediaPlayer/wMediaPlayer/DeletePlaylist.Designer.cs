namespace wMediaPlayer
{
    partial class DeletePlaylist
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
            this.btn_np_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_np_delete = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_np_cancel
            // 
            this.btn_np_cancel.Location = new System.Drawing.Point(145, 58);
            this.btn_np_cancel.Name = "btn_np_cancel";
            this.btn_np_cancel.Size = new System.Drawing.Size(127, 23);
            this.btn_np_cancel.TabIndex = 7;
            this.btn_np_cancel.Text = "Cancel";
            this.btn_np_cancel.UseVisualStyleBackColor = true;
            this.btn_np_cancel.Click += new System.EventHandler(this.btn_np_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Playlist name:";
            // 
            // btn_np_delete
            // 
            this.btn_np_delete.Location = new System.Drawing.Point(12, 58);
            this.btn_np_delete.Name = "btn_np_delete";
            this.btn_np_delete.Size = new System.Drawing.Size(127, 23);
            this.btn_np_delete.TabIndex = 5;
            this.btn_np_delete.Text = "Delete";
            this.btn_np_delete.UseVisualStyleBackColor = true;
            this.btn_np_delete.Click += new System.EventHandler(this.btn_np_delete_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(256, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // DeletePlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 89);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_np_cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_np_delete);
            this.Name = "DeletePlaylist";
            this.Text = "DeletePlaylist";
            this.Load += new System.EventHandler(this.DeletePlaylist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_np_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_np_delete;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}