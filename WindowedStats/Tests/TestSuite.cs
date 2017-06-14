using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowedStats.Classes;

using System.IO;

namespace WindowedStats.Tests
{
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
