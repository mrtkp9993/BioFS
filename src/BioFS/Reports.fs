namespace BioFS

module Reports =
    
    let private _ncmapToString (mapping: Map<string,int>) =
        let total =
            mapping
            |> Map.toArray
            |> Array.map snd
            |> Array.sum
            |> float
            
        mapping
        |> Map.map (fun k v -> $"{k}: {v} - {round2 (float(v) / total)*100.0}%%".PadRight(80).Replace(',', '.'))
        |> Map.toArray
        |> Array.map snd
        |> String.concat System.Environment.NewLine
        
    let private _codonsToString (seql: BioSequence list) =
        seql
        |> List.map (fun bs -> Stats.getKmerCounts 3 bs)
        |> List.map (fun m -> _ncmapToString m)
        |> String.concat System.Environment.NewLine
    
    let nucleotideSequenceReport (seq : BioSequence) =
        let stats = Stats.NucleotideSequenceStatistics seq
        let monomers = Stats.getKmerCounts 1 seq
        let dimers = Stats.getKmerCounts 2 seq
        let rfs = readingFrames seq
        let codons = _codonsToString rfs
            
        [|
            null
            null
            "DNA Sequence Statistics Report"
            null
            null
            multiplyString "*" 80
            null
            null
            "Common Statistics"
            multiplyString "-" 80
            $"""Length: {stats.["Length"]} nt""".PadRight(80).Replace(',', '.')
            $"""GC content: {stats.["GC-Content"]}%%""".PadRight(80).Replace(',', '.')
            $"""Melting temperature: {stats.["Melting Temperature"]} °C""".PadRight(80).Replace(',', '.')
            $"""Molecular weight: {stats.["Molecular Weight"]} Da ^""".PadRight(80).Replace(',', '.')
            $"""Extinction coefficient: {stats.["Extinction Coefficient"]} l/(mol * cm) ^""".PadRight(80).Replace(',', '.')
            $"""nmole/OD₂₆₀: {stats.["nmole/OD260"]}""".PadRight(80).Replace(',', '.')
            $"""μg/OD₂₆₀: {stats.["μg/OD260"]}""".PadRight(80).Replace(',', '.')
            multiplyString "-" 80
            "^ These statistics calculated for ssDNA."
            multiplyString "-" 80
            null
            null
            "Characters Occurence"
            multiplyString "-" 80
            _ncmapToString monomers
            multiplyString "-" 80
            null
            null
            "Dinucleotides"
            multiplyString "-" 80
            _ncmapToString dimers
            multiplyString "-" 80
            null
            null
            "Codons"
            codons
            multiplyString "-" 80
            "These counts calculated for whole sequence and all reading frames."
            multiplyString "-" 80
            null
            null
            multiplyString "*" 80
            null
            null
        |] |> String.concat System.Environment.NewLine
