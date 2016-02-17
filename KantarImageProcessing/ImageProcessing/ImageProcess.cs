using System;
using System.Collections.Generic;
using System.Linq;
using MODI;
using System.Data;
using System.Drawing;
using System.Threading;
using AForge.Video.FFMPEG;
using DexterLib;

namespace ImageProcessing
{
       #region Image Processing

    public class ImageProcess
    {
        #region Global Declaration

        #endregion

        #region Image Processing
        /// <summary>
        /// Reason : To get Text from image
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        
        public string ExtractTextFromImage(string fileName)
        {
            try
            {
                GC.WaitForPendingFinalizers();
                
                string ExtractedTest = "";
                string filePath = fileName.Trim() != "" ? @fileName : @"C:\Users\Cuelogic\Desktop\sample images\Untitled.png";
                Document doc = new Document();
                doc.Create(filePath);
                Thread.Sleep(500);
                try
                {
                    Thread.BeginCriticalRegion();
                    doc.OCR(MiLANGUAGES.miLANG_ENGLISH, true, true);
                }
                catch (Exception) { return ""; }
                
                MODI.Image modiImage = (doc.Images[0] as MODI.Image);
                ExtractedTest = modiImage.Layout.Text;
                doc.Close();
                Thread.EndCriticalRegion();
                return ExtractedTest;
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Reason : To get color codes from image
        /// Image devided into #x# parts to pickup pixel colors
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
      
        public string GetImageColors(string fileName)
        {            
            Bitmap bmp = new Bitmap(fileName);

            HashSet<string> colors = new HashSet<string>();
            List<string> colorList = new List<string>();
            int Red = 0, Blue = 0, Green = 0, Yellow = 0, Pink = 0, SkyBlue = 0, Orange = 0, Purple = 0, White = 0, Black = 0;
            for (int x = 0; x < bmp.Size.Width; x += 8)
            {
                //int yIndex = thread == 1 ? 0 : bmp.Size.Height / 2;
                //int heightLimit = thread == 1 ? bmp.Size.Height / 2 : bmp.Size.Height;

                for (int y = 0; y < bmp.Size.Height; y += 8)
                {
                    try
                    {
                        var v = from t in colors where t == (bmp.GetPixel(x, y)).Name select t;
                        if (!(v.Count() > 0))
                        {
                            colors.Add(bmp.GetPixel(x, y).Name.ToString());
                            string color = GetColor(bmp.GetPixel(x, y).R, bmp.GetPixel(x, y).G, bmp.GetPixel(x, y).B);
                            var colorNameList = from t in colorList where t == color select t;
                            if (!(colorNameList.Count() > 0) && color != "")
                                colorList.Add(color);

                            switch (color.Replace(" ", ""))
                            {
                                case "Red":
                                    Red += 1;
                                    break;
                                case "Blue":
                                    Blue += 1;
                                    break;
                                case "Green":
                                    Green += 1;
                                    break;
                                case "Yellow":
                                    Yellow += 1;
                                    break;
                                case "Pink":
                                    Pink += 1;
                                    break;
                                case "SkyBlue":
                                    SkyBlue += 1;
                                    break;
                                case "Orange":
                                    Orange += 1;
                                    break;
                                case "Purple":
                                    Purple += 1;
                                    break;
                                case "White":
                                    White += 1;
                                    break;
                                case "Black":
                                    Black += 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return GetUniqueList(colorList, Red, Blue, Green, Yellow, Pink, SkyBlue, Orange, Purple, White, Black);

        }

        /// <summary>
        /// Reason : To get applox Color of pixel based on its category
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public String GetColor(int R, int G, int B)
        {
            try
            {               
                if ((R > 180 && G < 90 && B < 90 ) || (R > G & R > B && (R - B) > 15 && (R - G) > 15))
                {
                    return "Red";
                }
                if ((B > 180 && G < 90 && R < 90)||(B>G &B>R && (B-R)>15 && (B - G) > 15))
                {
                    return "Blue";
                }
                if ((G > 180 && B < 150 && R < 150) || (G > 50 && (G>B) && (G-R) >15 &&B<200 &&B<G))
                {
                    return "Green";
                }
                if (G > 180 && R > 180 && B < 90)
                {
                    return "Yellow";
                }
                if (R > 180 && B > 180 && G < 90)
                {
                    return "Pink";
                }
                if (G > 180 && B > 180 && R < 90)
                {
                    return "Sky Blue";
                }
                if (R > 120 && (G>70 && G<200) && B < 70 && (R-G)>50)
                {
                    return "Orange";
                }
                if (B > 120 && (R > 80 && R < 170) && G < 100)
                {
                    return "Purple";
                }
                if (B > 225 && R > 225 && G > 225)
                {
                    return "White";
                }
                if ((B < 55 && R < 55 && G < 55)||(B < 125 && R < 125 && G < 125 && (R>B ?R-B<10: B-R<10) && (R > G ? R - G < 10 : G - R < 10) && (B > G ? B - G < 10 : G - B < 10)))
                {
                    return "Black";
                }

                return "";

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reason : To get distinct colors from list with their dencity
        /// </summary>
        /// <param name="colorList"></param>
        /// <param name="Red"></param>
        /// <param name="Blue"></param>
        /// <param name="Green"></param>
        /// <param name="Yellow"></param>
        /// <param name="Pink"></param>
        /// <param name="SkyBlue"></param>
        /// <param name="Orange"></param>
        /// <param name="Purple"></param>
        /// <param name="White"></param>
        /// <param name="Black"></param>
        /// <returns></returns>
        public string GetUniqueList(List<string> colorList,int Red, int Blue, int Green, int Yellow, int Pink, int SkyBlue, int Orange, int Purple, int White, int Black)
        {
            try
            {
                if (!(colorList.Count() > 0))
                    return "";

                string colorNames = "Following colors are used in image, ";

                var list = new List<KeyValuePair<string, int>>();

                foreach (var x in colorList)
                {
                    switch (x.Replace(" ", ""))
                    {
                        case "Red":
                            list.Add(new KeyValuePair<string, int>("Red", Red));
                            break;
                        case "Blue":
                            list.Add(new KeyValuePair<string, int>("Blue", Blue));
                            break;
                        case "Green":
                            list.Add(new KeyValuePair<string, int>("Green", Green));
                            break;
                        case "Yellow":
                            list.Add(new KeyValuePair<string, int>("Yellow", Yellow));
                            break;
                        case "Pink":
                            list.Add(new KeyValuePair<string, int>("Pink", Pink));
                            break;
                        case "SkyBlue":
                            list.Add(new KeyValuePair<string, int>("SkyBlue", SkyBlue));
                            break;
                        case "Orange":
                            list.Add(new KeyValuePair<string, int>("Orange", Orange));
                            break;
                        case "Purple":
                            list.Add(new KeyValuePair<string, int>("Purple", Purple));
                            break;
                        case "White":
                            list.Add(new KeyValuePair<string, int>("White", White));
                            break;
                        case "Black":
                            list.Add(new KeyValuePair<string, int>("Black", Black));
                            break;
                        default:
                            break;
                    } 
                }

                var totalColors = (from t in list select t.Value).Sum();
                foreach (var colorObj in list)
                {
                    var colorValue = (from t in list where t.Key == colorObj.Key select t.Value).FirstOrDefault();
                    var percentage = Math.Round((Convert.ToDecimal( colorValue)/ Convert.ToDecimal(totalColors)) * 100);
                    if (percentage > 0)
                        colorNames += colorObj.Key + " " + percentage + "%, ";
                }

                return colorNames.Remove(colorNames.Length - 2, 2);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion       

        #region Video processing

        /// <summary>
        /// Reason : To read video and read frames from it
        /// </summary>
        public void VideoProcessing()
        {
            VideoFileReader reader = new VideoFileReader();
            string path = @"C:\Users\Cuelogic\Desktop\sample images\videos\drop.avi";
            reader.Open(path);
                       
            for (int i = 0; i < 100; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                videoFrame.Dispose();
            }
            reader.Close();

        }

        /*
        public void imageProcess()
        {
            // Load the multimedia file
            using (FFmpegMediaInfo info = new FFmpegMediaInfo(@"C:\Videos\Test.mp4"))
            {
                // Get the duration
                TimeSpan d = info.Duration;
                string duration = String.Format("{0}:{1:00}:{2:00}", d.Hours, d.Minutes, d.Seconds);

                // Get the video resolution
                Size s = info.VideoResolution;
                string resolution = String.Format("{0}x{1}", s.Width, s.Height);

                // Get the first video stream information
                FFmpegStreamInfo vs = info.Streams
                    .FirstOrDefault(v => v.StreamType == FFmpegStreamType.Video);

                // If the video stream  exists, extract two random frames
                List<Bitmap> imgs = new List<Bitmap>();
                if (vs != null)
                {
                    // Prepare random timestamps
                    Random rnd = new Random();
                    long dTicks = info.Duration.Ticks;
                    TimeSpan t1 = new TimeSpan(Convert.ToInt64(dTicks * rnd.NextDouble()));
                    TimeSpan t2 = new TimeSpan(Convert.ToInt64(dTicks * rnd.NextDouble()));

                    // Extract images
                    imgs = info.GetFrames(
                        info.Streams.IndexOf(vs), // stream index
                        new List<TimeSpan>() { t1, t2 }, // time positions of the frames
                        true, // force exact time positions; not previous keyframes only
                        (index, count) =>
                        {
                // Get the progress percentage
                double percent = Convert.ToDouble(index) / Convert.ToDouble(count) * 100.0;

                            return false; // Not canceling the extraction
            }
                    );
                }

                // Extract a standard 6x5 frame thumbnail sheet from the default video stream
                Bitmap thumb = info.GetThumbnailSheet(
                    -1, // Default video stream, will throw Exception if there is none
                    new VideoThumbSheetOptions(6, 5), // preset sheet options with 6 columns and 5 rows
                    (index, count) =>
                    {
            // Get the progress percentage
            double percent = Convert.ToDouble(index) / Convert.ToDouble(count) * 100.0;

                        return false; // Not canceling the extraction
        }
                );
            }

        }
        */
        #endregion
    }

    #endregion
}


//public string GetImageColors(string fileName)
//{
//    Bitmap bmp = new Bitmap(fileName);

//    ThreadStart ts = delegate
//    {
//        getImageColors(fileName, 1);
//    };

//    ThreadStart ts2 = delegate
//    {
//        getImageColors(fileName, 2);
//    };
//    new Thread(ts).Start();
//    new Thread(ts2).Start();

//    //getImageColors(fileName, bmp.Height, bmp.Width);
//}

/// <summary>
/// Reason : To split out image processing in two parts
/// </summary>
/// <param name="fileName"></param>
/// <param name="thread"></param>
/// <returns></returns>