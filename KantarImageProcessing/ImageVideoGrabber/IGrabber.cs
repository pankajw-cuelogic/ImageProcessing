using ImageVideoProcessing;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    public interface IGrabber
    {
        ImageContent ExtractTextFromImage(ImageFile fileNameObj);
        List<Colors> GetImageColors(ImageFile imageInputObj);
        VideoContent GetVideoDetails(VideoFile fileInputObj);
        List<DuplicateImages> GetAllSimilarImages(ImageFileDuplicateCheck fileName);
        AudioTextContent ConvertAudioToText(AudioFileInput audioFileObj);
        void UploadImageFile(string filePath, string appStartPath);
        byte[] DownloadFile(string downloadFileName);
  }
}
