namespace ImageProcessing
{
    partial class frmImageProcessing
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtColors = new System.Windows.Forms.TextBox();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.btnProcessVideo = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.btnFaceDetect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(483, 27);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(115, 23);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process Image";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(388, 26);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Browse";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(145, 26);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(212, 20);
            this.txtFilePath.TabIndex = 2;
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(383, 95);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(495, 269);
            this.txtResult.TabIndex = 3;
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(12, 94);
            this.pbImage.MinimumSize = new System.Drawing.Size(350, 350);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(360, 395);
            this.pbImage.TabIndex = 6;
            this.pbImage.TabStop = false;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(459, 68);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(137, 13);
            this.lblContent.TabIndex = 7;
            this.lblContent.Text = "Content from Image/Video :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 383);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Colors from Image/Video :";
            // 
            // txtColors
            // 
            this.txtColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColors.Location = new System.Drawing.Point(380, 414);
            this.txtColors.Multiline = true;
            this.txtColors.Name = "txtColors";
            this.txtColors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtColors.Size = new System.Drawing.Size(500, 78);
            this.txtColors.TabIndex = 9;
            // 
            // lblImageSize
            // 
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImageSize.Location = new System.Drawing.Point(93, 69);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(0, 17);
            this.lblImageSize.TabIndex = 10;
            // 
            // btnProcessVideo
            // 
            this.btnProcessVideo.Location = new System.Drawing.Point(616, 27);
            this.btnProcessVideo.Name = "btnProcessVideo";
            this.btnProcessVideo.Size = new System.Drawing.Size(115, 23);
            this.btnProcessVideo.TabIndex = 11;
            this.btnProcessVideo.Text = "Process Video";
            this.btnProcessVideo.UseVisualStyleBackColor = true;
            this.btnProcessVideo.Click += new System.EventHandler(this.btnProcessVideo_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 95);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(360, 394);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(25, 29);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(107, 13);
            this.lblSelectFile.TabIndex = 12;
            this.lblSelectFile.Text = "Select Image/Video :";
            // 
            // btnFaceDetect
            // 
            this.btnFaceDetect.Location = new System.Drawing.Point(753, 29);
            this.btnFaceDetect.Name = "btnFaceDetect";
            this.btnFaceDetect.Size = new System.Drawing.Size(75, 23);
            this.btnFaceDetect.TabIndex = 13;
            this.btnFaceDetect.Text = "Detect Face";
            this.btnFaceDetect.UseVisualStyleBackColor = true;
            this.btnFaceDetect.Click += new System.EventHandler(this.btnFaceDetect_Click);
            // 
            // frmImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.btnFaceDetect);
            this.Controls.Add(this.lblSelectFile);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnProcessVideo);
            this.Controls.Add(this.lblImageSize);
            this.Controls.Add(this.txtColors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnProcess);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImageProcessing";
            this.Text = "Image Processing";
            this.Load += new System.EventHandler(this.frmImageProcessing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtColors;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.Button btnProcessVideo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblSelectFile;
        private System.Windows.Forms.Button btnFaceDetect;
    }
}

