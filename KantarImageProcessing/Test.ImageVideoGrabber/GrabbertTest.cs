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
            Assert.AreEqual(expectedResult, result.Content,"Exception occured in extracting text from Image for Invalid path");
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
        public void ExtractTextFromImageWithNullPath()
        {
            //Arrange
            ImageFile fileNullObj = new ImageFile { FileName = null };
            ImageFile fileEmptyObj = new ImageFile { FileName = "    " };
            string expectedResult = "";
            //Act
            ImageContent resultForNull = _grab.ExtractTextFromImage(fileNullObj);
            ImageContent resultForEmpty = _grab.ExtractTextFromImage(fileEmptyObj);

            //Assert
            Assert.AreEqual(expectedResult, resultForNull.Content, "Exception occured in extracting text from Image  of null type of Image Path");
            Assert.AreEqual(expectedResult, resultForEmpty.Content, "Exception occured in extracting text from Image of empty type of Image path");
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
            Assert.IsTrue((actualResult.Count >0), "Exception occured in geting colors from image for valid remote image");

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
        public void GetVideoDetails()
        {
            //Arrange
            VideoFileDetail videoObj = new VideoFileDetail();
            string appStartPath = @"D:\git-code\ImageProcessing\KantarImageProcessing\ImageProcessing\bin\Debug";//Application.StartupPath;

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

        #endregion
        
        #region Duplicate Image Processing
        [TestMethod]
        public void GetAllSimilarImages()
        {
            //Action
            ImageFileDetails imageObj = new ImageFileDetails();
            imageObj.ApplicationStartupPath = "";
            imageObj.FileLength = 100000;
            imageObj.FilePath = @"D:\videos\images\unitTest\red11.png";
            imageObj.FolderPath = "";
            imageObj.Message = "";

            //Act
            List<DuplicateImage> actualResult = _grab.GetAllSimilarImages(imageObj);
            //Assert

        }

        #endregion
        
        #region Audio to Text [TestMethod]
        [TestMethod]
        public void ConvertAudioToText()
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
