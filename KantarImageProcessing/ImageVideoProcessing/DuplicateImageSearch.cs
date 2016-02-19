using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ImageVideoProcessing
{
    public class DuplicateImageSearch
    {
        #region Global Declaration
        string[] imageFilters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
        #endregion

        /// <summary>
        /// Reason : To get all similar files from folder with percentage of similarity for selected file.
        /// compare files from folder which are having length of file +- 100000 of original file.
        /// </summary>
        /// <param name="filePath">Input file path of image</param>
        /// <param name="length">Length of image file that varies to compare with another file</param>
        /// <param name="folderPath">Folder location where to search duplocate files</param>
        /// <param name="message">return message in reference variable for no of matches</param>
        /// <param name="fileNames">returns names of files which are matched in target flder seperated by comma(,)</param>
        /// <param name="percentageString"> returns percentage of similarities of matched images in string seperated by comma(,)</param>
        public void GetAllSimilarImages(string filePath,double length, string folderPath, ref string message, ref string fileNames, ref string percentageString)
        {
            {
                try
                {
                    fileNames += filePath + ",";
                    percentageString += "Original file,";

                    Boolean flag = false;
                    int count = 0; int percentage = 0;
                    message = "Following files are matches with selected image,\r\n";
                    FileInfo fileInnfo = new FileInfo(filePath);

                    DirectoryInfo directory = new DirectoryInfo(folderPath);
                    FileInfo[] files = directory.GetFiles("*.*", SearchOption.AllDirectories);

                    var fileEntries = GetFilesFrom(folderPath, imageFilters, false);

                    var query = from file in files
                                where file.Length > fileInnfo.Length - length && file.Length < fileInnfo.Length + length
                                select file;

                    foreach (FileInfo fileInfo in query)
                    {
                        flag = false;
                        CompareImages(filePath, fileInfo.FullName, ref flag, ref percentage);
                        if (flag)
                        {
                            count++;
                            message += "File " + count + ": " + fileInfo.FullName + ", Percentage : " + percentage + "%\r\n\r\n";
                            fileNames += fileInfo.FullName + ",";
                            percentageString +="Image matching percentage is " +percentage + "%,";
                        }
                    }
                    if (count > 0)
                        message = "Selected Image Match with " + count + " Images in target folder\r\n\r\n" + message;                    
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
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.AllDirectories;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
    }
}