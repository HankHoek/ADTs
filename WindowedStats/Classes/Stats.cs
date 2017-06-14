using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JetBlack.Core.Collections.Generic;

namespace WindowedStats.Classes
{
    class Stats
    {
        public CircularBuffer<int> SharedBuffer;
        public Stat[] AsArray;

        public Stats(IEnumerable<Stat> stats)
        {
            AsArray = stats.ToArray();

            if (AsArray.Length > 0)
                SharedBuffer = new CircularBuffer<int>(AsArray[0].Window.Lookback);
        }

        public void Observe(int add)
        {
            int? drop = SharedBuffer.Count == SharedBuffer.Capacity
                ? SharedBuffer.Dequeue()
                : default(int?);

            SharedBuffer.Enqueue(add);

            foreach (var stat in AsArray)
                stat.Observe(add, drop);
        }

        //public void Observe()
    }
}
