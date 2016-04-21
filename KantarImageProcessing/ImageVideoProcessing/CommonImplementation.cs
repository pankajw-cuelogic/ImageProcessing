﻿using System;

namespace ImageVideoProcessing
{
    public class CommonImplementation
    {
        /// <summary>
        /// Reason : To check input path is local or uri
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsLocalPath(string filePath)
        {
            try
            {
                return new Uri(filePath).IsFile;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
