namespace wMediaPlayer
{
    partial class NewPlaylist
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
            this.txtBox_np_name = new System.Windows.Forms.TextBox();
            this.btn_np_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_np_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBox_np_name
            // 
            this.txtBox_np_name.Location = new System.Drawing.Point(12, 29);
            this.txtBox_np_name.Name = "txtBox_np_name";
            this.txtBox_np_name.Size = new System.Drawing.Size(260, 20);
            this.txtBox_np_name.TabIndex = 0;
            // 
            // btn_np_ok
            // 
            this.btn_np_ok.Location = new System.Drawing.Point(12, 55);
            this.btn_np_ok.Name = "btn_np_ok";
            this.btn_np_ok.Size = new System.Drawing.Size(127, 23);
            this.btn_np_ok.TabIndex = 1;
            this.btn_np_ok.Text = "OK";
            this.btn_np_ok.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Playlist name:";
            // 
            // btn_np_cancel
            // 
            this.btn_np_cancel.Location = new System.Drawing.Point(145, 55);
            this.btn_np_cancel.Name = "btn_np_cancel";
            this.btn_np_cancel.Size = new System.Drawing.Size(127, 23);
            this.btn_np_cancel.TabIndex = 3;
            this.btn_np_cancel.Text = "Cancel";
            this.btn_np_cancel.UseVisualStyleBackColor = true;
            // 
            // NewPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 89);
            this.Controls.Add(this.btn_np_cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_np_ok);
            this.Controls.Add(this.txtBox_np_name);
            this.Name = "NewPlaylist";
            this.Text = "NewPlaylist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox_np_name;
        private System.Windows.Forms.Button btn_np_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_np_cancel;
    }
}