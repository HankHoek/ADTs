using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using NucleotideGrep.Algorithms;

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
            string testStream = "AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe";

            using (TextReader tr = new StringReader(testStream))
            {
                NucleotideContextGrep grep = new RabinKarp(
                    tPattern: T,
                    xPrior: x,
                    yFollowing: y);

                Nucleotide nucleotide;
                while (!(nucleotide = Next(tr)).IsEOF)   //  NOTE:  Missing EOFValue throws exception on end-of-stream, since it breaks spec.
                {
                    string contextMatch;
                    if (grep.HasCompleteMatchOnAdd(nucleotide.NucleotideAs2Bits, out contextMatch))
                    {
                        Console.WriteLine(contextMatch);    //  x may be incomplete, but y is populated.
                    }
                }
                foreach(string tailContextMatch in grep.GetTailContextMatches())
                {
                    Console.WriteLine(tailContextMatch);    //  y is incomplete, approaching EOF.
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
