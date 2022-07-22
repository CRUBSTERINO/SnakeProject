using System;
using System.Threading;

//Перепелицын Владимир
namespace GD_Summer
{
    public class Apple : GameObject
    {
        public Apple(int x, int y, char symbol, ConsoleColor color) : base(x, y, symbol, color)
        { }
    }

    public class AppleSpawner
    {
        private int _spawnInterval;

        private int _spawnX;
        private int _spawnY;

        private Timer timer;
        public AppleSpawner(int spawnInterval)
        {
            _spawnInterval = spawnInterval;

            StartTimer();
        }

        private void GenerateCords(object state)
        {
            var random = new Random();
            _spawnX = random.Next(1, Const.ScreenWidth - 2);
            _spawnY = random.Next(1, Const.ScreenHeight - 2);
        }

        private void InstantiateApple(object state)
        {
            GameObject.Instantiate(new Apple(_spawnX, _spawnY, DisplaySymbols.AppleSymbol, DisplayColors.AppleColor));
        }

        private void StartTimer()
        {
            var timerCallback = new TimerCallback(GenerateCords);
            timerCallback += InstantiateApple;

            timer = new Timer(timerCallback, null, 0, _spawnInterval);
        }
    }
}
