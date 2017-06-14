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

        public override void Observe(int add, int? drop)
        {
            Cnt++;
            if (Cnt == 1)
            {
                _mean = add;
                drop = 0;
            }
            else
            {
                _mean = _mean + (add - (drop??0) - _mean) / Cnt;
            }
        }

    }
}
