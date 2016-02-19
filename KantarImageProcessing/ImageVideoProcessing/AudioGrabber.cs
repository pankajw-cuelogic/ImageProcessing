using System;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

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
        /// <param name="frameName">Audio file name without extentions (extentions should be .wav file)</param>
        /// <param name="audioDuration">Audi duration in seconds</param>
        /// <param name="audioMessage">return audio message in reference variable</param>
        public void ConvertAudioToText(string applicationStartupPath,string audioFolderPath,string frameName, int audioDuration, ref string audioMessage)
        {
            //Break audio into #SECONDS sec parts
            int startPointOfAudio = 0;
            int noOfAudioFiles = 1;

            ProcessStartInfo psiAudioBreak = new ProcessStartInfo(applicationStartupPath + @"\ff-prompt-AudioBreak.bat");
            psiAudioBreak.RedirectStandardOutput = true;
            psiAudioBreak.WindowStyle = ProcessWindowStyle.Hidden;
            psiAudioBreak.CreateNoWindow = true;
            psiAudioBreak.UseShellExecute = false;

            for (noOfAudioFiles = 1; audioDuration > 0; noOfAudioFiles++)
            {
                psiAudioBreak.Arguments = String.Format("{0},{1},{2},{3} ", audioFolderPath + @"\" + frameName + ".wav", startPointOfAudio,
                    audioDuration > 10 ? 10 : audioDuration, frameName + noOfAudioFiles + ".wav");
                Process procAudioBreak = new Process();
                procAudioBreak.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                procAudioBreak = Process.Start(psiAudioBreak);
                procAudioBreak.WaitForExit();
                startPointOfAudio += 10;
                audioDuration -= 10;
            }
            //Get text for audio files
            new AudioGrabber().speechToText(audioFolderPath + @"\" + frameName, noOfAudioFiles, ref audioMessage);
        }

        /// <summary>
        /// Reason : To get speech to text data for given no of files
        /// </summary>
        /// <param name="path"></param>
        /// <param name="noOfAudioFiles"></param>
        /// <param name="audioMessage"></param>
        private void speechToText(string path,int noOfAudioFiles, ref string audioMessage)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                for (int i = 1; i < noOfAudioFiles; i++)
                {
                    recognizer.SetInputToWaveFile(path + i + ".wav");
                    RecognitionResult result = recognizer.Recognize();
                    audioMessage += "\r\n" + result.Text;
                }
            }
            catch (InvalidOperationException)
            {
                audioMessage = "Could not recognize input aduio.\r\n";
            }
            finally
            {
                recognizer.UnloadAllGrammars();
            }
        }

        #endregion
    }
}
public class Word
{
    public Word() { }
    public string Text { get; set; }
    public string AttachedText { get; set; }
    public bool IsShellCommand { get; set; }
}