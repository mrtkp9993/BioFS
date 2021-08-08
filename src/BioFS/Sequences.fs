namespace BioFS

module Sequences =
    
    let transcript (dnaSequence: BioSequence): BioSequence =
        let header = dnaSequence.header
        let transcriptedSeq = dnaSequence.sequence.Replace('T', 'U').Sequence
        {header=header; sequence=Sequence.create (transcriptedSeq, RNA)}
        