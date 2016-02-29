using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
