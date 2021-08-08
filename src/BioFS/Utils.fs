namespace BioFS

[<AutoOpen>]
module Utils =
    
    let stringToSet (str: string): Set<char> =
        (>>) Seq.toList Set.ofSeq str

    let stringReverse (str:string): string =
        System.String(str.ToCharArray() |> Array.rev)
