Project builds NucleotideGrep.exe on windows using free VisualStudio2017 community edition:
	https://www.visualstudio.com/vs/community/

Summary:

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


