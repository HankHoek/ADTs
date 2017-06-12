using System;
using System.IO;
using System.Linq;
using System.Text;

using NucleotideGrep.ADTs;
using NucleotideGrep.Algorithms;
using NucleotideGrep.Tests;

/*
 * Implicit Design-Goals:
 *  The spec doesn't state desirable properties other than correctness.
 *  I have designed according to the below heirarchy:
 *      Correctness     -- UnitTest results
 *      Big-O Time      -- O(input + output), For longer patterns we need an algo other than Naive.
 *      Memory usage    -- Circular buffer, ideally 2 bits/character?
 *      Style           -- OO decomposition and encapsulation
 *      Developer time  -- Excessive
 *      Conciseness     -- Insufficient
 *      Perf-Tuning     -- Only theoretical
 * 
 * Design:
 *  Use a BinaryReader of ascii bytes for input perf.
 *  Use a circular buffer to store a history of length(x + T.Length + y}
 *      Since the ACGT alphabet is 4 chars + an EOF, we only need 2 bits to store the 4 chars.  The implementation currently uses 8 bits.
 *      Assuming we are scanning DNA, we could choose not to expect worst-case behavior from an adversarial pattern or stream?
 *      
 *  Algorithms:
 *      Naive is implemented.  Does a byte-walk match of pattern against the buffer per incoming byte, OK for short patterns.
 *      Hooks are provided for additional algorithms.  Stubs provide optimization-discussion for Naive, RabinKarp, KnuthMorrisPratt and BoyerMoore+Galil.
 *      
 *  Language:
 *      This implementation is done in C#.
 *      Rather than finish optimization in C#, it's probably time to cut-over to C++ or GoLang.
 */


/// <summary>
/// On-line algorithm scans a stream of nucleotides for a matching pattern T.
/// T is returned in a context of up to x preceding and y trailing characters when available.
/// </summary>
namespace NucleotideGrep
{
    class Program
    {
        const string Usage = @"
==============================================================================
NucleotideGrep.exe Example Usage:

NucleotideGrep.exe (with no parameters)      : Prints help and runs selfTest.
NucleotideGrep.exe AGTA 5 7 true             : Greps STDIN for AGTA.
NucleotideGrep.exe AGTA 5 7 README.txt false : Greps README.txt for AGTA.

In the above examples, 5 is prior context, 7 is following context.
README.txt should be an ASCII-encoded binary file:
    Starting with only the characters A,C,G,T and e.
    'e' is used as EOF and is strictly required.
When reading STDIN, characters other than ACGTe are ignored for convenience.

Matches are output on STDOUT -- e.g.  AAAAGTAAA
Markers are output on STDERR -- e.g.     ^^^^
    unless a final parameter 'false' is given.
==============================================================================
";
        static void Main(string[] args)
        {
            bool showMarker = true;
            Stream stream = null;

            switch (args.Length)
            {
                case 3:
                    break;
                case 4:
                    if(!bool.TryParse(args[3], out showMarker))
                        stream = new FileStream(args[3], FileMode.Open, FileAccess.Read);
                    break;
                case 5:
                    showMarker = bool.Parse(args[4]);
                    stream = new FileStream(args[3], FileMode.Open, FileAccess.Read);
                    break;
                default:
                    Console.Write(Usage);
                    TestSuite.Run();
                    return;
            }

            string T = args[0];
            int x = int.Parse(args[1]);
            int y = int.Parse(args[2]);
            var algorithm = NucleotideContextGrepAlgorithm.Naive;

            using (BinaryReader br = stream == null ? null : new BinaryReader(stream))
                ShowContextGrep(x, y, T, br, algorithm, showMarker);
        }

        public static void ShowContextGrep(int x, int y, string T
            , BinaryReader br
            , NucleotideContextGrepAlgorithm algorithm
            , bool showMarker)
        {
            Nucleotide[] tPattern = T.Select(c => new Nucleotide { Char = c }).ToArray();

            NucleotideContextGrep grep = NucleotideContextGrep.Create(
                algorithm,
                tPattern: tPattern, //  e.g. "AGTA"
                xPrior: x,
                yFollowing: y);

            foreach (string contextMatch in grep.GetContextMatches(br))
            {
                Console.WriteLine(contextMatch);                        //  e.g. CAGTGAGTAGTACACC
                if (showMarker) Console.Error.WriteLine(grep.Marker);   //  e.g.      ^^^^
            }
        }
    }

}

