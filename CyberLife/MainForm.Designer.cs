// Hello, world!
namespace CyberLife
{
    partial class MainForm
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
            this.ColorTypeButton = new System.Windows.Forms.Button();
            this.statsLabel = new System.Windows.Forms.Label();
            this.sunEnergy = new System.Windows.Forms.TrackBar();
            this.mineralsEnergy = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sunLabel = new System.Windows.Forms.Label();
            this.mineralsLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.mutationPercent = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.mutationLabel = new System.Windows.Forms.Label();
            this.mapPicture2 = new CustomPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.sunEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineralsEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutationPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPicture2)).BeginInit();
            this.SuspendLayout();
            // 
            // ColorTypeButton
            // 
            this.ColorTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorTypeButton.Location = new System.Drawing.Point(342, 77);
            this.ColorTypeButton.Name = "ColorTypeButton";
            this.ColorTypeButton.Size = new System.Drawing.Size(137, 42);
            this.ColorTypeButton.TabIndex = 0;
            this.ColorTypeButton.Text = "Режим отображеия";
            this.ColorTypeButton.UseVisualStyleBackColor = true;
            this.ColorTypeButton.Click += new System.EventHandler(this.ColorTypeButton_Click);
            // 
            // statsLabel
            // 
            this.statsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statsLabel.AutoSize = true;
            this.statsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statsLabel.Location = new System.Drawing.Point(339, 12);
            this.statsLabel.Name = "statsLabel";
            this.statsLabel.Size = new System.Drawing.Size(41, 15);
            this.statsLabel.TabIndex = 2;
            this.statsLabel.Text = "label1";
            // 
            // sunEnergy
            // 
            this.sunEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sunEnergy.Location = new System.Drawing.Point(434, 125);
            this.sunEnergy.Maximum = 100;
            this.sunEnergy.Name = "sunEnergy";
            this.sunEnergy.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sunEnergy.Size = new System.Drawing.Size(45, 267);
            this.sunEnergy.TabIndex = 4;
            this.sunEnergy.Scroll += new System.EventHandler(this.sunEnergy_Scroll);
            // 
            // mineralsEnergy
            // 
            this.mineralsEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mineralsEnergy.LargeChange = 1;
            this.mineralsEnergy.Location = new System.Drawing.Point(343, 125);
            this.mineralsEnergy.Maximum = 100;
            this.mineralsEnergy.Name = "mineralsEnergy";
            this.mineralsEnergy.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mineralsEnergy.Size = new System.Drawing.Size(45, 267);
            this.mineralsEnergy.TabIndex = 5;
            this.mineralsEnergy.Scroll += new System.EventHandler(this.mineralsEnergy_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "1000";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "1000";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(466, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(328, 395);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Минералы";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(431, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Солнце";
            // 
            // sunLabel
            // 
            this.sunLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sunLabel.AutoSize = true;
            this.sunLabel.Location = new System.Drawing.Point(403, 135);
            this.sunLabel.Name = "sunLabel";
            this.sunLabel.Size = new System.Drawing.Size(35, 13);
            this.sunLabel.TabIndex = 12;
            this.sunLabel.Text = "label7";
            // 
            // mineralsLabel
            // 
            this.mineralsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mineralsLabel.AutoSize = true;
            this.mineralsLabel.Location = new System.Drawing.Point(317, 135);
            this.mineralsLabel.Name = "mineralsLabel";
            this.mineralsLabel.Size = new System.Drawing.Size(35, 13);
            this.mineralsLabel.TabIndex = 13;
            this.mineralsLabel.Text = "label8";
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLabel.Location = new System.Drawing.Point(23, 14);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(0, 15);
            this.infoLabel.TabIndex = 14;
            // 
            // mutationPercent
            // 
            this.mutationPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mutationPercent.LargeChange = 1;
            this.mutationPercent.Location = new System.Drawing.Point(266, 125);
            this.mutationPercent.Maximum = 100;
            this.mutationPercent.Name = "mutationPercent";
            this.mutationPercent.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mutationPercent.Size = new System.Drawing.Size(45, 267);
            this.mutationPercent.TabIndex = 15;
            this.mutationPercent.Scroll += new System.EventHandler(this.mutationPercent_Scroll);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "% мутаций";
            // 
            // mutationLabel
            // 
            this.mutationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mutationLabel.AutoSize = true;
            this.mutationLabel.Location = new System.Drawing.Point(235, 135);
            this.mutationLabel.Name = "mutationLabel";
            this.mutationLabel.Size = new System.Drawing.Size(35, 13);
            this.mutationLabel.TabIndex = 17;
            this.mutationLabel.Text = "label8";
            // 
            // mapPicture2
            // 
            this.mapPicture2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPicture2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.mapPicture2.Location = new System.Drawing.Point(3, 3);
            this.mapPicture2.Name = "mapPicture2";
            this.mapPicture2.Size = new System.Drawing.Size(100, 100);
            this.mapPicture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mapPicture2.TabIndex = 1;
            this.mapPicture2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 450);
            this.Controls.Add(this.mutationLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mutationPercent);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.mineralsLabel);
            this.Controls.Add(this.sunLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mineralsEnergy);
            this.Controls.Add(this.sunEnergy);
            this.Controls.Add(this.statsLabel);
            this.Controls.Add(this.mapPicture2);
            this.Controls.Add(this.ColorTypeButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sunEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineralsEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutationPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPicture2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ColorTypeButton;
        private CustomPictureBox mapPicture2;
        private System.Windows.Forms.Label statsLabel;
        private System.Windows.Forms.TrackBar sunEnergy;
        private System.Windows.Forms.TrackBar mineralsEnergy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label sunLabel;
        private System.Windows.Forms.Label mineralsLabel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.TrackBar mutationPercent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label mutationLabel;
    }
}