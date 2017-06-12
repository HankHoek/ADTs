﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using NucleotideGrep.Algorithms;
using System.IO;

namespace NucleotideGrep.Tests
{
    class Test
    {
        public string Name;
        string Pattern;
        public int X;
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

                Nucleotide[] tPattern = Pattern.Select(x => new Nucleotide { Char = x }).ToArray();
                byte[] streamBytes = Encoding.ASCII.GetBytes(Stream);

                NucleotideContextGrep grep = NucleotideContextGrep.Create(
                    NucleotideContextGrepAlgorithm.Naive,
                    tPattern: tPattern, //  e.g. "AGTA"
                    xPrior: X,
                    yFollowing: Y);

                using (MemoryStream stream = new MemoryStream(streamBytes))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    foreach (string contextMatch in grep.GetContextMatches(br))
                    {
                        Console.WriteLine(contextMatch);    //  e.g. CAGTGAGTAGTACACC
                        Console.WriteLine(grep.Marker);     //  e.g.      ^^^^
                    }
                }
            }
            catch (Exception e)
            {
                string strType = e.GetType().ToString();
                Console.WriteLine("ExceptionType={0}", strType);
                if (Expected != strType)
                    throw;
            }
        }

        public override string ToString()
        {
            return string.Format("Name={0}  Pattern=\"{1}\"  X={2}  Y={3}  Stream=\"{4}\"  Expected={6}", Name, Pattern, X, Y, Stream, Environment.NewLine, Expected);
        }
    }

    class TestSuite
    {
        public static void Run()
        {
            var tests = new List<Test>()
            {
                //new Test("Empty pattern throws", "", 5, 7, "ACGTe", "System.ApplicationException"),

                //new Test("0,A,0", "A", 0, 0, "ACGTe", "A"),
                //new Test("0,A,1", "A", 0, 1, "ACGTe", "AC"),
                //new Test("0,A,2", "A", 0, 2, "ACGTe", "ACG"),

                //new Test("1,A,0", "A", 1, 0, "ACGTe", " A"),
                //new Test("1,A,1", "A", 1, 1, "ACGTe", " AC"),
                //new Test("1,A,2", "A", 1, 2, "ACGTe", " ACG"),

                //new Test("2,A,0", "A", 2, 0, "ACGTe", "  A"),
                //new Test("2,A,1", "A", 2, 1, "ACGTe", "  AC"),
                //new Test("2,A,2", "A", 2, 2, "ACGTe", "  ACG"),


                //new Test("Name", "C", 0, 0, "ACGTe", "C"),
                //new Test("Name", "C", 0, 1, "ACGTe", "CG"),
                //new Test("Name", "C", 0, 2, "ACGTe", "CGT"),

                //new Test("Name", "C", 1, 0, "ACGTe", "AC"),
                //new Test("Name", "C", 1, 1, "ACGTe", "ACG"),
                //new Test("Name", "C", 1, 2, "ACGTe", "ACGT"),

                //new Test("Name", "C", 2, 0, "ACGTe", " AC"),
                //new Test("Name", "C", 2, 1, "ACGTe", " ACG"),
                //new Test("Name", "C", 2, 2, "ACGTe", " ACGT"),


                //new Test("Name", "G", 0, 0, "ACGTe", "G"),
                //new Test("Name", "G", 0, 1, "ACGTe", "GT"),
                //new Test("Name", "G", 0, 2, "ACGTe", "GTE"),

                //new Test("Name", "G", 1, 0, "ACGTe", "CG"),
                //new Test("Name", "G", 1, 1, "ACGTe", "CGT"),
                //new Test("Name", "G", 1, 2, "ACGTe", "CGTE"),

                //new Test("Name", "G", 2, 0, "ACGTe", "ACG"),
                //new Test("Name", "G", 2, 1, "ACGTe", "ACGT"),
                //new Test("Name", "G", 2, 2, "ACGTe", "ACGT"),

                //new Test("Name", "T", 0, 0, "ACGTe", "T"),
                //new Test("Name", "T", 0, 1, "ACGTe", "T"),
                //new Test("Name", "T", 0, 2, "ACGTe", "T"),

                //new Test("Name", "T", 1, 0, "ACGTe", "GT"),
                //new Test("Name", "T", 1, 1, "ACGTe", "GT"),
                //new Test("Name", "T", 1, 2, "ACGTe", "GT"),

                //new Test("Name", "T", 2, 0, "ACGTe", "CGT"),
                //new Test("Name", "T", 2, 1, "ACGTe", "CGT"),
                //new Test("Name", "T", 2, 2, "ACGTe", "CGT"),

                new Test("Name", "A", 2, 2, "e", ""),
                new Test("Name", "A", 2, 2, "Ae", "  A"),
                new Test("Name", "A", 2, 2, "AAe", "Multiple"),
            };

            foreach (var test in tests)
                test.Assert();
        }
    }
}
