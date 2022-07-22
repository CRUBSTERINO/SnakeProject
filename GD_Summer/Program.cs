using System;
using System.Collections.Generic;
using System.Threading;


//Перепелицын Владимир
namespace GD_Summer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.SetWindowSize(Const.ScreenWidth, Const.ScreenHeight);
            Console.SetBufferSize(Const.ScreenWidth, Const.ScreenHeight);
            Console.CursorVisible = false;

            var snakeGame = new SnakeGame();
            snakeGame.StartLoop();
        }
    }

    public static class StringBuilder
    {
        private static char[] charArray = new char[Const.ScreenWidth * Const.ScreenHeight];

        public static void AddToArray(Pixel pixel)
        {
            Console.ForegroundColor = pixel.Color;
            charArray[pixel.PostionY * Const.MapWidth + pixel.PostionX] = pixel.Symbol;
        }

        public static string ReturnString() => new string(charArray);

        public static void ClearArray()
        {
            Array.Clear(charArray, 0, charArray.Length);
        }
    }

    public class SnakeGame : GameLoop
    {
        protected override int _timing { get; set; }
        private int _appleSpawnDelay = 5000;
        
        private Snake snake;
        private AppleSpawner appleSpawner;

        public SnakeGame()
        {
             snake = new Snake(Const.MapWidth / 2, Const.MapHeight / 2, DisplaySymbols.SnakeSymbol, DisplayColors.AppleColor);
            appleSpawner = new AppleSpawner(_appleSpawnDelay);
            _timing = 100;
        }

        protected override void Input()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                snake.CheckDirection(key.Key);
            }
        }

        protected override void Update()
        {
            snake.SnakeUpdate();
        }

        protected override void Render()
        {
            StringBuilder.ClearArray();

            Borders.DrawBorders();
            snake.DrawPixel();
            GameWorld.DrawGameObjects();

            Console.SetCursorPosition(0, 0);
            Console.Write(StringBuilder.ReturnString());
        }
    }

    public class GameWorld
    {
        public static List<GameObject> _gameObjects = new List<GameObject>();

        public static void DrawGameObjects()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.DrawPixel();
            }
        }
    }

    public class Snake : GameObject
    {
        private enum SnakeDirection
        {
            Right,
            Left,
            Up,
            Down
        }

        private SnakeDirection direction = SnakeDirection.Right;
        private const int snakeMoveSpeed = 1;

        public Snake(int x, int y, char symbol, ConsoleColor color) : base(x, y, symbol, color)
        { }

        public void CheckDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    direction =  SnakeDirection.Left;
                    break;
                case ConsoleKey.D:
                    direction =  SnakeDirection.Right;
                    break;
                case ConsoleKey.W:
                    direction = SnakeDirection.Up;
                    break;
                case ConsoleKey.S:
                    direction = SnakeDirection.Down;
                    break;
                default:
                    break;
            }
        }

        public void SnakeUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            switch (direction)
            {
                case SnakeDirection.Right:
                    PostionX += snakeMoveSpeed;
                    break;
                case SnakeDirection.Left:
                    PostionX -= snakeMoveSpeed;
                    break;
                case SnakeDirection.Down:
                    PostionY += snakeMoveSpeed;
                    break;
                case SnakeDirection.Up:
                    PostionY -= snakeMoveSpeed;
                    break;
            }
            CheckBorderCollision();

            void CheckBorderCollision()
            {
                if (PostionX >= Const.MapWidth - 1) PostionX = 1;
                else if (PostionX <= 0) PostionX = Const.MapWidth - 2;
                else if (PostionY >= Const.MapHeight - 1) PostionY = 1;
                else if (PostionY <= 0) PostionY = Const.MapHeight - 2;
            }
        }
    }

    public static class Borders
    {
        public static void DrawBorders()
        {
            for (int i = 0; i < Const.MapWidth; i++)
            {
                var topBorder = new Pixel(i, 0, DisplaySymbols.BorderSymbol, DisplayColors.BorderColor);
                var bottomBorder = new Pixel(i, Const.MapHeight - 1, DisplaySymbols.BorderSymbol, DisplayColors.BorderColor);

                topBorder.DrawPixel();
                bottomBorder.DrawPixel();
            }

            for (int i = 0; i < Const.MapHeight; i++)
            {
                var leftBorder = new Pixel(0, i, DisplaySymbols.BorderSymbol, DisplayColors.BorderColor);
                var rightBorder = new Pixel(Const.MapWidth - 1, i, DisplaySymbols.BorderSymbol, DisplayColors.BorderColor);

                leftBorder.DrawPixel();
                rightBorder.DrawPixel();
            }
        }
    }
}
