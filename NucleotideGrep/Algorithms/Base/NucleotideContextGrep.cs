using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using JetBlack.Core.Collections.Generic;

namespace NucleotideGrep.Algorithms
{
    interface IContextMatchNucleotides
    {
        bool HasCompleteMatchOnAdd(Nucleotide nucleotide, out string contextMatch);
        IEnumerable<string> GetTailContextMatches();
    }

    public abstract class NucleotideContextGrep : IContextMatchNucleotides
    {
        protected Nucleotide[] TPattern;
        protected CircularBuffer<Nucleotide> Buffer;

        protected readonly int XPrior;
        protected readonly int YFollowing;
        protected int TPatternOffset;

        protected NucleotideContextGrep(
            Nucleotide[] tPattern,
            int xPrior,
            int yFollowing
            )
        {
            this.TPattern = tPattern;
            this.XPrior = xPrior;
            this.YFollowing = yFollowing;

            int elementsCnt = xPrior + tPattern.Length + yFollowing;

            Buffer = new CircularBuffer<Nucleotide>(elementsCnt);
        }

        public abstract bool HasCompleteMatchOnAdd(Nucleotide nucleotide, out string contextMatch);
        public abstract IEnumerable<string> GetTailContextMatches();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("XPrior={0}  YFollowing={1}, TPatternOffset={2}, ",
                XPrior, YFollowing, TPatternOffset));

            sb.Append("Buffer:  ");
            foreach (var element in Buffer)
                sb.Append(element.Char);
            sb.AppendLine();

            return sb.ToString();
        }
    }

}
