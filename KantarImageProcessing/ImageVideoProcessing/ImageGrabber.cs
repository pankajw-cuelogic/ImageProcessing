﻿using System;
using System.Collections.Generic;
using System.Linq;
using MODI;
using System.Data;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace ImageVideoProcessing
{
    public class ImageGrabber
    {
        #region Global Declaration
        int pixelIncrementVal = 2;

        #endregion

        #region Image Processing

        /// <summary>
        /// Reason : To get Text from image
        /// </summary>
        /// <param name="fileName">Input file name</param>
        /// <returns>Return Text from image</returns>
        public string ExtractTextFromImage(string fileName)
        {
            try
            {
                if (fileName.Trim() == "")
                    return "File name can not be null";

                string ExtractedTest = "";
                Document doc = new Document();

                GC.WaitForPendingFinalizers();
                doc.Create(fileName);
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
        /// <param name="fileName">Input filr name</param>
        /// <param name="colorNames">Returns colors and their density in image</param>
        /// <returns>returns color model</returns>
        public List<ColorModel> GetImageColors(string fileName, ref string colorNames)
        {
            Bitmap bmp = new Bitmap(fileName);
            HashSet<string> colors = new HashSet<string>();
            List<string> colorList = new List<string>();
            int Red = 0, Blue = 0, Green = 0, Yellow = 0, Pink = 0, SkyBlue = 0, Orange = 0, Purple = 0, White = 0, Black = 0, Grey=0,Brown=0;
            for (int x = 0; x < bmp.Size.Width; x +=pixelIncrementVal)
            {
                for (int y = 0; y < bmp.Size.Height; y += pixelIncrementVal)
                {
                    try
                    {
                        //var v = from t in colors where t == (bmp.GetPixel(x, y)).Name select t;
                        //if (!(v.Count() > 0))
                        {
                            colors.Add(bmp.GetPixel(x, y).Name.ToString());
                            string color = GetColorFromRGB(bmp.GetPixel(x, y).R, bmp.GetPixel(x, y).G, bmp.GetPixel(x, y).B);
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
                                case "Grey":
                                    Grey += 1;
                                    break;
                                case "Brown":
                                    Brown += 1;
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
            return GetUniqueColorList(colorList, Red, Blue, Green, Yellow, Pink, SkyBlue, Orange, Purple, White, Black,Grey,Brown,ref colorNames);
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
        private List<ColorModel> GetUniqueColorList(List<string> colorList, int Red, int Blue, int Green, int Yellow, int Pink, int SkyBlue, int Orange, int Purple, int White, int Black,int Grey,int Brown, ref string colorNames)
        {
            try
            {
                if (!(colorList.Count() > 0))
                    return null;

                colorNames = "";
                var colorKeyValuelist = new List<KeyValuePair<string, int>>();
                List<ColorModel> cmList = new List<ColorModel>();

                foreach (var x in colorList)
                {
                    switch (x.Replace(" ", ""))
                    {
                        case "Red":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Red", Red));
                            break;
                        case "Blue":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Blue", Blue));
                            break;
                        case "Green":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Green", Green));
                            break;
                        case "Yellow":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Yellow", Yellow));
                            break;
                        case "Pink":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Pink", Pink));
                            break;
                        case "SkyBlue":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Sky Blue", SkyBlue));
                            break;
                        case "Orange":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Orange", Orange));
                            break;
                        case "Purple":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Purple", Purple));
                            break;
                        case "White":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("White", White));
                            break;
                        case "Black":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Black", Black));
                            break;
                        case "Grey":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Grey", Grey));
                            break;
                        case "Brown":
                            colorKeyValuelist.Add(new KeyValuePair<string, int>("Brown", Brown));
                            break;
                        default:
                            break;
                    }
                }

                var totalColors = (from t in colorKeyValuelist select t.Value).Sum();
                foreach (var colorObj in colorKeyValuelist)
                {
                    ColorModel cmObj = new ColorModel();
                    var colorValue = (from t in colorKeyValuelist where t.Key == colorObj.Key select t.Value).FirstOrDefault();
                    var percentage = Math.Round((Convert.ToDecimal(colorValue) / Convert.ToDecimal(totalColors)) * 100);
                    if (percentage > 0)
                        colorNames +="\r\n"+ colorObj.Key + " " + percentage + "%, ";

                    switch (colorObj.Key)
                    {
                        case "Red":                            
                            cmObj.color = "Red";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Blue":
                            cmObj.color = "Blue";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Green":
                            cmObj.color = "Green";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Yellow":
                            cmObj.color = "Yellow";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Pink":
                            cmObj.color = "Pink";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "SkyBlue":
                            cmObj.color = "Sky Blue";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Orange":
                            cmObj.color = "Orange";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Purple":
                            cmObj.color = "Purple";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "White":
                            cmObj.color = "White";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Black":
                            cmObj.color = "Black";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;

                        case "Grey":
                            cmObj.color = "Grey";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        case "Brown":
                            cmObj.color = "Brown";
                            cmObj.pecentage = Convert.ToInt32(percentage);
                            cmList.Add(cmObj);
                            break;
                        default:
                            break;
                    }
                }

                colorNames.Remove(colorNames.Length - 2, 2);
                return cmList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reason : To get applox Color of pixel based on its category
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        private String GetColorFromRGB(int R, int G, int B)
        {
            try
            {
               
                if ((R < 50 && G < 50 && B > 150)|| (B > 180 && G < 90 && R < 90) || (B > G & B > R && (B - R) > 25 && (B - G) > 25))
                {
                    return "Blue";
                }
                if ((R < 50 && G > 150 && B < 50) || (G > 180 && B < 150 && R < 150) || (G > 50 && (G > B) && (G - R) > 25 && B < 200 && B < G))
                {
                    return "Green";
                }
                if (G > 180 && R > 180 && B < 90)
                {
                    return "Yellow";
                }
                if ( (R > 150 && G < 50 && B > 150)|| (R > 180 && B > 180 && G < 90))
                {
                    return "Pink";
                }
                if (G > 180 && B > 180 && R < 90)
                {
                    return "Sky Blue";
                }
                if (R > 120 && (G > 70 && G < 200) && B < 70 && (R - G) > 50)
                {
                    return "Orange";
                }
                if ( (R > 80 && R< 120 && G > 0 && G< 110 && B > 180)|| (B > 120 && (R > 80 && R < 170) && G < 100))
                {
                    return "Purple";
                }
                if (B > 225 && R > 225 && G > 225)
                {
                    return "White";
                }
                if ((B < 55 && R < 55 && G < 55) || (B < 125 && R < 125 && G < 125 && (R > B ? R - B < 10 : B - R < 10) && (R > G ? R - G < 10 : G - R < 10) && (B > G ? B - G < 10 : G - B < 10)))
                {
                    return "Black";
                }
                if ( (R > 70 && R< 120 && G < 50 && B < 50)|| (R>120&&R<210 && G > 60 && G < 150 && B > 40 && B < 120 && (R-G)>50 && (R - G) < 75) && (G-B) < 55 )
                {
                    return "Brown";
                }
                if (R > 40 && R < 240 && G > 40 && G < 240 && B > 40 && B < 240)
                {
                    return "Grey";
                }
                if ((R > 150 && G < 50 && B < 50) || (R > 180 && G < 90 && B < 90) || (R > G & R > B && (R - B) > 35 && (R - G) > 35))
                {
                    return "Red";
                }
                return "";

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion       
                
    }    
}
