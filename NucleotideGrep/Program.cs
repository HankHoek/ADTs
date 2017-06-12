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

NucleotideGrep.exe (with no parameters)   : Prints this help and runs tests.
NucleotideGrep.exe AGTA 5 7 testFile.txt  : Greps file for AGTA, with x=5, y=7
NucleotideGrep.exe AGTA 5 7               : Same as above, but from STDIN.

When reading STDIN, newlines are ignored so online behavior can be tested.
==============================================================================
";
        static void Main(string[] args)
        {
            TestSuite.Run();
            return;

            int x = 5;
            int y = 7;
            string T = "AGTA";
            var algorithm = NucleotideContextGrepAlgorithm.Naive;

            byte[] streamBytes = Encoding.ASCII.GetBytes("AAAAe");
            using (MemoryStream stream = new MemoryStream(streamBytes))
            using (BinaryReader br = new BinaryReader(stream))
                ShowContextGrep(x, y, T, br);
        }

        public static void ShowContextGrep(int x, int y, string T, BinaryReader br
            , bool showMarker = false)
        {
            Nucleotide[] tPattern = T.Select(c => new Nucleotide { Char = c }).ToArray();

            NucleotideContextGrep grep = NucleotideContextGrep.Create(
                NucleotideContextGrepAlgorithm.Naive,
                tPattern: tPattern, //  e.g. "AGTA"
                xPrior: x,
                yFollowing: y);

            foreach (string contextMatch in grep.GetContextMatches(br))
            {
                Console.WriteLine(contextMatch);                //  e.g. CAGTGAGTAGTACACC
                if (showMarker) Console.WriteLine(grep.Marker);  //  e.g.      ^^^^
            }
        }
    }

}

