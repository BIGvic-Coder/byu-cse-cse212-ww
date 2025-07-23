using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// DO NOT MODIFY THIS FILE

[TestClass]
public class FindPairsTests
{
    [TestMethod]
    public void FindPairs_TwoPairs()
    {
        var actual = SetsAndMaps.FindPairs(["am", "at", "ma", "if", "fi"]);
        var expected = new[] { "ma & am", "fi & if" };

        Assert.AreEqual(expected.Length, actual.Count);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual.ToArray()));
    }

    [TestMethod]
    public void FindPairs_OnePair()
    {
        var actual = SetsAndMaps.FindPairs(["ab", "bc", "cd", "de", "ba"]);
        var expected = new[] { "ba & ab" };

        Assert.AreEqual(expected.Length, actual.Count);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual.ToArray()));
    }

    [TestMethod]
    public void FindPairs_SameChar()
    {
        var actual = SetsAndMaps.FindPairs(["ab", "aa", "ba"]);
        var expected = new[] { "ba & ab" };

        Assert.AreEqual(expected.Length, actual.Count);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual.ToArray()));
    }

    [TestMethod]
    public void FindPairs_ThreePairs()
    {
        var actual = SetsAndMaps.FindPairs(["ab", "ba", "ac", "ad", "da", "ca"]);
        var expected = new[] { "ba & ab", "da & ad", "ca & ac" };

        Assert.AreEqual(expected.Length, actual.Count);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual.ToArray()));
    }

    [TestMethod]
    public void FindPairs_ThreePairsNumbers()
    {
        var actual = SetsAndMaps.FindPairs(["23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14"]);
        var expected = new[] { "32 & 23", "94 & 49", "31 & 13" };

        Assert.AreEqual(expected.Length, actual.Count);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual.ToArray()));
    }

    [TestMethod]
    public void FindPairs_NoPairs()
    {
        var actual = SetsAndMaps.FindPairs(["ab", "ac"]);
        var expected = new string[0];

        Assert.AreEqual(expected.Length, actual.Count);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual.ToArray()));
    }

    [TestMethod, Timeout(60_000)]
    public void FindPairs_NoPairs_Efficiency()
    {
        double CalibrateCpuSpeed()
        {
            var sw = Stopwatch.StartNew();
            long sum = 0;
            for (int i = 0; i < 10_000_000; i++) sum += i;
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        double baseline = CalibrateCpuSpeed();

        var count = 1_000_000;
        var input = new List<string>(count);
        for (int i = 0; i < count; ++i)
        {
            char[] chars = ['a', 'b'];
            string s = new(chars);
            input.Add(s);
        }

        var sw = Stopwatch.StartNew();
        var actual = SetsAndMaps.FindPairs(input.ToArray());
        sw.Stop();

        double elapsed = sw.Elapsed.TotalMilliseconds;
        double ratio = elapsed / baseline;

        Debug.WriteLine($"Elapsed: {elapsed:F2}ms | Baseline: {baseline:F2}ms | Ratio: {ratio:F2}");
        Assert.IsTrue(ratio < 15.0, "Your algorithm is too slow. Make sure it runs in O(n) time.");
        Assert.AreEqual(0, actual.Count);
    }

    private string Canonicalize(string[] array)
    {
        if (array.Length == 0)
        {
            return "";
        }

        var canonicalString = array.Select(item =>
        {
            var parts = item.Split('&');
            return parts
                .Select(part => part.Trim())
                .OrderBy(x => x)
                .Aggregate((current, next) => current + "&" + next);
        })
        .OrderBy(x => x)
        .Aggregate((current, next) => current + "," + next);

        return canonicalString;
    }

    [TestClass]
public class SummarizeDegreesTests
{
    [TestMethod]
    public void SummarizeDegrees_BasicTest()
    {
        string testFile = "census.txt"; // Make sure this file exists in your project root or bin folder
        var result = SetsAndMaps.SummarizeDegrees(testFile);

        // Example assertion — change keys/values depending on census.txt content
        Assert.IsTrue(result.ContainsKey("Bachelor's degree"), "Expected Bachelor's degree in results.");
        Assert.IsTrue(result["Bachelor's degree"] > 0, "Expected non-zero count for Bachelor's degree.");
    }
}

}

// (The rest of your test classes were already correct and do not use .Length incorrectly)

