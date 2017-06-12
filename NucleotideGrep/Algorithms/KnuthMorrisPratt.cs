using System;
using System.Collections.Generic;
using NucleotideGrep.ADTs;

namespace NucleotideGrep.Algorithms
{
    /// <summary>
    /// NOT Implemented
    /// 
    /// Perf-Note:
    ///     Worst-case O(streamLength + patternLength) perf-guarantee would be equivalent to BoyerMoore with Galil rule.
    ///     BoyerMoore with Galil rule is expected to out-perform KnuthMorrisPratt.
    /// </summary>
    sealed class KnuthMorrisPratt : NucleotideContextGrep
    {
        public KnuthMorrisPratt(
            Nucleotide[] tPattern,
            int xPrior,
            int yFollowing
            ) : base(tPattern, xPrior, yFollowing)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetLeadInMatches()
        {
            throw new NotImplementedException();
        }

        protected override bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetTailOutMatches(bool eofDuringLeadIn)
        {
            throw new NotImplementedException();
        }

    }
}
