using System;
using System.Collections.Generic;
using NucleotideGrep.ADTs;

/*
 *  https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore_string_search_algorithm#The_Galil_Rule
 *  https://stackoverflow.com/questions/38206841/boyer-moore-galil-rule
 *  http://www.blackbeltcoder.com/Articles/algorithms/fast-text-search-with-boyer-moore
 * 
 * Boyer-Moore may provide the fastest c# algorithm. 
 *
 * Boyer-Moore should provide good matching, with a worst-case performance caveat:
 *  Worst-case performance is for long patterns of e.g. "aaa...aaa" over stream "aaaaaa...".
 *      Worst-case performance is of the same order as the output printing, so we could choose to ignore it.
 *      
 *  Adding support for the Galil rule would improve worst-case match-inference.
 *      If we are ok with output-batching and some spec modification, the Galil rule could reduce worst-case Match-cost:
 *          from O(Input + outputMatches*Length) to O(Input + OutputMatches*Length/MaxBatchSize)
 *          
 *          e.g. for MaxBatchSize of 16 below:
 *              "16 instances of contextMatch:   aaaaaaaaaaaaaaa"
 *              "16 instances of contextMatch:   aaaaaaaaaaaaaaa"
 *              " 5 instances of contextMatch:   aaaaaaaaaaaaaaa"
 * 
 * Worst-case would approach O(input-size) for large values of BatchSize.
 * Buffer-Size could be kept small, since consecutive repetition makes context inferrable for central matches.
 * 
 * Implementation:
 *  Copy-paste-modify for Boyer-Moore should be relatively cheap.
 *  Implementing the Galil rule should be OK.
 *  Adding output batching should be OK.
 *  Optimizing output batching for large MaxBatchSize under strong memory-constraints should be OK, but probably painful.
 *  
 * Conclusion:  Probably better to demonstrate something in GoLang than go further down this path in C#.
 */

namespace NucleotideGrep.Algorithms
{
    /// <summary>
    /// NOT Implemented
    /// </summary>
    sealed class BoyerMoore : NucleotideContextGrep
    {
        public BoyerMoore(
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
