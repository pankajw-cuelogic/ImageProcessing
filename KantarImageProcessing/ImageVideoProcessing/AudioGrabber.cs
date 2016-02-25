using System;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Threading;

namespace ImageVideoProcessing
{
    public class AudioGrabber
    {        
        #region SpeechRecognition for audio
        public Thread recThread;
        public Boolean recongnizerState = true;
        #endregion

        #region Speech Recognition for wav file

        /// <summary>
        /// Reason : To get break audio into multiple parts and get speech to text
        /// </summary>
        /// <param name="applicationStartupPath">Application startup path from where application is running</param>
        /// <param name="audioFolderPath">Audio file folder path</param>
        /// <param name="frameName">Audio file name without extentions (extentions should be .wav file). FrameName should not contain extension</param>
        /// <param name="audioDuration">Audio duration in seconds</param>
        /// <param name="audioMessage">Return audio message in reference variable</param>
        public void ConvertAudioToText(string applicationStartupPath,string audioFolderPath,string frameName, int audioDuration, ref string audioMessage)
        {
            //Break audio into # SECONDS sec parts
            int startPointOfAudio = 0;
            int noOfAudioFiles = 1;

            ProcessStartInfo psiAudioBreak = new ProcessStartInfo(applicationStartupPath + @"\ff-prompt-AudioBreak.bat");
            psiAudioBreak.RedirectStandardOutput = true;
            psiAudioBreak.WindowStyle = ProcessWindowStyle.Hidden;
            psiAudioBreak.CreateNoWindow = true;
            psiAudioBreak.UseShellExecute = false;
            string audioFilePath = String.Format("\"{0}\"", audioFolderPath + @"\" + frameName + ".wav");

            for (noOfAudioFiles = 1; audioDuration > 0; noOfAudioFiles++)
            {
                psiAudioBreak.Arguments = String.Format("{0},{1},{2},{3} ", audioFilePath, startPointOfAudio,
                    audioDuration > 10 ? 10 : audioDuration, frameName + noOfAudioFiles + ".wav");
                Process procAudioBreak = new Process();
                procAudioBreak.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                procAudioBreak = Process.Start(psiAudioBreak);
                procAudioBreak.WaitForExit();
                startPointOfAudio += 10;
                audioDuration -= 10;
            }
            //Get text for audio files
            new AudioGrabber().SpeechToText(audioFolderPath + @"\" + frameName, noOfAudioFiles, ref audioMessage);
        }

        /// <summary>
        /// Reason : To get speech to text data for given no of files
        /// </summary>
        /// <param name="audioFilePath"></param>
        /// <param name="noOfAudioFiles"></param>
        /// <param name="audioMessage"></param>
        private void SpeechToText(string audioFilePath,int noOfAudioFiles, ref string audioMessage)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                for (int i = 1; i < noOfAudioFiles; i++)
                {
                    recognizer.SetInputToWaveFile(audioFilePath + i + ".wav");
                    RecognitionResult result = recognizer.Recognize();
                    audioMessage += "\r\n" + result.Text;
                }
            }
            catch (InvalidOperationException)
            {
                audioMessage = "Could not recognize input audio.\r\n";
            }
            finally
            {
                recognizer.UnloadAllGrammars();
            }
        }

        #endregion
    }
}