using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

public class ComputerVisionExample {
    // OCR
    public async Task ReadFromFileUrl(string endpoint, string key) {

        var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };

        var urlFile = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSKFRVFSeMVRUFw84vlO85nldAHDvp0LTyqTw&usqp=CAU";

        var textHeaders = await client.ReadAsync(urlFile);
        // Operation location-ID
        string operationLocation = textHeaders.OperationLocation;
        Thread.Sleep(2000);

        const int numberOfCharsInOperationId = 36;
        string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

        ReadOperationResult results;

        do {
            results = await client.GetReadResultAsync(Guid.Parse(operationId));
        }
        while ((results.Status == OperationStatusCodes.Running ||
            results.Status == OperationStatusCodes.NotStarted));

        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (ReadResult page in textUrlFileResults) {
            foreach (Line line in page.Lines) {
                Console.WriteLine(line.Text);
            }
        }
    }

    public async Task ReadFromLocalImage(string endpoint, string key) {

        var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };

        var localFile = @"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Vision\note.jpg";

        var textHeaders = await client.ReadInStreamAsync(File.OpenRead(localFile));
        string operationLocation = textHeaders.OperationLocation;
        Thread.Sleep(2000);

        const int numberOfCharsInOperationId = 36;
        string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

        ReadOperationResult results;

        do {
            results = await client.GetReadResultAsync(Guid.Parse(operationId));
        }
        while ((results.Status == OperationStatusCodes.Running ||
            results.Status == OperationStatusCodes.NotStarted));

        var textInImageResult = results.AnalyzeResult.ReadResults;

        foreach (ReadResult page in textInImageResult) {
            foreach (Line line in page.Lines) {
                Console.WriteLine(line.Text);
            }
        }
    }

    // Image analysis
    //https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/quickstarts-sdk/image-analysis-client-library?tabs=visual-studio&pivots=programming-language-csharp
}
