namespace Day01;

internal class Program
{
    static async Task Main(string[] args)
    {
        var lines = await File.ReadAllLinesAsync("input.txt");
        var elves = ParseElves(lines);

        int resultPart1 = elves.Max(x => x.Calories);
        int resultPart2 = elves.OrderByDescending(x => x.Calories).Take(3).Sum(x => x.Calories);

        Console.WriteLine($"result part 1: {resultPart1}");
        Console.WriteLine($"result part 2: {resultPart2}");
    }

    private static List<Elve> ParseElves(string[] lines)
    {
        List<Elve> elves = new();
        Elve? current = null;

        foreach (string line in lines)
        {
            if (current is null)
            {
                current = new();
                elves.Add(current);
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                current = null;
            }
            else if (int.TryParse(line, out int cal))
            {
                current.AddCalories(cal);
            }
        }

        return elves;
    }
}