namespace BioFS

open System.Text.RegularExpressions

module IO =

    // Source: https://stackoverflow.com/a/68660247
    let private fasta: Regex =
        Regex(
            fastaRegexString,
            RegexOptions.IgnorePatternWhitespace
            ||| RegexOptions.Multiline
        )

    let readFastA (file: string) : BioSequence list =
        let lines = System.IO.File.ReadAllText(file)

        [ for m in fasta.Matches(lines) do
              let hd = m.Groups.[1].Value

              let sq =
                  Regex.Replace(m.Groups.[2].Value, @"\*|\r|\n", "")

              { header = hd
                sequence = Sequence.create sq } ]

    let writeReport (report: string) (outfile: string) =
        System.IO.File.WriteAllText(outfile, report)
