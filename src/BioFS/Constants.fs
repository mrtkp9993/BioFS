namespace BioFS

[<AutoOpen>]
module Constants =

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

    let Complements: (SequenceType * (char * char) list) list =
        [ DNA,
          [ 'A', 'T'
            'C', 'G'
            'G', 'C'
            'T', 'A'
            'W', 'W'
            'S', 'S'
            'M', 'K'
            'K', 'M'
            'R', 'Y'
            'Y', 'R'
            'B', 'V'
            'D', 'H'
            'H', 'D'
            'V', 'B'
            'N', 'N' ]
          RNA,
          [ 'A', 'U'
            'C', 'G'
            'G', 'C'
            'U', 'A'
            'W', 'W'
            'S', 'S'
            'M', 'K'
            'K', 'M'
            'R', 'Y'
            'Y', 'R'
            'B', 'V'
            'D', 'H'
            'H', 'D'
            'V', 'B'
            'N', 'N' ] ]
