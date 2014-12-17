namespace Pinch_Me
{
    partial class ImageForm
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
            this.debugImage = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.debugImage)).BeginInit();
            this.SuspendLayout();
            // 
            // debugImage
            // 
            this.debugImage.Location = new System.Drawing.Point(12, 12);
            this.debugImage.Name = "debugImage";
            this.debugImage.Size = new System.Drawing.Size(600, 417);
            this.debugImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.debugImage.TabIndex = 0;
            this.debugImage.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 435);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Take Picture";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 468);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.debugImage);
            this.Name = "ImageForm";
            this.Text = "Image Form";
            ((System.ComponentModel.ISupportInitialize)(this.debugImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox debugImage;
        private System.Windows.Forms.Button button1;
    }
}