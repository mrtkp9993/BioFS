namespace BioFS

[<AutoOpen>]
module Utils =
    
    let stringToSet (str: string): Set<char> =
        (>>) Seq.toList Set.ofSeq str

    let stringReverse (str:string): string =
        str |> Seq.toArray |> Array.rev |> System.String
