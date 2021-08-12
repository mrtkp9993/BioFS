namespace BioFS

module Stats =

    let getKmerCounts (k: int) (seq: BioSequence) : Map<string, int> =
        seq
        |> flip getKmers k
        |> Array.countBy id
        |> Map.ofArray

    let NucleotideSequenceStatistics (seq : BioSequence) : Map<string, float> =
        [
            "Length", float(seq.sequence.Length)
            "GC-Content", 0.0
        ] |> Map.ofList
