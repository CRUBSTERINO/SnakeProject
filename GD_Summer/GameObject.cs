using System;
using System.Collections.Generic;

namespace GD_Summer
{
    public class GameObject : Pixel
    {
        public GameObject(int x, int y, char symbol, ConsoleColor color) : base(x, y, symbol, color)
        { }

        public static GameObject Instantiate(GameObject gameObject)
        {
            GameWorld._gameObjects.Add(gameObject);
            return gameObject;
        }
    }
}
