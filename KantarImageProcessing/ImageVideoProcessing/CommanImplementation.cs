using System;

namespace ImageVideoProcessing
{
    public class CommanImplementation
    {
        /// <summary>
        /// Reason : To check input path is local or uri
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsLocalPath(string filePath)
        {
            return new Uri(filePath).IsFile;
        }
    }
}
