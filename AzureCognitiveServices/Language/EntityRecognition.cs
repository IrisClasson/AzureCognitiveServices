using Azure;
using Azure.AI.TextAnalytics;

public class EntityRecognition {
    public void ExtractEnities(string endpoint, string key) {
        var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

        string document = ExampleText.BlogPost;

        try {
            Response<CategorizedEntityCollection> response = client.RecognizeEntities(document);
            CategorizedEntityCollection entitiesInDocument = response.Value;

            Console.WriteLine($"Recognized {entitiesInDocument.Count} entities:");

            foreach (CategorizedEntity entity in entitiesInDocument) {
                Console.WriteLine($"  Text: {entity.Text}"); // Offset, Lenght
                Console.WriteLine($"  Category: {entity.Category} {Environment.NewLine}");
            }
        } catch (RequestFailedException exception) {
            Console.WriteLine($"Error Code: {exception.ErrorCode}");
            Console.WriteLine($"Message: {exception.Message}");
        }
    }

    // This is new!
    public async Task TextSummarization(string endpoint, string key) {
        var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

        string document = ExampleText.BookPreface;
        var batchInput = new List<string> {document};

        TextAnalyticsActions actions = new TextAnalyticsActions() {
            ExtractSummaryActions = new List<ExtractSummaryAction>() { new ExtractSummaryAction() }
        };

        AnalyzeActionsOperation operation = await client.StartAnalyzeActionsAsync(batchInput, actions);
        await operation.WaitForCompletionAsync();

        Console.WriteLine($"AnalyzeActions has completed");
        Console.WriteLine("-----");

        await foreach (AnalyzeActionsResult documentsInPage in operation.Value) {
            IReadOnlyCollection<ExtractSummaryActionResult> summaryResults = documentsInPage.ExtractSummaryResults;

            foreach (ExtractSummaryActionResult summaryActionResults in summaryResults.Where(x => !x.HasError)) {

                foreach (ExtractSummaryResult documentResults in summaryActionResults.DocumentsResults.Where(x => !x.HasError)) {

                    Console.WriteLine($"  Extracted {documentResults.Sentences.Count} sentence(s):");

                    foreach (SummarySentence sentence in documentResults.Sentences.OrderByDescending( x => x.RankScore)) {
                        Console.WriteLine($"  Sentence (rankscore {sentence.RankScore}): {sentence.Text} {Environment.NewLine}");
                    }
                }
            }
        }
    }
}