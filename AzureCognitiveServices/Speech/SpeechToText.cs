using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
public class SpeechToText {
        public async Task Recognize(string key, string region) {
        var speechConfig = SpeechConfig.FromSubscription(key,region);
        speechConfig.SpeechRecognitionLanguage = "en-US";

        //using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var audioConfig = AudioConfig.FromWavFileInput(@"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Speech\chapter7.wav");

        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        var phraseList = PhraseListGrammar.FromRecognizer(speechRecognizer);
        phraseList.Clear();
        phraseList.AddPhrase("Bror");
        phraseList.AddPhrase("Ulrik");
        phraseList.AddPhrase("Leo");

        var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();

        switch (speechRecognitionResult.Reason) {
            case ResultReason.RecognizedSpeech:
                Console.WriteLine($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                break;
            case ResultReason.NoMatch:
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");
                if (cancellation.Reason == CancellationReason.Error) {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode} | ErrorDetails={cancellation.ErrorDetails}");
                }
                break;
        }
    }
}
