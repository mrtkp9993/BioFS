namespace BioFS

[<AutoOpen>]
module Utils =

    let stringToSet (str: string) : Set<char> = (>>) Seq.toList Set.ofSeq str

    let stringReverse (str: string) : string =
        str |> Seq.toArray |> Array.rev |> System.String

    let inline flip f x y = f y x

    let inline add x y = x + y
    let inline multiply x y = x * y

    let inline round2 (x:float): float = System.Math.Round(x, 2)

    let multiplyString text times = String.replicate times text
