namespace BioFS

[<AutoOpen>]
module Utils =
    
    let stringToSet str = (>>) Seq.toList Set.ofSeq str
