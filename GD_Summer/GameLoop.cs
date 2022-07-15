using System.Threading;

namespace GD_Summer
{
    public abstract class GameLoop
    {
        protected virtual int _timing { get; set; }
        public virtual void StartLoop()
        {
            Timing();
            Input();
            Update();
            Render();
        }

        private void Timing()
        {
            Thread.Sleep(_timing);
        }

        protected abstract void Input();

        protected abstract void Update();

        protected abstract void Render();
    }
}
