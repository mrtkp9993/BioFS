module BioFS.Tests.TestSubstitutionMatrices

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestSubstitutionMatrices() =
    [<TestMethod>]
    member this.TestSubsMatricesBLOSUM45() =
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM45].[('S', 'P')], -1)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM45].[('H', 'C')], -3)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM45].[('W', 'Z')], -2)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM45].[('M', 'V')], 1)

    [<TestMethod>]
    member this.TestSubsMatricesBLOSUM50() =
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM50].[('R', 'C')], -4)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM50].[('D', 'Q')], 0)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM50].[('L', 'H')], -3)

    [<TestMethod>]
    member this.TestSubsMatricesBLOSUM62() =
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM62].[('W', 'F')], 1)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM62].[('Y', 'M')], -1)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM62].[('A', 'Z')], -1)

    [<TestMethod>]
    member this.TestSubsMatricesBLOSUM80() =
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM80].[('F', 'X')], -3)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM80].[('T', 'K')], -1)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM80].[('P', 'L')], -5)

    [<TestMethod>]
    member this.TestSubsMatricesBLOSUM90() =
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM90].[('Y', 'Y')], 8)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM90].[('V', 'L')], 0)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM90].[('Z', 'W')], -4)

    [<TestMethod>]
    member this.TestSubsMatricesBLOSUM100() =
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM100].[('Q', 'K')], 2)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM100].[('T', 'Z')], -3)
        Assert.AreEqual(SubstitutionMatrices.[BLOSUM100].[('W', 'Y')], 2)

    [<TestMethod>]
    member this.TestSubsMatricesPAM30() =
        Assert.AreEqual(SubstitutionMatrices.[PAM30].[('I', 'I')], 8)
        Assert.AreEqual(SubstitutionMatrices.[PAM30].[('E', 'Q')], 1)
        Assert.AreEqual(SubstitutionMatrices.[PAM30].[('S', 'L')], -8)

    [<TestMethod>]
    member this.TestSubsMatricesPAM70() =
        Assert.AreEqual(SubstitutionMatrices.[PAM70].[('D', 'T')], -2)
        Assert.AreEqual(SubstitutionMatrices.[PAM70].[('K', 'K')], 6)
        Assert.AreEqual(SubstitutionMatrices.[PAM70].[('B', 'H')], 0)

    [<TestMethod>]
    member this.TestSubsMatricesPAM250() =
        Assert.AreEqual(SubstitutionMatrices.[PAM250].[('G', 'L')], -4)
        Assert.AreEqual(SubstitutionMatrices.[PAM250].[('E', 'I')], -2)
