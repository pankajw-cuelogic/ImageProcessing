using ImageVideoProcessing;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    public interface IGrabber
    {
        ImageContent ExtractTextFromImage(ImageFile fileNameObj);
        List<Colors> GetImageColors(ImageFile imageInputObj);
        VideoContent GetVideoDetails(VideoFileDetail fileInputObj);
        List<DuplicateImage> GetAllSimilarImages(ImageFileDetails fileName);
        AudioTextContent ConvertAudioToText(AudioFileDetails audioFileObj);
        void UploadImageFile(string filePath, string appStartPath);
        byte[] DownloadFile(string downloadFileName);
  }
}
