namespace BioFS

[<AutoOpen>]
module CalculationConstants =

    let gcContentCoeffs: Map<char, float> =
        [ 'A', 0.000
          'C', 1.000
          'G', 1.000
          'T', 0.000
          'U', 0.000
          'W', 0.000
          'S', 1.000
          'M', 0.500
          'K', 0.500
          'R', 0.500
          'Y', 0.500
          'B', 0.667
          'D', 0.500
          'H', 0.500
          'V', 0.667
          'N', 0.500 ]
        |> Map.ofList

    let molecularWeights: Map<SequenceType, Map<char, float>> =
        [ DNA,
          [ 'A', 251.240
            'C', 227.220
            'G', 267.240
            'T', 242.230
            'W', 246.735
            'S', 247.230
            'M', 239.230
            'K', 254.735
            'R', 259.240
            'Y', 234.725
            'B', 245.563
            'D', 253.570
            'H', 240.230
            'V', 248.567
            'N', 246.983 ]
          |> Map.ofList
          RNA,
          [ 'A', 267.240
            'C', 243.220
            'G', 283.240
            'U', 244.200
            'W', 255.720
            'S', 263.230
            'M', 255.230
            'K', 263.720
            'R', 275.240
            'Y', 243.710
            'B', 256.887
            'D', 264.893
            'H', 251.553
            'V', 264.567
            'N', 259.475 ]
          |> Map.ofList ]
        |> Map.ofList

    // Degenerate characters not supported yet
    let extinctionCoeffs =
        [ DNA,
          [ "A", 15400
            "C", 7400
            "G", 11500
            "T", 8700
            "AA", 27400
            "AC", 21200
            "AG", 25000
            "AT", 22800
            "CA", 21200
            "CC", 14600
            "CG", 18000
            "CT", 15200
            "GA", 25200
            "GC", 17600
            "GG", 21600
            "GT", 20000
            "TA", 23400
            "TC", 16200
            "TG", 19000
            "TT", 16800 ]
          |> Map.ofList
          RNA,
          [ "A", 15400
            "C", 7200
            "G", 11500
            "U", 9900
            "AA", 27400
            "AC", 21000
            "AG", 25000
            "AU", 24000
            "CA", 21000
            "CC", 14200
            "CG", 17800
            "CU", 16200
            "GA", 25200
            "GC", 17400
            "GG", 21600
            "GU", 21200
            "UA", 24600
            "UC", 17200
            "UG", 20000
            "UU", 19600 ]
          |> Map.ofList ]
        |> Map.ofList
