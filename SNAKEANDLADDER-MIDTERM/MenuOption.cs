using Spectre.Console;
using System;
using SNAKEANDLADDER_MIDTERM.styling;

namespace SNAKEANDLADDER_MIDTERM
{
    internal class MenuOption
    {

        private Menu? menu;
        Display display = new Display();
        PrintFormat printFormat = new PrintFormat();
        
        public MenuOption()
        {

        }

        public void SetMenu(Menu menu)
        {
            this.menu = menu;
        }

        static void ClearConsoleWindows()
        {
            Console.Clear();  // Clears visible screen
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight); // Resets buffer size to match window
        }

        public void howToPlay()
        {

            var font = FigletFont.Load("fonts/nancyj.flf.txt");
            AnsiConsole.Write(
            new FigletText(font, "HowToPlay")
            .Centered()
            .Color(Color.Orange1));

            Console.WriteLine();
            printFormat.printCenterRed("OBJECTIVE");
            printFormat.printCenter("------------------------------------------");
            Console.WriteLine();
            printFormat.printCenterRed("Be the first player to reach tile 100 by rolling the dice and moving forward.");
            Console.WriteLine();
            Console.WriteLine();
            printFormat.printCenter("BASIC RULES");
            printFormat.printCenter("------------------------------------------");
            Console.WriteLine();
            printFormat.printCenterRed("1. Players take turns rolling a dice to move forward.          ");
            printFormat.printCenterRed("2. Landing on a ladder allows you to climb up to a higher tile.");
            printFormat.printCenterRed("3. Landing on a snake's head makes you slide down to its tail. ");
            Console.WriteLine();
            Console.WriteLine();
            printFormat.printCenterGreen("SKILL SYSTEM");
            printFormat.printCenter("------------------------------------------");
            printFormat.printCenterGreen("1. Skill Tiles: Some tiles contain hidden skills. If a player lands on a         ");
            printFormat.printCenterGreen("   Skill Tile, they receive a random skill.                                      ");
            printFormat.printCenterGreen("2. Max Skills: Each player can hold up to two skills at a time. If they land on a");
            printFormat.printCenterGreen("   Skill Tile while already having two skills, they must replace one.            ");
            printFormat.printCenterGreen("3. No Stacking: Skills do not stack—having two of the same skill is not possible.");Console.WriteLine();
            Console.WriteLine();
            printFormat.printCenterPurple("AVAILABLE SKILLS");
            printFormat.printCenter("------------------------------------------");
            printFormat.printCenterPurple("1. 🛡️Shield – Protects against one negative effect (snake, stun, sabotage, etc.). ");
            printFormat.printCenterPurple("2. ⚡Stun – Prevents an opponent from rolling the dice on their next turn.        ");
            printFormat.printCenterPurple("3. 🔄Swap (Rare) – Swaps positions with an opponent.                              ");
            printFormat.printCenterPurple("4. 🎲Dice Manipulation – Allows the player to pick their dice roll outcome.       ");
            printFormat.printCenterPurple("5. ⚓Anchor – If the player lands on a snake’s head, they can ignore it and       ");
            printFormat.printCenterPurple("   stay in place.                                                               ");
            printFormat.printCenterPurple("6. 💣Sabotage – Forces an opponent to roll the dice but move backward instead     ");
            printFormat.printCenterPurple("   of forward.                                                                  ");

            Console.WriteLine();
            Console.WriteLine();

            printFormat.print("BACK TO MENU (PRESS ANY KEY)");

            Console.ReadKey();
            display.ClearConsole();
            menu.displayMenu();
        }

        public void Developers()
        {
            var font = FigletFont.Load("fonts/nancyj.flf.txt");
            AnsiConsole.Write(
            new FigletText(font,"DEVS")
            .Centered()
            .Color(Color.Green));

            printFormat.printCenterGreen("ALLEN PAUL BELARMINO");
            printFormat.printCenterGreen("----------------------------");
            printFormat.printCenterGreen("A Computer engineering student and an");
            printFormat.printCenterGreen("aspiring software developer with a");
            printFormat.printCenterGreen("strong interest in game mechanics and");
            printFormat.printCenterGreen("innovative technologies. Constantly");
            printFormat.printCenterGreen("exploring new ways to improve gameplay.");

            Console.WriteLine();

            printFormat.printCenterRed("JOHN PAUL RAYCO");
            printFormat.printCenterRed("----------------------------");
            printFormat.printCenterRed("A Computer Engineering student");
            printFormat.printCenterRed("and an aspiring software developer");
            printFormat.printCenterRed("with a passion for algorithms and");
            printFormat.printCenterRed("problem-solving. Enjoys optimizing");
            printFormat.printCenterRed("code to create efficient and engaging");
            printFormat.printCenterRed("gameplay mechanics.");

            Console.WriteLine();

            printFormat.printCenterYellow("VINCE ROSALIJOS");
            printFormat.printCenterYellow("----------------------------");
            printFormat.printCenterYellow("A Computer engineering student with a");
            printFormat.printCenterYellow("passion for software development and");
            printFormat.printCenterYellow("game mechanics. Always exploring new");
            printFormat.printCenterYellow("technologies and innovative ideas.");

            printFormat.print("BACK TO MENU (PRESS ANY KEY)");
            Console.ReadKey();
            display.ClearConsole();
            menu.displayMenu();
        }

    }
}
