using ImageVideoProcessing;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    public interface IGrabber
    {
        string ExtractTextFromImage(ImageFileInput fileNameObj);
        List<ColorModel> GetImageColors(ImageFileInput imageInputObj);
        VideoContent GetVideoDetails(VideoFileInput fileInputObj);
        List<DuplicateImageCheck> GetAllSimilarImages(DuplicateImageSearchPath fileName);
        AudioTextContent ConvertAudioToText(AudioInput audioFileObj);
  }
}
