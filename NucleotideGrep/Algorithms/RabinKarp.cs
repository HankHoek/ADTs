using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public override bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch)
        {
            base.Buffer.Enqueue(nucleotide);

            //  

            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetTailOutMatches()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetLeadInMatches()
        {
            throw new NotImplementedException();
        }
    }
}
