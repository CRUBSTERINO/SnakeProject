using System;
using System.Collections.Generic;

namespace GD_Summer
{
    public class GameObject : Pixel
    {
        public static List<GameObject> _gameObjectsList = new List<GameObject>();
        public GameObject(int x, int y, char symbol, ConsoleColor color) : base(x, y, symbol, color)
        { }

        public static GameObject Instantiate(GameObject gameObject)
        {
            _gameObjectsList.Add(gameObject);
            return gameObject;
        }

        public static void DrawGameObjects()
        {
            foreach (GameObject gameObject in _gameObjectsList)
            {
                gameObject.DrawPixel();
            }
        }
    }
}
