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

            string testStream = "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe";

            using (TextReader tr = new StringReader(testStream))
            {
                NucleotideContextGrep grep = new Naive( //new RabinKarp(
                    tPattern: tPattern,
                    xPrior: x,
                    yFollowing: y);

                Nucleotide nucleotide;

                //  Fill grep's contextBuffer before evaluating any lead-in matches.
                while (!(nucleotide = Next(tr)).IsEOF)   //  NOTE:  Missing EOFValue throws exception on end-of-stream, since it breaks spec.
                {
                    if (grep.HasCompleteContextOnAdd(nucleotide))
                        break;
                }

                //  output any lead-in matches
                foreach(string contextMatch in grep.GetLeadInMatches())
                {
                    Console.WriteLine(contextMatch);    //  x may be incomplete, but y is populated if possible.
                    Console.WriteLine(grep.Marker);
                }

                //  output any rolling buffer matches
                string rollingContextMatch = null;
                while (!(nucleotide = Next(tr)).IsEOF)   //  NOTE:  Missing EOFValue throws exception on end-of-stream, since it breaks spec.
                {
                    if (grep.HasCompleteMatchOnAdd(nucleotide, ref rollingContextMatch))
                    {
                        Console.WriteLine(rollingContextMatch);    //  x, isPattern and y are populated. this loop should be perf-optimized.
                        Console.WriteLine(grep.Marker);
                    }
                    grep.ToString();
                }

                foreach (string tailContextMatch in grep.GetTailOutMatches())
                {
                    Console.WriteLine(tailContextMatch);    //  y is incomplete, approaching EOF.
                    Console.WriteLine(grep.Marker);
                }




                //    if (grep.IsFilledYFollowing)
                //if(grep.TryGetMatchWithFilledTrailingBuffer())
                //ReturnMatchIfTrailingBuffer(nucleotide)

                //Nucleotide.Nucleotide2Bits n2 = nucleotide.As2BitEnum();

                //buffer.Add(n2);
                //if(! IsPopulatedTrailingBuffer)
                ////  PERF-NOTE:  Since subsequent operations deal with 2-bit Nucleotide.Enums, input-stream


                ////buffer.


                //Console.Write(nucleotide.Char);
                //}


            }

        }

        static void PrintMatchesWithContext(string target)
        {

        }

        //  For convenience in .NET -- beware IO bottleneck.
        static Nucleotide Next(TextReader tr)
        {
            return new Nucleotide { Char = (char)tr.Read() };
        }

        //  For speed, when reading raw ascii bytes.
        static Nucleotide Next(BinaryReader br)
        {
            return new Nucleotide { Ascii = br.ReadByte() };
        }
    }
}
