using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables("AZURE")
    .Build();

#region SPEECH

var speechKey = config[":SPEECH:SUBSCRIPTION:KEY"];
var speechRegion = config[":SPEECH:SUBSCRIPTION:REGION"];

//new SpeechToText().Recognize(speechKey, speechRegion).Wait();
//new TextToSpeech().SynthesizeAudioAsync(speechKey, speechRegion).Wait();
//new TranslationService().Translate(speechKey, speechRegion).Wait();

//// No demo: custom keyword, custom commands
#endregion


#region LANGUAGE

var languageKey = config[":LANGUAGE:SUBSCRIPTION:KEY"];
var languageEndpoint = config[":LANGUAGE:SUBSCRIPTION:ENDPOINT"];

//new EntityRecognition().ExtractEnities(languageEndpoint, languageKey);
//new EntityRecognition().TextSummarization(languageEndpoint, languageKey).Wait();
//new SentimenAnalysis().Analyze(languageEndpoint, languageKey); // Not finsihed!

//// No demo: QuestionAnswering, ConversationalLanguageUnderstanding,Translator
#endregion

#region VISION

var visionKey = config[":VISION:SUBSCRIPTION:KEY"];
var visionEndpoint = config[":VISION:SUBSCRIPTION:ENDPOINT"];

//new ComputerVisionExample().ReadFromLocalImage(visionEndpoint, visionKey).Wait();
//new FaceApi().DetectFaceExtract(visionEndpoint, visionKey).Wait();

//// No demo: Custom Vision
#endregion

#region DECISION

var decisionKey = config[":DECISION:SUBSCRIPTION:KEY"];
var decisionEndpoint = config[":DECISION:SUBSCRIPTION:ENDPOINT"];

var moderatorKey = config[":MODERATOR:SUBSCRIPTION:KEY"];
var moderatorEndpoint = config[":MODERATOR:SUBSCRIPTION:ENDPOINT"];

//new ContentModerator().ModerateText(moderatorEndpoint, moderatorKey);
//new AnomalityDetector().Detect(decisionEndpoint, decisionKey).Wait();

//// No demo: Personalizer
#endregion
