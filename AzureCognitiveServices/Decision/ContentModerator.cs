using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Text;

public class ContentModerator {
    public void ModerateText(string endpoint, string key) {

        Console.WriteLine("-----------------------------Text moderation---------------------------------");

        string text = ExampleText.ChapterExample;

        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        MemoryStream stream = new MemoryStream(textBytes);

        using var client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint};
        var screenResult = client.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);

        Console.WriteLine($"{String.Join(',',screenResult.Terms.Select(x => x.Term))}");
    }
}
