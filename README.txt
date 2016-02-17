# ImageProcessing
This project contains Image, Audio, Video Processing, Face detection, duplicate video detection in folder  

*Image processing 
- Install Microsoft Office Document Imaging (MODI) library from following link
	ref: http://www.aspsnippets.com/Articles/Read-Extract-Text-from-Image-OCR-in-ASPNet-using-C-and-VBNet.aspx
- Follow all steps from link
- After installation add reference Microsoft Office Documentation Imaging 12.0 Type Library from com component.

*Video Processing
- Download FFMPEG library for windows from following link,
- http://www.wikihow.com/Install-FFmpeg-on-Windows
- After download keep downloaded folder in startup path(Debug/Release) folder, from where application is running.
- Add following folders inside application run folder,
	- bin\Debug\bin\img
	- bin\Debug\bin\video
- Add Following batch files in Application startup folder,
	- ff-prompt.bat(this bat file is used to get all video information and split frames from video)
	- ff-prompt-AudioBreak.bat(this file is used to break audio files into chunks)
- Add following files in folder, - %bin\Debug\bin\
	- ffmpeg.exe
	- ffplay.exe
	- ffprobe.exe

* Voice Processing
- Add following reference from Microsoft framework,
  	- System.Speech.dll to use in build class
- If .net framework is not available then download it from following link and install it,
 	- https://www.microsoft.com/en-us/download/confirmation.aspx?id=17851

* Fave Detection
- Add following libraries in executable folder(From where application is running)
- opencv_calib3d220.dll
- opencv_contrib220.dll
- opencv_core220.dll
- opencv_features2d220.dll
- opencv_ffmpeg220.dll
- opencv_flann220.dll
- opencv_gpu220.dll
- opencv_highgui220.dll
- opencv_imgproc220.dll
- opencv_legacy220.dll
- opencv_ml220.dll
- opencv_objdetect220.dll
- opencv_video220.dll
- cxcore110.dll
- cvextern.dll
- cvaux110.dll
- cv110.dll
-- haarcascade_frontalface_default.xml
- Emgu.Util.dll
- Emgu.CV.UI.dll
- Emgu.CV.ML.dll
- Emgu.CV.GPU.dll
- Emgu.CV.dll

Add references of following dll in project:
-Emgu.Util.dll
-Emgu.CV.UI.dll
-Emgu.CV.dll

- Following are some configuration setting to scan face in image,
- Detection face size is set to (width of image/32, height of image/32), To search smallest faces in image.
- Scale factor set to : 1.79
- No of minimum neighbor set to : 4

* fileinfo.txt
- This file is written when video is uploaded and processed, this file contains metadata of video file.
- File location,
		- bin\Debug\bin\
