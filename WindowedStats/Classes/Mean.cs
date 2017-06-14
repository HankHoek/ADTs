﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowedStats.Classes
{
    class Mean : Stat
    {
        long Window;
        long Total;
        long Cnt;
        double _mean;

        public override double Value
        {
            get { return _mean; }
        }

        public override void Observe(int add, int drop)
        {
            Cnt++;
            if (Cnt == 1)
            {
                _mean = add;
                drop = 0;
            }
            else
            {
                _mean = _mean + (add - drop - _mean) / Cnt;
            }
        }

        protected Mean(long window)
        {
            Window = window;
        }
    }
}