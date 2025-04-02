using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICTACTOE_MIDTERM.audio;

namespace SNAKEANDLADDER_MIDTERM
{
    internal class GameLogic
    {
        public readonly Random random = new();
        public readonly Dictionary<int, int> snakes = new()
        {
            { 16, 6 }, { 47, 26 }, { 49, 11 }, { 56, 53 }, { 62, 19 }, { 64, 60 }, { 87, 24 }, { 93, 73 }, { 95, 75 }, { 98, 78 }
        };
        Soundfx soundfx = new Soundfx();    
        public readonly Dictionary<int, int> ladders = new()
        {
            { 1, 38 }, { 4, 14 }, { 9, 31 }, { 21, 42 }, { 28, 84 }, { 36, 44 }, { 51, 67 }, { 71, 91 }, { 80, 100 }
        };

        public readonly HashSet<int> skillTiles = new() { 3, 10, 14, 20, 24, 30, 33 ,40, 44 , 50, 54, 60, 66 ,70, 74, 77, 85, 90, 96};
        public readonly string[] availableSkills = { "Shield 🛡️", "Stun ⚡", "Swap 🔄", "Dice Manipulation 🎲", "Anchor ⚓", "Sabotage 💣" };

        public int? lastRoll = null;
        public string lastAction = "";
        public string lastSkillUsed = "";
        public bool gameEnded = false;

        public class Player
        {
            public string Name { get; }
            public int Position { get; set; }
            public List<string> Skills { get; } = new();
            public bool SkipTurn { get; set; } = false;
            public string Color { get; }
            public bool HasWon { get; set; } = false;
            public bool IsShielded { get; set; } = false;

            public bool isAnchored { get; set; } = false;

            public Player(string name, string color)
            {
                Name = name;
                Position = 0;
                Color = color;
            }
        } 

        /*public List<Player> players = new();*/

        public void DisplayBoard(List<Player> players, int currentPlayerIndex)
        {

            var table = new Table();
            table.Border = TableBorder.Heavy;
            table.ShowRowSeparators();
            table.HideHeaders();

            for (int col = 0; col < 10; col++)
                table.AddColumn(new TableColumn((col + 1).ToString()).Centered());

            for (int row = 9; row >= 0; row--)
            {
                var rowCells = new List<string>();
                for (int col = 0; col < 10; col++)
                {
                    int cellNumber = (row % 2 == 0) ? (row * 10 + col + 1) : (row * 10 + (9 - col) + 1);
                    string cellContent = cellNumber.ToString("D2");

                    if (snakes.ContainsKey(cellNumber))
                        cellContent = $"[red]🐍{cellContent}[/]";
                    else if (ladders.ContainsKey(cellNumber))
                        cellContent = $"[green]🪜{cellContent}[/]";
                    else if (skillTiles.Contains(cellNumber))
                        cellContent = $"[blue]✨{cellContent}[/]";

                    var occupyingPlayers = players.Where(p => p.Position == cellNumber && !p.HasWon).ToList();
                    if (occupyingPlayers.Count > 0)
                    {
                        cellContent = string.Join("", occupyingPlayers.Select(p => $"[black on {p.Color}]{p.Name.Substring(0, 4)}[/]"));
                    }

                    rowCells.Add(cellContent);
                }
                table.AddRow(rowCells.ToArray());
            }

            //GAME INFO PANEL
            var rightPanel = new Panel(
                new Rows(
                    new Markup($"[bold]Current Turn:[/] [{players[currentPlayerIndex].Color}]{players[currentPlayerIndex].Name}[/]"),
                    new Text(""),
                    lastRoll.HasValue ? new Markup($"[bold]Last Roll:[/] {lastRoll}") : new Markup("[bold]Last Roll:[/] -"),
                    new Text(""),
                    !string.IsNullOrEmpty(lastSkillUsed) ? new Markup($"[bold]Last Skill Used:[/] {lastSkillUsed}") : new Markup("[bold]Last Skill Used:[/] -"),
                    new Text(""),
                    !string.IsNullOrEmpty(lastAction) ? new Markup($"[bold]Last Action:[/] {lastAction}") : new Markup("[bold]Last Action:[/] -")
                ))
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Yellow)
                .Header("[bold]Game Info[/]", Justify.Center); 

            var statusTable = new Table().Border(TableBorder.Simple);
            statusTable.AddColumns(
                new TableColumn("Player").Width(15),
                new TableColumn("Position").Width(8).Centered(),
                new TableColumn("Skills").Width(20),
                new TableColumn("Status").Width(12).Centered());

            foreach (var player in players.OrderBy(p => p.HasWon))
            {
                string status = player.HasWon ? "[green]WINNER![/]" :
                              player.SkipTurn ? "[red]STUNNED[/]" : 
                              player.IsShielded ? "[cyan]SHIELDED[/]":  
                              player.isAnchored ? "[orange1]ANCHORED[/]": "[yellow]ACTIVE[/]";
                statusTable.AddRow(
                    $"[{player.Color}]{player.Name}[/]",
                    $"[bold]{player.Position}[/]",
                    player.Skills.Count > 0 ? string.Join(", ", player.Skills) : "None",
                    status);
            }

            statusTable.AddEmptyRow();
            statusTable.AddRow("[bold cyan]Legend[/]", "", "", "");
            statusTable.AddRow("[green]🪜Ladder[/]", "", "", "");
            statusTable.AddRow("[red]🐍Snake[/]", "", "", "");
            statusTable.AddRow("[blue]✨Skill Tile[/]", "", "", "");

            var rightPanelContent = new Rows(statusTable);

            var grid = new Grid();
            grid.AddColumn().AddColumn();
            grid.AddRow(table, rightPanelContent);

            AnsiConsole.Clear();
            AnsiConsole.Write(grid);
            AnsiConsole.Write(rightPanel);
        }

        public int RollDice(List<Player> players, int currentPlayerIndex)
        {
            AnsiConsole.MarkupLine($"[{players[currentPlayerIndex].Color}]{players[currentPlayerIndex].Name}[/][yellow] Press any key to roll the dice...[/]");
            Console.ReadKey();
            AnsiConsole.Status()
                .Start("ROLLING THE DICE...", ctx =>
                {
                    Thread.Sleep(800);

                    ctx.Spinner(Spinner.Known.Moon);
                    ctx.SpinnerStyle(Style.Parse("red"));
                });
            int roll = random.Next(1, 7);
            lastAction = $"[bold white on blue]{players[currentPlayerIndex].Name} rolled a {roll}![/]";
            return roll;
        }

        public void MovePlayer(Player player, int roll)
        {
            int newPosition = player.Position + roll;
            if (newPosition > 100)
            {
                lastAction = $"[yellow]{player.Name} stays at {player.Position} (Roll exceeds 100)[/]";
                return;
            }

            player.Position = newPosition;
            lastAction = $"[bold white]{player.Name} moves to {newPosition}[/]";

            if (snakes.ContainsKey(newPosition))
            {
                if (player.isAnchored)
                {
                    soundfx.anchorSound();
                    player.Skills.Remove("Anchor ⚓");
                    player.isAnchored = false;
                    lastAction = $"[bold green]{player.Name} used Anchor to resist the snake![/]";
                }
                else if (player.Skills.Contains("Shield 🛡️"))
                {
                    soundfx.shieldSound();
                    player.IsShielded = false;
                    player.Skills.Remove("Shield 🛡️");
                    lastAction = $"[bold green]{player.Name} blocked snake with Shield![/]";
                }
                else
                {
                    soundfx.snakeBiteSound();
                    player.Position = snakes[newPosition];
                    lastAction = $"[red]🐍 {player.Name} got bitten! Moves down to {snakes[newPosition]}[/]";
                }
            }
            else if (ladders.ContainsKey(newPosition))
            {
                soundfx.climbLadderSound();
                player.Position = ladders[newPosition];
                lastAction = $"[green]🪜 {player.Name} climbed a ladder! Moves up to {ladders[newPosition]}[/]";
            }

        }

        public void UseSkill(Player player, List<Player> players)
        {
            var skillChoices = new List<string>();
            for (int i = 0; i < player.Skills.Count; i++)
            {
                skillChoices.Add($"{i + 1}. {player.Skills[i]}");
            }
            skillChoices.Add("Cancel");

            var skillSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[{player.Color}]{player.Name}'s Skills:[/]")
                    .AddChoices(skillChoices));

            if (skillSelection == "Cancel") return;

            int skillIndex = int.Parse(skillSelection.Split('.')[0]) - 1;
            string skill = player.Skills[skillIndex];
            player.Skills.RemoveAt(skillIndex);
            lastSkillUsed = $"[{player.Color}]{player.Name}[/] used [bold]{skill}[/]";
            lastAction = $"[bold white on red]{player.Name} used {skill}![/]";

            if (skill == "Stun ⚡" || skill == "Sabotage 💣" || skill == "Swap 🔄")
            {
                var targetPlayers = players.Where(p => p != player && !p.HasWon).ToList();
                if (targetPlayers.Count == 0)
                {
                    lastAction = $"[red]No valid players to target![/]";
                    return;
                }

                var targetSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select target player:")
                        .AddChoices(targetPlayers.Select(p => p.Name)));

                var targetPlayer = targetPlayers.First(p => p.Name == targetSelection);
                ExecuteSkill(skill, player, targetPlayer);
            }
            else if (skill == "Dice Manipulation 🎲")
            {
                var rollChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose your dice roll:")
                        .AddChoices(new[] { "1", "2", "3", "4", "5", "6" }));

                int chosenRoll = int.Parse(rollChoice);
                lastAction = $"[bold white on red]{player.Name} chose to roll a {chosenRoll}![/]";
                MovePlayer(player, chosenRoll);
                CheckSkillTile(player);
            }
            else if (skill == "Shield 🛡️")
            {
                soundfx.shieldSound();
                player.IsShielded = true;
                lastAction = $"[green]{player.Name} is shielded with 🛡️![/]";
            }
            else if (skill == "Anchor ⚓")
            {
                soundfx.anchorSound();
                player.isAnchored = true;
                lastAction = $"[green]{player.Name} is anchored with ⚓![/]";
            }
        }

        public void ExecuteSkill(string skill, Player player, Player targetPlayer)
        {
            if(skill == "Stun ⚡")
{
                if (targetPlayer.IsShielded)
                {
                    soundfx.shieldSound();
                    targetPlayer.IsShielded = false;
                    lastAction += $"\n[{targetPlayer.Color}]{targetPlayer.Name}[/] [green]blocked the stun with 🛡️ Shield![/]";
                }
                else
                {
                    soundfx.stunSound();
                    Console.ReadKey();
                    targetPlayer.SkipTurn = true;
                    lastAction += $"\n[red]{targetPlayer.Name}[/] [green]is stunned ⚡ and will skip next turn![/]";
                }
             }
            else if (skill == "Swap 🔄")
            {
                if (targetPlayer.IsShielded)
                {
                    targetPlayer.IsShielded = false;
                    lastAction += $"\n[{targetPlayer.Color}]{targetPlayer.Name}[/] [green]blocked the swap with 🛡️ Shield![/]";
                }
                else
                {
                    soundfx.swapSound();
                    (player.Position, targetPlayer.Position) = (targetPlayer.Position, player.Position);
                    lastAction += $"\n[{targetPlayer.Color}]{player.Name}[/] [green]swapped positions 🔄 with {targetPlayer.Name}![/]";
                }
            }
            else if (skill == "Sabotage 💣")
            {
                if (targetPlayer.IsShielded)
                {
                    targetPlayer.IsShielded = false;
                    lastAction += $"\n[{targetPlayer.Color}]{targetPlayer.Name}[/] [green]blocked the sabotage with 🛡️ Shield![/]";
                }
                else
                {
                    soundfx.sabotageSound();
                    int sabotageRoll = random.Next(1, 7);
                    targetPlayer.Position = Math.Max(0, targetPlayer.Position - sabotageRoll);
                    lastAction += $"\n[{targetPlayer.Color}]{targetPlayer.Name}[/] [green]was sabotaged 💣 and moved back {sabotageRoll} spaces! ⬇️[/]";
                }
            }
        }

        public void CheckSkillTile(Player player)
        {
            if (skillTiles.Contains(player.Position))
            {
                if (player.Skills.Count >= 2)
                {
                    lastAction = $"[blue]{player.Name} cannot acquire more skills. Maximum skill limit reached![/]";
                    return;
                }

                string newSkill;
                do
                {
                    newSkill = availableSkills[random.Next(availableSkills.Length)];
                } while (player.Skills.Contains(newSkill));
                soundfx.PlayObtainSkill();
                player.Skills.Add(newSkill);
                lastAction = $"[blue]{player.Name} acquired {newSkill}![/]";
                lastSkillUsed = $"[{player.Color}]{player.Name}[/] got [bold]{newSkill}[/]";
            }
        }

        public void ResetGameState(int currentPlayerIndex)
        {
            lastRoll = null;
            lastAction = "";
            lastSkillUsed = "";
            currentPlayerIndex = 0;
            gameEnded = false;
        }

    }
}
