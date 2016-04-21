﻿using CommonImplementation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ImageVideoProcessing
{
    public class DuplicateImageSearch
    {
        #region Global Declaration
        string[] imageFilters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
        FaceDetection _faceDetection =null;
        ImageGrabber _imgGrabber = null;
        DataUpload _blobWrapper = null;
        #endregion

        /// <summary>
        /// Reason : To get all similar files from folder with percentage of similarity for selected file.
        /// compare files from folder which are having length of file +- 100000 of original file.
        /// </summary>
        /// <param name="inputFilePath">Input file path of image</param>
        /// <param name="length">Length of image file that varies to compare with another file</param>
        /// <param name="folderPath">Folder location where to search duplocate files</param>
        /// <param name="percentageString"> returns percentage of similarities of matched images in string seperated by comma(,)</param>
        public void GetAllSimilarImages(string inputFilePath,double length, string folderPath, ref List<DuplicateImageDetails> duplicateImageList)
        {
            {
                try
                {
                    DuplicateImageDetails imgOriginalFile = new DuplicateImageDetails();
                    imgOriginalFile.FileName = inputFilePath;
                    imgOriginalFile.Percentage = "Original Selected File";
                    duplicateImageList.Add(imgOriginalFile);

                    Boolean flag = false;
                    int count = 0; int percentage = 0;
                    FileInfo fileInfo = new FileInfo(inputFilePath);
                    DirectoryInfo directory = new DirectoryInfo(folderPath);
                    String[] files = GetFilesFrom(folderPath, imageFilters, true);
                    //Filter images by size
                    var imageList = from file in files
                                where (file.Length > fileInfo.Length - length && file.Length < fileInfo.Length + length)
                                && file != inputFilePath
                                select file;

                    foreach (var imgName in imageList)
                    {
                        flag = false;
                        CompareImages(inputFilePath, imgName, ref flag, ref percentage);
                        if (flag)
                        {
                            DuplicateImageDetails imgDupCheck = new DuplicateImageDetails();                           
                            imgDupCheck.FilePath = imgName;
                            imgDupCheck.FilePath = imgName.Contains("\\") ? imgName.Split('\\')[imgName.Split('\\').Count() - 1] : imgName;
                            imgDupCheck.Percentage = percentage + "%";
                            duplicateImageList.Add(imgDupCheck);
                            count++;
                        }
                    }               
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Reason : To Compare selected image with another image from folder
        /// Return true only if similarity is upto 60 % between two images.
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="comapareImage"></param>
        /// <param name="flag"></param>
        /// <param name="percentage"></param>
        private void CompareImages(string originalImage, string comapareImage, ref Boolean flag, ref int percentage)
        {
            flag = false;
            int count1 = 0, count2 = 0;
            Bitmap img1 = new Bitmap(originalImage);
            Bitmap img2 = new Bitmap(comapareImage); try
            {
                if (img1.Width <= img2.Width && img1.Height <= img2.Height)
                {
                    for (int i = 0; i < img1.Width; i+=2)
                    {
                        for (int j = 0; j < img1.Height; j+=2)
                        {
                            Color img1_refColor = img1.GetPixel(i, j);
                            Color img2_refColor = img2.GetPixel(i, j);
                            if ((img1_refColor.R >= img2_refColor.R -6 && img1_refColor.R <= img2_refColor.R + 6)
                                && (img1_refColor.G >= img2_refColor.G - 6 && img1_refColor.G <= img2_refColor.G + 6)
                                && (img1_refColor.B >= img2_refColor.B - 6 && img1_refColor.B <= img2_refColor.B + 6))
                            {
                                count1++; flag = true;
                            }
                            else
                            {
                                count2++; flag = false;
                            }
                        }
                    }

                    percentage = Convert.ToInt32(((double)count1 / (count1 + count2)) * 100);
                    if (percentage >= 60)
                    {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reason To get all filtered images from folder
        /// </summary>
        /// <param name="searchFolder"></param>
        /// <param name="filters"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        private String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        #region GetMetadata

        /// <summary>
        /// Get and save metadata of all files from input folder
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="appStartPath"></param>
        public void SaveMetadataOfAllImages(string folderPath, string appStartPath)
        {
            try
            {
                _blobWrapper = new DataUpload();
                _faceDetection = new FaceDetection(appStartPath);
                _imgGrabber = new ImageGrabber();
                DirectoryInfo directory = new DirectoryInfo(folderPath);
                List<DataLayer.EntityModel.Image> imageList = new List<DataLayer.EntityModel.Image>();
                String[] files =  GetFilesFrom(folderPath, imageFilters,true);
                List<string> fileNameList = new List<string>();               
                fileNameList = GetUniqueImages(files.ToList());

                foreach (var fileObj in fileNameList)
                {
                    //Get metadata of file and save it
                    imageList.Add(GetImageMetadata(fileObj, appStartPath));
                    //Upload file to file to azure blob
                    _blobWrapper.UploadFile(fileObj);
                }

                new DataLayer.ModelClasses.Image().SaveUpdateMetadata(imageList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get and save metadata of file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="appStartPath"></param>
        public void SaveMetadataOfImage(string filePath, string appStartPath)
        {
            try
            {
                _blobWrapper = new DataUpload();
                _faceDetection = new FaceDetection(appStartPath);
                _imgGrabber = new ImageGrabber(); 
                List<DataLayer.EntityModel.Image> imageList = new List<DataLayer.EntityModel.Image>();
                String[] files = new string[1];
                files[0] = filePath;
                List<string> fileNameList = new List<string>();
                fileNameList = GetUniqueImages(files.ToList());

                foreach (var fileObj in fileNameList)
                {
                    //Get metadata of file and save it
                    imageList.Add(GetImageMetadata(fileObj, appStartPath));
                    //Upload file to file to azure blob
                    _blobWrapper.UploadFile(fileObj);
                }

                new DataLayer.ModelClasses.Image().SaveUpdateMetadata(imageList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get metadata of image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="appStartPath"></param>
        /// <returns>DataLayer.EntityModel.Image</returns>
        public DataLayer.EntityModel.Image GetImageMetadata(string imagePath, string appStartPath)
        {
            int  height = 0, width = 0, RedPercentage = 0, BluePercentage = 0, GreenPercentage = 0;
            long length = 0;
            DataLayer.EntityModel.Image imageObj = new DataLayer.EntityModel.Image();
            //to get checksum
            string checksume = GetChecksumMD5(imagePath);
            //to get no of faces from image
            int noOfFaces = _faceDetection.GetNoOfFacesFromImage(appStartPath, imagePath);
            //to get metadata
            _imgGrabber.GetImageMetadata(imagePath, ref height, ref width, ref length, ref RedPercentage, ref BluePercentage, ref GreenPercentage);
            //to check image contains text or not
            string imageContent = _imgGrabber.ExtractTextFromImage(imagePath);
            imageContent = string.IsNullOrEmpty(imageContent) ? imageContent : imageContent.Replace("\r\n", "");
            imageObj.RedPercentage = RedPercentage;
            imageObj.GreenPercentage = GreenPercentage;
            imageObj.BluePercentage = BluePercentage;
            imageObj.Checksum = checksume;
            imageObj.Height = height;
            imageObj.Width = width;
            imageObj.ImagePath = imagePath;
            imageObj.IsImageContainsFace = noOfFaces > 0 ? true : false;
            imageObj.IsImageContainsText = imageContent.Length > 0 ? true : false;
            imageObj.Length = length;
            imageObj.Name = imagePath.Contains("\\") ? imagePath.Split('\\')[imagePath.Split('\\').Count() - 1] : imagePath;
            imageObj.FaceCount = noOfFaces;
            imageObj.CreatedDatetime = DateTime.Now;
            imageObj.IsDeleted = false;
            imageObj.Description = imageContent;

            return imageObj;
        }

        /// <summary>
        /// Get checksume of image
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetChecksumMD5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        /// <summary>
        /// To get Unique files
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public List<string> GetUniqueImages(List<string> fileList)
        {
            List<string> PathList = null;
            PathList = new DataLayer.ModelClasses.Image().GetAllImagePath();
            if (PathList.Count() <= 0)
                return fileList;

            fileList.RemoveAll(p => PathList.Contains(p));
            return fileList;
        }

        /// <summary>
        /// Reason : To get all similar files from folder with percentage of similarity for selected file.
        /// compare files from folder which are having length of file +- 100000 of original file.
        /// </summary>
        /// <param name="inputFilePath">Input file path of image</param>
        /// <param name="length">Length of image file that varies to compare with another file</param>
        /// <param name="percentageString"> returns percentage of similarities of matched images in string seperated by comma(,)</param>
        public void GetAllSimilarImages(string inputFilePath,string appStartPath, double length, ref List<DuplicateImageDetails> duplicateImageList)
        {
            {
                try
                {
                    if (!File.Exists(inputFilePath))
                        return;

                    DuplicateImageDetails imgOriginalFile = new DuplicateImageDetails();
                    imgOriginalFile.FilePath = inputFilePath;
                    imgOriginalFile.FileName = inputFilePath.Contains("\\") ? inputFilePath.Split('\\')[inputFilePath.Split('\\').Count() - 1] : inputFilePath;
                    imgOriginalFile.Percentage = "Original Selected File";
                    duplicateImageList.Add(imgOriginalFile);

                    int count = 0;
                    FileInfo fileInfo = new FileInfo(inputFilePath);
                    DataLayer.EntityModel.Image metadataInputImgObj = new DataLayer.EntityModel.Image();
                    //Get metadata of input file
                    _faceDetection = new FaceDetection(appStartPath);
                    _imgGrabber = new ImageGrabber();
                    metadataInputImgObj = GetImageMetadata(inputFilePath, appStartPath);

                    var bestMatchImageList = new DataLayer.ModelClasses.Image().GetImagesByBestMatch(metadataInputImgObj);
                    // for Image similarity percentage need to compare both images
                    foreach (var infoObj in bestMatchImageList)
                    {
                        DuplicateImageDetails duplicateImageCheck = new DuplicateImageDetails();
                        duplicateImageCheck.FilePath = infoObj.ImagePath;
                        duplicateImageCheck.FileName = infoObj.ImagePath.Contains("\\") ? infoObj.ImagePath.Split('\\')[infoObj.ImagePath.Split('\\').Count() - 1] : infoObj.ImagePath;
                        duplicateImageCheck.Percentage = "";
                        duplicateImageList.Add(duplicateImageCheck);
                        count++;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion
    }
}