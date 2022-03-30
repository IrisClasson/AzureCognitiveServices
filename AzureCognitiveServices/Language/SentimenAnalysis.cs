using Azure;
using Azure.AI.TextAnalytics;

public class SentimenAnalysis {
    public void Analyze(string endpoint, string key) {
        var documents = ExampleText.BookReviews;

        var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

        var options = new AnalyzeSentimentOptions() { IncludeOpinionMining = true };
        Response<AnalyzeSentimentResultCollection> response = client.AnalyzeSentimentBatch(documents, options: options);
        AnalyzeSentimentResultCollection reviews = response.Value;

        Dictionary<string, int> positiveReviews = GetPositiveReviews(reviews);

        Console.WriteLine("---Positive mentions:");
        foreach (KeyValuePair<string, int> complaint in positiveReviews.OrderByDescending(x => x.Value)) {
            Console.WriteLine($"   {complaint.Key}, {complaint.Value}");
        }
    }

    private Dictionary<string, int> GetPositiveReviews(AnalyzeSentimentResultCollection reviews) {
        var complaints = new Dictionary<string, int>();
        foreach (AnalyzeSentimentResult review in reviews) {
            foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences) {
                foreach (SentenceOpinion opinion in sentence.Opinions) {
                    if (opinion.Target.Sentiment == TextSentiment.Positive) {
                        complaints.TryGetValue(opinion.Target.Text, out var value);
                        complaints[opinion.Target.Text] = value + 1;
                    }
                }
            }
        }
        return complaints;
    }
}