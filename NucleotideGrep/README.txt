AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe
==========================================================================
==========  Above portion of README.txt serves as a streaming test-file.
==========	Try:  NucleotideGrep.exe SelfTest
==========	Try:  NucleotideGrep.exe AGTA 5 7 README.txt true
==========	Try:  NucleotideGrep.exe AGTA 5 7 true
==========	      (STDIN -- try entering AGTAAGTAAGTAe )
==========================================================================
==========	README:
==========================================================================

Project builds NucleotideGrep.exe on windows using free VisualStudio2017 community edition:
	https://www.visualstudio.com/vs/community/

Complete:
	Naive implementation with tests
		O(streamLength * patternLength + totalOutputSize).
		Seems to be correct.

Incomplete:
	O(streamlength + (totalOutputSize==patternLength*numMatches)) is achievable via multiple algorithms:
		RabinKarp, BoyerMoore, KnuthMorrisPratt.

	See BoyerMoore.cs for discussion on using Galil rule and modified output-spec to reduce worst-case totalOutputSize.


For perf, this C# implementation should probably be replaced by something in a lower-level language.
Given that the team has a preference for GoLang:

Bits and pieces of a GoLang vNext:
	https://golang.org/pkg/bufio/			//  Context Buffer
	https://golang.org/src/strings/search.go	//  Boyer-Moore

To demonstrate an interest, I will follow-up with a GoLang-based solution.

==========================================================================
==========================================================================
==========  Proof of Correctness / Test Summary:
==========================================================================
==========================================================================
==========	Try:  NucleotideGrep.exe AGTA 5 7 README.txt true
C:\read\vs2017\ADTs\NucleotideGrep\bin\Debug>NucleotideGrep.exe AGTA 5 7 README.txt true
    AAGTACGTGCAG
     ^^^^
CAGTGAGTAGTAGACC
     ^^^^
TGAGTAGTAGACCTGA
     ^^^^
ATATAAGTAGCTA
     ^^^^
==========================================================================
==========	Try:  NucleotideGrep.exe AGTA 5 7 true
==========	      (STDIN -- try entering AGTAAGTAAGTAe )
C:\read\vs2017\ADTs\NucleotideGrep\bin\Debug>NucleotideGrep.exe AGTA 5 7 true
AGTA -- input
AGTA -- input
e -- input
     AGTAAGTA
     ^^^^
 AGTAAGTA
     ^^^^
==========================================================================
==========	Try:  NucleotideGrep.exe SelfTest
==========  (Outputs helpText, and dumps the output of the self-test suite.)
C:\read\vs2017\ADTs\NucleotideGrep\bin\Debug>NucleotideGrep.exe

==============================================================================
NucleotideGrep.exe Example Usage:

NucleotideGrep.exe SelfTest                  : Prints help and runs selfTest.
NucleotideGrep.exe AGTA 5 7 true             : Greps STDIN for AGTA.
NucleotideGrep.exe AGTA 5 7 README.txt false : Greps README.txt for AGTA.

In the above examples, 5 is prior context, 7 is following context.
README.txt should be an ASCII-encoded binary file:
    Starting with only the characters A,C,G,T and e.
    'e' is used as EOF and is strictly required.
When reading STDIN, characters other than ACGTe are ignored for convenience.

Matches are output on STDOUT -- e.g.  AAAAGTAAA
Markers are output on STDERR -- e.g.     ^^^^
    unless a final parameter 'false' is given.
==============================================================================

==============================================================================
NucleotideGrep.exe Example Usage:

NucleotideGrep.exe (with no parameters)      : Prints help and runs selfTest.
NucleotideGrep.exe AGTA 5 7 true             : Greps STDIN for AGTA.
NucleotideGrep.exe AGTA 5 7 README.txt false : Greps README.txt for AGTA.

In the above examples, 5 is prior context, 7 is following context.
README.txt should be an ASCII-encoded binary file:
    Starting with only the characters A,C,G,T and e.
    'e' is used as EOF and is strictly required.
When reading STDIN, characters other than ACGTe are ignored for convenience.

Matches are output on STDOUT -- e.g.  AAAAGTAAA
Markers are output on STDERR -- e.g.     ^^^^
    unless a final parameter 'false' is given.
==============================================================================

Starting Self-Test:
Starting Self-Test:  Failure ends with an uncaught exception.
Starting Self-Test:  Success ends with 3 lines like "!!! SUCCESS !!! -- All tests succeeded."
Starting Self-Test:

Name=Empty pattern throws  Pattern=""  X=5  Y=7  Stream="ACGTe"  Expected="System.ApplicationException"
  ExceptionType=System.ApplicationException
Name=Missing 'e' throws  Pattern="A"  X=5  Y=7  Stream="ACGT"  Expected="System.IO.EndOfStreamException"
  ExceptionType=System.IO.EndOfStreamException
Name=0,A,0  Pattern="A"  X=0  Y=0  Stream="ACGTe"  Expected="A"
A
^
Name=0,A,1  Pattern="A"  X=0  Y=1  Stream="ACGTe"  Expected="AC"
AC
^
Name=0,A,2  Pattern="A"  X=0  Y=2  Stream="ACGTe"  Expected="ACG"
ACG
^
Name=1,A,0  Pattern="A"  X=1  Y=0  Stream="ACGTe"  Expected=" A"
 A
 ^
Name=1,A,1  Pattern="A"  X=1  Y=1  Stream="ACGTe"  Expected=" AC"
 AC
 ^
Name=1,A,2  Pattern="A"  X=1  Y=2  Stream="ACGTe"  Expected=" ACG"
 ACG
 ^
Name=2,A,0  Pattern="A"  X=2  Y=0  Stream="ACGTe"  Expected="  A"
  A
  ^
Name=2,A,1  Pattern="A"  X=2  Y=1  Stream="ACGTe"  Expected="  AC"
  AC
  ^
Name=2,A,2  Pattern="A"  X=2  Y=2  Stream="ACGTe"  Expected="  ACG"
  ACG
  ^
Name=Name  Pattern="C"  X=0  Y=0  Stream="ACGTe"  Expected="C"
C
^
Name=Name  Pattern="C"  X=0  Y=1  Stream="ACGTe"  Expected="CG"
CG
^
Name=Name  Pattern="C"  X=0  Y=2  Stream="ACGTe"  Expected="CGT"
CGT
^
Name=Name  Pattern="C"  X=1  Y=0  Stream="ACGTe"  Expected="AC"
AC
 ^
Name=Name  Pattern="C"  X=1  Y=1  Stream="ACGTe"  Expected="ACG"
ACG
 ^
Name=Name  Pattern="C"  X=1  Y=2  Stream="ACGTe"  Expected="ACGT"
ACGT
 ^
Name=Name  Pattern="C"  X=2  Y=0  Stream="ACGTe"  Expected=" AC"
 AC
  ^
Name=Name  Pattern="C"  X=2  Y=1  Stream="ACGTe"  Expected=" ACG"
 ACG
  ^
Name=Name  Pattern="C"  X=2  Y=2  Stream="ACGTe"  Expected=" ACGT"
 ACGT
  ^
Name=Name  Pattern="G"  X=0  Y=0  Stream="ACGTe"  Expected="G"
G
^
Name=Name  Pattern="G"  X=0  Y=1  Stream="ACGTe"  Expected="GT"
GT
^
Name=Name  Pattern="G"  X=0  Y=2  Stream="ACGTe"  Expected="GT"
GT
^
Name=Name  Pattern="G"  X=1  Y=0  Stream="ACGTe"  Expected="CG"
CG
 ^
Name=Name  Pattern="G"  X=1  Y=1  Stream="ACGTe"  Expected="CGT"
CGT
 ^
Name=Name  Pattern="G"  X=1  Y=2  Stream="ACGTe"  Expected="CGT"
CGT
 ^
Name=Name  Pattern="G"  X=2  Y=0  Stream="ACGTe"  Expected="ACG"
ACG
  ^
Name=Name  Pattern="G"  X=2  Y=1  Stream="ACGTe"  Expected="ACGT"
ACGT
  ^
Name=Name  Pattern="G"  X=2  Y=2  Stream="ACGTe"  Expected="ACGT"
ACGT
  ^
Name=Name  Pattern="T"  X=0  Y=0  Stream="ACGTe"  Expected="T"
T
^
Name=Name  Pattern="T"  X=0  Y=1  Stream="ACGTe"  Expected="T"
T
^
Name=Name  Pattern="T"  X=0  Y=2  Stream="ACGTe"  Expected="T"
T
^
Name=Name  Pattern="T"  X=1  Y=0  Stream="ACGTe"  Expected="GT"
GT
 ^
Name=Name  Pattern="T"  X=1  Y=1  Stream="ACGTe"  Expected="GT"
GT
 ^
Name=Name  Pattern="T"  X=1  Y=2  Stream="ACGTe"  Expected="GT"
GT
 ^
Name=Name  Pattern="T"  X=2  Y=0  Stream="ACGTe"  Expected="CGT"
CGT
  ^
Name=Name  Pattern="T"  X=2  Y=1  Stream="ACGTe"  Expected="CGT"
CGT
  ^
Name=Name  Pattern="T"  X=2  Y=2  Stream="ACGTe"  Expected="CGT"
CGT
  ^
Name=MatchEvery  Pattern="A"  X=2  Y=2  Stream="e"  Expected="0"
Name=MatchEvery  Pattern="A"  X=2  Y=2  Stream="Ae"  Expected="  A"
  A
  ^
Name=MatchEvery  Pattern="A"  X=2  Y=2  Stream="AAe"  Expected="2"
  AA
  ^
 AA
  ^
Name=MatchEvery  Pattern="A"  X=2  Y=2  Stream="AAAe"  Expected="3"
  AAA
  ^
 AAA
  ^
AAA
  ^
Name=MatchEvery  Pattern="A"  X=2  Y=2  Stream="AAAAe"  Expected="4"
  AAA
  ^
 AAAA
  ^
AAAA
  ^
AAA
  ^
Name=MatchEvery  Pattern="A"  X=2  Y=2  Stream="AAAAAe"  Expected="5"
  AAA
  ^
 AAAA
  ^
AAAAA
  ^
AAAA
  ^
AAA
  ^
Name=RollStart  Pattern="AGTA"  X=3  Y=3  Stream="AAAAAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
AAAAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollStart  Pattern="AGTA"  X=3  Y=3  Stream="AAAAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
AAAAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollStart  Pattern="AGTA"  X=3  Y=3  Stream="AAAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
 AAAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollStart  Pattern="AGTA"  X=3  Y=3  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
  AAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollStart  Pattern="AGTA"  X=3  Y=3  Stream="AGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
   AGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollEnd  Pattern="AGTA"  X=3  Y=3  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
  AAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollEnd  Pattern="AGTA"  X=3  Y=3  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTe"  Expected="4"
  AAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGCT
   ^^^^
Name=RollEnd  Pattern="AGTA"  X=3  Y=3  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCe"  Expected="4"
  AAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAGC
   ^^^^
Name=RollEnd  Pattern="AGTA"  X=3  Y=3  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGe"  Expected="4"
  AAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTAG
   ^^^^
Name=RollEnd  Pattern="AGTA"  X=3  Y=3  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAe"  Expected="4"
  AAGTACGT
   ^^^^
GTGAGTAGTA
   ^^^^
AGTAGTAGAC
   ^^^^
ATAAGTA
   ^^^^
Name=SpecTest  Pattern="AGTA"  X=5  Y=7  Stream="AAGTACGTGCAGTGAGTAGTAGACCTGACGTAGACCGATATAAGTAGCTAe"  Expected="4"
    AAGTACGTGCAG
     ^^^^
CAGTGAGTAGTAGACC
     ^^^^
TGAGTAGTAGACCTGA
     ^^^^
ATATAAGTAGCTA
     ^^^^
!!! SUCCESS !!! -- All tests succeeded.
!!! SUCCESS !!! -- All tests succeeded.
!!! SUCCESS !!! -- All tests succeeded.

C:\read\vs2017\ADTs\NucleotideGrep\bin\Debug>