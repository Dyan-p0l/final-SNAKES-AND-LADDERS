using System;
using System.Collections.Generic;
using Spectre.Console;

internal class Program
{

    static Random random = new Random();
    static Dictionary<int, int> snakes = new Dictionary<int, int>()
    {
        { 16, 6 }, { 47, 26 }, { 49, 11 }, { 56, 53 }, { 62, 19 }, { 64, 60 }, { 87, 24 }, { 93, 73 }, { 95, 75 }, { 98, 78 }
    };

    static Dictionary<int, int> ladders = new Dictionary<int, int>()
    {
        { 1, 38 }, { 4, 14 }, { 9, 31 }, { 21, 42 }, { 28, 84 }, { 36, 44 }, { 51, 67 }, { 71, 91 }, { 80, 100 }
    };

    static int MovePlayer(int position, int roll, string player)
    {
        int newPosition = position + roll;
        if (newPosition > 100)
        {
            Console.WriteLine($"{player} stays at {position} (Roll exceeds 100)");
            return position;
        }

        if (snakes.ContainsKey(newPosition))
        {
            Console.WriteLine($"🐍 {player} got bitten by a snake! Moves down to {snakes[newPosition]}");
            return snakes[newPosition];
        }
        if (ladders.ContainsKey(newPosition))
        {
            Console.WriteLine($"🪜 {player} climbed a ladder! Moves up to {ladders[newPosition]}");
            return ladders[newPosition];
        }

        Console.WriteLine($"{player} moves to {newPosition}");
        return newPosition;
    }

    static void DisplayBoard(int pos1, int pos2)
    {
        var table = new Table();
        table.Border = TableBorder.Heavy;
        table.ShowRowSeparators();
        table.HideHeaders();
        for (int col = 0; col < 10; col++)
        {
            table.AddColumn(new TableColumn((col + 1).ToString()).Centered());
        }

        for (int row = 8; row >= 0; row--)
        {
            var rowCells = new List<string>();

            for (int col = 0; col < 10; col++)
            {
                int cellNumber = (row % 2 == 0) ? (row * 10 + col + 1) : (row * 10 + (9 - col) + 1);
                string cellContent = cellNumber.ToString("D2");

                if (snakes.ContainsKey(cellNumber))
                    cellContent = $"[red]🐍{cellNumber}[/]";
                else if (ladders.ContainsKey(cellNumber))
                    cellContent = $"[green]🪜{cellNumber}[/]";

                if (pos1 == cellNumber && pos2 == cellNumber)
                    cellContent = $"[bold blue]P1+P2[/]";
                else if (pos1 == cellNumber)
                    cellContent = $"[bold yellow]P1[/]";
                else if (pos2 == cellNumber)
                    cellContent = $"[bold cyan]P2[/]";
                        
                rowCells.Add(cellContent);
            }
            table.AddRow(rowCells.ToArray());
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Snake and Ladder!");
        Console.Write("Enter Player 1 Name: ");
        string player1 = Console.ReadLine();
        Console.Write("Enter Player 2 Name: ");
        string player2 = Console.ReadLine();

        int pos1 = 0, pos2 = 0;
        bool turn = true;

        while (pos1 < 100 && pos2 < 100)
        {
            DisplayBoard(pos1, pos2);

            Console.WriteLine($"\n{(turn ? player1 : player2)}'s turn. Press Enter to roll the dice...");
            Console.ReadKey();
            int roll = random.Next(1, 7);
            Console.WriteLine($"{(turn ? player1 : player2)} rolled a {roll}!");

            if (turn)
                pos1 = MovePlayer(pos1, roll, player1);
            else
                pos2 = MovePlayer(pos2, roll, player2);

            if (pos1 == 100 || pos2 == 100)
            {
                Console.WriteLine($"\n🎉 {(pos1 == 100 ? player1 : player2)} wins! 🎉");
                break;
            }

            turn = !turn;
        }

        Console.WriteLine("Game Over. Thanks for playing!");
    }
}