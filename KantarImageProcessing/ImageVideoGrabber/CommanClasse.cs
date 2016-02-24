using ImageVideoProcessing;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    /// <summary>
    /// ImageFileInput : To accept Image input file name
    /// </summary>
    public class ImageFile
    {
        public string FileName { get; set; }
    }

    /// <summary>
    /// ImageContent : To return content of image
    /// </summary>
    public class ImageContent
    {
        public string Content { get; set; }
    }

    /// <summary>
    /// VideoFileInput : Model  to take video file input
    /// </summary>
    public class VideoFile
    {
        public string ApplicationStartupPath { get; set; }
        public string OutputImagePath { get; set; }
        public string InputFilePath { get; set; }
        public string BatchFilePath { get; set; }
        public string FrameName { get; set; }
    }

    /// <summary>
    /// VideoContent : Model to return content of of video
    /// </summary>
    public class VideoContent
    {
        public string ContentMessage { get; set; }
        public string VideoInfo { get; set; }
        public string AudioMessage { get; set; }
        public List<Colors> ColorList { get; set; }
    }

    /// <summary>
    /// DuplicateSearchImagePath : Model to take Image path to search duplicate images in specified folder
    /// </summary>
    public class DuplicateImagePath
    {
        public string FilePath { get; set; }
        public double FileLength { get; set; }
        public string FolderPath { get; set; }
        public string Message { get; set; }
    }
     
    /// <summary>
    /// AudioInput : Model to take details of Audio input file
    /// </summary>
    public class AudioInput
    {
        public string ApplicationStartupPath { get; set; }
        public string AudioFolderPath { get; set; }
        public string FrameName { get; set; }
        public int AudioDuration { get; set; }
    }

    /// <summary>
    /// AudioContent : Model to retunt content(text) of audio file
    /// </summary>
    public class AudioTextContent
    {
        public string AudioText { get; set; }
    }

    /// <summary>
    /// Returns color list
    /// </summary>
    public class Colors
    {
        public string Color { get; set; }
        public int Pecentage { get; set; }
    }

    public class DuplicateImages
    {
        public string FileName { get; set; }
        public string Percentage { get; set; }
    }
}
