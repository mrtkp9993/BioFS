module BioFS.Tests.TestsIO

open BioFS
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestsIO() =

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
