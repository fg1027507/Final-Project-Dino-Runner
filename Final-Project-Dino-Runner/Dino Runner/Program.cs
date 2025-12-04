/*
dinorunner
Made by Finn Gilbert
On 11/10/2025
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
string choice = "";
string PlayerName = "";
int highScore = 0;
int currentScore = 0;
int groundY = 15;
int airY = 14;
int SpawnObstacle = 74;
bool hit = false;
const int MaxHighScoreEntries = 10;
const string HighScoreHeader = "Name,HighScore";
string highScoreFilePath = Path.Combine(AppContext.BaseDirectory, "High Score.csv");

// List for High Score CSV
List<(string Name, int Score)> LoadHighScoreEntries()
{
    List<(string Name, int Score)> entries = new List<(string Name, int Score)>();
    if (!File.Exists(highScoreFilePath))
    {
        return entries;
    }

    foreach (string line in File.ReadAllLines(highScoreFilePath))
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            continue;
        }

        string trimmedLine = line.Trim();
        if (trimmedLine.StartsWith("Name,", StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        string[] parts = trimmedLine.Split(',');
        if (parts.Length < 2)
        {
            continue;
        }

        string parsedName = parts[0].Trim();
        if (!int.TryParse(parts[1].Trim(), out int parsedScore))
        {
            continue;
        }

        entries.Add((parsedName, parsedScore));
    }

    entries.Sort((a, b) => b.Score.CompareTo(a.Score));
    return entries;
}

// Functions for various controls
void WriteHighScoreEntries(IEnumerable<(string Name, int Score)> entries)
{
    using StreamWriter writer = new StreamWriter(highScoreFilePath, false);
    writer.WriteLine(HighScoreHeader);
    foreach ((string Name, int Score) entry in entries)
    {
        writer.WriteLine($"{entry.Name},{entry.Score}");
    }
}

void TryAddHighScore(string player, int score)
{
    if (string.IsNullOrWhiteSpace(player) || score <= 0)
    {
        return;
    }

    List<(string Name, int Score)> entries = LoadHighScoreEntries();
    entries.Add((player.Trim(), score));
    entries.Sort((a, b) => b.Score.CompareTo(a.Score));
    if (entries.Count > MaxHighScoreEntries)
    {
        entries.RemoveRange(MaxHighScoreEntries, entries.Count - MaxHighScoreEntries);
    }

    WriteHighScoreEntries(entries);
}

void WaitForEnter(string prompt = "Press Enter to return to the main menu.")
{
    Console.WriteLine();
    Console.WriteLine(prompt);
    while (Console.ReadKey(true).Key != ConsoleKey.Enter)
    {
        // wait until Enter/Return is pressed
    }
}

// Asks user for their name for high scores
Console.WriteLine("Welcome to the Dino Runner game!");
Console.WriteLine("What is your name?");
Console.Write(">> ");
PlayerName = Console.ReadLine() ?? "";
while (true)
{
    Console.Clear();
    while (true)
    {
        // Main Menu Loop
        Console.WriteLine($"Hello, {PlayerName}! What would you like to do?");
        Console.WriteLine("To play the game, press 1");
        Console.WriteLine("To see the instructions, press 2");
        Console.WriteLine("To see the high scores, press 3");
        Console.WriteLine("To exit the game, press 4");
        choice = Console.ReadLine() ?? "";

        // Switch statement for the main menu
        switch (choice)
        {
            case "1":
                Console.WriteLine("You have chosen to play the game!");
                break;
            case "2":
                Console.WriteLine("You have chosen to see the instructions!");
                break;
            case "3":
                Console.WriteLine("You have chosen to see the high scores!");
                break;
            case "4":
                Console.WriteLine("You have chosen to exit the game!");
                break;
            default:
                Console.WriteLine("Invalid choice! Please try again.");
                break;
        }
        if (choice == "1" || choice == "2" || choice == "3" || choice == "4")
        {
            break;
        }
    }
    // If the user chooses to play the game, the game will start
    if (choice == "1")
    {
        Console.Clear();
        hit = false;
        currentScore = 0;

        Dino dino = new Dino(1, groundY, airY);
        Obstacle obstacle = new Obstacle(SpawnObstacle, groundY);

        while (true)
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.J)
            {
                dino.StartJump();
            }

            int previousObstacleX = obstacle.X;
            obstacle.Move();
            obstacle.ClearPrevious(previousObstacleX);
            obstacle.Draw();

            dino.Update();
            dino.Render();

            if (obstacle.CollidesWith(dino))
            {
                hit = true;
            }

            currentScore++;
            if (currentScore > highScore)
            {
                highScore = currentScore;
            }

            Console.SetCursorPosition(1, groundY - 4);
            Console.Write($"High Score: {highScore}   ");

            if (hit)
            {
                Console.SetCursorPosition(0, groundY + 2);
                Console.WriteLine("Game Over!");
                TryAddHighScore(PlayerName, highScore);
                currentScore = 0;
                highScore = 0;
                break;
            }

            System.Threading.Thread.Sleep(50);
        }
    }
    // If the user chooses to see the instructions, the instructions will be displayed
    else if (choice == "2")
    {
        // For the instructions 
        Console.WriteLine("The instructions are as follows:");
        Console.WriteLine("1. Press the \"J\" to jump");
        Console.WriteLine("2. Avoid the obstacles");
        Console.WriteLine("4. Make it as long as possible");
        WaitForEnter();
    }
    else if (choice == "3")
    {
        List<(string Name, int Score)> entries = LoadHighScoreEntries();
        if (entries.Count == 0)
        {
            Console.WriteLine("No high scores yet. Play a round to make the list.");
        }
        else
        {
            Console.WriteLine("High Scores List:\n");
            int place = 1;
            foreach ((string Name, int Score) entry in entries.Take(MaxHighScoreEntries))
            {
                Console.WriteLine($"{place++}. Name: {entry.Name}, High Score: {entry.Score}");
            }
        }

        WaitForEnter();
    }
    // If the user chooses to exit the game, the game will exit
    else if (choice == "4")
    {
        Environment.Exit(0);
    }
} 
