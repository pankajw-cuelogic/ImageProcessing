using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageVideoProcessing
{
    public class FaceDetection
    {
        #region Face Detection code

        private HaarCascade haar;
        /// <summary>
        /// Reason : To detect face in image, return bitmap image with rectangle on face if face detected in image
        /// </summary>
        /// <param name="appStartPath">Application startup path from where application is running, to load xml data</param>
        /// <param name="filePath">Input file path</param>
        /// <param name="noOfFaces">returns in reference variable no of faces matched</param>
        /// <param name="newFrame">return new bitmap image if face detected else empty bitmap return</param>
        public void FaceDetect(string appStartPath,string filePath, ref int noOfFaces, ref Bitmap newFrame)
        {
            LoadXML(appStartPath);
               Bitmap bmp = (Bitmap)Image.FromFile(filePath);
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
                }
            }
        }

        private void LoadXML(string haarcascadeFilePath)
        {
            haar = new HaarCascade(haarcascadeFilePath+ @"\haarcascade_frontalface_default.xml");
        }
        #endregion
    }
}
