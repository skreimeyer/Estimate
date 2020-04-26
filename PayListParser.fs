/// PayListParser handles parsing of ARDTO and potentially other CSV files which
/// contain information about payable quantities for construction contracts.
module PayListParser

open FSharp.Data
open FParsec
open ComRep

type PayVals = {
    unit: string;
    n: int;
    high: float
    low: float;
    mean: float;
}

let paylist : Map<ComRep.Payable,PayVals> = Map.empty
let paylistSrc = CsvFile.Load("ARDOT Payables.csv").Cache()


// Wishlist:
// for row in paylist parse to item option
// if Some item, add to master list