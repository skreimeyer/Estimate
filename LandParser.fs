/// LandParser handles the ingestion of LandXML files and returns a map of
/// common representation of payable items and their respective measurements
/// 
/// Over time, this should be expected to use `<measure>` to validate operations
/// on unit quantities. 
module LandParser

open FSharp.Data
open ComRep

type Land = XmlProvider<"LandXML-1_2.xml">
let tally : Map<ComRep.Payable,float> = Map.empty
let input landfile = Land.Parse(landfile)

let matchMats m =
    match m with
        | "RCP" -> ComRep.Material.Concrete
        | "CPP" | "HDPE" -> ComRep.Material.HDPE
        | "CMP" | "Metal" -> ComRep.Material.Steel
        | _ -> ComRep.Material.Concrete

let countPipes (tally: Map<ComRep.Payable,float>) (landfile: Land) =
    for network in landfile.LandXML.PipeNetworks do
        for pipe in network.Pipes do
            let length = pipe.Length
            match pipe.CircPipe with
                | Some p -> tally.Add(ComRep.Payable.Pipe({
                        rise=p.Diameter;
                        span=p.Diameter;
                        shape=ComRep.PipeShape.Circular;
                        material= matchMats p.Material;
                    }),length)
                | None -> tally
                |> ignore


/// Wishlist:
/// open landXML
/// Section.iter 
/// |> Object kind.iter
/// |> object.iter
/// |> convert to ComRep
/// export map
 