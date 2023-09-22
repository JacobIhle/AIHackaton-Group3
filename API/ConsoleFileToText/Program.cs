using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;

internal class Program
{
    static string speechKey = Environment.GetEnvironmentVariable("SpeechKey");
    static string speechRegion = Environment.GetEnvironmentVariable("SpeechRegion");

    private static async Task Main(string[] args)
    {
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        speechConfig.SpeechRecognitionLanguage = "en-US";

        // Selects the audio input to be a file
        using var audioConfig = AudioConfig.FromWavFileInput("fruits.wav");
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        Console.WriteLine("Listening to file");
        await SpeechToTextCL.SpeechServices.RecognizeSpeech(speechRecognizer);
    }
}