using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowedStats.Classes;

using System.IO;

namespace WindowedStats.Tests
{
    class Test
    {
        public string Name;
        Assertion[] Assertions;

        public Test(string name, IEnumerable<Assertion> assertions)
        {
            Name = name;
            Assertions = assertions.ToArray();
        }

        public void Assert()
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(stream))
            using (BinaryReader br = new BinaryReader(stream))
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
        }
    }

}
