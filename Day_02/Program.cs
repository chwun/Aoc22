namespace Day_02;
class Program
{
    static async Task Main(string[] args)
    {
        var lines = await File.ReadAllLinesAsync("input.txt");

        var rounds = ParseRounds(lines);
        int resultPart1 = DoPart1(rounds);
        Console.WriteLine($"result part 1: {resultPart1}");

        var roundOutcomes = ParseRoundOutcomes(lines);
        int resultPart2 = DoPart2(roundOutcomes);
        Console.WriteLine($"result part 2: {resultPart2}");
    }

    private static List<Round> ParseRounds(string[] lines)
    {
        List<Round> rounds = new();

        foreach (string line in lines)
        {
            var parts = line.Split(' ');
            var figOpp = ParseOpponent(parts[0]);
            var figSelf = ParseSelf(parts[1]);
            rounds.Add(new Round(figOpp, figSelf));
        }

        return rounds;
    }

    private static List<RoundOutcome> ParseRoundOutcomes(string[] lines)
    {
        List<RoundOutcome> roundOutcomes = new();

        foreach (string line in lines)
        {
            var parts = line.Split(' ');
            var figOpp = ParseOpponent(parts[0]);
            var outcome = ParseOutcome(parts[1]);
            roundOutcomes.Add(new RoundOutcome(figOpp, outcome));
        }

        return roundOutcomes;
    }

    private static Figure ParseOpponent(string key) => key switch
    {
        "A" => Figure.Rock,
        "B" => Figure.Paper,
        "C" => Figure.Scissors,
        _ => throw new ArgumentException()
    };

    private static Figure ParseSelf(string key) => key switch
    {
        "X" => Figure.Rock,
        "Y" => Figure.Paper,
        "Z" => Figure.Scissors,
        _ => throw new ArgumentException()
    };

    private static Outcome ParseOutcome(string key) => key switch
    {
        "X" => Outcome.Lose,
        "Y" => Outcome.Draw,
        "Z" => Outcome.Win,
        _ => throw new ArgumentException()
    };

    private static int DoPart1(List<Round> rounds)
    {
        int totalPoints = 0;

        foreach (var round in rounds)
        {
            totalPoints += PlayRound(round.self, round.opponent);
        }

        return totalPoints;
    }

    private static int DoPart2(List<RoundOutcome> roundOutcomes)
    {
        int totalPoints = 0;

        foreach (var roundOutcome in roundOutcomes)
        {
            Figure self;

            switch (roundOutcome.opponent)
            {
                case Figure.Rock:
                    self = roundOutcome.outcome switch
                    {
                        Outcome.Lose => Figure.Scissors,
                        Outcome.Draw => Figure.Rock,
                        Outcome.Win => Figure.Paper,
                        _ => throw new ArgumentException()
                    };
                    break;

                case Figure.Paper:
                    self = roundOutcome.outcome switch
                    {
                        Outcome.Lose => Figure.Rock,
                        Outcome.Draw => Figure.Paper,
                        Outcome.Win => Figure.Scissors,
                        _ => throw new ArgumentException()
                    };
                    break;

                case Figure.Scissors:
                    self = roundOutcome.outcome switch
                    {
                        Outcome.Lose => Figure.Paper,
                        Outcome.Draw => Figure.Scissors,
                        Outcome.Win => Figure.Rock,
                        _ => throw new ArgumentException()
                    };
                    break;

                default:
                    self = Figure.None;
                    break;
            }

            totalPoints += PlayRound(self, roundOutcome.opponent);
        }

        return totalPoints;
    }

    private static int PlayRound(Figure self, Figure opponent)
    {
        int roundPoints = self switch
        {
            Figure.Rock => 1,
            Figure.Paper => 2,
            Figure.Scissors => 3,
            _ => 0
        };

        switch (self)
        {
            case Figure.Rock:
                roundPoints += opponent switch
                {
                    Figure.Rock => 3,
                    Figure.Paper => 0,
                    Figure.Scissors => 6,
                    _ => 0
                };
                break;

            case Figure.Paper:
                roundPoints += opponent switch
                {
                    Figure.Rock => 6,
                    Figure.Paper => 3,
                    Figure.Scissors => 0,
                    _ => 0
                };
                break;

            case Figure.Scissors:
                roundPoints += opponent switch
                {
                    Figure.Rock => 0,
                    Figure.Paper => 6,
                    Figure.Scissors => 3,
                    _ => 0
                };
                break;
        }

        return roundPoints;
    }
}
