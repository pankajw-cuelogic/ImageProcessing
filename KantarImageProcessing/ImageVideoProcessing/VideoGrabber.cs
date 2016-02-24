using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ImageVideoProcessing
{
    public class VideoGrabber
    {
        #region Global Declaration
        string[] imageFilters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };

        #endregion

        #region Video processing

        /// <summary>
        /// Reason : To get Video details of its all frames, traverse each frame from video
        /// </summary>
        /// <param name="outputImagePath">This path must be application startup + \bin\img\</param>
        /// <param name="inputFilePath">This is input video file path</param>
        /// <param name="batchFilePath">This is batch file path</param>
        /// <param name="contentMessage">Returns contents from each frame by frame from video</param>
        /// <param name="colorMessage">Returns color density from video</param>
        /// <param name="videoInfo">Returns Metadata of video</param>
        /// <param name="audioMessage">Returns text from video</param>
        public void GetVideoDetails(string applicationStartupPath, string outputImagePath, string inputFilePath, string batchFilePath, string frameName, ref string contentMessage, ref List<ColorModel> colorList, ref string videoInfo, ref string audioMessage)
        {
            try
            {
                colorList = new List<ColorModel>();
                string duration = "";
                int audioDuration = 0;
                string infoFileName = "fileInfo";
                string videoPath = outputImagePath.Replace("\\img\\", "\\video\\");
                CreateDirectoryIfNotExist(outputImagePath);
                CreateDirectoryIfNotExist(videoPath);
                DeleteAllFiles(outputImagePath);

                 //Start a process to execute batch file 
                 ProcessStartInfo psi = new ProcessStartInfo(batchFilePath);
                psi.RedirectStandardOutput = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.Arguments = String.Format("{0},{1},{2} ", inputFilePath, infoFileName, frameName);
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc = Process.Start(psi);
                proc.WaitForExit();

                colorList = GetFrameDetailsOfVideo(outputImagePath, frameName, ref contentMessage);
                GetVideoPropertyInfo(applicationStartupPath,infoFileName, ref videoInfo, ref duration);
                GetDurationInSeconds(duration, ref audioDuration);

                //Break audio into #SECONDS sec parts and get speech to text
                new AudioGrabber().ConvertAudioToText(applicationStartupPath, videoPath, frameName, audioDuration, ref audioMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            GC.Collect();
        }

        /// <summary>
        /// Reason To Delete all files from give path
        /// </summary>
        /// <param name="folderPath">folder path to delete all files from folder</param>
        private void DeleteAllFiles(string folderPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                foreach (FileInfo file in di.GetFiles())
                { file.Delete(); }
                foreach (DirectoryInfo dir in di.GetDirectories())
                { dir.Delete(true); }
            }
            catch (Exception)
            {
                throw new AccessViolationException();
            }            
        }

        /// <summary>
        ///Reason : To get frame details from video 
        /// </summary>
        /// <param name="extractedImageFolderPath"></param>
        /// <param name="contentMessage"></param>
        /// <param name="colorMessage"></param>
        private List<ColorModel> GetFrameDetailsOfVideo(string extractedImageFolderPath, string frameName, ref string contentMessage )
        {
            try
            {
                List<VideoDetails> videoDetailsList = new List<VideoDetails>();
                List<ColorModel> colorList = new List<ColorModel>();
                List<ColorModel> avgVideoColorList = new List<ColorModel>();

                int i = 1;
                string blankFrames = "";
                string colorNames = "";
                
                var fileEntries = GetFilesFrom(extractedImageFolderPath, imageFilters, false, frameName);
                foreach (string fileName in fileEntries)
                {
                    VideoDetails videoDetailsObj = new VideoDetails();
                    videoDetailsObj.FileName = fileName;
                    videoDetailsObj.Content = new ImageVideoProcessing.ImageGrabber().ExtractTextFromImage(fileName);
                    colorList.AddRange(new ImageVideoProcessing.ImageGrabber().GetImageColors(fileName));

                    videoDetailsObj.ColorDetails = colorNames;
                    if (videoDetailsObj.Content.Trim() != "")
                        contentMessage += "Frame " + i + ": \r\n" + videoDetailsObj.Content + "\r\n";
                    else
                        blankFrames += "\r\n Frame " + i;

                    i += 1;
                }

                contentMessage += !string.IsNullOrEmpty(blankFrames) ?
                    "\r\n\r\nFollowing frames are not having any content:" + blankFrames : "";
                var avgColorList = colorList.GroupBy(g => g.color, r => r.pecentage).Select(g => new
                {
                    color = g.Key,
                    colorPercentage = g.Average()
                });

                foreach (var clr in avgColorList)
                {
                    ColorModel colrMod = new ColorModel();
                    colrMod.color = clr.color;
                    colrMod.pecentage = Convert.ToInt32(Math.Round(clr.colorPercentage));
                    avgVideoColorList.Add(colrMod);
                }

                return avgVideoColorList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Reason : To get metata data of video file.
        /// </summary>
        /// <param name="infoFileName"></param>
        /// <param name="VideoInfo"></param>
        /// <returns></returns>
        private string GetVideoPropertyInfo(string applicationStartupPath, string infoFileName, ref string VideoInfo, ref string Duration)
        {
            try
            {
                Boolean isResultStart = false;
                string[] lines = System.IO.File.ReadAllLines(applicationStartupPath + "\\bin\\" + infoFileName + ".txt");
                foreach (string line in lines)
                {
                    if (line.Contains("Metadata:") && isResultStart == false)
                    {
                        isResultStart = true;
                    }
                    if (!line.Contains("Metadata:") && (!line.Contains("At least one output file")) && isResultStart == true)
                    {
                        if (line.Contains("major_brand"))
                        {
                            VideoInfo += "\r\n Major Brand\t" + line.Replace("major_brand", "").Replace(" ", "").Replace(":",":  ");
                            continue;
                        }
                        if (line.Contains("minor_version"))
                        {
                            VideoInfo += !VideoInfo.Contains("Minor Version") ? "\r\n Minor Version\t" + line.Replace("minor_version", "").Replace(" ", "").Replace(":", ":  ") : "";
                            continue;
                        }
                        if (line.Contains("compatible_brands"))
                        {
                            VideoInfo += "\r\n Compatible Brands\t" + line.Replace("compatible_brands", "").Replace(" ", "").Replace(":", ":  ");
                            continue;
                        }
                        if (line.Contains("creation_time") || line.Contains("creation_time:"))
                        {
                            VideoInfo += !VideoInfo.Contains("Creation Time") ? "\r\n Creation Time\t" + line.Replace("creation_time", "").Replace(" ", "").Replace(":", ":  ") : "";
                            continue;
                        }
                        if (line.Contains("encoder"))
                        {
                            VideoInfo += !VideoInfo.Contains("Encoder") ? "\r\n Encoder\t" + line.Replace("encoder", "").Replace(" ", "").Replace(":", ":  ") : "";
                            continue;
                        }
                        if (line.Contains("Duration"))
                        {
                            Duration =  "Duration" + line.Replace("Duration", "").Replace(" ", "").Replace(":", ":  ");
                            VideoInfo += !VideoInfo.Contains("Duration") ? "\r\n Duration\t" + line.Replace("Duration", "").Replace(" ", "").Replace(":", ":  ") : "";
                            continue;
                        }
                        if (line.Contains("Stream #0:0(und)"))
                        {
                            VideoInfo += "\r\n Stream #0:0(und)\t" + line.Replace("Stream #0:0(und)", "").Replace(" ", "").Replace(":", ":  ");
                            continue;
                        }
                        if (line.Contains("Stream #0:1(eng)"))
                        {
                            VideoInfo += "\r\n Stream #0:1(eng)\t" + line.Replace("Stream #0:1(eng)", "").Replace(" ", "").Replace(":", ":  ");
                            continue;
                        }
                        if (line.Contains("title"))
                        {
                            VideoInfo += "\r\n Title\t" + line.Replace("title", "").Replace(" ", "").Replace(":", ":  ");
                            continue;
                        }
                        if (line.Contains("handler_name"))
                        {
                            VideoInfo += "\r\n Handler Name\t" + line.Replace("handler_name", "").Replace(" ", "").Replace(":", ":  ");
                            continue;
                        }
                        else
                            VideoInfo += "\r\n " + line.Replace(" ", "").Replace(":", ":  ");

                    }
                }
                return VideoInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reason : To support/select all images(extensions of images) from directory
        /// </summary>
        /// <param name="searchFolder"></param>
        /// <param name="filters"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        private static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive, string frameEntries)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format(frameEntries + "*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        /// <summary>
        /// Reason : To Calculate duration of audio file in seconds
        /// </summary>
        /// <param name="Duration"></param>
        /// <returns></returns>
        private void GetDurationInSeconds(string Duration, ref int audioDuration)
        {
            try
            {
                if (string.IsNullOrEmpty(Duration))
                    return;

                int hours =Convert.ToInt32( Duration.Split(':')[1]);
                int minutes = Convert.ToInt32(Duration.Split(':')[2]);
                int seconds =(Int32)Math.Round( Convert.ToDouble(Duration.Split(':')[3].Split(',')[0]));

                audioDuration= hours * 60 * 60 + minutes * 60 + seconds;
            }
            catch (Exception)
            {               
            }
        }

        /// <summary>
        /// Reason : To create directory if not exists
        /// </summary>
        /// <param name="FolderPath"></param>
        private void CreateDirectoryIfNotExist(string FolderPath)
        {
            if (!System.IO.Directory.Exists(FolderPath))
            {
                System.IO.Directory.CreateDirectory(FolderPath);
            }
        }
        #endregion
    }
}
