using CommonImplementation;
using ImageVideoProcessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImageVideoGrabber
{
    public class Grabber:IGrabber
    {
        #region Global Declararion
        AudioGrabber _audioGrab = new AudioGrabber();
        DuplicateImageSearch _dupSearch = new DuplicateImageSearch();
        VideoGrabber _videoGrab = new VideoGrabber();
        ImageVideoProcessing.ImageGrabber _imageGrabber = new ImageVideoProcessing.ImageGrabber();
        DataUpload _blobWrapper = new DataUpload();
        string[] imageExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".TIF" };

        #endregion

        #region Image Processing

        /// <summary>
        ///Extract text from Image frame 
        /// </summary>
        /// <param name="fileNameObj">Accepts Object of ImageFileInput</param>
        /// <returns></returns>
        public ImageContent ExtractTextFromImage(ImageFile fileNameObj)
        {
            ImageContent _imageContent = new ImageContent();
            _imageContent.Content = _imageGrabber.ExtractTextFromImage(fileNameObj.FileName);
            return _imageContent;
        }

        /// <summary>
        /// Reason : To get color codes from image
        /// Image devided into #x# parts to pickup pixel colors
        /// </summary>
        /// <param name="imageInputObj">Accepts object of ImageFileInput</param>
        /// <returns>returns list of List<ColorModel> model</returns>
        public List<Colors> GetImageColors(ImageFile imageInputObj)
        {
            List<ColorModel> colorModel = _imageGrabber.GetImageColors(imageInputObj.FileName);
            return PrepareColorModel(colorModel);
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
        public VideoContent GetVideoDetails(VideoFileDetail fileInputObj)
        {
            try
            {
                VideoContent videoContent = new VideoContent();
                List<ColorModel> colorList = new List<ColorModel>();
                string contentMessage = ""; string videoInfo = ""; string audioMessage = "";
                _videoGrab.GetVideoDetails(fileInputObj.ApplicationStartupPath, fileInputObj.OutputImagePath, fileInputObj.InputFilePath, fileInputObj.BatchFilePath, Guid.NewGuid().ToString(), ref contentMessage, ref colorList, ref videoInfo, ref audioMessage);

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
        public List<DuplicateImage> GetAllSimilarImages(ImageFileDetails duplicateImageInputObj)
        {
            try
            {
                List<DuplicateImageDetails> duplicateImageList = new List<DuplicateImageDetails>();
                //dupSearch.GetAllSimilarImages(duplicateImageInputObj.FilePath, duplicateImageInputObj.FileLength, duplicateImageInputObj.FolderPath, ref duplicateImageList);
                _dupSearch.GetAllSimilarImages(duplicateImageInputObj.FilePath, duplicateImageInputObj.ApplicationStartupPath, duplicateImageInputObj.FileLength, ref duplicateImageList);

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
        private List<DuplicateImage> PrepareDuplicateFileModel(List<DuplicateImageDetails> duplicateImagesModel)
        {
            try
            {
                List<DuplicateImage> duplicateImagesList = new List<DuplicateImage>();
                if (duplicateImagesModel.Count == 0)
                    return duplicateImagesList;

                foreach (var clr in duplicateImagesModel)
                {
                    DuplicateImage duplicateImage = new DuplicateImage();
                    duplicateImage.FileName = clr.FileName;
                    duplicateImage.Percentage = clr.Percentage;
                    duplicateImagesList.Add(duplicateImage);
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
        public AudioTextContent ConvertAudioToText(AudioFileDetails audioFileObj)
        {
            try
            {
                AudioTextContent audioOutput = new AudioTextContent();
                string textMessage = "";
                _audioGrab.ConvertAudioToText(audioFileObj.ApplicationStartupPath, audioFileObj.AudioFolderPath, audioFileObj.FrameName, audioFileObj.AudioDuration, ref textMessage);
                audioOutput.AudioText = textMessage;
                return audioOutput;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Upload Download images to server/azure blob

        /// <summary>
        /// Upload Image/ inmages from folder file to blob service
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="appStartPath"></param>
        public void UploadImageFile(string filePath, string appStartPath)
        {
            if (!IsMediaFile(filePath, imageExtensions))
            {
                new DuplicateImageSearch().SaveMetadataOfAllImages(filePath, appStartPath);
            }
            else {
                new DuplicateImageSearch().SaveMetadataOfImage(filePath, appStartPath);
            }
        }
         
        /// <summary>
        /// Download File, Returns byte array. This byte array convert into Image
        /// </summary>
        /// <param name="downloadFileName"></param>
        /// <returns></returns>
        public byte[] DownloadFile(string downloadFileName)
        {
              _blobWrapper = new DataUpload();
            byte[] data = _blobWrapper.DownloadFileFromBlob(downloadFileName);
            return data;
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
        #endregion
    }
}
