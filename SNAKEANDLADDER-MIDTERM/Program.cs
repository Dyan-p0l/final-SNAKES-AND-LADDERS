using System;
using System.Collections.Generic;
using Spectre.Console;
using TICTACTOE_MIDTERM;

internal class Program
{

    private static void Main(string[] args)
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        MenuOption menuOption = new MenuOption();
        Game game = new Game(menuOption);
        Menu menu = new Menu(menuOption, game);

        menuOption.SetMenu(menu);

        menu.displayMenu();

    }
}