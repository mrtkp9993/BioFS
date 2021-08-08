namespace BioFS

module Sequences =
    
    let transcript (dnaSequence: BioSequence): BioSequence =
        let header = dnaSequence.header + " [Transcription to RNA]"
        let transcriptedSeq = dnaSequence.sequence.Replace('T', 'U').Sequence
        {header=header; sequence=Sequence.create (transcriptedSeq, RNA)}
        
    let private _reverseComp (str: string, mapping: Map<char,char>): string =
        str |> stringReverse |> String.map (fun x -> mapping.[x])
    
    let reverseComplement (seq: BioSequence): BioSequence =
        let header = seq.header + " [Reverse Complement]"
        if seq.sequence.Type = AA then
            invalidArg "seq" "Input must be DNA or RNA sequence"
        else
            let res = _reverseComp (seq.sequence.Sequence, Complements.[seq.sequence.Type])
            {header= header
             sequence= Sequence.create(res, seq.sequence.Type)}
