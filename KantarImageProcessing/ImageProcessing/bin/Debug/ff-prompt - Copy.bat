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
rem ECHO FileName %1
rem echo foldername %3
rem set input= %1
set imgformat= "png"
rem creating folder img in application startup path
rem convert input video(% 1) to frmaes. for 1 sec 1 frame,  -r 1 : means rates of frame per second
rem ffmpeg -i %1 -r 0.25 -s 320x220 -f image2 -q 2 img\%3_%%05d.png
rem ffmpeg -i video.avi -r 0.5 -f image2 output_%05d.jpg

rem Extract audio from input video file
rem ffmpeg -i %1 video\%3_.mp3
rem ffmpeg -i %1 video\%3_.wav

rem %2 if info file name
rem @echo output start>%2.txt
rem ffmpeg -i %1 2>>%2.txt

mkdir split; X=0; while( [ $X -lt 5 ] ); do echo $X; ffmpeg -i D:\POC Project\ImageProcessing\ImageProcessing\bin\Debug\bin\bill2.mp3  -acodec copy -t 00:01:00 -ss 0$X:00:00 split/${X}a.mp3; ffmpeg -i D:\POC Project\ImageProcessing\ImageProcessing\bin\Debug\bin\bill2.mp3 -acodec copy -t 00:01:00 -ss 0$X:01:00 split/${X}b.mp3;  X=$((X+1)); done;
 
ECHO.
rem exit

CMD /Q /K 
GOTO:EOF
rem exit

:error
ECHO.
ECHO Press any key to exit.
PAUSE >
GOTO:EOF
exit