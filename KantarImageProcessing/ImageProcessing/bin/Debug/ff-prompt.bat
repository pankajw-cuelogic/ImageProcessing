@ECHO OFF

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

REM set variables
REM %1 means first paramameter(dynamic) which is pass from code
ECHO FileName %1
echo foldername %3
set input= %1
set imgformat= "png"
rem creating folder img in application startup path
rem convert input video(% 1) to frmaes. for 1 sec 1 frame,  -r 1 : means rates of frame per second
ffmpeg -i %1 -r 0.25 -s 320x220 -f image2 -q 2 img\%3_%%05d.png
rem ffmpeg -i video.avi -r 0.5 -f image2 output_%05d.jpg

rem Extract audio from input video file
rem ffmpeg -i %1 video\%3.mp3
ffmpeg -i %1 video\%3.wav

rem %2 if info file name
@echo output start>%2.txt
ffmpeg -i %1 2>>%2.txt

ECHO.
ECHO For help run: ffmpeg -h
ECHO For formats run: ffmpeg -formats ^| more
ECHO For codecs run: ffmpeg -codecs ^| more
ECHO.
ECHO Current directory is now: "%CD%"
ECHO The bin directory has been added to PATH
 
ECHO.
exit

CMD /Q /K 
GOTO:EOF
rem exit

:error
ECHO.
ECHO Press any key to exit.
PAUSE >
GOTO:EOF
exit