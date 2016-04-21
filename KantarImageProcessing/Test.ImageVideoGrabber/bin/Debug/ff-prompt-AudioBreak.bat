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

rem Execute Audio cutting commands to break aduio into no of chunks

ECHO FileName %1
rem %1 : Input file name
rem %2 : Audio start time
rem %3 : No of seconds from aduio file
rem %4 : Out file name

set Folder=%1

     ffmpeg -ss %2 -t %3 -i %Folder% video\%4	
rem  ffmpeg -ss %2 -t %3 -i %1 video\%4

rem ffmpeg -i %1 video\%3_.wav
rem ffmpeg -ss 0 -t 10 -i "D:\POC Project\ImageProcessing\ImageProcessing\bin\Debug\bin\video\3ed549a1-3538-493b-8768-78be7a604bd4_.wav" file1.wav

ECHO.
exit

CMD /Q /K 
GOTO:EOF
exit

:error
ECHO.
ECHO Press any key to exit.
PAUSE >
GOTO:EOF
exit