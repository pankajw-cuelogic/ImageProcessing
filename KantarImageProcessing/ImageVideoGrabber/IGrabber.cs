using ImageVideoProcessing;
using System.Collections.Generic;

namespace ImageVideoGrabber
{
    public interface IGrabber
    {
        ImageContent ExtractTextFromImage(ImageFile fileNameObj);
        List<Colors> GetImageColors(ImageFile imageInputObj);
        VideoContent GetVideoDetails(VideoFile fileInputObj);
        List<DuplicateImages> GetAllSimilarImages(DuplicateImagePath fileName);
        AudioTextContent ConvertAudioToText(AudioInput audioFileObj);
  }
}
