using System;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Threading;

namespace ImageVideoProcessing
{
    public class AudioGrabber
    {
        
        #region SpeechRecognition for Live audio

        bool completed;
        public Thread recThread;
        public Boolean recongnizerState = true;
        
        public void converAudioToVideo( string path)

        // Initialize an in-process speech recognition engine.
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine())
            {
                // Create and load a grammar.
                GrammarBuilder build = new GrammarBuilder();
                build.AppendDictation();
                Grammar wordsList = new Grammar(build);
                recognizer.LoadGrammar(wordsList);

                // Configure the input to the recognizer.
                recognizer.SetInputToWaveFile(path);

                // Attach event handlers for the results of recognition.
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.RecognizeCompleted +=
                  new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

                // Perform recognition on the entire file.
                Console.WriteLine("Starting asynchronous recognition...");
                completed = false;
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.
                while (!completed)
                { Console.ReadLine(); }
                
                RecognitionResult result = recognizer.Recognize();
                string a = result.Text;
                
            }
        }

        // Handle the SpeechRecognized event.
        public void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            { MessageBox.Show("  Recognized text =  {0}", e.Result.Text); }
            else
            { MessageBox.Show("Recognized text not available."); }
        }

        // Handle the RecognizeCompleted event.
        public void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("  Error encountered, {0}: {1}",
                e.Error.GetType().Name, e.Error.Message);
            }
            if (e.Cancelled)
            {
                Console.WriteLine("  Operation cancelled.");
            }
            if (e.InputStreamEnded)
            {
                Console.WriteLine("  End of stream encountered.");
            }
            completed = true;
        }

        #endregion

        #region Speech Recognition for wav file

        public void speechToText(string path,int noOfAudioFiles, ref string audioMessage)
        {
           //converAudioToVideo(path);
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