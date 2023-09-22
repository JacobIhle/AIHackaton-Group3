using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;

namespace SustainaBiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        static string speechKey = Environment.GetEnvironmentVariable("SpeechKey");
        static string speechRegion = Environment.GetEnvironmentVariable("SpeechRegion");

        [HttpGet("from-file")]
        public async Task<IActionResult> GetSpeechFromFile()
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechRecognitionLanguage = "en-US";

            // Selects the audio input to be a file
            using var audioConfig = AudioConfig.FromWavFileInput("food.wav");
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            Console.WriteLine("Listening to file");
            var speech = await SpeechToTextCL.SpeechServices.RecognizeSpeech(speechRecognizer);
            return Ok(speech);
        }

        [HttpGet("live-caption")]
        public async Task<IActionResult> GetSpeechLive()
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechRecognitionLanguage = "en-US";

                // Sets the audio to to be transcribed to be from the microphone
                using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
                using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            Console.WriteLine("Listening...");
            var speech = await SpeechToTextCL.SpeechServices.RecognizeSpeech(speechRecognizer);
            return Ok(speech);
        }
    }
}
