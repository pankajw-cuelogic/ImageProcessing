﻿using CommonImplementation;
using ImageVideoGrabber;
using ImageVideoProcessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class frmImageProcessing : Form
    {
        #region Global Declarations

        string[] videoExtensions = { ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV",".3GP", ".FLV" };
        string[] imageExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".TIF" };
        int noOfFaces = 0;
        Bitmap newFrame = null;
        string appStartPath = Application.StartupPath;

        FaceDetection _faceDetection = null;
        Boolean isRemoteImage = false;// value will be true when images stored on remote server
        #endregion
        public frmImageProcessing()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Reason : To get Image details from uploaded image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), imageExtensions))
                {
                    MessageBox.Show("Please select valid Image file!!");
                    return;
                }

                DisposeControls();
                ShowLoader();
                pbImage.BringToFront();
                string imagePath = txtFilePath.Text.ToString();
                string imageContent = new ImageVideoProcessing.ImageGrabber().ExtractTextFromImage(imagePath);
                txtResult.Text = string.IsNullOrEmpty(imageContent) ? "There is no text found in Image" : imageContent;
                txtColors.Text = "\tImage contains following major colors: " + ParseColorList(new ImageVideoProcessing.ImageGrabber().GetImageColors(imagePath));

                Cursor.Current = Cursors.AppStarting;
                HideLoader();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Reason : To select file(Image/Video) from directory to process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog1.FileName;
                    if (!(openFileDialog1.FileName.Trim() == ""))
                    {
                        if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), imageExtensions))
                            return;

                        pbImage.BringToFront();
                        pbImage.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                        pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                        Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                        lblImageSize.Text = "Image size : " + bmp.Size.Width + " x " + bmp.Size.Height;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///Reason: Process video, get all its details and show it to end user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessVideo_Click(object sender, EventArgs e)
        {
            if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), videoExtensions))
            {
                MessageBox.Show("Please select valid Video file!!"); return;
            }

            //Cursor.Current = Cursors.WaitCursor;
            ShowLoader();
            DisposeControls();
            flowLayoutPanel1.BringToFront();
            List<ColorModel> colorList = new List<ColorModel>();
            string contentMessage = "";
            string videoInfo = "";
            string audiMessage = "";
            string frameName = Guid.NewGuid().ToString();
            string imageOutputFolderPath = appStartPath + @"\bin\img\";
            string filePath = txtFilePath.Text.ToString().Trim();
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            try
            {
                new ImageVideoProcessing.VideoGrabber().GetVideoDetails(appStartPath, imageOutputFolderPath, filePath, batchFilePath, frameName, ref contentMessage, ref colorList, ref videoInfo, ref audiMessage);
                txtColors.Text = "\t Video contains following major colors :\r\n"+ ParseColorList(colorList);
                txtResult.Text = "\t Video contains following properties :" + videoInfo
                                    + "\r\n\r\n Content from video, frame by frame:\r\n" + contentMessage
                                    + "\r\n\r\n Audio contains following text: \r\n" + audiMessage;
                       
                DirectoryInfo directory = new DirectoryInfo(appStartPath + @"\bin\img");
                FileInfo[] files = directory.GetFiles(frameName+ "*.png").ToArray();

                ShowAllFramesOnPanel(files, frameName);
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GC.Collect();
                Cursor.Current = Cursors.AppStarting;
                HideLoader();
            }
        }

        /// <summary>
        /// To Parse list of color and return message
        /// </summary>
        /// <param name="colorList"></param>
        /// <returns></returns>
        public string ParseColorList(List<ColorModel> colorList)
        {
            string colorMessage = "";
            if (colorList.Count() == 0)
                return "There is no colors in Video";

            foreach (var clr in colorList)
            {
                colorMessage += "\r\n" + clr.color + " : " + clr.pecentage +"%";
            }

            return colorMessage;
        }

        public void DisposeControls()
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                flowLayoutPanel1.Controls.Remove(control);
                control.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }           
           
            flowLayoutPanel1.Controls.Clear();
            txtColors.Text = "";
            txtResult.Text = "";
            lblImageSize.Text = "";
        }

        /// <summary>
        /// reason : To show all frames of video in panel
        /// </summary>
        /// <param name="files"></param>
        /// <param name="imageName"></param>
        private void ShowAllFramesOnPanel(FileInfo[] files,string imageName)
        {
            flowLayoutPanel1.BringToFront();
            try
            {

                PictureBox[] pics = new PictureBox[files.Count()];
                FlowLayoutPanel[] flws = new FlowLayoutPanel[files.Count()];
                Label[] lbl = new Label[files.Count()];
                _faceDetection = new FaceDetection(appStartPath);
                int brh = 0;
                for (int i = 0; i < files.Count(); i++)
                {
                    if (!File.Exists(files[i].FullName))
                        continue;

                    noOfFaces = 0;
                    newFrame = null;
                    _faceDetection.DetectFace(appStartPath, files[i].FullName, ref noOfFaces, ref newFrame);
                    flws[i] = new FlowLayoutPanel();
                    flws[i].Name = "flw" + i;
                    flws[i].Location = new Point(3, brh);
                    flws[i].Size = new Size(217, 210);
                    flws[i].BackColor = Color.DarkCyan;
                    flws[i].BorderStyle = BorderStyle.Fixed3D;

                    lbl[i] = new Label();
                    lbl[i].Name = files[i].Name;
                    lbl[i].Size = new Size(100, 35);
                    lbl[i].Text = "Frame " + i + " Contains " + noOfFaces + " Face(s)";

                    pics[i] = new PictureBox();
                    pics[i].Name = files[i].FullName;
                    pics[i].Size = new Size(217, 175);
                    pics[i].Image = newFrame==null?  System.Drawing.Image.FromFile(files[i].FullName): newFrame;
                    pics[i].SizeMode = PictureBoxSizeMode.StretchImage;

                    flws[i].Controls.Add(lbl[i]);
                    flws[i].Controls.Add(pics[i]);
                   
                    this.Controls.Add(flws[i]);
                    flowLayoutPanel1.Controls.Add(flws[i]);
                }
            }
            catch(Exception)
            {
            }
        }

        /// <summary>
        /// Reason : To check valid media format
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mediaExtensions"></param>
        /// <returns></returns>
        static bool IsMediaFile(string path, string[] mediaExtensions)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }
        
        private void frmImageProcessing_Load(object sender, EventArgs e)
        {
            HideLoader();
        }

        /// <summary>
        /// Reason : TO detect face on selected image, if face(s) detect then mark it in box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaceDetect_Click(object sender, EventArgs e)
        {
            if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), imageExtensions))
            {
                MessageBox.Show("Please select valid Image file!!");
                return;
            }
            ShowLoader();
            _faceDetection = new FaceDetection(appStartPath);
            _faceDetection.DetectFace(appStartPath, txtFilePath.Text.ToString(), ref noOfFaces, ref newFrame);
            HideLoader();
            pbImage.Image = newFrame;
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImage.BringToFront();
            MessageBox.Show("Selected Image contains "+ noOfFaces +" faces.");
        }

        private void DuplicateSearch_Click(object sender, EventArgs e)
        {
            if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), imageExtensions))
            {
                MessageBox.Show("Please select valid Image file!!");
                return;
            }

            DisposeControls();
            ShowLoader();
            string message = "Following files are matches with selected image,\r\n";
            double length = 100000;
            string targetDirPath =appStartPath+  @"\bin\img";
            List<DuplicateImageDetails> duplicateImageList = new List<DuplicateImageDetails>();
            //Comparison on pixel by pixel
            //new DuplicateImageSearch().GetAllSimilarImages(txtFilePath.Text.ToString(), length, targetDirPath, ref duplicateImageList);
            //compare by database
            new DuplicateImageSearch().GetAllSimilarImages(txtFilePath.Text.ToString(), appStartPath, length, ref duplicateImageList);
                                   
            if (duplicateImageList.Count() > 1)
            {
                foreach (var x in duplicateImageList)
                {
                    message += "File Name : " + x.FileName +", Percentage : "+ x.Percentage +"\r\n\r\n";
                }

                ShowAllFramesOnPanel(duplicateImageList, isRemoteImage);
            }

            txtResult.Text = message.Trim() != "Following files are matches with selected image," ? message : "Selected Image not matched with any existing images";
            HideLoader();
        }

        /// <summary>
        /// Reason : To show all Duplicate matched images on grid panel
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="percentage"></param>
        private void ShowAllFramesOnPanel(List<DuplicateImageDetails> duplicateImageList, Boolean isRemoteImage)
        {
            flowLayoutPanel1.BringToFront();
            try
            {
                int totalFiles = duplicateImageList.Count();
                PictureBox[] pics = new PictureBox[totalFiles];
                FlowLayoutPanel[] flws = new FlowLayoutPanel[totalFiles];                
                Label[] lbl = new Label[totalFiles];

                int brh = 0;
                for (int i = 0; i < totalFiles; i++)
                {
                    try
                    {
                        if (!isRemoteImage)
                        {
                            if (!File.Exists(duplicateImageList[i].FilePath))
                                continue;
                        }

                        noOfFaces = 0;
                        newFrame = null;
                        flws[i] = new FlowLayoutPanel();
                        flws[i].Name = "flw" + i;
                        flws[i].Location = new Point(3, brh);
                        flws[i].Size = new Size(217, 210);
                        flws[i].BackColor = Color.DarkCyan;
                        flws[i].BorderStyle = BorderStyle.Fixed3D;

                        lbl[i] = new Label();
                        lbl[i].Name = duplicateImageList[i].Percentage;
                        lbl[i].Size = new Size(100, 35);
                        lbl[i].Text = isRemoteImage == true
                            ? "Match File name : " + duplicateImageList[i].FileName
                            : "Image matching percentage is " + duplicateImageList[i].Percentage;

                        pics[i] = new PictureBox();
                        pics[i].Name = duplicateImageList[i].FilePath;
                        pics[i].Size = new Size(217, 175);
                        //pics[i].Image = System.Drawing.Image.FromFile(duplicateImageList[i].FileName);
                        pics[i].Image = isRemoteImage == true
                            ? System.Drawing.Image.FromStream(new MemoryStream(DownloadFile(duplicateImageList[i].FileName)))
                            : System.Drawing.Image.FromFile(duplicateImageList[i].FilePath);
                        pics[i].SizeMode = PictureBoxSizeMode.StretchImage;

                        flws[i].Controls.Add(lbl[i]);
                        flws[i].Controls.Add(pics[i]);

                        this.Controls.Add(flws[i]);
                        flowLayoutPanel1.Controls.Add(flws[i]);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public void ShowLoader()
        {
            pbLoading.Show();
        }
        public void HideLoader()
        {
            pbLoading.Hide();
        }

        #region Stub test code      
        public void Stub()
        {
            IGrabber imageGrab = new ImageVideoGrabber.Grabber();
            ImageFile fileInput = new ImageFile();
            fileInput.FileName = txtFilePath.Text;
            //test1
             
            var result = imageGrab.ExtractTextFromImage(fileInput);
            var v = result;
            //test2
            fileInput.FileName = txtFilePath.Text; 
            List<Colors> result2= imageGrab.GetImageColors(fileInput);
            var v2 = result2;

            //test3
            VideoFileDetail videoFielOutput = new VideoFileDetail();
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = txtFilePath.Text.ToString().Trim();
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoFielOutput.ApplicationStartupPath = appStartPath;
            videoFielOutput.OutputImagePath = outputImgFilePath;
            videoFielOutput.InputFilePath = filePath;
            videoFielOutput.BatchFilePath = batchFilePath;          
             
            var result3 = imageGrab.GetVideoDetails(videoFielOutput);
            var v3 = result3;

            //test4
            ImageFileDetails imageFileDupCheck = new ImageFileDetails();
            string targetDirPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageProcessing\bin\Debug\bin\img";
            imageFileDupCheck.FilePath = txtFilePath.Text.ToString();
            imageFileDupCheck.FileLength = 100000;
            imageFileDupCheck.FolderPath = targetDirPath;
            imageFileDupCheck.ApplicationStartupPath = appStartPath;
            var result4 = imageGrab.GetAllSimilarImages(imageFileDupCheck);
            var v4 = result4;

            //upload image
            imageGrab.UploadImageFile(txtFilePath.Text.ToString(),appStartPath);

            //download file
            string imagePath = txtFilePath.Text.ToString();
            imageGrab.DownloadFile(imagePath.Contains("\\") ? imagePath.Split('\\')[imagePath.Split('\\').Count() - 1] : imagePath);

        }
        #endregion

        /// <summary>
        /// Get and upload metad data of images/image
        /// user can input file or folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveMetadata_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoader();
                string filePath = txtFilePath.Text;
                if (!IsMediaFile(filePath, imageExtensions))
                {
                    new DuplicateImageSearch().SaveMetadataOfAllImages(filePath, appStartPath);
                }
                else {
                    new DuplicateImageSearch().SaveMetadataOfImage(filePath, appStartPath);
                }
                HideLoader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            ShowLoader();
            UploadFile(txtFilePath.Text);
            HideLoader();
        }

        private void btnDownloadImage_Click(object sender, EventArgs e)
        {
            ShowLoader();
            //DownloadFile(txtFilePath.Text);
            HideLoader();
        }
        private async void UploadFile(string filePath)
        {
            DataUpload _blobWrapper = new DataUpload();
            _blobWrapper.UploadFile(filePath);
        }

        public byte[] DownloadFile(string downloadFileName)
        {
            DataUpload _blobWrapper = new DataUpload();
            byte[] data = _blobWrapper.DownloadFileFromBlob(downloadFileName);
            return data;
        }
    }
}
