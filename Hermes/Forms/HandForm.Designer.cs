namespace Hermes
{
    partial class HandForm
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
            this.leftHand = new System.Windows.Forms.PictureBox();
            this.handCountLabel = new System.Windows.Forms.Label();
            this.rightHand = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.leftOrien = new System.Windows.Forms.Label();
            this.rightOrien = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leftHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightHand)).BeginInit();
            this.SuspendLayout();
            // 
            // leftHand
            // 
            this.leftHand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.leftHand.Location = new System.Drawing.Point(12, 27);
            this.leftHand.Name = "leftHand";
            this.leftHand.Size = new System.Drawing.Size(230, 230);
            this.leftHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftHand.TabIndex = 0;
            this.leftHand.TabStop = false;
            // 
            // handCountLabel
            // 
            this.handCountLabel.AutoSize = true;
            this.handCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.handCountLabel.Location = new System.Drawing.Point(12, 406);
            this.handCountLabel.Name = "handCountLabel";
            this.handCountLabel.Size = new System.Drawing.Size(203, 26);
            this.handCountLabel.TabIndex = 2;
            this.handCountLabel.Text = "Number of hands: 0";
            // 
            // rightHand
            // 
            this.rightHand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightHand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rightHand.Location = new System.Drawing.Point(382, 27);
            this.rightHand.Name = "rightHand";
            this.rightHand.Size = new System.Drawing.Size(230, 230);
            this.rightHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightHand.TabIndex = 3;
            this.rightHand.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Left Hand";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(427, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Right Hand";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(282, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(248, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Palm Orientation";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // leftOrien
            // 
            this.leftOrien.AutoSize = true;
            this.leftOrien.Location = new System.Drawing.Point(248, 54);
            this.leftOrien.Name = "leftOrien";
            this.leftOrien.Size = new System.Drawing.Size(33, 13);
            this.leftOrien.TabIndex = 8;
            this.leftOrien.Text = "None";
            // 
            // rightOrien
            // 
            this.rightOrien.AutoSize = true;
            this.rightOrien.Location = new System.Drawing.Point(342, 54);
            this.rightOrien.Name = "rightOrien";
            this.rightOrien.Size = new System.Drawing.Size(33, 13);
            this.rightOrien.TabIndex = 9;
            this.rightOrien.Text = "None";
            this.rightOrien.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.rightOrien);
            this.Controls.Add(this.leftOrien);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rightHand);
            this.Controls.Add(this.handCountLabel);
            this.Controls.Add(this.leftHand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Pinch Me";
            ((System.ComponentModel.ISupportInitialize)(this.leftHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightHand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox leftHand;
        private System.Windows.Forms.Label handCountLabel;
        private System.Windows.Forms.PictureBox rightHand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label leftOrien;
        private System.Windows.Forms.Label rightOrien;
    }
}

