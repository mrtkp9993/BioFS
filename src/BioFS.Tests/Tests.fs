namespace BioFS.Tests

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass() =

    [<TestMethod>]
    member this.TestFastAReader() =
        let filename =
            "exampleData/example_multiple_seq_FASTA.fasta"

        let header =
            "lcl|NG_011747.2_cds_NP_001036.1_1 [gene=SLC6A4] [db_xref=CCDS:CCDS11256.1,GeneID:6532] [protein=sodium-dependent serotonin transporter] [protein_id=NP_001036.1] [location=join(18979..19321,22006..22140,22600..22819,23633..23771,24721..24855,25223..25326,28070..28197,29513..29625,30291..30422,31695..31794,33105..33205,37598..37765,42407..42481)] [gbkey=CDS]"

        let expectedLength = 1893
        let expectedType = DNA
        let seqs = IO.readFastA filename

        let selectedSeq =
            seqs
            |> List.tryFind (fun s -> s.header.Equals(header))

        Assert.AreEqual(expectedLength, selectedSeq.Value.sequence.Length)
        Assert.AreEqual(expectedType, selectedSeq.Value.sequence.Type)

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

    [<TestMethod>]
    member this.TestDNAReverseComplement() =
        let dnaSeq =
            { header = "Example Seq1"
              sequence = Sequence.create ("ACCGTGAGCCAGATCGGCAACGACAAGTGG", DNA) }

        let expectedSeq =
            { header = "Expected Seq1"
              sequence = Sequence.create ("CCACTTGTCGTTGCCGATCTGGCTCACGGT", DNA) }

        let actualSeq = Sequences.reverseComplement dnaSeq
        Assert.AreEqual(expectedSeq.sequence, actualSeq.sequence)

    [<TestMethod>]
    member this.TestRNATranslation() =
        let dnaSeq =
            { header = "Example Seq1"
              sequence = Sequence.create ("ACCGTGAGCCAGATCGGCAACGACAAGTGG", DNA) }

        let rnaSeq = Sequences.transcript dnaSeq
        let actualSeq = Sequences.translate rnaSeq

        let expectedSeq =
            { header =
                  dnaSeq.header
                  + " [Transcription to RNA]"
                  + " [Translation to AA]"
              sequence = Sequence.create ("TVSQIGNDKW", AA) }

        Assert.AreEqual(expectedSeq, actualSeq)

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
    member this.TestReadingFrames() =
        let inputStr =
            "ATGCCCAAGCTGAATAGCGTAGAGGGGTTTTCATCATTTGAGGACGATGTATAA"

        let inputDNA =
            { header = "input"
              sequence = Sequence.create inputStr }

        Assert.AreEqual(inputDNA.sequence.Type, DNA)

        let rfs = readingFrames inputDNA

        let expectedSeql =
            [| "MPKLNSVEGFSSFEDDVX"
               "CPSXIAXRGFHHLRTMY"
               "AQAEXRRGVFIIXGRCI"
               "LYIVLKXXKPLYAIQLGH"
               "YTSSSNDENPSTLFSLG"
               "IHRPQMMKTPLRYSAWA" |]

        for i in [ 0 .. 5 ] do
            Assert.AreEqual(
                ((>>) transcript translate rfs.[i])
                    .sequence
                    .Sequence,
                expectedSeql.[i]
            )
