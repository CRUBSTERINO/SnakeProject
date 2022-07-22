using System;

namespace GD_Summer
{
    public static class Const
    {
        public const int ScreenWidth = 100;
        public const int ScreenHeight = 45;

        public const int MapWidth = 100;
        public const int MapHeight = 45;

        public const int UIWidth = 0;
        public const int UIHeight = 0;
    }

    public static class DisplaySymbols
    {
        public const char SnakeSymbol = '*';
        public const char BorderSymbol = '█';
        public const char AppleSymbol = '#';
    }

    public static class DisplayColors
    {
        public const ConsoleColor SnakeColor = ConsoleColor.White;
        public const ConsoleColor BorderColor = ConsoleColor.White;
        public const ConsoleColor AppleColor = ConsoleColor.Green;
    }
}
