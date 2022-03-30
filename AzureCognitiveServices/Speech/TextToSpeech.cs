using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
public class TextToSpeech {
    public async Task SynthesizeAudioAsync(string key, string region) {
        var config = SpeechConfig.FromSubscription(key,region);

        using var audioConfig = AudioConfig.FromWavFileOutput(@"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Speech\chapter7-OUT.wav");
        using var synthesizer = new SpeechSynthesizer(config, audioConfig);
        await synthesizer.SpeakTextAsync(ExampleText.ChapterExample2);
    }
}
