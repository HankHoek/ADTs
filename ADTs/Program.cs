using System;
using ADTs.PriorityQueue;

namespace ADTs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var heap = new MinHeap<long>();
            Console.WriteLine(heap.ToString());
            foreach (var entry in new long[] {
                9,8,7,6,5,4,3,2,888,
                9,8,7,6,5,4,3,2,888,
                9,8,7,6,5,4,3,2,888,
            })
            {
                heap.Add(entry);
                Console.WriteLine(heap.ToString());
            }

            int initialHeapCount = heap.Count;
            int cnt = 0;
            long priorMin = long.MaxValue;
            while(heap.Count > 0)
            {
                Console.WriteLine("=================");
                var min = heap.ExtractMin();
                Console.WriteLine(min);
                Console.WriteLine(heap.ToString());
                if (min > priorMin)
                    throw new Exception("Bad sort-order on ExtractMin.");
                cnt++;
            }
            if (initialHeapCount != cnt)
                throw new Exception("Bad ExtractMin loop.");
        }
    }
}