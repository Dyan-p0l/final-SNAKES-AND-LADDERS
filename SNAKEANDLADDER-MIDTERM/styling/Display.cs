using Spectre.Console;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace SNAKEANDLADDER_MIDTERM.styling
{
    internal class Display
    {


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char cCharacter, int nLength, Coord dwWriteCoord, out int lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FillConsoleOutputAttribute(IntPtr hConsoleOutput, short wAttribute, int nLength, Coord dwWriteCoord, out int lpNumberOfAttrsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, Coord dwCursorPosition);

        private const int STD_OUTPUT_HANDLE = -11;

        [StructLayout(LayoutKind.Sequential)]
        struct Coord
        {
            public short X;
            public short Y;

            public Coord(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        public void ClearConsole()
        {
            IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            Coord coordScreen = new Coord(0, 0);
            int consoleSize = Console.BufferWidth * Console.BufferHeight;
            int charsWritten;

            // Fill entire screen with spaces (clear it)
            FillConsoleOutputCharacter(hConsole, ' ', consoleSize, coordScreen, out charsWritten);
            FillConsoleOutputAttribute(hConsole, 0, consoleSize, coordScreen, out charsWritten);

            // Move cursor to top-left
            SetConsoleCursorPosition(hConsole, coordScreen);
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

