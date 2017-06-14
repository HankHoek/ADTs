using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowedStats.Classes;

using System.IO;

namespace WindowedStats.Tests
{
    class Assertion
    {
        public Stats Stats;
        public int Value;
        public Expectation Expectation;

        public Assertion(Stats stats, int value, Expectation expectation)
        {
            Stats = stats;
            Value = value;
            Expectation = expectation;
        }
        public void Assert()
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Expectation = " + Expectation);
            sb.AppendLine("value = " + Value);
            sb.AppendLine("===========");

            return sb.ToString();
        }
    }
    class Expectation
    {
        public double[] Values;
    }

    class Test
    {
        //           name               windows     stream  expectations
        //  new Test("1,2,3...  {3,5}", new[]{3,5}, s6,     new Expected(), ),

        public string Name;
        long[] Windows;
        int[] Stream;
        BinaryReader Br;
        Assertion[] Assertions;
        Stats Stats;


        public Test(string name, IEnumerable<Assertion> assertions)
        {
            Name = name;
            Assertions = assertions.ToArray();
        }

        public void Assert()
        {
            //try
            //{
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(stream))
            using (BinaryReader br = new BinaryReader(stream))
            {
                try
                {
                    foreach (var assertion in Assertions)
                    {
                        //  Prep the stream
                        bw.Seek(0, SeekOrigin.Begin);
                        bw.Write(assertion.Value);
                        bw.Seek(0, SeekOrigin.Begin);

                        Console.WriteLine(assertion);

                        //  Consume from the stream
                        int i = br.Read();
                        if (assertion.Value != i)
                            throw new ApplicationException(string.Format(
                                "INPUT ERROR -- Expected != Actual : {0} != {1}", assertion.Value, i));

                        assertion.Assert();
                    }
                }
                catch (IOException)
                {

                }
            }
            //}
            //catch (Exception e)
            //{
            //    string strType = e.GetType().ToString();
            //    Console.WriteLine("  ExceptionType={0}", strType);
            //    throw;
            //}
        }

        public override string ToString()
        {
            return "NotImplemented...";
            //            return string.Format("Name={0}  Pattern=\"{1}\"  X={2}  Y={3}  Stream=\"{4}\"  Expected=\"{6}\"", Name, Pattern, X, Y, Stream, Environment.NewLine, Expected);
        }
    }

    class TestSuite
    {
        public static void Run()
        {
            int[] s6 = new int[] { 1, 2, 3, 4, 5, 6 };

            var stats3_5 = new Stats(new Stat[] {
                new Mean(new Window{ Lookback=3 }),
                new Max(new Window{ Lookback=3 }),

                new Mean(new Window{ Lookback=5 }),
                new Max(new Window{ Lookback=5 }),
            });

            var assertionsS6W3_5 = new Assertion[]
            {
                new Assertion(stats3_5, 1, new Expectation{ Values=new double[]{ double.NaN, double.NaN, double.NaN, double.NaN } }),
                new Assertion(stats3_5, 2, new Expectation{ Values=new double[]{ double.NaN, double.NaN, double.NaN, double.NaN } }),
                new Assertion(stats3_5, 3, new Expectation{ Values=new double[]{ 2,3, double.NaN, double.NaN } }),
                new Assertion(stats3_5, 4, new Expectation{ Values=new double[]{ 3,4, double.NaN, double.NaN } }),
                new Assertion(stats3_5, 5, new Expectation{ Values=new double[]{ 4,5,3,5 } }),
                new Assertion(stats3_5, 6, new Expectation{ Values=new double[]{ 5,6,4,6 } }),
            };

            int[] s21 = new int[] {
                1,1,1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,1,1,
                1,
            };

            var stats3_20 = new Stats(new Stat[] {
                new Mean(new Window{ Lookback=3 }),
                new Max(new Window{ Lookback=3 }),

                new Mean(new Window{ Lookback=20 }),
                new Max(new Window{ Lookback=20 }),
            });

            var assertionsS21W3_20 = new Assertion[]
            {
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ double.NaN, double.NaN, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ double.NaN, double.NaN, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 1, new Expectation{ Values=new double[]{ 1,1, double.NaN, double.NaN } }),
            };

            var tests = new List<Test>()
            {
                new Test("name", assertionsS6W3_5),
                new Test("name", assertionsS21W3_20 ),
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
