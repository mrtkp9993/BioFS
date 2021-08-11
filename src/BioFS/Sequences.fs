namespace BioFS

open System.Text.RegularExpressions

[<AutoOpen>]
module Sequences =

    let transcript (dnaSequence: BioSequence) : BioSequence =
        if dnaSequence.sequence.Type <> DNA then
            invalidArg "dnaSequence" "Input must be DNA sequence"
        else
            let header =
                dnaSequence.header + " [Transcription to RNA]"

            let transcriptedSeq =
                dnaSequence.sequence.Replace('T', 'U').Sequence

            { header = header
              sequence = Sequence.create (transcriptedSeq, RNA) }

    let private _reverseComp (str: string, mapping: Map<char, char>) : string =
        str
        |> stringReverse
        |> String.map (fun x -> mapping.[x])

    let reverseComplement (seq: BioSequence) : BioSequence =
        if seq.sequence.Type = AA then
            invalidArg "seq" "Input must be DNA or RNA sequence"
        else
            let header = seq.header + " [Reverse Complement]"

            let res =
                _reverseComp (seq.sequence.Sequence, Complements.[seq.sequence.Type])

            { header = header
              sequence = Sequence.create (res, seq.sequence.Type) }

    let translate (rnaSequence: BioSequence) : BioSequence =
        if rnaSequence.sequence.Type <> RNA then
            invalidArg "rnaSequence" "Input must be RNA sequence"
        else
            let header =
                rnaSequence.header + " [Translation to AA]"

            let res: string =
                rnaSequence.sequence.Sequence
                |> Seq.chunkBySize 3
                |> Seq.filter (fun s -> Seq.length s = 3)
                |> Seq.toArray
                |> Array.map (fun arr -> Codons.[System.String arr])
                |> System.String.Concat

            { header = header
              sequence = Sequence.create (res, AA) }

    let getKmers (seq: BioSequence) (k: int) : string [] =
        seq.sequence.Sequence
        |> Seq.windowed k
        |> Seq.toArray
        |> Array.map System.String

    let readingFrames (seq: BioSequence) : BioSequence list =
        if seq.sequence.Type = AA then
            invalidArg "seq" "Input must be DNA/RNA sequence"
        else
            let header = seq.header
            let fiveToThreeSeq = seq.sequence.Sequence

            let threeToFiveseq =
                (reverseComplement seq).sequence.Sequence

            [ for s in [ 1 .. 3 ] ->
                  { header = header + $" [5'3' Frame {s}]"
                    sequence = Sequence.create fiveToThreeSeq.[(s - 1)..] } ]
            @ [ for s in [ 1 .. 3 ] ->
                    { header = header + $" [3'5' Frame {s}]"
                      sequence = Sequence.create threeToFiveseq.[(s - 1)..] } ]

    let private possibleProteins: Regex =
        Regex(
            proteinRegexString,
            RegexOptions.IgnorePatternWhitespace
        )

    let getPossibleProteins (seq: BioSequence) : BioSequence list =
        let rfs: BioSequence list =
            match seq.sequence.Type with
            | DNA -> readingFrames (transcript seq)
            | RNA -> readingFrames seq

        let mutable res: BioSequence list = []

        for i in [ 0 .. 5 ] do
            let translated = translate rfs.[i]
            let header = translated.header
            let seq = translated.sequence.Sequence
            let pps = possibleProteins.Matches(seq)

            if pps.Count <> 0 then
                for j in [ 0 .. (pps.Count - 1) ] do
                    res <- res
                          @ [ { header = header
                                sequence = Sequence.create pps.[j].Value } ]

        res
