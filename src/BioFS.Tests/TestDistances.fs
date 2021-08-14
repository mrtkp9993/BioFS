module BioFS.Tests.TestDistances

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestDistances() =
    
    [<TestMethod>]
    member this.TestLevenshteinDistance() =
        let str1 = "levenshtein"
        let str2 = "meilenstein"
        
        Assert.AreEqual(4, Distances.Levenshtein str1 str2)
    
    [<TestMethod>]
    member this.TestHammingDistance() =
        let str1 = "karolin"
        let str2 = "kathrin"
        let str3 = "kerstin"
        
        Assert.AreEqual(3, Distances.Hamming str1 str2)
        Assert.AreEqual(3, Distances.Hamming str1 str3)
        Assert.AreEqual(4, Distances.Hamming str2 str3)
    