namespace BioFS

[<AutoOpen>]
module Utils =
    
    let stringToSet str = (>>) Seq.toList Set.ofSeq str

    let stringReverse (str:string) =
        System.String(str.ToCharArray() |> Array.rev)
