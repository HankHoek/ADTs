using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace WindowedStats.Tests
{
    struct IntExpectation
    {
        public int Value;
        public Expectation Expectation;
    }
    class Expectation
    {
        public Stat[] Stats;
    }
    class Stat
    {

    }
    class Test
    {
        //           name               windows     stream  expectations
        //  new Test("1,2,3...  {3,5}", new[]{3,5}, s6,     new Expected(), ),

        public string Name;
    long[] Windows;
    BinaryReader Br;
    IEnumerable<Expectation> Expectations;
        public int Y;
        public string Stream;
        public string Expected;

        public Test(string name, string pattern, int x, int y, string stream, string expected = "Unknown")
        {
            Name = name;
            Pattern = pattern;
            X = x;
            Y = y;
            Stream = stream;
            Expected = expected;
        }

        public void Assert()
        {
            try
            {
                Console.WriteLine(this.ToString());

                byte[] streamBytes = Encoding.ASCII.GetBytes(Stream);
                using (MemoryStream stream = new MemoryStream(streamBytes))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    //  Program.ShowContextGrep(X, Y, Pattern, br, showMarker: true);

                    Nucleotide[] tPattern = Pattern.Select(x => new Nucleotide { Char = x }).ToArray();

                    NucleotideContextGrep grep = NucleotideContextGrep.Create(
                        NucleotideContextGrepAlgorithm.Naive,
                        tPattern: tPattern, //  e.g. "AGTA"
                        xPrior: X,
                        yFollowing: Y);

                    int cnt = 0;
                    string lastContextMatch = null;
                    foreach (string contextMatch in grep.GetContextMatches(br))
                    {
                        cnt++;
                        lastContextMatch = contextMatch;
                        Console.WriteLine(contextMatch);        //  e.g. CAGTGAGTAGTACACC
                        Console.Error.WriteLine(grep.Marker);   //  e.g.      ^^^^
                    }

                    //  Assert Expectations
                    if (cnt == 1)
                    {
                        if (lastContextMatch != Expected)
                            throw new ApplicationException(string.Format(
                                "ERROR:  lastContextMatch != Expected : {0} != {1}", lastContextMatch, Expected));
                    }
                    else
                    {
                        int expectedCnt = int.Parse(Expected);
                        if (cnt != expectedCnt)
                            throw new ApplicationException(string.Format(
                                "ERROR:  count != expectedCnt : {0} != {1}", cnt, expectedCnt));
                    }
                }
            }
            catch (Exception e)
            {
                string strType = e.GetType().ToString();
                Console.WriteLine("  ExceptionType={0}", strType);
                if (Expected != strType)
                    throw;
            }
        }

        public override string ToString()
        {
            return string.Format("Name={0}  Pattern=\"{1}\"  X={2}  Y={3}  Stream=\"{4}\"  Expected=\"{6}\"", Name, Pattern, X, Y, Stream, Environment.NewLine, Expected);
        }
    }

    class TestSuite
    {
        public static void Run()
        {
            int[] s6 = new int[] { 1, 2, 3, 4, 5, 6 };



            var tests = new List<Test>()
            {
                //       name           windows     stream  expectations
                new Test("1,2,3,4,5,6", new[]{3,5}, s6,     new Expected(), ),




                new Test("Missing 'e' throws", "A", 5, 7, "ACGT", "System.IO.EndOfStreamException"),

                //        Name     T   x  y  stream   matchExpectation/matchCount
                new Test("0,A,0", "A", 0, 0, "ACGTe", "A"),
                new Test("0,A,1", "A", 0, 1, "ACGTe", "AC"),
                new Test("0,A,2", "A", 0, 2, "ACGTe", "ACG"),

                new Test("1,A,0", "A", 1, 0, "ACGTe", " A"),
                new Test("1,A,1", "A", 1, 1, "ACGTe", " AC"),
                new Test("1,A,2", "A", 1, 2, "ACGTe", " ACG"),

                new Test("2,A,0", "A", 2, 0, "ACGTe", "  A"),
                new Test("2,A,1", "A", 2, 1, "ACGTe", "  AC"),
                new Test("2,A,2", "A", 2, 2, "ACGTe", "  ACG"),


                new Test("Name", "C", 0, 0, "ACGTe", "C"),
                new Test("Name", "C", 0, 1, "ACGTe", "CG"),
                new Test("Name", "C", 0, 2, "ACGTe", "CGT"),

                new Test("Name", "C", 1, 0, "ACGTe", "AC"),
                new Test("Name", "C", 1, 1, "ACGTe", "ACG"),
                new Test("Name", "C", 1, 2, "ACGTe", "ACGT"),

                new Test("Name", "C", 2, 0, "ACGTe", " AC"),
                new Test("Name", "C", 2, 1, "ACGTe", " ACG"),
                new Test("Name", "C", 2, 2, "ACGTe", " ACGT"),


                new Test("Name", "G", 0, 0, "ACGTe", "G"),
                new Test("Name", "G", 0, 1, "ACGTe", "GT"),
                new Test("Name", "G", 0, 2, "ACGTe", "GT"),

                new Test("Name", "G", 1, 0, "ACGTe", "CG"),
                new Test("Name", "G", 1, 1, "ACGTe", "CGT"),
                new Test("Name", "G", 1, 2, "ACGTe", "CGT"),

                new Test("Name", "G", 2, 0, "ACGTe", "ACG"),
                new Test("Name", "G", 2, 1, "ACGTe", "ACGT"),
                new Test("Name", "G", 2, 2, "ACGTe", "ACGT"),

                new Test("Name", "T", 0, 0, "ACGTe", "T"),
                new Test("Name", "T", 0, 1, "ACGTe", "T"),
                new Test("Name", "T", 0, 2, "ACGTe", "T"),

                new Test("Name", "T", 1, 0, "ACGTe", "GT"),
                new Test("Name", "T", 1, 1, "ACGTe", "GT"),
                new Test("Name", "T", 1, 2, "ACGTe", "GT"),

                new Test("Name", "T", 2, 0, "ACGTe", "CGT"),
                new Test("Name", "T", 2, 1, "ACGTe", "CGT"),
                new Test("Name", "T", 2, 2, "ACGTe", "CGT"),

                new Test("MatchEvery", "A", 2, 2, "e", "0"),
                new Test("MatchEvery", "A", 2, 2, "Ae", "  A"),
                new Test("MatchEvery", "A", 2, 2, "AAe", "2"),
                new Test("MatchEvery", "A", 2, 2, "AAAe", "3"),
                new Test("MatchEvery", "A", 2, 2, "AAAAe", "4"),
                new Test("MatchEvery", "A", 2, 2, "AAAAAe", "5"),

                new Test("RollStart", "AGTA", 3, 3, "AAAAAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),
                new Test("RollStart", "AGTA", 3, 3,  "AAAAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),
                new Test("RollStart", "AGTA", 3, 3,   "AAAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),
                new Test("RollStart", "AGTA", 3, 3,    "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),
                new Test("RollStart", "AGTA", 3, 3,     "AGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),

                new Test("RollEnd", "AGTA", 3, 3, "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),
                new Test("RollEnd", "AGTA", 3, 3, "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTe", "4"),
                new Test("RollEnd", "AGTA", 3, 3, "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCe", "4"),
                new Test("RollEnd", "AGTA", 3, 3, "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGe", "4"),
                new Test("RollEnd", "AGTA", 3, 3, "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAe", "4"),


                new Test("SpecTest", "AGTA", 5, 7, "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe", "4"),
            };

            Console.WriteLine();
            Console.WriteLine("Starting Self-Test:");
            Console.WriteLine("Starting Self-Test:  Failure ends with an uncaught exception.");
            Console.WriteLine("Starting Self-Test:  Success ends with 3 lines like \"!!! SUCCESS !!! -- All tests succeeded.\"");
            Console.WriteLine("Starting Self-Test:");
            Console.WriteLine();

            foreach (var test in tests)
                test.Assert();

            Console.WriteLine("!!! SUCCESS !!! -- All tests succeeded.");
            Console.WriteLine("!!! SUCCESS !!! -- All tests succeeded.");
            Console.WriteLine("!!! SUCCESS !!! -- All tests succeeded.");
        }

    }
}
