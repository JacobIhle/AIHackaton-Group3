using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;

namespace SpeechToTextCL
{
    public static class GetTextFromFile
    {
        static string speechKey = Environment.GetEnvironmentVariable("SpeechKey");
        static string speechRegion = Environment.GetEnvironmentVariable("SpeechRegion");

        public static async Task GetSpeechFromFile() 
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechRecognitionLanguage = "en-US";

            //using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var audioConfig = AudioConfig.FromWavFileInput("fruits.wav");
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            Console.WriteLine("Listening to file");
            await SpeechServices.RecognizeSpeech(speechRecognizer);

        }
    }
}
