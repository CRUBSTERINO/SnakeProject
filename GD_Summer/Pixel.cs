//Перепелицын Владимир
namespace GD_Summer
{
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
            StringBuilder.AddToArray(this);
        }
    }
}
