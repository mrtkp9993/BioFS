module BioFS.Tests.TestsSequences

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestsSequences() =
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
