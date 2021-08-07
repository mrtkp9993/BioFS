namespace BioFS

module Constants =

    let Alphabets: Map<SequenceType, string> =
        [ DNA, "ACGTMRWSYKVHDBN"
          RNA, "ACGUMRWSYKVHDBN"
          AA, "ACDEFGHIKLMNPQRSTVWY-" ]
        |> Map.ofList

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
