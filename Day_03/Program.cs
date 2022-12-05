namespace Day_03;
class Program
{
    static async Task Main(string[] args)
    {
        var lines = await File.ReadAllLinesAsync("input.txt");

        // part 1
        int prioritySum = 0;
        foreach (string line in lines)
        {
            int midIndex = line.Length / 2;

            string s1 = line[..midIndex];
            string s2 = line[midIndex..];

            char commonCharacter = s1.Intersect(s2).First();
            int asciiOffset = char.IsLower(commonCharacter) ? 96 : 38;
            int priority = (int)commonCharacter - asciiOffset;

            prioritySum += priority;
        }

        Console.WriteLine($"result part 1: {prioritySum}");

        // part 2
        prioritySum = 0;
        for (int i = 0; i <= lines.Length - 3; i += 3)
        {
            string s1 = lines[i];
            string s2 = lines[i + 1];
            string s3 = lines[i + 2];

            var badgeChar = s1.Intersect(s2).Intersect(s3).First();
            Console.WriteLine(badgeChar);
            int asciiOffset = char.IsLower(badgeChar) ? 96 : 38;
            int priority = (int)badgeChar - asciiOffset;

            prioritySum += priority;
        }

        Console.WriteLine($"result part 2: {prioritySum}");

    }
}
