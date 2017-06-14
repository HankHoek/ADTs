using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowedStats.Classes
{
    interface IObserve
    {
        void Observe(int add, int? drop);
    }
    interface IReport
    {
        double Value { get; }
    }

    class Window
    {
        public int Lookback;
    }

    abstract class Stat : IObserve, IReport
    {
        public Window Window { get; private set; }
        public abstract double Value { get; }

        public Stat(Window window)
        {
            Window = window;
        }

        public abstract void Observe(int add, int? drop);
    }
}
