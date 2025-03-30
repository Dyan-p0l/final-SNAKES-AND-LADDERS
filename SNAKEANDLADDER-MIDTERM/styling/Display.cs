using Spectre.Console;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace SNAKEANDLADDER_MIDTERM.styling
{
    internal class Display
    {

        public void ClearConsole()
        {

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetStdHandle(int nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

            const int STD_OUTPUT_HANDLE = -11;
            const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

            static void EnableAnsiOnWindows()
            {
                IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
                if (GetConsoleMode(handle, out uint mode))
                {
                    SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
                }
            }

            Console.Write("\u001b[2J\u001b[3J\u001b[H");

        }

        public void paddingTop()
        {
            int paddingTop = Console.WindowHeight / 8;
            for (int i = 0; i < paddingTop; i++)
            {
                Console.WriteLine();
            }
        }

        public void centerSpace(string text)
        {
            int screenWidth = Console.WindowWidth;

            string strippedText = Regex.Replace(text, @"\[(.*?)\]", "");
            int textWidth = strippedText.Length;

            int space = (screenWidth - textWidth) / 2;
            if (space < 0) space = 0;

            AnsiConsole.Markup(new string(' ', space) + text + "\n");
        }

    }
}

