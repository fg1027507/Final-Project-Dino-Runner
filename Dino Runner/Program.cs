/*
dinorunner
Made by Finn Gilbert
On 11/10/2025
*/
// Main Menu
using System.Configuration.Assemblies;
string choice = "";
string PlayerName = "";
string PastNames = "";
string highScore = "";
while (true)
{
    Console.WriteLine("1 Welcome to the Dino Runner game!");
    Console.WriteLine("2 What is your name?");
    Console.Write("3 >> ");
    PlayerName = Console.ReadLine() ?? "";
    Console.WriteLine($"4 Hello, {PlayerName}! What would you like to do?");
    Console.WriteLine("5 To play the game, press 1");
    Console.WriteLine("6 To see the instructions, press 2");
    Console.WriteLine("7 To see the high scores, press 3");
    Console.WriteLine("8 To exit the game, press 4");
    choice = Console.ReadLine() ?? "";

    // Switch statement for the main menu
    switch (choice)
    {
        case "1":
            Console.WriteLine("9 You have chosen to play the game!");
            break;
        case "2":
            Console.WriteLine("9 You have chosen to see the instructions!");
            break;
        case "3":
            Console.WriteLine("9 You have chosen to see the high scores!");
            break;
        case "4":
            Console.WriteLine("9 You have chosen to exit the game!");
            break;
        default:
            Console.WriteLine("9 Invalid choice! Please try again.");
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
    // For the Game Loop
    /*
    Console.WriteLine("You have chosen to play the game!");
    while (true)
    {
        Console.Clear();
        for (int i = 0; i < 10; i++)
        {
            Console.Write("D");
        }
        for (int i = 0; i < 10; i++)
        {
            Console.Write("_");
        }
    }
    */
    Console.WriteLine("10");
    Console.WriteLine("11");
    Console.WriteLine("12");
    Console.WriteLine("13");
    Console.WriteLine("14");
    Console.WriteLine("15");
    Console.WriteLine("16");
    Console.WriteLine("17");
    Console.WriteLine("18");
    Console.WriteLine("19");
    Console.WriteLine("20");
}
// If the user chooses to see the instructions, the instructions will be displayed
 else if (choice == "2")
{
    // For the instructions 
    Console.WriteLine("You have chosen to see the instructions!");
    Console.WriteLine("The instructions are as follows:");
    Console.WriteLine("1. Press the spacebar to jump");
    Console.WriteLine("2. Avoid the obstacles");
    Console.WriteLine("4. Make it as long as possible");
}
else if (choice == "3")
{
    // For the high scores
    Console.WriteLine("You have chosen to see the high scores!");
    string filePath = "High Scores.csv";

        // Make sure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!");
            return;
        }

        // Read all lines into an array
        string[] lines = File.ReadAllLines(filePath);

        // Print the header
        Console.WriteLine("High Scores List:\n");

    // Skip the first line (header)
    for (int i = 1; i < lines.Length; i++)
    {
        string[] parts = lines[i].Split(',');

        PastNames = parts[0].Trim();
        highScore = parts[1].Trim();

        Console.WriteLine($"Name: {PastNames}, High Score: {highScore}");
    }
}
// If the user chooses to exit the game, the game will exit
else if (choice == "4")
{
    Console.WriteLine("You have chosen to exit the game!");
    Environment.Exit(0);
}