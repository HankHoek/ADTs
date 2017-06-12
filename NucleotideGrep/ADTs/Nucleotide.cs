using System;

namespace NucleotideGrep.ADTs
{
    /// <summary>
    /// Nucleotide stores an ascii byte over the alphabet "ACGTe", so that we can use BinaryReader for IO performance.
    ///
    /// For in-memory storage, Nucleotide is also addressable via a NucleotideAs2Bits property.
    ///     2-bit storage has been abandoned in favor of simplicity, given no tight memory bounds and only a 4:1 compression ratio.
    ///         See discussion under Naive.cs for potential 4x perf-benefit.
    ///
    /// For convenience in .NET, Nucleotide is also addressable via a Char property and via ToString().
    /// </summary>
    public struct Nucleotide
    {
        /// <summary>
        /// Nucleotide2Bits is an enum with a stated BitCnt of 2.
        /// Only the two low-order bits are used for the alphabet.
        /// </summary>
        public enum Nucleotide2Bits : byte
        {
            A = 0,
            C = 1,
            G = 2,
            T = 3,
            //  e = 4   -- e is not needed or encodable in 2 bit format.
        }
        public enum NucleotideAscii : byte
        {
            A = 65,
            C = 67,
            G = 71,
            T = 84,
            e = 101
        }
        public const int NucleotideBitCnt = 2;

        public const byte EOFValue = (byte)NucleotideAscii.e;
        public bool IsEOF { get { return EOFValue == Ascii; } }

        /// Nucleotide stores an ascii byte over the alphabet "ACGTe", so that we can use BinaryReader for IO performance.
        public byte Ascii;

        /// For in-memory storage, Nucleotide is also addressable via a NucleotideAs2Bits property.
        public Nucleotide2Bits NucleotideAs2Bits
        {
            get
            {
                switch (Ascii)
                {
                    case (byte)NucleotideAscii.A:
                        return Nucleotide2Bits.A;

                    case (byte)NucleotideAscii.C:
                        return Nucleotide2Bits.C;

                    case (byte)NucleotideAscii.G:
                        return Nucleotide2Bits.G;

                    case (byte)NucleotideAscii.T:
                        return Nucleotide2Bits.T;

                    case (byte)NucleotideAscii.e:
                    default:
                        throw new ApplicationException(string.Format(
                            "ERROR: Nucleotide value == '{0}' is outside the Nucleotide2Bits ACGT alphabet.  Note:  'e' == EOF is implicit, not stored in Nucleotide2Bits format.", Char));
                }
            }
            set
            {

            }
        }

        /// For convenience in .NET, Nucleotide is also addressable via a Char property and via ToString().
        public Char Char
        {
            get
            {
                switch (Ascii)
                {
                    case (byte)NucleotideAscii.A:
                        return 'A';
                    case (byte)NucleotideAscii.C:
                        return 'C';
                    case (byte)NucleotideAscii.G:
                        return 'G';
                    case (byte)NucleotideAscii.T:
                        return 'T';
                    case (byte)NucleotideAscii.e:
                        return 'e';
                    default:
                        throw new ApplicationException(string.Format(
                            "ERROR: Nucleotide.Ascii == {0} (decimal) is outside the ACGTe alphabet.", Ascii));
                }
            }
            set
            {
                switch (value)
                {
                    case 'A':
                        Ascii = (byte)NucleotideAscii.A;
                        break;
                    case 'C':
                        Ascii = (byte)NucleotideAscii.C;
                        break;
                    case 'G':
                        Ascii = (byte)NucleotideAscii.G;
                        break;
                    case 'T':
                        Ascii = (byte)NucleotideAscii.T;
                        break;
                    case 'e':
                        Ascii = (byte)NucleotideAscii.e;
                        break;

                    default:
                        throw new ApplicationException(string.Format(
                            "ERROR: Nucleotide.Char == '{0}' is outside the ACGTe alphabet.", value));
                }
            }
        }

        public override string ToString()
        {
            return Char.ToString();
        }
    }

}
