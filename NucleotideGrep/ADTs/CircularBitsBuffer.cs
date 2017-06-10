////  Abandoned -- 4:1 compression without a spec requirement is silly.
////  Abandoned -- 4:1 compression without a spec requirement is silly.
////  Abandoned -- 4:1 compression without a spec requirement is silly.

//using System;
//using System.Collections;
//using System.Collections.Generic;
////using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NucleotideGrep.ADTs
//{
//    /// <summary>
//    /// Space-efficient CircularBuffer can store ACGT alphabet in 2 bits per Nucleotide.
//    /// Adding a next element to the buffer overwrites the oldest element.
//    /// </summary>
//    public class CircularBitsBuffer <T> : IEnumerable <T>
//    {
//        int BitsPerElement;
//        int ElementsCnt;

//        const int BucketLength = 64;
//        System.Int64[] _buffer;

//        int Head;
//        int Tail;

//        bool IsRecycling; 

//        public CircularBitsBuffer(int elementsCnt, int bitsPerElement)
//        {
//            if (bitsPerElement > 64)
//                throw new NotImplementedException("Max supported bitsPerElement is 64.");
//            BitsPerElement = bitsPerElement;
//            ElementsCnt = elementsCnt;

//            Tail = -1;  //  Initially -1 == implicitly empty buffer
//            IsRecycling = false;      //  While false, buffer starts at offset zero, else at next logicalOffset.

//            int bits = ElementsCnt * BitsPerElement;
//            int longArrayLength = (bits + 63) % 64;
//            _buffer = new long[longArrayLength];
//        }

//        public void Add(T element)
//        {
//            ++Tail;
//            if (Tail >= ElementsCnt)
//            {
//                Tail = 0;
//                IsRecycling = true;
//            }

//            this[Tail] = element;
//        }

//        // Indexer reads the CircularBuffer as a logical array of Nucleotides
//        // from cb[0] == oldest to cb[cb.Length-1] == latest.
//        public T this[int i]
//        {
//            get
//            {
//                int offsetBase = IsRecycling ? Tail + 1 : 0;
//                int logicalOffset = (offsetBase + i) % ElementsCnt;
//                int bufferOffset = logicalOffset / BucketLength;
//                int bitsOffset = logicalOffset % BucketLength;



//                throw new NotImplementedException();
//                //return Nucleotide.Nucleotide2Bits.A;
//            }
//            set
//            {
//            }
//        }

//        public IEnumerator<T> GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        public override string ToString()
//        {
//            var sb = new StringBuilder();
//            for (int i = 0; i < ElementsCnt; i++)
//                sb.Append(new Nucleotide { Char = 'A' });

//            return base.ToString();
//        }


//    }
//}
