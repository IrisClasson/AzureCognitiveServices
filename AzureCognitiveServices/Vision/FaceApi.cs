// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

public class FaceApi {
    public async Task DetectFaceExtract(string endpoint, string key) {
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var client = new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };

        using FileStream stream = new FileStream(@"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Vision\Images\loke.jpeg", FileMode.Open);
       
        var detectedFaces = await client.Face.DetectWithStreamAsync(stream, 
                    returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Accessories, FaceAttributeType.Age,
                        FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                        FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                        FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile,
                        FaceAttributeType.Smile},
                    detectionModel: DetectionModel.Detection01,
                    recognitionModel: RecognitionModel.Recognition04);


            foreach (var face in detectedFaces) {

                // Get face bounding box
                Console.WriteLine($"Rectangle(Left/Top/Width/Height) : {face.FaceRectangle.Left} {face.FaceRectangle.Top} {face.FaceRectangle.Width} {face.FaceRectangle.Height}");

                // Get face accessorie´s
                List<Accessory> accessoriesList = (List<Accessory>)face.FaceAttributes.Accessories;
                int count = face.FaceAttributes.Accessories.Count;
                string accessory; string[] accessoryArray = new string[count];
                if (count == 0) { accessory = "NoAccessories"; } else {
                    for (int i = 0; i < count; ++i) { accessoryArray[i] = accessoriesList[i].Type.ToString(); }
                    accessory = string.Join(",", accessoryArray);
                }
                Console.WriteLine($"Accessories : {accessory}");

                // Get other attributes
                Console.WriteLine($"Age : {face.FaceAttributes.Age}");
                Console.WriteLine($"Blur : {face.FaceAttributes.Blur.BlurLevel}");

                // Get emotions on the face
                string emotionType = string.Empty;
                double emotionValue = 0.0;
                Emotion emotion = face.FaceAttributes.Emotion;
                if (emotion.Anger > emotionValue) { emotionValue = emotion.Anger; emotionType = "Anger"; }
                if (emotion.Contempt > emotionValue) { emotionValue = emotion.Contempt; emotionType = "Contempt"; }
                if (emotion.Disgust > emotionValue) { emotionValue = emotion.Disgust; emotionType = "Disgust"; }
                if (emotion.Fear > emotionValue) { emotionValue = emotion.Fear; emotionType = "Fear"; }
                if (emotion.Happiness > emotionValue) { emotionValue = emotion.Happiness; emotionType = "Happiness"; }
                if (emotion.Neutral > emotionValue) { emotionValue = emotion.Neutral; emotionType = "Neutral"; }
                if (emotion.Sadness > emotionValue) { emotionValue = emotion.Sadness; emotionType = "Sadness"; }
                if (emotion.Surprise > emotionValue) { emotionType = "Surprise"; }
                Console.WriteLine($"Emotion : {emotionType.ToString()}");

                // Get more face attributes
                Console.WriteLine($"Exposure : {face.FaceAttributes.Exposure.ExposureLevel}");
                Console.WriteLine($"FacialHair : {string.Format("{0}", face.FaceAttributes.FacialHair.Moustache + face.FaceAttributes.FacialHair.Beard + face.FaceAttributes.FacialHair.Sideburns > 0 ? "Yes" : "No")}");
                Console.WriteLine($"Glasses : {face.FaceAttributes.Glasses}");

                // Get hair color
                Hair hair = face.FaceAttributes.Hair;
                string color = null;
                if (hair.HairColor.Count == 0) { if (hair.Invisible) { color = "Invisible"; } else { color = "Bald"; } }
                HairColorType returnColor = HairColorType.Unknown;
                double maxConfidence = 0.0f;
                foreach (HairColor hairColor in hair.HairColor) {
                    if (hairColor.Confidence <= maxConfidence) { continue; }
                    maxConfidence = hairColor.Confidence; returnColor = hairColor.Color; color = returnColor.ToString();
                }
                Console.WriteLine($"Hair : {color}");

                // Get more attributes
                Console.WriteLine($"HeadPose : {string.Format("Pitch: {0}, Roll: {1}, Yaw: {2}", Math.Round(face.FaceAttributes.HeadPose.Pitch, 2), Math.Round(face.FaceAttributes.HeadPose.Roll, 2), Math.Round(face.FaceAttributes.HeadPose.Yaw, 2))}");
                Console.WriteLine($"Makeup : {string.Format("{0}", (face.FaceAttributes.Makeup.EyeMakeup || face.FaceAttributes.Makeup.LipMakeup) ? "Yes" : "No")}");
                Console.WriteLine($"Noise : {face.FaceAttributes.Noise.NoiseLevel}");
                Console.WriteLine($"Occlusion : {string.Format("EyeOccluded: {0}", face.FaceAttributes.Occlusion.EyeOccluded ? "Yes" : "No")} " +
                    $" {string.Format("ForeheadOccluded: {0}", face.FaceAttributes.Occlusion.ForeheadOccluded ? "Yes" : "No")}   {string.Format("MouthOccluded: {0}", face.FaceAttributes.Occlusion.MouthOccluded ? "Yes" : "No")}");
                Console.WriteLine($"Smile : {face.FaceAttributes.Smile}");

            }
}
    // Detect faces from image url for recognition purpose. This is a helper method for other functions in this quickstart.
    // Parameter `returnFaceId` of `DetectWithUrlAsync` must be set to `true` (by default) for recognition purpose.
    // Parameter `FaceAttributes` is set to include the QualityForRecognition attribute. 
    // Recognition model must be set to recognition_03 or recognition_04 as a result.
    // Result faces with insufficient quality for recognition are filtered out. 
    // The field `faceId` in returned `DetectedFace`s will be used in Face - Find Similar, Face - Verify. and Face - Identify.
    // It will expire 24 hours after the detection call.
    // <snippet_face_detect_recognize>
    //public  async Task<List<DetectedFace>> DetectFaceRecognize(IFaceClient faceClient, string url, string recognition_model) {
    //    // Detect faces from image URL. Since only recognizing, use the recognition model 1.
    //    // We use detection model 3 because we are not retrieving attributes.
    //    IList<DetectedFace> detectedFaces = await faceClient.Face.DetectWithUrlAsync(url, recognitionModel: recognition_model, detectionModel: DetectionModel.Detection03, FaceAttributes: new List<FaceAttributeType> { FaceAttributeType.QualityForRecognition });
    //    List<DetectedFace> sufficientQualityFaces = new List<DetectedFace>();
    //    foreach (DetectedFace detectedFace in detectedFaces) {
    //        var faceQualityForRecognition = detectedFace.FaceAttributes.QualityForRecognition;
    //        if (faceQualityForRecognition.HasValue && (faceQualityForRecognition.Value >= QualityForRecognition.Medium)) {
    //            sufficientQualityFaces.Add(detectedFace);
    //        }
    //    }
    //    Console.WriteLine($"{detectedFaces.Count} face(s) with {sufficientQualityFaces.Count} having sufficient quality for recognition detected from image `{Path.GetFileName(url)}`");

    //    return sufficientQualityFaces.ToList();
    //}

    //public static async Task Verify(IFaceClient client, string url, string recognitionModel03) {
    //    Console.WriteLine("========VERIFY========");
    //    Console.WriteLine();

    //    List<string> targetImageFileNames = new List<string> { "Family1-Dad1.jpg", "Family1-Dad2.jpg" };
    //    string sourceImageFileName1 = "Family1-Dad3.jpg";
    //    string sourceImageFileName2 = "Family1-Son1.jpg";


    //    List<Guid> targetFaceIds = new List<Guid>();
    //    foreach (var imageFileName in targetImageFileNames) {
    //        // Detect faces from target image url.
    //        List<DetectedFace> detectedFaces = await DetectFaceRecognize(client, $"{url}{imageFileName} ", recognitionModel03);
    //        targetFaceIds.Add(detectedFaces[0].FaceId.Value);
    //        Console.WriteLine($"{detectedFaces.Count} faces detected from image `{imageFileName}`.");
    //    }

    //    // Detect faces from source image file 1.
    //    List<DetectedFace> detectedFaces1 = await DetectFaceRecognize(client, $"{url}{sourceImageFileName1} ", recognitionModel03);
    //    Console.WriteLine($"{detectedFaces1.Count} faces detected from image `{sourceImageFileName1}`.");
    //    Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;

    //    // Detect faces from source image file 2.
    //    List<DetectedFace> detectedFaces2 = await DetectFaceRecognize(client, $"{url}{sourceImageFileName2} ", recognitionModel03);
    //    Console.WriteLine($"{detectedFaces2.Count} faces detected from image `{sourceImageFileName2}`.");
    //    Guid sourceFaceId2 = detectedFaces2[0].FaceId.Value;

    //    // Verification example for faces of the same person.
    //    VerifyResult verifyResult1 = await client.Face.VerifyFaceToFaceAsync(sourceFaceId1, targetFaceIds[0]);
    //    Console.WriteLine(
    //        verifyResult1.IsIdentical
    //            ? $"Faces from {sourceImageFileName1} & {targetImageFileNames[0]} are of the same (Positive) person, similarity confidence: {verifyResult1.Confidence}."
    //            : $"Faces from {sourceImageFileName1} & {targetImageFileNames[0]} are of different (Negative) persons, similarity confidence: {verifyResult1.Confidence}.");

    //    // Verification example for faces of different persons.
    //    VerifyResult verifyResult2 = await client.Face.VerifyFaceToFaceAsync(sourceFaceId2, targetFaceIds[0]);
    //    Console.WriteLine(
    //        verifyResult2.IsIdentical
    //            ? $"Faces from {sourceImageFileName2} & {targetImageFileNames[0]} are of the same (Negative) person, similarity confidence: {verifyResult2.Confidence}."
    //            : $"Faces from {sourceImageFileName2} & {targetImageFileNames[0]} are of different (Positive) persons, similarity confidence: {verifyResult2.Confidence}.");

    //    Console.WriteLine();
    //}
 }
