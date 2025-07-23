using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public class SetsAndMaps
{
    // Problem 1 - FindPairs
    public static List<string> FindPairs(string[] words)
    {
        HashSet<string> wordSet = new HashSet<string>(words);
        HashSet<string> seen = new HashSet<string>();
        List<string> result = new List<string>();

        foreach (string word in wordSet) // iterate once per unique word
        {
            char[] arr = word.ToCharArray();
            Array.Reverse(arr);
            string reversed = new string(arr);

            if (word != reversed && wordSet.Contains(reversed) && !seen.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                seen.Add(word);
            }
        }

        return result;
    }



    // Problem 2 - SummarizeDegrees
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        Dictionary<string, int> degreeCount = new Dictionary<string, int>();

        // Mapping from raw values in CSV to expected output
        Dictionary<string, string> degreeMap = new Dictionary<string, string>
        {
            ["bachelors"] = "Bachelor's degree",
            ["hs-grad"] = "High School graduate",
            ["11th"] = "11th grade",
            ["masters"] = "Master's degree",
            ["9th"] = "9th grade",
            ["some-college"] = "Some college",
            ["assoc-acdm"] = "Associate's degree (academic)",
            ["assoc-voc"] = "Associate's degree (vocational)",
            ["7th-8th"] = "7th-8th grade",
            ["doctorate"] = "Doctorate degree",
            ["prof-school"] = "Professional school degree",
            ["5th-6th"] = "5th-6th grade",
            ["10th"] = "10th grade",
            ["1st-4th"] = "1st-4th grade",
            ["preschool"] = "Preschool",
            ["12th"] = "12th grade, no diploma"
        };

        foreach (string line in File.ReadLines(filename))
        {
            string[] columns = line.Split(',');
            if (columns.Length >= 5)
            {
                string raw = columns[3].Trim().ToLowerInvariant();

                if (degreeMap.ContainsKey(raw))
                {
                    string displayName = degreeMap[raw];
                    if (!degreeCount.ContainsKey(displayName))
                    {
                        degreeCount[displayName] = 0;
                    }
                    degreeCount[displayName]++;
                }
            }
        }

        return degreeCount;
    }

    // Problem 5 - EarthquakeDailySummary
    public static async Task<List<string>> EarthquakeDailySummary()
    {
        using HttpClient client = new HttpClient();
        string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        string json = await client.GetStringAsync(url);
        FeatureCollection data = JsonSerializer.Deserialize<FeatureCollection>(json);

        List<string> result = new List<string>();

        foreach (Feature feature in data.Features)
        {
            string place = feature.Properties.Place;
            double? mag = feature.Properties.Mag;

            if (!string.IsNullOrEmpty(place) && mag.HasValue)
            {
                result.Add($"{place} - Mag {mag.Value}");
            }
        }

        return result;
    }
    public static bool IsAnagram(string word1, string word2)
{
    // Remove spaces and convert to lowercase
    string w1 = new string(word1.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLowerInvariant();
    string w2 = new string(word2.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLowerInvariant();

    // Quick length check
    if (w1.Length != w2.Length) return false;

    Dictionary<char, int> letterCounts = new Dictionary<char, int>();

    // Count letters in word1
    foreach (char c in w1)
    {
        if (letterCounts.ContainsKey(c))
            letterCounts[c]++;
        else
            letterCounts[c] = 1;
    }

    // Subtract counts for letters in word2
    foreach (char c in w2)
    {
        if (!letterCounts.ContainsKey(c))
            return false;

        letterCounts[c]--;

        if (letterCounts[c] < 0)
            return false;
    }

    // All counts should be zero now
    return letterCounts.Values.All(count => count == 0);
}

}
