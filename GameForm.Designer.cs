namespace Catch_The_Bee
{
    partial class GameForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelBestScore = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Catch_The_Bee.Properties.Resources.header;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1233, 123);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.BackColor = System.Drawing.Color.Transparent;
            this.labelScore.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Location = new System.Drawing.Point(77, 44);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(141, 48);
            this.labelScore.TabIndex = 1;
            this.labelScore.Text = "Score:";
            this.labelScore.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelBestScore
            // 
            this.labelBestScore.AutoSize = true;
            this.labelBestScore.BackColor = System.Drawing.Color.Transparent;
            this.labelBestScore.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBestScore.Location = new System.Drawing.Point(428, 44);
            this.labelBestScore.Name = "labelBestScore";
            this.labelBestScore.Size = new System.Drawing.Size(237, 48);
            this.labelBestScore.TabIndex = 2;
            this.labelBestScore.Text = "Best Score:";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(864, 44);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(140, 48);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "Timer:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Catch_The_Bee.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1232, 656);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelBestScore);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GameForm";
            this.Text = "GameForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelBestScore;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timer1;
    }
}