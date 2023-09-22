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

        // Sets the audio to to be transcribed to be from the microphone
        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        Console.WriteLine("Listening...");
        await SpeechToTextCL.SpeechServices.RecognizeSpeech(speechRecognizer);
    }
}