using ImageVideoProcessing;
using Newtonsoft.Json;
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
        public ImageContent ExtractTextFromImage(ImageFileInput fileNameObj)
        {
            ImageContent ImageContent = new ImageContent();
            ImageContent.Content = imageGrab.ExtractTextFromImage(fileNameObj.FileName);
            return ImageContent;
            //return JsonConvert.SerializeObject(ImageContent);
        }

        /// <summary>
        /// Reason : To get color codes from image
        /// Image devided into #x# parts to pickup pixel colors
        /// </summary>
        /// <param name="imageInputObj">Accepts object of ImageFileInput</param>
        /// <returns>returns list of List<ColorModel> model</returns>
        public List<Colors> GetImageColors(ImageFileInput imageInputObj)
        {
            List<ColorModel> colorModel = imageGrab.GetImageColors(imageInputObj.FileName);
            return prepareColorModel( colorModel);
            //return JsonConvert.SerializeObject(colorModel);
        }

        /// <summary>
        /// Prepare color list model
        /// </summary>
        /// <param name="colorModel"></param>
        /// <returns></returns>
        private List<Colors> prepareColorModel(List<ColorModel> colorModel)
        {
            List<Colors> colorList = new List<Colors>();
            if (colorModel.Count == 0)
                return colorList;

            foreach (var clr in colorModel)
            {
                Colors clrObj = new Colors();
                clrObj.color = clr.color;
                clrObj.pecentage = clrObj.pecentage;
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
        public VideoContent GetVideoDetails(VideoFileInput fileInputObj)
        {
            VideoContent videoContent = new VideoContent();
            List<ColorModel> colorList = new List<ColorModel>();
            string contentMessage = ""; string videoInfo = ""; string audioMessage = "";
            videoGrab.GetVideoDetails(fileInputObj.ApplicationStartupPath, fileInputObj.OutputImagePath, fileInputObj.InputFilePath, fileInputObj.BatchFilePath, fileInputObj.FrameName, ref contentMessage, ref colorList, ref videoInfo, ref audioMessage);

            videoContent.AudioMessage = audioMessage;
            videoContent.ContentMessage = contentMessage;
            videoContent.VideoInfo= videoInfo;
            videoContent.ColorList = prepareColorModel( colorList);
            return videoContent;
            //string jsonString = JsonConvert.SerializeObject(videoContent);
            //return jsonString;
        }

        #endregion

        #region Duplicate Image Processing

        /// <summary>
        /// Reason : To get all similar files from folder with percentage of similarity for selected file.
        /// compare files from folder which are having length of file +- 100000 of original file.
        /// </summary>
        /// <param name=">model DuplicateImageSearchPath requires following Input parameter list</param>
        /// <param name="filePath">Input file path of image</param>
        /// <param name="length">Length of image file that varies to compare with another file</param>
        /// <param name="folderPath">Folder location where to search duplocate files</param>
        /// <param name="message">return message in reference variable for no of matches</param>
        /// <param name="fileNames">returns names of files which are matched in target flder seperated by comma(,)</param>
        /// <param name="percentageString"> returns percentage of similarities of matched images in string seperated by comma(,)</param>
        public List<DuplicateImageCheck> GetAllSimilarImages(DuplicateImageSearchPath duplicateImageInputObj)
        {
            List<DuplicateImageCheck> duplicateImageList = new List<DuplicateImageCheck>();    
            dupSearch.GetAllSimilarImages(duplicateImageInputObj.FilePath, duplicateImageInputObj.FileLength, duplicateImageInputObj.FolderPath, ref duplicateImageList);
            return duplicateImageList;
            //return JsonConvert.SerializeObject(duplicateImageList);
        }

        #endregion

        #region Audio to Text
        public AudioTextContent ConvertAudioToText(AudioInput audioFileObj)
        {
            AudioTextContent audioOutput = new AudioTextContent();
            string textMessage = ""; 
            audioGrab.ConvertAudioToText(audioFileObj.ApplicationStartupPath, audioFileObj.AudioFolderPath, audioFileObj.FrameName, audioFileObj.AudioDuration, ref textMessage);
            audioOutput.AudioText = textMessage;
            return audioOutput;
            //return JsonConvert.SerializeObject(audioOutput);
        }

        #endregion
    }
}
