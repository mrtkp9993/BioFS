namespace BioFS

module Distances =

    let Hamming (str1: string) (str2: string) : int =
        let len1 = str1.Length
        let len2 = str2.Length
        
        let mutable distance = System.Math.Abs(len1 - len2)
        
        for b in Seq.map2 (=) str1 str2 do
            if b = false then
                distance <- distance + 1
        
        distance

    let Levenshtein (str1: string) (str2: string) : int =
        let darr : int[,] = Array2D.zeroCreate (str1.Length + 1) (str2.Length + 1)
         
        for i = 0 to str1.Length do
            darr.[i, 0] <- i
        for j = 0 to str2.Length do
            darr.[0, j] <- j
            
        for j = 1 to str2.Length do
            for i = 1 to str1.Length do
                if str1.[i-1] = str2.[j-1] then
                    darr.[i, j] <- darr.[i-1, j-1]
                else
                    darr.[i, j] <- List.min [
                        darr.[i-1,j]+1
                        darr.[i,j-1]+1
                        darr.[i-1,j-1]+1
                    ]
        darr.[str1.Length, str2.Length]
