using JetBlack.Core.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowedStats.Classes
{
    sealed class Mean : Stat
    {
        long Total;
        long Cnt;
        double _mean;

        public override double Value
        {
            get
            {
                return Cnt >= base.Window.Lookback
                  ? _mean : double.NaN;
            }
        }

        public Mean(Window window) : base(window)
        {
        }

        public override void Observe(int add, int? bufferDrop, CircularBuffer<int> circularBuffer)
        {
            int dropOffset = circularBuffer.Count - this.Window.Lookback - 1;
            int? drop = Window.Lookback == circularBuffer.Capacity ? bufferDrop
                : dropOffset < 0 ? 0
                : Cnt < this.Window.Lookback ? 0
                : circularBuffer[dropOffset];

            if (Cnt <= this.Window.Lookback)
            {
                Cnt++;
                if (Cnt <= this.Window.Lookback)
                {
                    if (Cnt == 1)
                    {
                        _mean = add;
                        drop = 0;
                        return;
                    }
                    else
                    {   //  Adding, but not dropping...
                        _mean = _mean + (add - _mean) / Cnt;
                        return;
                    }
                }
                else
                {   //  drop 
                    Cnt--;
                    ////  cnt == actual count
                    //_mean = _mean - ((drop ?? 0) - _mean) / (Cnt - 1);
                    //_mean = _mean + (add - _mean) / Cnt;
                    //return;
                }
            }
            _mean = _mean + (add - (drop??0)) / Cnt;
        }
    }
}
