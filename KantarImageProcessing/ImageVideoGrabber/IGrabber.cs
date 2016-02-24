using ImageVideoProcessing;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    public interface IGrabber
    {
        ImageContent ExtractTextFromImage(ImageFileInput fileNameObj);
        List<Colors> GetImageColors(ImageFileInput imageInputObj);
        VideoContent GetVideoDetails(VideoFileInput fileInputObj);
        List<DuplicateImageCheck> GetAllSimilarImages(DuplicateImageSearchPath fileName);
        AudioTextContent ConvertAudioToText(AudioInput audioFileObj);
  }
}
