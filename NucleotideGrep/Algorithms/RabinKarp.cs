using System;
using System.Collections.Generic;
using NucleotideGrep.ADTs;

namespace NucleotideGrep.Algorithms
{
    /// <summary>
    /// The RabinKarp algorithm calculates a rolling hash used to cheaply discard most mis-matches.
    /// It is an optimization for the case where we scan for a long target-string.
    /// 
    /// Worst-case O(input*patternLength) is a property of this algorithm for long patterns of e.g. "aaa...aaa" over stream "aaaaaa...".
    /// 
    /// See KnuthMorrisPratt has better guarantees, as does BoyerMoore with Galil rule.
    /// See BoyerMoore.cs for a discussion of something potentially faster.
    /// </summary>
    sealed class RabinKarp : NucleotideContextGrep
    {
        public RabinKarp(
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

        protected override IEnumerable<string> GetTailOutMatches(bool eofDuringLeadIn)
        {
            throw new NotImplementedException();
        }

    }
}
