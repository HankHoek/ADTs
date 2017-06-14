using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowedStats.MicroHeap;

namespace WindowedStats.Classes
{
    sealed class Max : Stat
    {
        Window Window;
        private Heap<int> MaxHeap;

        int _value;
        public override double Value
        {
            get { return _value; }
        }

        public Max(Window window)
        {
            Window = window;
            MaxHeap = new Heap<int>(10, Comparer<int>.Default, true);
        }

        public override void Observe(int add, int drop)
        {
            MaxHeap.Add(add);
            _value = MaxHeap.Pop();
            if (MaxHeap.Count < Window.Lookback)
                MaxHeap.Push(_value);
        }
    }
}
