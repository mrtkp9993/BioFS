namespace BioFS

[<AutoOpen>]
module RegexStrings =
        // Source: https://stackoverflow.com/a/68660247
    let internal fastaRegexString: string =
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
        
    let internal proteinRegexString: string = @"M[ACDEFGHIKLMNPQRSTVWY]+(?=X|$)"
