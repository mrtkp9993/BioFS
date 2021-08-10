module BioFS.Tests.TestStats

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestsStats() =
    [<TestMethod>]
    member this.TestGetKmerCounts() =
        let filename =
            "exampleData/example_single_seq_FASTA.fasta"

        let seq = IO.readFastA filename

        let expected1merCounts =
            [| ("A", 172)
               ("C", 219)
               ("G", 207)
               ("T", 98) |]
            |> Map.ofArray

        let actual1merCounts = Stats.getKmerCounts 1 seq.[0]
        let actual2merCounts = Stats.getKmerCounts 2 seq.[0]

        Assert.AreEqual(expected1merCounts, actual1merCounts)
        Assert.AreEqual(79, actual2merCounts.["CC"])
        Assert.AreEqual(5, actual2merCounts.["TA"])
        Assert.AreEqual(55, actual2merCounts.["TG"])
        Assert.AreEqual(50, actual2merCounts.["AC"])
