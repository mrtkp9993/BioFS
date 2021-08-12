namespace BioFS

module Stats =

    let getKmerCounts (k: int) (seq: BioSequence) : Map<string, int> =
        seq
        |> flip getKmers k
        |> Array.countBy id
        |> Map.ofArray
        
    let calcGCContent (seq: BioSequence) : float =
        seq.sequence.Sequence
        |> Seq.map (fun s -> gcContentCoeffs.[s])
        |> Seq.average
        |> multiply 100.0
        |> round2
        
    let calcMolecularWeight (seq: BioSequence) : float =
        seq.sequence.Sequence
        |> Seq.map (fun s -> molecularWeights.[seq.sequence.Type].[s])
        |> Seq.sum
        |> add ((float (seq.sequence.Length - 1)) * 61.97)
        |> round2
        
    let calcMeltingTemp (seq: BioSequence) : float =
        let monomerCount = getKmerCounts 1 seq
        let nA = float(monomerCount.["A"])
        let nT = if seq.sequence.Type = DNA then float(monomerCount.["T"]) else float(monomerCount.["U"])
        let nG = float(monomerCount.["G"])
        let nC = float(monomerCount.["C"])
        let mutable tm = 0.0
        if seq.sequence.Length >= 15 then
            tm <-  64.9 + 41.0 * (nG + nC - 16.4) / (nA + nT + nG + nC)
        else
            tm <- (nA + nT) * 2.0 + (nG + nC) * 4.0
        tm |> round2
        
    let calcExtinctionCoefficient (seq: BioSequence) : float =
        let innerPart = seq.sequence.Sequence.Substring(1, seq.sequence.Length-2)
        let monomerCounts = getKmerCounts 1 seq
        let dimerCounts = getKmerCounts 2 seq
        let sumDimer : int =
            dimerCounts
            |> Map.toSeq
            |> Seq.map (fun (s, i) -> i*extinctionCoeffs.[seq.sequence.Type].[s])
            |> Seq.sum
            
        let sumInner =
            monomerCounts
            |> Map.toSeq
            |> Seq.map (fun (s, i) -> i*extinctionCoeffs.[seq.sequence.Type].[s])
            |> Seq.sum
            
        float(sumDimer - sumInner)    
    
    let calcnmole0D260 (seq : BioSequence) : float =
        1000000.0 / calcExtinctionCoefficient seq
        |> round2
        
    let calcμgOD260 (seq : BioSequence) : float =
        let n = calcnmole0D260 seq
        let m = calcMolecularWeight seq
        n * m * 0.001 |> round2
    
    let NucleotideSequenceStatistics (seq : BioSequence) : Map<string, float> =
        [
            "Length", float(seq.sequence.Length)
            "GC-Content", calcGCContent seq
            "Molecular Weight", calcMolecularWeight seq
            "Melting Temperature", calcMeltingTemp seq
            "Extinction Coefficient", calcExtinctionCoefficient seq
            "nmole/OD260", calcnmole0D260 seq
            "μg/OD260", calcμgOD260 seq
        ] |> Map.ofList
