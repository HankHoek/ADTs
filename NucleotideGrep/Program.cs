using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using NucleotideGrep.Algorithms;

/*
 * Implicit Design-Goals:
 *  The spec doesn't state desirable properties other than correctness.
 *  I have designed according to the below heirarchy:
 *      Correctness     -- UnitTest results
 *      Big-O Time      -- O(input + output), using RabinKarp
 *      Memory usage    -- Circular buffer, 2 bits/character
 *      Style           -- OO decomposition and encapsulation
 *      Developer time  -- 
 *      Conciseness     --  
 *      Perf-Tuning     -- 
 * 
 * Design:
 *  Use a BinaryReader of ascii bytes for input perf.
 *  Use a circular buffer to store a history of {x,isPattern,y}
 *      Since the ACGT alphabet is 4 chars + an EOF, we only need 2 bits to store the 4 chars.
 *      Assuming we are scanning DNA, we could choose not to expect worst-case behavior from an adversarial scan-stream?
 *  Algorithms:
 *      Naive -- Demonstrate do a char-walk match of patternT and isPatternT per incoming byte -- O(n*T.Length)
 *      Opportunistic -- If patternT.Length <= 32, represent patternT and isPatternT with 2bit chars in an Int64, and do an straight int-comparison.
 *  Reduce the duplication via alternate algorithm(s):  https://en.wikipedia.org/wiki/String_searching_algorithm
 *      RabinKarp -- Use a rolling hash to pre-filter:
 *          Easiest to implement from scratch
 *          Uses minimal extra memory
 *          Optimal big-O runtime if we ignore hash collisions.
 *              Comparison of a successful match is O(T.Length), but so is printing the result, so big-O of the solution isn't impacted.
 *
 *      BoyerMoore or variant -- Faster
 *      NFA/DFA
 *      index-building on the input stream seems like overkill without additional spec.
 *      
 *      Finding and consuming an appropriate library seems like the right way to go.  Not sure if ok in an interview context.
 *      
 *      
 *      Tests:
 *          0, "", 0
 *          0, "A", 0
 *          0, "A", 1
 *          1, "A", 0
 *          1, "A", 1
 *          Spec-Test
 *          ??
 *          
 */


/// <summary>
/// On-line algorithm scans a stream of nucleotides for a matching pattern T.
/// T is returned in a context of up to x preceding and y trailing characters when available.
/// </summary>
namespace NucleotideGrep
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            int y = 7;
            string T = "AGTA";
            Nucleotide[] tPattern = T
                .Select(c => new Nucleotide { Char = c })
                .ToArray();

            byte[] streamBytes = Encoding.ASCII.GetBytes("AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe");

            NucleotideContextGrep grep = NucleotideContextGrep.Create(
                NucleotideContextGrepAlgorithm.Naive,
                tPattern: tPattern, //  e.g. "AGTA"
                xPrior: x,
                yFollowing: y);

            using (MemoryStream stream = new MemoryStream(streamBytes))
            using (BinaryReader br = new BinaryReader(stream))
            {
                foreach (string contextMatch in grep.GetContextMatches(br))
                {
                    Console.WriteLine(contextMatch);    //  e.g. CAGTGAGTAGTACACC
                    Console.WriteLine(grep.Marker);     //  e.g.      ====
                }
            }

        }

    }
}
