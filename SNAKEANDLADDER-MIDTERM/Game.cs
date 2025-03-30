using Spectre.Console;
using System;
using System.Data;
using SNAKEANDLADDER_MIDTERM.styling;
using System.Numerics;
using SNAKEANDLADDER_MIDTERM;
using static SNAKEANDLADDER_MIDTERM.GameLogic;

namespace TICTACTOE_MIDTERM
{
    internal class Game
    {

        GameLogic logic = new GameLogic();
        PrintFormat printFormat = new PrintFormat();
        Display display = new Display();
        Menu menu;


        public Game(MenuOption menuOption)
        {
            menu = new Menu(menuOption, this);
        }

        private static List<Player> players = new();

        public void StartGame()
        {
            Console.Clear();

            players.Clear();

            int numberOfPlayers;
            bool isValid;
            bool gameEnded = false;
            bool playAgain = false;
            int currentPlayerIndex = 0;

            var font = FigletFont.Load("fonts/wavy.flf.txt");

            AnsiConsole.Write(
            new FigletText(font, "SNAKES & LADDERS")
            .Centered()
            .Color(Color.Orange3));

            do
            {   
                again:
                printFormat.print("Enter the number of players(2-4): ");
                string tonum = Console.ReadLine();
                isValid = int.TryParse(tonum, out numberOfPlayers) && (numberOfPlayers >= 2 && numberOfPlayers <= 4);
                if (!isValid)
                {
                    printFormat.printCenterRed("Invalid Input.");
                    printFormat.printCenterRed("Press any key to try again");
                    Console.ReadKey();
                    Console.Clear();
                    goto again;
                }
            }
            while (!isValid);

            var availableCharacters = new List<string>
            {
                "Sung Jinwoo", "Beru", "Iron", "Saitama"
            };

            string[] colors = { "red", "green", "blue", "yellow" };

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                printFormat.printCenterRed($"For Player {i}");
                Console.WriteLine();
                
                var character = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[red]CHOOSE CHARACTER TO PLAY (use ⬆️ ⬇️ arrow)[/]")
                    .PageSize(10)
                    .AddChoices(availableCharacters)
                 );
                
                printFormat.printCenterRed($"Player {i} chose {character}");
                Console.WriteLine();
                players.Add(new Player(character, colors[i-1]));
                availableCharacters.Remove(character);
                if (i == numberOfPlayers)
                {
                    AnsiConsole.MarkupLine("[red]Press any key to continue...[/]");
                    Console.ReadKey();
                }
            }

            Again:
            while (!gameEnded)
            {
                
                Player currentPlayer = players[currentPlayerIndex];

                if (currentPlayer.HasWon)
                {
                    currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                    continue;
                }

                logic.DisplayBoard(players, currentPlayerIndex);

                if (currentPlayer.SkipTurn)
                {
                    logic.lastAction = $"[red]{currentPlayer.Name} is stunned ⚡ and skips this turn![/]";
                    currentPlayer.SkipTurn = false;
                    logic.DisplayBoard(players, currentPlayerIndex);
                    Console.ReadKey();
                }
                else
                {
                    
                    logic.DisplayBoard(players, currentPlayerIndex);

                    if (currentPlayer.Skills.Count > 0 && AnsiConsole.Confirm($"[{currentPlayer.Color}]{currentPlayer.Name}[/], do you want to use a skill?"))
                    {
                        logic.UseSkill(currentPlayer, players);
                        logic.DisplayBoard(players, currentPlayerIndex);
                    }

                    int roll = logic.RollDice(players, currentPlayerIndex);
                    logic.lastRoll = roll;

                    logic.MovePlayer(currentPlayer, roll);
                    logic.CheckSkillTile(currentPlayer);

                    if (currentPlayer.Position >= 100)
                    {
                        currentPlayer.HasWon = true;
                        logic.DisplayBoard(players, currentPlayerIndex);
                        AnsiConsole.MarkupLine($"\n[bold green]🎉 Congratulations {currentPlayer.Name}! You win the game! 🏆[/]");

                        if (players.Count(p => !p.HasWon) == 0 || AnsiConsole.Confirm("\nDo you want to continue playing?"))
                        {
                            Console.Clear();
                            goto Again;
                        }
                        else
                        {
                            gameEnded = true;
                            menu.displayMenu();
                        }
                    }
                }

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                /*Console.ReadKey();*/
            }
        }

    }
}
