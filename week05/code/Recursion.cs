using System;
using System.Collections.Generic;

public class Recursion
{
    public static int CountCharacter(string input, char target)
    {
        if (string.IsNullOrEmpty(input))
            return 0;

        int count = (input[0] == target) ? 1 : 0;
        return count + CountCharacter(input.Substring(1), target);
    }

    public static int Sum(List<int> numbers)
    {
        if (numbers.Count == 0)
            return 0;

        int first = numbers[0];
        numbers.RemoveAt(0);
        return first + Sum(numbers);
    }

    public static int Power(int baseNum, int exponent)
    {
        if (exponent == 0)
            return 1;

        return baseNum * Power(baseNum, exponent - 1);
    }

    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;

        return n * n + SumSquaresRecursive(n - 1);
    }

    public static void PermutationsChoose(List<string> output, string s, int k)
    {
        Permute(output, "", s, k);
    }

    private static void Permute(List<string> output, string prefix, string remaining, int k)
    {
        if (prefix.Length == k)
        {
            output.Add(prefix);
            return;
        }

        for (int i = 0; i < remaining.Length; i++)
        {
            string next = prefix + remaining[i];
            string rest = remaining.Remove(i, 1);
            Permute(output, next, rest, k);
        }
    }

    public static decimal CountWaysToClimb(int steps)
    {
        Dictionary<int, decimal> memo = new();
        return CountWays(steps, memo);
    }

    private static decimal CountWays(int n, Dictionary<int, decimal> memo)
    {
        if (n < 0)
            return 0;
        if (n == 0)
            return 1;
        if (memo.ContainsKey(n))
            return memo[n];

        decimal result = CountWays(n - 1, memo) + CountWays(n - 2, memo) + CountWays(n - 3, memo);
        memo[n] = result;
        return result;
    }

    public static void WildcardBinary(string s, List<string> output)
    {
        WildcardHelper(s, 0, "", output);
    }

    private static void WildcardHelper(string s, int index, string current, List<string> output)
    {
        if (index == s.Length)
        {
            output.Add(current);
            return;
        }

        if (s[index] == '*')
        {
            WildcardHelper(s, index + 1, current + '0', output);
            WildcardHelper(s, index + 1, current + '1', output);
        }
        else
        {
            WildcardHelper(s, index + 1, current + s[index], output);
        }
    }

    public static void SolveMaze(List<string> output, Maze maze)
    {
        Explore(maze, 0, 0, new List<(int, int)>(), output);
    }

    private static void Explore(Maze maze, int row, int col, List<(int, int)> path, List<string> results)
    {
        if (!maze.IsValidMove(path, col, row))
            return;

        path.Add((col, row));

        if (maze.IsEnd(col, row))
        {
            results.Add(path.AsString());
        }
        else
        {
            Explore(maze, row + 1, col, new List<(int, int)>(path), results); // Down
            Explore(maze, row - 1, col, new List<(int, int)>(path), results); // Up
            Explore(maze, row, col + 1, new List<(int, int)>(path), results); // Right
            Explore(maze, row, col - 1, new List<(int, int)>(path), results); // Left
        }
    }
}
