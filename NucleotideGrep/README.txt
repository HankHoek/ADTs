Summary:

Rabin-Karp would be relatively simple to implement from scratch.
Boyer-Moore seems best-in-class.
	Using the Galil rule could further reduce worst-case costs,
	but output-batching spec-changes would be required to reduce the program Big-O.

For perf, this C# implementation should probably be replaced by something in a lower-level language.
Given that the team has a preference for GoLang:

Bits and pieces of a GoLang vNext:
	https://golang.org/pkg/bufio/			//  Context Buffer
	https://golang.org/src/strings/search.go	//  Boyer-Moore

To demonstrate an interest in migrating toward team-standards, I will follow-up with a GoLang-based solution.


