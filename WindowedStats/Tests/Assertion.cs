using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowedStats.Classes;

using System.IO;

namespace WindowedStats.Tests
{
    class Expectation
    {
        public double[] Values;
    }

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
            for (int i = 0; i < Expectation.Values.Length; i++)
            {
                int add = this.Value;
                Stats.Observe(add);

                double stat = this.Stats.AsArray[i].Value;
                double expectation = Expectation.Values[i];

                if(stat == expectation
                    || System.Double.IsNaN(stat) && System.Double.IsNaN(expectation))
                {
                    Console.Error.WriteLine(
                        "       stat == expectation -- {0} == {1}", stat, expectation);
                }
                else
                {
                    Console.Error.WriteLine(
                        "ERROR: stat != expectation -- {0} != {1}", stat, expectation);
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("===========");
            //sb.AppendLine("Expectation = " + Expectation);
            sb.AppendLine("value = " + Value);

            return sb.ToString();
        }
    }
}
