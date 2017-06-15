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
                new Assertion(stats3_20, 2, new Expectation{ Values=new double[]{ double.NaN, double.NaN, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 3, new Expectation{ Values=new double[]{ 2,3, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 4, new Expectation{ Values=new double[]{ 3,4, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 5, new Expectation{ Values=new double[]{ 4,5, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 6, new Expectation{ Values=new double[]{ 5,6, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 7, new Expectation{ Values=new double[]{ 6,7, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 8, new Expectation{ Values=new double[]{ 7,8, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 9, new Expectation{ Values=new double[]{ 8, 9, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 10, new Expectation{ Values=new double[]{ 9, 10, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 11, new Expectation{ Values=new double[]{ 10, 11, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 12, new Expectation{ Values=new double[]{ 11, 12, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 13, new Expectation{ Values=new double[]{ 12, 13, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 14, new Expectation{ Values=new double[]{ 13, 14, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 15, new Expectation{ Values=new double[]{ 14, 15, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 16, new Expectation{ Values=new double[]{ 15, 16, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 17, new Expectation{ Values=new double[]{ 16, 17, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 18, new Expectation{ Values=new double[]{ 17, 18, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 19, new Expectation{ Values=new double[]{ 18, 19, double.NaN, double.NaN } }),
                new Assertion(stats3_20, 20, new Expectation{ Values=new double[]{ 19, 20, 10.5, 20 } }),
                new Assertion(stats3_20, 21, new Expectation{ Values=new double[]{ 20, 21, 11.5, 21 } }),
            };

            var tests = new List<Test>()
            {
                new Test("stream 1 - 6,  last3(Mean&Max) + last5(Mean&Max)", assertionsS6W3_5),
                new Test("stream 1 - 20, last3(Mean&Max) + last20(Mean&Max)", assertionsS21W3_20 ),
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
