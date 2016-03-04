using System;

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
    public class DuplicateImageDetails
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Percentage { get; set; }
    }

    public class ImageMetadata
    {
        public string Length { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImagePath { get; set; }
        public Boolean IsImageContainsFace { get; set; }
        public Boolean IsImageContainsText { get; set; }
        public int  RedPercentage { get; set; }
        public int GreenPercentage { get; set; }
        public int BluePercentage { get; set; }
        public string Checksum { get; set; }
        public string CreatedDatetime { get; set; }
        public string UpdatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string IsDeleted { get; set; }
        public int FaceCount { get; set; }
    }
}
