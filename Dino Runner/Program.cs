/*
dinorunner
Made by Finn Gilbert
On 11/10/2025
*/
using System;
using System.IO;
string choice = "";
string PlayerName = "";
string PastNames = "";
string score = "";
int highScore = 0;
int currentScore = 0;
int groundY = 15;
int airY = 14;
int SpawnObstacle = 74;
bool hit = false;

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
        hit = false;
        currentScore = 0;

        var dino = new Dino(1, groundY, airY);
        var obstacle = new Obstacle(SpawnObstacle, groundY);

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
        System.Threading.Thread.Sleep(5000);
    }
    else if (choice == "3")
    {
        // For the high scores
        string filePath = Path.Combine(AppContext.BaseDirectory, "High Score.csv");

        // Make sure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"High score file not found at {filePath}");
            System.Threading.Thread.Sleep(3000);
        }
        else
        {
            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("High Scores List:\n");
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 2)
                {
                    continue;
                }

                PastNames = parts[0].Trim();
                score = parts[1].Trim();
                Console.WriteLine($"Name: {PastNames}, High Score: {score}");
            }
            System.Threading.Thread.Sleep(5000);
        }
    }
    // If the user chooses to exit the game, the game will exit
    else if (choice == "4")
    {
        Environment.Exit(0);
    }
} 
