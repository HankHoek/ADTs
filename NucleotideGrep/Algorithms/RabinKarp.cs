using System;
using System.Collections.Generic;
using NucleotideGrep.ADTs;

namespace NucleotideGrep.Algorithms
{
    /// <summary>
    /// The RabinKarp algorithm calculates a rolling hash used to cheaply discard most mis-matches.
    /// It is an optimization for the case where we scan for a long target-string.
    /// The class is sealed so the compiler can optimize the virtual method-calls.
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
