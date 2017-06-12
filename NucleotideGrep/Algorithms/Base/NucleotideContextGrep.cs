﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NucleotideGrep.ADTs;
using JetBlack.Core.Collections.Generic;
using System.IO;

namespace NucleotideGrep.Algorithms
{
    public enum NucleotideContextGrepAlgorithm
    {
        Naive,
        RabinKarp,
        KnuthMorrisPratt,
        BoyerMoore  //  Probably fastest
    }

    interface IContextMatchNucleotides
    {
        IEnumerable<string> GetContextMatches(BinaryReader br);
    }

    public abstract class NucleotideContextGrep : IContextMatchNucleotides
    {
        public static NucleotideContextGrep Create(
            NucleotideContextGrepAlgorithm algorithm,
            Nucleotide[] tPattern,
            int xPrior,
            int yFollowing
            )
        {
            switch(algorithm)
            {
                case NucleotideContextGrepAlgorithm.Naive:
                    return new Naive(tPattern, xPrior, yFollowing);

                case NucleotideContextGrepAlgorithm.RabinKarp:
                    return new RabinKarp(tPattern, xPrior, yFollowing);

                case NucleotideContextGrepAlgorithm.KnuthMorrisPratt:
                    return new KnuthMorrisPratt(tPattern, xPrior, yFollowing);

                case NucleotideContextGrepAlgorithm.BoyerMoore:
                    return new BoyerMoore(tPattern, xPrior, yFollowing);

                default:
                    throw new NotImplementedException();
            }
        }

        protected readonly Nucleotide[] TPattern;
        protected readonly CircularBuffer<Nucleotide> Buffer;

        protected readonly int XPrior;
        protected readonly int YFollowing;
        protected int TPatternOffset;

        protected NucleotideContextGrep(
            Nucleotide[] tPattern,
            int xPrior,
            int yFollowing
            )
        {
            this.TPattern = tPattern;
            this.XPrior = xPrior;
            this.YFollowing = yFollowing;

            int elementsCnt = xPrior + tPattern.Length + yFollowing;

            Buffer = new CircularBuffer<Nucleotide>(elementsCnt);
        }

        public IEnumerable<string> GetContextMatches(BinaryReader br)
        {
            Nucleotide nucleotide;

            //  Fill grep's contextBuffer before evaluating any lead-in matches.
            while (!(nucleotide = Next(br)).IsEOF)   //  NOTE:  Missing EOFValue throws exception on end-of-stream, since it breaks spec.
            {
                if (HasCompleteContextOnAdd(nucleotide))
                    break;
            }

            //  output any lead-in matches
            foreach (string contextMatch in GetLeadInMatches())
                yield return contextMatch;    //  x may be incomplete, but y is populated if possible.

            //  output any rolling buffer matches.  This loop should be especially perf-optimized.
            string rollingContextMatch = null;
            while (!(nucleotide = Next(br)).IsEOF)   //  NOTE:  Missing EOFValue throws exception on end-of-stream, since it breaks spec.
            {
                if (HasCompleteMatchOnAdd(nucleotide, ref rollingContextMatch))
                {
                    yield return rollingContextMatch;   //  x and y are both populated.
                }
            }

            //  output any remaining matches in the tail
            foreach (string tailContextMatch in GetTailOutMatches())
            {
                yield return tailContextMatch;    //  y is incomplete, approaching EOF.
            }
        }

        //  For speed when reading raw ascii bytes.
        static Nucleotide Next(BinaryReader br)
        {
            return new Nucleotide { Ascii = br.ReadByte() };
        }


        private bool HasCompleteContextOnAdd(Nucleotide nucleotide)
        {
            Buffer.Enqueue(nucleotide);
            return Buffer.Count == Buffer.Capacity;
        }
        protected abstract IEnumerable<string> GetLeadInMatches();
        protected abstract bool HasCompleteMatchOnAdd(Nucleotide nucleotide, ref string contextMatch);
        protected abstract IEnumerable<string> GetTailOutMatches();

        public string Marker
        {
            get
            {
                var sb = new StringBuilder();
                for (int i = 0; i < XPrior; i++) sb.Append(' ');
                for (int i = 0; i < TPattern.Length; i++) sb.Append('=');
                return sb.ToString();
            }
        }

    }
}
