using JetBlack.Core.Collections.Generic;
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
        private Heap<int> MaxHeap;

        int _value;
        public override double Value
        {
            get
            {
                return MaxHeap.Count >= base.Window.Lookback ? _value : double.NaN;
            }
        }

        public Max(Window window) : base(window)
        {
            MaxHeap = new Heap<int>(10, Comparer<int>.Default, true);
        }

        public override void Observe(int add, int? bufferDrop, CircularBuffer<int> circularBuffer)
        {
            MaxHeap.Add(add);
            _value = MaxHeap.Pop();
            if (MaxHeap.Count < Window.Lookback)
                MaxHeap.Push(_value);
        }
    }
}
