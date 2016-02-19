﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;
using ImageVideoProcessing;

namespace ImageProcessing
{
    public partial class frmImageProcessing : Form
    {
        #region Global Declarations

        string[] videoExtensions = { ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV",".3GP" };
        string[] imageExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF" };
        int noOfFaces = 0;
        Bitmap newFrame = null;
        string appStartPath = Application.StartupPath;
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
            if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), imageExtensions))
            {
                MessageBox.Show("Please select valid Image file!!");
                return;
            }

            DisposeControls();
            ShowLoader();
            string colorNames = "";
            string path = txtFilePath.Text.ToString();
            //Cursor.Current = Cursors.WaitCursor;
            
            txtResult.Text = new ImageVideoProcessing.ImageGrabber().ExtractTextFromImage(path);
            new ImageVideoProcessing.ImageGrabber().GetImageColors(path, ref colorNames);
            txtColors.Text = "\tImage contains following major colors:"+ colorNames;
            Cursor.Current = Cursors.AppStarting;
            HideLoader();
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
            string contentMessage = "";
            string colorMessage = "";
            string videoInfo = "";
            string audiMessage = "";
            string frameName = Guid.NewGuid().ToString();
            string path = appStartPath + @"\bin\img\";
            string filePath = txtFilePath.Text.ToString().Trim();
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            try
            {
                new ImageVideoProcessing.VideoGrabber().GetVideoDetails(appStartPath, path, filePath, batchFilePath, frameName, ref contentMessage, ref colorMessage, ref videoInfo, ref audiMessage);
                txtColors.Text = "\t Video contains following major colors :\r\n"+ colorMessage;
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
                
                int brh = 0;
                for (int i = 0; i < files.Count(); i++)
                {
                    noOfFaces = 0;
                    newFrame = null;
                    ///FaceDetect(files[i].FullName, ref noOfFaces, ref newFrame);

                    flws[i] = new FlowLayoutPanel();
                    flws[i].Name = "flw" + i;
                    flws[i].Location = new Point(3, brh);
                    flws[i].Size = new Size(217, 210);
                    flws[i].BackColor = Color.DarkCyan;
                    flws[i].BorderStyle = BorderStyle.Fixed3D;

                    lbl[i] = new Label();
                    lbl[i].Name = files[i].Name;
                    lbl[i].Size = new Size(100, 35);
                    //lbl[i].Image = System.Drawing.Image.FromFile(files[i].FullName);
                    lbl[i].Text = "Frame "+i +" Contains " + noOfFaces + " Face(s)";

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

        #region Face Detection code
        
        //private HaarCascade haar;
        /*
        private void FaceDetect(string path,ref int noOfFaces, ref Bitmap newFrame )
        {
            Bitmap bmp = (Bitmap)Image.FromFile(path);
            using (Image<Bgr, byte> nextFrame = new Image<Bgr, Byte>(bmp))
            {
                if (nextFrame != null)
                {
                    // there's only one channel (greyscale), hence the zero index
                    Image<Gray, byte> grayframe = nextFrame.Convert<Gray, byte>();
                    var faces =
                            grayframe.DetectHaarCascade(
                                    haar, 1.79, 4,
                                    HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                                    new Size(nextFrame.Width / 20, nextFrame.Height / 20)
                                    )[0];
                    noOfFaces = faces.Count();
                    foreach (var face in faces)
                    {
                        nextFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);
                    }
                    newFrame = nextFrame.ToBitmap();
                    pbImage.Image = nextFrame.ToBitmap();
                    pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pbImage.BringToFront();
                }
            }
        }
        */
        private void LoadXML()
        {
            // haar = new HaarCascade(appStartPath + @"\haarcascade_frontalface_default.xml");
        }
        #endregion

        private void frmImageProcessing_Load(object sender, EventArgs e)
        {
            HideLoader();
            LoadXML();
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
            new FaceDetection().FaceDetect(appStartPath, txtFilePath.Text.ToString(), ref noOfFaces, ref newFrame);
            HideLoader();
            pbImage.Image = newFrame;
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImage.BringToFront();
            MessageBox.Show("Selected Image contains "+ noOfFaces +" faces.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsMediaFile(txtFilePath.Text.ToString().Trim(), imageExtensions))
            {
                MessageBox.Show("Please select valid Image file!!");
                return;
            }

            DisposeControls();
            ShowLoader();
            string message = "";
            string fileNames = "";
            string percentage = "";
            string targetDirPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageProcessing\bin\Debug\bin\img";
            new DuplicateImageSearch().GetAllSimilarImages(txtFilePath.Text.ToString(), 100000, targetDirPath , ref message, ref fileNames, ref percentage);
            txtResult.Text =message.Trim()!= "Following files are matches with selected image,"?message: "Selected Image not matched with any existing images";

            if(fileNames.Trim()!="" && percentage.Trim() != "")
            {
                ShowAllFramesOnPanel(fileNames, percentage);
            }

            HideLoader();
        }

        /// <summary>
        /// Reason : To show all Duplicate matched images on grid panel
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="percentage"></param>
        private void ShowAllFramesOnPanel( string imageName, string percentage)
        {
            flowLayoutPanel1.BringToFront();
            try
            {
                string[] fileArray = imageName.Split(',');
                string[] PercentageArray = percentage.Split(',');

                int totalFiles = imageName.Split(',').Count();
                PictureBox[] pics = new PictureBox[totalFiles];
                FlowLayoutPanel[] flws = new FlowLayoutPanel[totalFiles];
                
                Label[] lbl = new Label[totalFiles];

                int brh = 0;
                for (int i = 0; i <= totalFiles; i++)
                {
                    noOfFaces = 0;
                    newFrame = null;

                    flws[i] = new FlowLayoutPanel();
                    flws[i].Name = "flw" + i;
                    flws[i].Location = new Point(3, brh);
                    flws[i].Size = new Size(217, 210);
                    flws[i].BackColor = Color.DarkCyan;
                    flws[i].BorderStyle = BorderStyle.Fixed3D;

                    lbl[i] = new Label();
                    lbl[i].Name = PercentageArray[i];
                    lbl[i].Size = new Size(100, 35);
                    lbl[i].Text = PercentageArray[i];

                    pics[i] = new PictureBox();
                    pics[i].Name = fileArray[i];
                    pics[i].Size = new Size(217, 175);
                    pics[i].Image = System.Drawing.Image.FromFile(fileArray[i]);
                    pics[i].SizeMode = PictureBoxSizeMode.StretchImage;

                    flws[i].Controls.Add(lbl[i]);
                    flws[i].Controls.Add(pics[i]);

                    this.Controls.Add(flws[i]);
                    flowLayoutPanel1.Controls.Add(flws[i]);
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
    }
}
