using ImageVideoProcessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    public class Grabber:IGrabber
    {
        #region Global Declararion
        AudioGrabber audioGrab = new AudioGrabber();
        DuplicateImageSearch dupSearch = new DuplicateImageSearch();
        FaceDetection faceDetect = new FaceDetection();
        VideoGrabber videoGrab = new VideoGrabber();
        ImageVideoProcessing.ImageGrabber imageGrab = new ImageVideoProcessing.ImageGrabber();
        #endregion

        #region Image Processing

        /// <summary>
        ///Extract text from Image frame 
        /// </summary>
        /// <param name="fileNameObj">Accepts Objec of ImageFileInput</param>
        /// <returns></returns>
        public ImageContent ExtractTextFromImage(ImageFile fileNameObj)
        {
            ImageContent ImageContent = new ImageContent();
            ImageContent.Content = imageGrab.ExtractTextFromImage(fileNameObj.FileName);
            return ImageContent;
        }

        /// <summary>
        /// Reason : To get color codes from image
        /// Image devided into #x# parts to pickup pixel colors
        /// </summary>
        /// <param name="imageInputObj">Accepts object of ImageFileInput</param>
        /// <returns>returns list of List<ColorModel> model</returns>
        public List<Colors> GetImageColors(ImageFile imageInputObj)
        {
            List<ColorModel> colorModel = imageGrab.GetImageColors(imageInputObj.FileName);
            return PrepareColorModel( colorModel);
        }

        /// <summary>
        /// Prepare color list model
        /// </summary>
        /// <param name="colorModel"></param>
        /// <returns></returns>
        private List<Colors> PrepareColorModel(List<ColorModel> colorModel)
        {
            List<Colors> colorList = new List<Colors>();
            if (colorModel.Count == 0)
                return colorList;

            foreach (var clr in colorModel)
            {
                Colors clrObj = new Colors();
                clrObj.Color = clr.color;
                clrObj.Pecentage = clr.pecentage;
                colorList.Add(clrObj);
            }
            return colorList;
        }
        #endregion

        #region Video Processing 

        /// <summary>
        /// Reason : To get Video file details
        /// </summary>
        /// <param name="fileInputObj">Accepts input object of VideoFileInput</param>
        /// <returns>object of VideoContent</returns>
        public VideoContent GetVideoDetails(VideoFile fileInputObj)
        {
            try
            {
                VideoContent videoContent = new VideoContent();
                List<ColorModel> colorList = new List<ColorModel>();
                string contentMessage = ""; string videoInfo = ""; string audioMessage = "";
                videoGrab.GetVideoDetails(fileInputObj.ApplicationStartupPath, fileInputObj.OutputImagePath, fileInputObj.InputFilePath, fileInputObj.BatchFilePath, Guid.NewGuid().ToString(), ref contentMessage, ref colorList, ref videoInfo, ref audioMessage);

                videoContent.AudioMessage = audioMessage;
                videoContent.ContentMessage = contentMessage;
                videoContent.VideoInfo = videoInfo;
                videoContent.ColorList = PrepareColorModel(colorList);
                return videoContent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Duplicate Image Processing

        /// <summary>
        /// Reason : To get all similar files from folder with percentage of similarity for selected file.
        /// compare files from folder which are having length of file +- 100000 of original file.
        /// </summary>
        /// <param name=">model DuplicateImageSearchPath requires following Input parameter list</param>
        public List<DuplicateImages> GetAllSimilarImages(DuplicateImagePath duplicateImageInputObj)
        {
            try
            {
                List<DuplicateImageCheck> duplicateImageList = new List<DuplicateImageCheck>();
                dupSearch.GetAllSimilarImages(duplicateImageInputObj.FilePath, duplicateImageInputObj.FileLength, duplicateImageInputObj.FolderPath, ref duplicateImageList);
                return PrepareDuplicateFileModel(duplicateImageList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Prepare color list model
        /// </summary>
        /// <param name="colorModel"></param>
        /// <returns></returns>
        private List<DuplicateImages> PrepareDuplicateFileModel(List<DuplicateImageCheck> duplicateImagesModel)
        {
            try
            {
                List<DuplicateImages> duplicateImagesList = new List<DuplicateImages>();
                if (duplicateImagesModel.Count == 0)
                    return duplicateImagesList;

                foreach (var clr in duplicateImagesModel)
                {
                    DuplicateImages DuplicateImagesObj = new DuplicateImages();
                    DuplicateImagesObj.FileName = clr.FileName;
                    DuplicateImagesObj.Percentage = clr.Percentage;
                    duplicateImagesList.Add(DuplicateImagesObj);
                }
                return duplicateImagesList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Audio to Text
        public AudioTextContent ConvertAudioToText(AudioInput audioFileObj)
        {
            try
            {
                AudioTextContent audioOutput = new AudioTextContent();
                string textMessage = "";
                audioGrab.ConvertAudioToText(audioFileObj.ApplicationStartupPath, audioFileObj.AudioFolderPath, audioFileObj.FrameName, audioFileObj.AudioDuration, ref textMessage);
                audioOutput.AudioText = textMessage;
                return audioOutput;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
