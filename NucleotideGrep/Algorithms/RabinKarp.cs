using System;
using System.Collections.Generic;
using System.Linq;
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
            string tPattern,
            int xPrior,
            int yFollowing
            ) : base(tPattern, xPrior, yFollowing)
        {
        }

        public override bool HasCompleteMatchOnAdd(Nucleotide.Nucleotide2Bits bits, out string contextMatch)
        {
            base.Buffer.Add((int)bits);
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetTailContextMatches()
        {
            throw new NotImplementedException();
        }

    }
}
