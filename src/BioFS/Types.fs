namespace BioFS

[<AutoOpen>]
module Types =

    type SequenceType =
        | DNA
        | RNA
        | AA
        | None
        
    type SubsMatrixType =
        | BLOSUM45
        | BLOSUM50
        | BLOSUM62
        | BLOSUM80
        | BLOSUM90
        | BLOSUM100
        | PAM30
        | PAM70
        | PAM250

    let Alphabets: Map<SequenceType, Set<char>> =
        [ DNA, stringToSet "ACGTMRWSYKVHDBN"
          RNA, stringToSet "ACGUMRWSYKVHDBN"
          AA, stringToSet "ACDEFGHIKLMNPQRSTVWY-" ]
        |> Map.ofList

    type Sequence =
        private
            { seq: string
              seqtype: SequenceType }
        static member public create(seq) =
            let inputAlphabet = stringToSet seq
            if Set.isProperSubset inputAlphabet Alphabets.[DNA] then
                { seq = seq; seqtype = DNA }
            elif Set.isProperSubset inputAlphabet Alphabets.[RNA] then
                { seq = seq; seqtype = RNA }
            elif Set.isProperSubset inputAlphabet Alphabets.[AA] then
                { seq = seq; seqtype = AA }
            else
                invalidArg seq "Input string contains invalid characters."
                { seq = ""; seqtype = None }

        member public this.Length = this.seq.Length
        member public this.Type = this.seqtype
        member public this.Replace(c1:char, c2:char) =
            {seq = this.seq.Replace(c1, c2); seqtype = this.seqtype}
        member public this.Reverse() =
            {seq = stringReverse this.seq; seqtype = this.seqtype}

    type BioSequence = { header: string; sequence: Sequence }
