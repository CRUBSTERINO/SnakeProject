using System;
using System.Collections.Generic;
using System.Threading;


//Перепелицын Владимир
namespace GD_Summer
{
    public static class Const
    {
        public const int ScreenWidth = 100;
        public const int ScreenHeight = 45;

        public const int MapWidth = 80;
        public const int MapHeight = 45;

        public const int UIWidth = ScreenWidth - MapWidth;
        public const int UIHeight = ScreenHeight;
    }

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

    public class SnakeGame : GameLoop
    {
        protected override int _timing { get; set; }
        
        private Snake snake;

        private FPSCounter fpsCounter;

        public SnakeGame()
        {
             snake = new Snake(Const.MapWidth / 2, Const.MapHeight / 2, DisplaySymbols.SnakeSymbol);
            fpsCounter = new FPSCounter();
            _timing = 100;
        }

        public override void StartLoop()
        {
            while (true)
            {
                fpsCounter.GetStartFrameTime();

                base.StartLoop();

                fpsCounter.GetEndFrameTime();

                FPSUpdate();
            }
        }

        protected override void Input()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                snake.CheckDirection(key);
            }
        }

        protected override void Update()
        {
            snake.SnakeUpdate();
        }

        protected override void Render()
        {
            Console.Clear();

            Borders.DrawBorders();
            snake.DrawPixel();
        }

        private void FPSUpdate()
        {
            fpsCounter.UpdateFPSCounter();
            fpsCounter.DrawFPSCounter();
        }
    }

    public static class DisplaySymbols
    {
        public const char SnakeSymbol = '*';
        public const char BorderSymbol = '█';
    }

    public class Pixel
    {
        public int PostionX { get; set; }
        public int PostionY { get; set; }
        public char Symbol { get; set; } = ' ';

        public Pixel(int x, int y, char symbol)
        {
            PostionX = x;
            PostionY = y;
            Symbol = symbol;
        }

        public virtual void DrawPixel()
        {
            Console.SetCursorPosition(PostionX, PostionY);
            Console.Write(Symbol);
        }
    }

    public class Snake : Pixel
    {
        private enum SnakeDirection
        {
            Right,
            Left,
            Up,
            Down
        }
        /*private int snakePartsCount = 1;*/
        private SnakeDirection direction = SnakeDirection.Right;
        private const int snakeMoveSpeed = 1;

        public Snake(int x, int y, char symbol) : base(x, y, symbol)
        {}

        public void CheckDirection(ConsoleKeyInfo keyInfo)
        {
            ConsoleKey keyPressed = keyInfo.Key;
            switch (keyPressed)
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
                var topBorder = new Pixel(i, 0, DisplaySymbols.BorderSymbol);
                var bottomBorder = new Pixel(i, Const.MapHeight - 1, DisplaySymbols.BorderSymbol);

                topBorder.DrawPixel();
                bottomBorder.DrawPixel();
            }

            for (int i = 0; i < Const.MapHeight; i++)
            {
                var leftBorder = new Pixel(0, i, DisplaySymbols.BorderSymbol);
                var rightBorder = new Pixel(Const.MapWidth - 1, i, DisplaySymbols.BorderSymbol);

                leftBorder.DrawPixel();
                rightBorder.DrawPixel();
            }
        }
    }

    public class FPSCounter
    {
        private long startFrameTime;
        private long endFrameTime;

        private double framesCounter;

        public void DrawFPSCounter()
        {
            Console.SetCursorPosition(Const.MapWidth + Const.UIWidth / 2 - 3, Const.UIHeight / 2);
            Console.Write($"FPS: {framesCounter}");
        }

        public void UpdateFPSCounter()
        {
            if (endFrameTime - startFrameTime > 0)
            {
                framesCounter = 1000 / (endFrameTime - startFrameTime);
            }
            else
            {
                framesCounter = 0;
            }
        }        

        public void GetStartFrameTime()
        {
            startFrameTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public void GetEndFrameTime()
        {
            endFrameTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
