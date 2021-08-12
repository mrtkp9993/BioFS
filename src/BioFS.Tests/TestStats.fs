module BioFS.Tests.TestStats

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting
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

    [<TestMethod>]
    member this.NucleotideSequenceStatistics() =
        let filename =
            "exampleData/example_single_seq_FASTA.fasta"

        let seq = IO.readFastA filename
        
        let stats = Stats.NucleotideSequenceStatistics seq.[0]
        
        Assert.AreEqual(696.0, stats.["Length"])
        Assert.AreEqual(61.21, stats.["GC-Content"])
        Assert.AreEqual(215100.83, stats.["Molecular Weight"])
        Assert.AreEqual(89.03, stats.["Melting Temperature"])
        Assert.AreEqual(6613500.0, stats.["Extinction Coefficient"])
        Assert.AreEqual(0.15, stats.["nmole/OD260"])
        // Assert.AreEqual(32.52, stats.["μg/OD260"])
