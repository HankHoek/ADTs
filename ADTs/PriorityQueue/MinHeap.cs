using System;
using System.Collections.Generic;
using System.Text;

namespace ADTs.PriorityQueue
{
    class MinHeap<T> where T:IComparable<T>
    {
        public int Count { get { return List.Count; } }

        private List<T> List = new List<T>();

        public void Add(T element)
        {
            List.Add(element);
            BubbleUp(List.Count - 1);
        }

        public T ExtractMin()
        {
            if (List.Count == 0)
                throw new Exception("Empty Heap Exception, cannot ExtractMin().");

            T min = List[0];

            PullUp();
            return min;
        }

        /// <summary>
        /// Replace top element with MinChild recursively.
        /// Backfill bottom with last element, drop last element.
        /// Bubble-up last element backfill.
        /// </summary>
        private void PullUp()
        {
            int maxOffset = List.Count - 1;
            int maxParentOffset = (maxOffset - 1) / 2;
            int pullOffset=maxOffset;
            if (List.Count > 1)
            {
                for (int parentOffset = 0; parentOffset <= maxParentOffset; parentOffset = pullOffset)
                {
                    int firstChildOffset = 2 * parentOffset + 1;
                    int secondChildOffset = 2 * parentOffset + 2;

                    pullOffset = secondChildOffset >= List.Count ? firstChildOffset
                        : IsLessThan(List[firstChildOffset], List[secondChildOffset]) ? firstChildOffset
                        : secondChildOffset;

                    List[parentOffset] = List[pullOffset];  //  Replace parent with minChild recursively.
                }
            }
            List[pullOffset] = List[maxOffset]; //  Rackfill with last element.
            List.RemoveAt(maxOffset);           //  Drop the last element.
            if (pullOffset != maxOffset)
                BubbleUp(pullOffset);
        }

        private void BubbleUp(int childOffset)
        {
            var lastOffset = List.Count - 1;
            while(childOffset > 0)
            {
                var parentOffset = (childOffset - 1) / 2;
                if(IsLessThan(List[childOffset], List[parentOffset]))
                {
                    Swap(childOffset, parentOffset);
                }
                childOffset = parentOffset;
            }
        }

        private void Swap(int offsetA, int offsetB)
        {
            T temp = List[offsetA];
            List[offsetA] = List[offsetB];
            List[offsetB] = temp;
        }




        public static bool IsGreaterThan(T value, T other)
        {
            return Comparer<T>.Default.Compare(value, other) > 0;
        }

        public static bool IsLessThan(T value, T other)
        {
            return Comparer<T>.Default.Compare(value, other) < 0;
        }


        public override string ToString()
        {
            var sb = new StringBuilder("----------");

            int rank = 1;
            int cnt = 0;
            foreach (var element in List)
            {
                if (++cnt >= rank)
                {
                    sb.AppendLine();
                    rank+=cnt;
                }

                sb.Append(element);
                sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}
