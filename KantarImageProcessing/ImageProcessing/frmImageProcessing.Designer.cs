﻿namespace ImageProcessing
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
            this.lblContent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtColors = new System.Windows.Forms.TextBox();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.btnProcessVideo = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.btnFaceDetect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnSaveMetadata = new System.Windows.Forms.Button();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.btnDownloadImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(409, 26);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(87, 23);
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
            this.btnUpload.Location = new System.Drawing.Point(339, 26);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(63, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Browse";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(117, 26);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(212, 20);
            this.txtFilePath.TabIndex = 2;
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(383, 109);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(495, 269);
            this.txtResult.TabIndex = 3;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(723, 85);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(137, 13);
            this.lblContent.TabIndex = 7;
            this.lblContent.Text = "Content from Image/Video :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Colors from Image/Video :";
            // 
            // txtColors
            // 
            this.txtColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColors.Location = new System.Drawing.Point(380, 428);
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
            this.lblImageSize.Location = new System.Drawing.Point(93, 83);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(0, 17);
            this.lblImageSize.TabIndex = 10;
            // 
            // btnProcessVideo
            // 
            this.btnProcessVideo.Location = new System.Drawing.Point(504, 26);
            this.btnProcessVideo.Name = "btnProcessVideo";
            this.btnProcessVideo.Size = new System.Drawing.Size(88, 23);
            this.btnProcessVideo.TabIndex = 11;
            this.btnProcessVideo.Text = "Process Video";
            this.btnProcessVideo.UseVisualStyleBackColor = true;
            this.btnProcessVideo.Click += new System.EventHandler(this.btnProcessVideo_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 109);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(360, 394);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(6, 29);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(107, 13);
            this.lblSelectFile.TabIndex = 12;
            this.lblSelectFile.Text = "Select Image/Video :";
            // 
            // btnFaceDetect
            // 
            this.btnFaceDetect.Location = new System.Drawing.Point(601, 26);
            this.btnFaceDetect.Name = "btnFaceDetect";
            this.btnFaceDetect.Size = new System.Drawing.Size(79, 23);
            this.btnFaceDetect.TabIndex = 13;
            this.btnFaceDetect.Text = "Detect Face";
            this.btnFaceDetect.UseVisualStyleBackColor = true;
            this.btnFaceDetect.Click += new System.EventHandler(this.btnFaceDetect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(689, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Duplicate Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DuplicateSearch_Click);
            // 
            // pbLoading
            // 
            this.pbLoading.Image = global::ImageProcessing.Properties.Resources.loading_spinner;
            this.pbLoading.Location = new System.Drawing.Point(387, 257);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(125, 116);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoading.TabIndex = 0;
            this.pbLoading.TabStop = false;
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(12, 108);
            this.pbImage.MinimumSize = new System.Drawing.Size(350, 350);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(360, 395);
            this.pbImage.TabIndex = 6;
            this.pbImage.TabStop = false;
            // 
            // btnSaveMetadata
            // 
            this.btnSaveMetadata.Location = new System.Drawing.Point(339, 66);
            this.btnSaveMetadata.Name = "btnSaveMetadata";
            this.btnSaveMetadata.Size = new System.Drawing.Size(147, 23);
            this.btnSaveMetadata.TabIndex = 15;
            this.btnSaveMetadata.Text = "Upload and Save Metadata";
            this.btnSaveMetadata.UseVisualStyleBackColor = true;
            this.btnSaveMetadata.Click += new System.EventHandler(this.btnSaveMetadata_Click);
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(492, 67);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(91, 23);
            this.btnUploadImage.TabIndex = 16;
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // btnDownloadImage
            // 
            this.btnDownloadImage.Location = new System.Drawing.Point(590, 67);
            this.btnDownloadImage.Name = "btnDownloadImage";
            this.btnDownloadImage.Size = new System.Drawing.Size(91, 23);
            this.btnDownloadImage.TabIndex = 17;
            this.btnDownloadImage.Text = "Download Image";
            this.btnDownloadImage.UseVisualStyleBackColor = true;
            this.btnDownloadImage.Click += new System.EventHandler(this.btnDownloadImage_Click);
            // 
            // frmImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.btnDownloadImage);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.btnSaveMetadata);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.button1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Button btnSaveMetadata;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Button btnDownloadImage;
    }
}

