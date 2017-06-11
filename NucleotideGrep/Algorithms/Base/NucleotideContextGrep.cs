using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using JetBlack.Core.Collections.Generic;

namespace NucleotideGrep.Algorithms
{
    interface IContextMatchNucleotides
    {
        bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch);
        IEnumerable<string> GetTailOutMatches();
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

        public bool HasCompleteContextOnAdd(Nucleotide nucleotide)
        {
            Buffer.Enqueue(nucleotide);
            return Buffer.Count == Buffer.Capacity;
        }
        public abstract IEnumerable<string> GetLeadInMatches();
        public abstract bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch);
        public abstract IEnumerable<string> GetTailOutMatches();

        public string Marker
        {
            get
            {
                var sb = new StringBuilder();
                for (int i = 0; i < XPrior; i++) sb.Append(' ');
                for (int i = 0; i < TPattern.Length; i++) sb.Append('=');
                return sb.ToString();
            }
        }

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
