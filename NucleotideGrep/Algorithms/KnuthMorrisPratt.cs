using System;
using System.Collections.Generic;
using NucleotideGrep.ADTs;

namespace NucleotideGrep.Algorithms
{
    /// <summary>
    /// NOT Implemented
    /// </summary>
    sealed class KnuthMorrisPratt : NucleotideContextGrep
    {
        public KnuthMorrisPratt(
            Nucleotide[] tPattern,
            int xPrior,
            int yFollowing
            ) : base(tPattern, xPrior, yFollowing)
        {
            throw new NotFiniteNumberException();
        }

        protected override IEnumerable<string> GetLeadInMatches()
        {
            throw new NotImplementedException();
        }

        protected override bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetTailOutMatches()
        {
            throw new NotImplementedException();
        }

    }
}
