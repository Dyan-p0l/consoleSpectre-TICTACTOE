using Spectre.Console;
using System.Text.RegularExpressions;

namespace TICTACTOE_MIDTERM.styling
{
    internal class Display
    {
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
