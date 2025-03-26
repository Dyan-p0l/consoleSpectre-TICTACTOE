using Spectre.Console;
using System;

namespace TICTACTOE_MIDTERM.styling
{
    internal class PrintFormat
    {

        public void printCenter(string text)
        {
            Display display = new Display();
            Gradient gradient = new Gradient(Color.Yellow, Color.Orange3);
            display.centerSpace(gradient.GetGradientText(text));
        }

        public void printCenterGreen(string text)
        {
            Display display = new Display();
            Gradient gradient = new Gradient(Color.GreenYellow, Color.Blue);
            display.centerSpace(gradient.GetGradientText(text));
        }

        public void printCenterRed(string text)
        {
            Display display = new Display();
            Gradient gradient = new Gradient(Color.Orange1, Color.Red1);
            display.centerSpace(gradient.GetGradientText(text));
        }

        public void printCenterBlue(string text)
        {
            Display display = new Display();
            Gradient gradient = new Gradient(Color.LightSteelBlue1, Color.Blue1);
            display.centerSpace(gradient.GetGradientText(text));
        }

        public void print(string text)
        {
            Gradient gradient = new Gradient(Color.Yellow, Color.Orange3);
            AnsiConsole.Markup(gradient.GetGradientText(text));
        }

    }
}
