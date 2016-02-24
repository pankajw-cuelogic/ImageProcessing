using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageVideoProcessing
{
    /// <summary>
    /// VideoDetails : Model to take input Video file details
    /// </summary>
    public class VideoDetails
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public string ColorDetails { get; set; }
    }

    /// <summary>
    /// DuplicateImageCheck : Model to take input image details to check duplicate search in specific folder
    /// </summary>
    public class DuplicateImageCheck
    {
        public string FileName { get; set; }
        public string Percentage { get; set; }
    }
}
