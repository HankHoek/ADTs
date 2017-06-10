using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBlack.Core.Collections.Generic;

namespace NucleotideGrep.ADTs
{
    interface IContextMatchNucleotides
    {
        bool HasCompleteMatchOnAdd(Nucleotide.Nucleotide2Bits bits, out string contextMatch);
        IEnumerable<string> GetTailContextMatches();
    }

    public abstract class NucleotideContextGrep : IContextMatchNucleotides
    {
        protected CircularBuffer<byte> Buffer;

        protected NucleotideContextGrep(
            string tPattern,
            int xPrior,
            int yFollowing
            )
        {
            int elementsCnt = xPrior + tPattern.Length + yFollowing;

            Buffer = new CircularBitsBuffer<byte>(elementsCnt: elementsCnt, bitsPerElement: Nucleotide.NucleotideBitCnt);
        }

        public abstract bool HasCompleteMatchOnAdd(Nucleotide.Nucleotide2Bits bits, out string contextMatch);
        public abstract IEnumerable<string> GetTailContextMatches();

    }

}
