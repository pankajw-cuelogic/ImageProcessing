﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Linq;

namespace ImageVideoProcessing
{
    public class FaceDetection
    {
        #region Global Declaration

        private HaarCascade haar;
        
        #endregion

        #region Face Detection code

        /// <summary>
        /// Reason : To detect face in image, return bitmap image with rectangle on face if face detected in image
        /// </summary>
        /// <param name="appStartPath">Application startup path from where application is running, to load xml data</param>
        /// <param name="inputFilePath">Input file path</param>
        /// <param name="noOfFaces">returns in reference variable no of faces matched</param>
        /// <param name="newFrame">return new bitmap image if face detected else empty bitmap return</param>
        public void DetectFace(string appStartPath,string inputFilePath, ref int noOfFaces, ref Bitmap newFrame)
        {
            LoadXML(appStartPath);
               Bitmap bmp = (Bitmap)Image.FromFile(inputFilePath);
            using (Image<Bgr, byte> nextFrame = new Image<Bgr, Byte>(bmp))
            {
                if (nextFrame != null)
                {
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
                }
            }
        }

        /// <summary>
        /// To load grammer of speech to text
        /// </summary>
        /// <param name="haarcascadeFilePath"></param>
        private void LoadXML(string haarcascadeFilePath)
        {
            haar = new HaarCascade(haarcascadeFilePath+ @"\haarcascade_frontalface_default.xml");
        }
        #endregion
    }
}