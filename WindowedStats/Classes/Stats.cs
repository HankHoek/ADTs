using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowedStats.Classes
{
    class Stats
    {
        Stat[] _stats;

        public Stats(IEnumerable<Stat> stats)
        {
            _stats = stats.ToArray();
        }


    }
}
