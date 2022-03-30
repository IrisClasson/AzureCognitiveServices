using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

public class TranslationService {
    public async Task Translate(string key, string region) {
        var translationConfig = SpeechTranslationConfig.FromSubscription(key, region);

        translationConfig.SpeechRecognitionLanguage = "en-US";
        translationConfig.AddTargetLanguage("es-PE");

        var speechConfig = SpeechConfig.FromSubscription(key,region);
        speechConfig.SpeechSynthesisVoiceName = "es-PE-CamilaNeural";
        speechConfig.SpeechRecognitionLanguage = "en-US";

        using var audioConfigIn = AudioConfig.FromWavFileInput(@"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Speech\chapter7.wav");
        using var recognizer = new TranslationRecognizer(translationConfig,audioConfigIn);

        // For longer: recognizer.StartContinuousRecognitionAsync();

        var result = await recognizer.RecognizeOnceAsync();
        if (result.Reason == ResultReason.TranslatedSpeech) {

            Console.WriteLine($"Recognized: \"{result.Text}\"");

            var (language, translation) = result.Translations.FirstOrDefault();

            Console.WriteLine(translation);

                using var audioConfigOut = AudioConfig.FromWavFileOutput(@$"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Speech\{language}-translation.wav");
                using var synthesizer = new SpeechSynthesizer(speechConfig, audioConfigOut);

                await synthesizer.SpeakTextAsync(translation);
        }
    }
}
