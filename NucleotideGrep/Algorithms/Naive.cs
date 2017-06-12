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
    /// Runtime is O(streamLength * PatternLength + contextMatches*outLength).
    /// Comparison-cost per nucleotide is O(PatternLength).
    ///     For comparison, RabinKarp can update a pre-filter rolling-hash in O(1) time per nucleotide.
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

        protected override bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch)
        {

            //  Update the buffer
            base.Buffer.Enqueue(nucleotide);

            //  Test for tPattern match
            TPatternOffset = XPrior;
            for (int i = 0; i < TPattern.Length; i++)
            {
                if (TPattern[i].Ascii != Buffer[TPatternOffset++].Ascii)
                {
                    contextMatch = null;
                    return false;
                }
            }

            //  Build contextMatch string
            var sb = new StringBuilder(Buffer.Capacity);
            foreach (var myNucleotide in Buffer)
            {
                sb.Append(myNucleotide.Char);
            }
            contextMatch = sb.ToString();

            return true;
        }
        protected override IEnumerable<string> GetLeadInMatches()
        {
            int maxOffset = Buffer.Count - TPattern.Length - YFollowing;
            //  Spool forward from start through expected 
            for (int offset = 0; offset <= maxOffset; offset++)
            {
                if (IsMatch(TPattern, Buffer, offset))
                {
                    //  print earliest matches with reduced length as needed.
                    int length = Math.Min(Buffer.Count, offset + TPattern.Length + YFollowing);
                    var sb = new StringBuilder(Buffer.Capacity);

                    //  prepend spaces to align match
                    int firstOffset = XPrior - offset;
                    for (int i = 0; i < firstOffset; i++)
                        sb.Append(' ');

                    //  Add the context-matched string
                    for (int i = 0; i < length; i++)
                        sb.Append(Buffer[i].Char);

                    yield return sb.ToString();
                }
            }
        }
        protected override IEnumerable<string> GetTailOutMatches(bool eofDuringLeadIn)
        {
            int startOffset = eofDuringLeadIn && XPrior + YFollowing >= Buffer.Count 
                ? XPrior : XPrior + 1;
            int maxOffset = Buffer.Count - TPattern.Length;
            for (int offset = startOffset; offset <= maxOffset; offset++)
            {
                if (IsMatch(TPattern, Buffer, offset))
                {
                    //  print earliest matches with reduced length as needed.
                    var sb = new StringBuilder(Buffer.Capacity);

                    //  Add the context-matched string
                    int firstOffset = Math.Max(0, offset - XPrior);
                    for (int i = firstOffset; i < Buffer.Count; i++)
                        sb.Append(Buffer[i].Char);

                    yield return sb.ToString();
                }
            }
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
