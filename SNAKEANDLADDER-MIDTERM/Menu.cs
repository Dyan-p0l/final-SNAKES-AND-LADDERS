﻿using System;
using SNAKEANDLADDER_MIDTERM.styling;
using Spectre.Console;

namespace TICTACTOE_MIDTERM
{

    internal class Menu
    {

        private MenuOption menuOption;
        private Game game;
        public Menu(MenuOption menuOption, Game game)
        {
            this.menuOption = menuOption;
            this.game = game;
        }

        public void displayMenu()
        {

            Display display = new Display();
            PrintFormat printFormat = new PrintFormat();
            
            bool isValid;
            int menuChoice;

            again:
            do {
                Console.Clear();

                var font = FigletFont.Load("fonts/nancyj.flf.txt");

                AnsiConsole.Write(
                new FigletText(font,"Snakes & Ladders")
                .Centered()
                .Color(Color.Orange3));

                Console.WriteLine();
                Console.WriteLine();
                printFormat.printCenter("1.PLAY GAME");
                printFormat.printCenterRed(" 2.HOW TO PLAY");
                printFormat.printCenterGreen("3.DEVELOPERS");
                printFormat.printCenterYellow("4.EXIT");
                Console.WriteLine();
                printFormat.print("Enter choice (1-4): ");
                string choice = Console.ReadLine();
                isValid = int.TryParse(choice, out menuChoice) && (menuChoice >= 1 && menuChoice <= 4);


                if (!isValid)
                {
                    printFormat.printCenterRed("Invalid Input. Please try again");
                    Console.WriteLine();
                    Console.WriteLine();
                    printFormat.printCenterRed("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (!isValid);

            switch (menuChoice)
            {
                case 1:
                    game.StartGame();
                    break;
                case 2:
                    menuOption.howToPlay();
                    break;
                case 3:
                    menuOption.Developers();
                    break;
                case 4:
                    Console.WriteLine();
                    var character = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[red]ARE YOU SURE YOU WANT TO EXIT THE GAME??[/]")
                        .PageSize(10)
                        .AddChoices("YES", "NO")
                     );

                    if (character == "YES")
                    {
                        Environment.Exit(0);
                    }
                    else if (character == "NO")
                    {
                        goto again;
                    }
                     break;
                
            }

            Console.Clear();

        }
    }
}
