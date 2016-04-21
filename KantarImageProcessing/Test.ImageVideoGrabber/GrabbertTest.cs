using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageVideoGrabber;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace Test.ImageVideoGrabber
{
    [TestClass]
    public class GrabbertTest
    {
        #region Global Declaration
        IGrabber _grab = new Grabber();

        #endregion

        #region Image Processing

        [TestMethod]
        public void ExtractTextFromImageWithInvalidPath()
        {
            //Arrange
            ImageFile fileObj = new ImageFile { FileName = @":\videos\images\red11.png" };
            string expectedResult = "";
            //Act
            ImageContent result = _grab.ExtractTextFromImage(fileObj);

            //Assert
            Assert.AreEqual(expectedResult, result.Content, "Exception occured in extracting text from Image for Invalid path");
        }

        [TestMethod]
        public void ExtractTextFromImageWithInvalidImage()
        {
            //Arrange
            ImageFile fileObj = new ImageFile { FileName = @"D:\videos\images\unitTest\invalid.png" };
            string expectedResult = "";
            //Act
            ImageContent result = _grab.ExtractTextFromImage(fileObj);

            //Assert
            Assert.AreEqual(expectedResult, result.Content, "Exception occured in extracting text from Image for Invalid Image");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "File name can not be null")]
        public void ExtractTextFromImageWithNullPath()
        {
            //Arrange
            ImageFile fileNullObj = new ImageFile { FileName = null };
            ImageFile fileEmptyObj = new ImageFile { FileName = "    " };
            ImageContent expectedResult = null;
            //Act
            ImageContent resultForNull = _grab.ExtractTextFromImage(fileNullObj);
            ImageContent resultForEmpty = _grab.ExtractTextFromImage(fileEmptyObj);

            //Assert
            Assert.AreEqual(expectedResult, resultForNull.Content, "Exception occurred in getting text from image for null path");
            Assert.AreEqual(expectedResult, resultForEmpty.Content, "Exception occurred in getting text from image for empty path");
        }

        [TestMethod]
        public void ExtractTextFromImageForExtraSmallAndLargeValidImage()
        {
            //Arrange
            ImageFile smallFileObj = new ImageFile { FileName = @"D:\videos\images\unitTest\small.png" };
            ImageFile LargefileObj = new ImageFile { FileName = @"D:\videos\images\unitTest\large.png" };
            string expectedResulForSmallImage = "";
            string expectedResulForLargeImage = "";

            //Act
            ImageContent resultForNull = _grab.ExtractTextFromImage(smallFileObj);
            ImageContent resultForEmpty = _grab.ExtractTextFromImage(LargefileObj);

            //Assert
            Assert.AreEqual(expectedResulForSmallImage, resultForNull.Content, "Exception occured in extracting text from small Image");
            Assert.AreNotEqual(expectedResulForLargeImage, resultForEmpty.Content, "Exception occured in extracting text from large Image");
        }

        [TestMethod]
        public void GetImageColorsForInvalidLocalImage()
        {
            //Arrange
            ImageFile FileObj = new ImageFile { FileName = @":\videos\images\unitTest\blank.png" };

            //Act
            List<Colors> actualResult = _grab.GetImageColors(FileObj);

            //Assert
            Assert.IsTrue((actualResult == null || actualResult.Count == 0), "Exception occured in geting colors from image for invalid image");

        }

        [TestMethod]
        public void GetImageColorsForInvalidRemoteImage()
        {
            //Arrange
            ImageFile FileObj = new ImageFile { FileName = @"https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcRJ1-GhmCqBF2qVYHNWXqsmUTVZAFDjoGSmbbxalfu1-8SuV8NTWA1" };

            //Act
            List<Colors> actualResult = _grab.GetImageColors(FileObj);

            //Assert
            Assert.IsTrue((actualResult == null || actualResult.Count == 0), "Exception occured in geting colors from image for invalid image");

        }

        [TestMethod]
        public void GetImageColorsForValidLocalImage()
        {
            //Arrange
            ImageFile FileObj = new ImageFile { FileName = @"D:\videos\images\unitTest\red11.png" };

            //Act
            List<Colors> actualResult = _grab.GetImageColors(FileObj);

            //Assert
            Assert.IsTrue((actualResult.Count > 0), "Exception occured in geting colors from image for valid local image");
        }

        [TestMethod]
        public void GetImageColorsForValidRemoteImage()
        {
            //Arrange
            ImageFile FileObj = new ImageFile { FileName = @"https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcRJ1-GhmCqBF2qVYHNWXqsmUTVZAFDjoGSmbbxalfu1-8SuV8NTWA" };

            //Act
            List<Colors> actualResult = _grab.GetImageColors(FileObj);

            //Assert
            Assert.IsTrue((actualResult.Count > 0), "Exception occured in geting colors from image for valid remote image");

        }

        [TestMethod]
        public void GetImageColorsForValidSmallLocalImage()
        {
            //Arrange
            ImageFile FileObj = new ImageFile { FileName = @"D:\videos\images\unitTest\small.png" };

            //Act
            List<Colors> actualResult = _grab.GetImageColors(FileObj);

            //Assert
            Assert.IsTrue((actualResult.Count > 0), "Exception occured in geting colors from image for valid small image");

        }

        [TestMethod]
        public void GetImageColorsForValidSmallRemoteImage()
        {
            //Arrange
            ImageFile FileObj = new ImageFile { FileName = @"https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSOi3VJdreEiZfSeyZUZQjyNTN1ekNchnmc2lkSTv9jA7GauWua1Q" };

            //Act
            List<Colors> actualResult = _grab.GetImageColors(FileObj);

            //Assert
            Assert.IsTrue((actualResult.Count > 0), "Exception occured in geting colors from image for valid small image");

        }

        #endregion

        #region Video Processing

        [TestMethod]
        [ExpectedException(typeof(System.AccessViolationException),
        "Attempted to read or write protected memory.This is often an indication that other memory is corrupt.")]
        public void GetVideoDetailsForInvalidStartupPath()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @":\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @"D:\videos\images\unitTest\bill2.mp4";
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent video = _grab.GetVideoDetails(videoObj);

            //Assert

        }
        [TestMethod]
        public void GetVideoDetailsForInvalidVideoPath()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @":\videos\images\unitTest\bill2.mp4";
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualResult = _grab.GetVideoDetails(videoObj);

            //Assert
            Assert.IsTrue((actualResult.AudioMessage == "" && actualResult.ColorList.Count == 0 && actualResult.ContentMessage == "" && actualResult.VideoInfo == ""),
                "Exception occurred in getting video details from invalid video path");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ComponentModel.Win32Exception),
        "Access is denied.")]
        public void GetVideoDetailsForInvalidBatchPath()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @"D:\videos\images\unitTest\bill2.mp4";
            string batchFilePath = appStartPath + @"\a\a\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualOutput = _grab.GetVideoDetails(videoObj);            
        }

        [TestMethod]
        [ExpectedException(typeof(System.AccessViolationException),
        "Attempted to read or write protected memory.This is often an indication that other memory is corrupt.")]
        public void GetVideoDetailsForWhenFolderStructureNotCreated()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img1\";
            string filePath = @"D:\videos\images\unitTest\bill2.mp4";
            string batchFilePath = appStartPath + @"\a\a\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualOutput = _grab.GetVideoDetails(videoObj);
        }

        [TestMethod]
        public void GetVideoDetailsForValidPathAndVideo()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @"D:\videos\images\unitTest\bill2.mp4";
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualOutput = _grab.GetVideoDetails(videoObj);

            //Assert
            Assert.IsTrue(actualOutput != null, "Exception occourred in getting video details for valid video file");
        }

        [TestMethod]
        public void GetVideoDetailsForValidSmallVideo()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @"D:\videos\images\unitTest\small.mp4";
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualOutput = _grab.GetVideoDetails(videoObj);

            //Assert
            Assert.IsTrue(actualOutput != null, "Exception occourred in getting video details for valid small video file");
        }

        [TestMethod]
        public void GetVideoDetailsForValidLargeVideo()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @"D:\videos\images\unitTest\large.mp4";
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualOutput = _grab.GetVideoDetails(videoObj);

            //Assert
            Assert.IsTrue(actualOutput != null, "Exception occourred in getting video details for valid large video file");
        }

        [TestMethod]
        public void GetVideoDetailsForValidBlankVideo()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\Test.ImageVideoGrabber\bin\Debug";//Application.StartupPath;
            string frameName = Guid.NewGuid().ToString();
            string outputImgFilePath = appStartPath + @"\bin\img\";
            string filePath = @"D:\videos\images\unitTest\blank.mp4";
            string batchFilePath = appStartPath + @"\ff-prompt.bat";

            videoObj.ApplicationStartupPath = appStartPath;
            videoObj.OutputImagePath = outputImgFilePath;
            videoObj.InputFilePath = filePath;
            videoObj.BatchFilePath = batchFilePath;
            //Act
            VideoContent actualOutput = _grab.GetVideoDetails(videoObj);

            //Assert
            Assert.IsTrue(actualOutput != null, "Exception occourred in getting video details for valid blank video file");
        }

        #endregion

        #region Duplicate Image Processing

        [TestMethod]
        public void GetAllSimilarImagesForValidInput()
        {
            //Action
            ImageFileDetails imageObj = new ImageFileDetails();
            imageObj.ApplicationStartupPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageVideoProcessing\bin\Debug";
            imageObj.FileLength = 100000;
            imageObj.FilePath = @"D:\videos\bmp images\1 - Copy (2).png";
            imageObj.FolderPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageVideoProcessing\bin\Debug";
            imageObj.Message = "";

            //Act
            List<DuplicateImage> actualResult = _grab.GetAllSimilarImages(imageObj);
            //Assert
            Assert.IsTrue(actualResult.Count!=0, "Exception Occurred in GetAllSimilarImage with valid input");
        }

        /// <summary>
        /// Invalid path for image
        /// </summary>
        [TestMethod]
        public void GetAllSimilarImagesForInValidFile()
        {
            //Action
            ImageFileDetails imageObj = new ImageFileDetails();
            imageObj.ApplicationStartupPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageVideoProcessing\bin\Debug";
            imageObj.FileLength = 100000;
            imageObj.FilePath = @"D:\videos\bmp images\1111 - Copy (2).png";
            imageObj.FolderPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageVideoProcessing\bin\Debug";
            imageObj.Message = "";

            //Act
            List<DuplicateImage> actualResult = _grab.GetAllSimilarImages(imageObj);
            //Assert
            Assert.IsTrue(actualResult.Count == 0, "Exception Occurred in GetAllSimilarImage with invalid input");
        }

        [TestMethod]
        public void GetAllSimilarImagesForInValidStartupPath()
        {
            //Action
            ImageFileDetails imageObj = new ImageFileDetails();
            imageObj.ApplicationStartupPath = @"D:\invalid\git-code\ImageProcessing\KantarImageProcessing\ImageVideoProcessing\bin\Debug";
            imageObj.FileLength = 100000;
            imageObj.FilePath = @"D:\videos\bmp images\1111 - Copy (2).png";
            imageObj.FolderPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageVideoProcessing\bin\Debug";
            imageObj.Message = "";

            //Act
            List<DuplicateImage> actualResult = _grab.GetAllSimilarImages(imageObj);
            //Assert
            Assert.IsTrue(actualResult.Count == 0, "Exception Occurred in GetAllSimilarImage with invalid application location path");
        }

        #endregion

        #region Audio to Text [TestMethod]
        [TestMethod]
        public void ConvertAudioToTextForValidAudioFile()
        {
        }
        [TestMethod]
        public void ConvertAudioToTextForInvalidAudioFile()
        {
        }

        [TestMethod]
        public void ConvertAudioToTextForValidLargeAudioFile()
        {
        }
        [TestMethod]
        public void ConvertAudioToTextForValidSmallAudioFile()
        {
        }
        #endregion

        #region Upload Download images to server/azure blob
        [TestMethod]
        public void UploadImageFile()
        {
        }
        [TestMethod]
        public void DownloadFile()
        {
        }
  
        #endregion

    }
}
