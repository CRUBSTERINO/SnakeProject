using System;

//Перепелицын Владимир
namespace GD_Summer
{
    public class Pixel
    {
        public int PostionX { get; set; }
        public int PostionY { get; set; }
        public char Symbol { get; set; } = ' ';
        public ConsoleColor Color { get; set; } = ConsoleColor.White;

        public Pixel(int x, int y, char symbol, ConsoleColor color)
        {
            PostionX = x;
            PostionY = y;
            Symbol = symbol;
            Color = color;
        }

        public void DrawPixel()
        {
            StringBuilder.AddToArray(this);
        }
    }
}
