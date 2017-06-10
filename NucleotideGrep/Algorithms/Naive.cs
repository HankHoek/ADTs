using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using JetBlack.Core.Collections.Generic;

namespace NucleotideGrep.Algorithms
{
    /// <summary>
    /// The naive algorithm does a string comparison at each position.
    /// Runtime is O(nT) -- StreamLength * PatternLength.
    /// Evaluating as an Online algorithm and dropping StreamLength, Runtime for each next Nucleotide is O(T) -- PatternLength.
    /// The class is sealed so the compiler can optimize the virtual method-calls.
    /// </summary>
    sealed class Naive : NucleotideContextGrep
    {
        public Naive(
            Nucleotide[] tPattern,
            int xPrior,
            int yFollowing
            ) : base(tPattern, xPrior, yFollowing)
        {
        }

        public override bool HasCompleteMatchOnAdd(Nucleotide nucleotide, out string contextMatch)
        {
            //  Lead-in-specific, Predictive branching should ignore after buffer fills.
            if (Buffer.Count < Buffer.Capacity)
            {
                if (Buffer.Count < XPrior + TPattern.Length)
                {
                    //  pre-calculate TPatternOffset only during lead-in.
                    int tPatternSentry = Buffer.Count + 1;
                    TPatternOffset = tPatternSentry - TPattern.Length;
                }

                if (Buffer.Count + 1 < TPattern.Length + YFollowing)
                {
                    //  Defer on-add Pattern-matching until the buffer is full.
                    base.Buffer.Enqueue(nucleotide);
                    contextMatch = null;
                    return false;
                }
                if(Buffer.Count + 1 == Buffer.Capacity)
                {
                    Console.Write("FirstBufferCharTested:  ");
                    for (int i = 0; i < TPatternOffset; i++)
                        Console.Write("{0}", Buffer[i]);
                    Console.WriteLine();

                    //  Spool forward from 
                    for (int offset = 0; offset < TPatternOffset; offset++)
                    {
                        if(IsMatch(TPattern, Buffer, offset))
                        {
                            //  print with reduced length as needed.
                            int length = offset + TPattern.Length + YFollowing;

                            //  Build contextMatch string
                            var stringBuilder = new StringBuilder(length);
                            for(int i = 0; i < length; i++)
                            {
                                stringBuilder.Append(Buffer[i].Char);
                            }
                            Console.WriteLine(stringBuilder.ToString());
                            //  contextMatch = stringBuilder.ToString();
                            contextMatch = "todo -- string should be output...";
                        }
                    }
                }
            }

            //  Update the buffer
            base.Buffer.Enqueue(nucleotide);

            //  Test for tPattern match
            for (int i = 0; i < TPattern.Length; i++)
            {
                if (TPattern[i].Ascii != Buffer[i + TPatternOffset].Ascii)
                {
                    contextMatch = null;
                    return false;
                }
            }

            //  Build contextMatch string
            var sb = new StringBuilder(Buffer.Capacity);
            foreach(var myNucleotide in Buffer)
            {
                sb.Append(myNucleotide.Char);
            }
            contextMatch = sb.ToString();

            return true;
        }

        public override IEnumerable<string> GetTailContextMatches()
        {
            throw new NotImplementedException();
        }

        private static bool IsMatch(Nucleotide[] tPattern, CircularBuffer<Nucleotide> buffer, int offset)
        {
            //  Test for tPattern match
            for (int i = 0; i < tPattern.Length; i++)
            {
                if (tPattern[i].Ascii != buffer[i + offset].Ascii)
                    return false;
            }
            return true;
        }
    }
}
