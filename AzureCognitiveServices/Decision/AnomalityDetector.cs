using Azure;
using Azure.AI.AnomalyDetector;
using Azure.AI.AnomalyDetector.Models;

public class AnomalityDetector {

    public IEnumerable<TimeSeriesPoint> GetData() {

            List<TimeSeriesPoint> timeSeriesPoints = new List<TimeSeriesPoint>();
        try {
            foreach (var line in File.ReadAllLines(@"C:\repos\AzureCognitiveServices\AzureCognitiveServices\Decision\companyAccount2021.csv")) {
                var data = line.Split(',');
                Console.WriteLine(line);
                var point = new TimeSeriesPoint(float.Parse(data[3].Replace(",",""))) { Timestamp = DateTime.Parse(data[0]) };
                timeSeriesPoints.Add(point);
            }
        } catch (Exception e) {

            throw;
        }

        return timeSeriesPoints.DistinctBy(x => x.Timestamp).OrderBy(x => x.Timestamp);
    }

    public async Task Detect(string endpoint, string key) {

        var endpointUri = new Uri(endpoint);
        var credential = new AzureKeyCredential(key);

        var client = new AnomalyDetectorClient(endpointUri, credential);

        var data = GetData().ToList();

        DetectRequest request = new DetectRequest(data) {
            Granularity = TimeGranularity.None,
            Sensitivity = 0
        };

        Console.WriteLine("Detecting anomalies");

        try {
            EntireDetectResponse result = await client.DetectEntireSeriesAsync(request).ConfigureAwait(false);

            bool hasAnomaly = false;
            for (int i = 0; i < request.Series.Count; ++i) {
                if (result.IsAnomaly[i]) {
                    
                    Console.WriteLine($"Anomaly! Severity {result.ExpectedValues[i]} Index: {i}. {data[i].Timestamp} | {data[i].Value}");
                    hasAnomaly = true;
                }
            }
            if (!hasAnomaly) {
                Console.WriteLine("No anomalies detected in the series.");
            }
        } catch (RequestFailedException ex) {
            Console.WriteLine(String.Format("Entire detection failed: {0}", ex.Message));
            throw;
        } catch (Exception ex) {
            Console.WriteLine(String.Format("Detection error. {0}", ex.Message));
            throw;
        }
    }
}
