﻿namespace BioFS

module Stats =

    let getKmerCounts (k: int) (seq: BioSequence) : Map<string, int> =
        seq
        |> flip getKmers k
        |> Array.countBy id
        |> Map.ofArray