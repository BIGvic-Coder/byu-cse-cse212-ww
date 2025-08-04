using System.Text.Json;
using System.Diagnostics;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> wordSet = new HashSet<string>(words);
        HashSet<string> seen = new HashSet<string>();
        List<string> result = new List<string>();

        foreach (string word in words)
        {
            if (word[0] == word[1]) continue;

            string reversed = new string(new char[] { word[1], word[0] });

            if (wordSet.Contains(reversed) && !seen.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                seen.Add(word);
                seen.Add(reversed);
            }
        }

        return result.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();
                if (degree != "")
                {
                    if (!degrees.ContainsKey(degree))
                    {
                        degrees[degree] = 1;
                    }
                    else
                    {
                        degrees[degree]++;
                    }
                }
            }
        }

        return degrees;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        string clean1 = new string(word1.ToLower().Where(char.IsLetter).ToArray());
        string clean2 = new string(word2.ToLower().Where(char.IsLetter).ToArray());

        if (clean1.Length != clean2.Length)
            return false;

        var dict = new Dictionary<char, int>();
        foreach (char c in clean1)
        {
            if (!dict.ContainsKey(c)) dict[c] = 0;
            dict[c]++;
        }

        foreach (char c in clean2)
        {
            if (!dict.ContainsKey(c)) return false;
            dict[c]--;
            if (dict[c] < 0) return false;
        }

        return true;
    }

    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        List<string> summary = new();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                var place = feature?.Properties?.Place;
                var mag = feature?.Properties?.Mag;

                if (!string.IsNullOrEmpty(place) && mag.HasValue)
                {
                    summary.Add($"{place} - Mag {mag.Value}");
                }
            }
        }

        return summary.ToArray();
    }
}
