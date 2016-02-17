@ECHO OFF
REM FF Prompt 1.2
REM Open a command prompt to run ffmpeg/ffplay/ffprobe
REM Copyright (C) 2013-2015  Kyle Schwarz

TITLE FF Prompt

IF NOT EXIST bin\ffmpeg.exe (
  CLS
  ECHO bin\ffmpeg.exe could not be found.
  GOTO:error
)

CD bin || GOTO:error
PROMPT $P$_$G
SET PATH=%CD%;%PATH%
CLS


set input= "D:\videos\SampleVideo_1280x720_1mb.mp4"
set imgformat= "png"

echo creating folder img in application startup path
echo %0 
echo %1
ffmpeg -i D:\videos\SampleVideo_1280x720_1mb.mp4 -r 1 -s 320x220 -f image2 -q 2 img\seq_%%05d.png


ECHO.
ECHO For help run: ffmpeg -h
ECHO For formats run: ffmpeg -formats ^| more
ECHO For codecs run: ffmpeg -codecs ^| more
ECHO.
ECHO Current directory is now: "%CD%"
ECHO The bin directory has been added to PATH
 
ECHO.


CMD /Q /K 
GOTO:EOF

:error
ECHO.
ECHO Press any key to exit.
PAUSE >ffmpeg -i D:\videos\SampleVideo_1280x720_1mb.mp4  -r 2 -s 240x320 -f image2 foo-%03d.png
GOTO:EOF
