namespace BioFS

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
