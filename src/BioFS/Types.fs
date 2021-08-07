namespace BioFS

open System.Text.RegularExpressions

[<AutoOpen>]
module Types =

    // Source: https://stackoverflow.com/a/68660247
    let fastaRegexString =
        @"
        ^>                  # Line Starting with >
          (.*)                 # Capture into $1
        \r?\n               # End-of-Line
        (                   # Capturing in $2
            (?:
                ^           # A Line ...
                  [A-Z]+       # .. containing A-Z
                \*? \r?\n   # Optional(*) followed by End-of-Line
            )+              # ^ Multiple of those lines
        )
        (?:
            (?: ^ [ \t\v\f]* \r?\n )  # Match an empty (whitespace) line ..
            |                         # or
            \z                        # End-of-String
        )
        "

    type SequenceType =
        | DNA
        | RNA
        | AA

    type FastASequence = { header: string; sequence: string }
