/// ComRep defines the data structures to be used as targets by all parsers

[<AutoOpen>]
module ComRep

/// Material is anything from which other pay items can be made which is needed
/// for identification.
type Material = 
    | Concrete
    | HDPE
    | HDPP
    | Steel

/// PipeShape is the cross-sectional shape of a type of pipe (ie RCHEAP vs RCP)
type PipeShape =
    | Circular
    | Elliptical
    | Arch
    | Rectangular

/// Pipe is any type of conduit used for conveying fluid. This could be a
/// circular pipe or a box culvert.
type Pipe = {
    rise: float;
    span: float;
    shape: PipeShape;
    material: Material;
}

/// JunctionInlet is one of the types of styles of intake that a junction box
/// may have. `Manhole` implies no intake.
type JunctionKind = 
    | Manhole
    | DropInlet
    | GrateInlet
    | ComboInlet

/// JunctionShape is the shape of a junction on the horizontal plane.
type JunctionShape =
    | Rectangular
    | Cylindrical

/// JunctionBox is any node structure which two or more pipes may intersect.
/// a junctionbox may or may not be an inlet and is not used in itself as the
/// primary conduit for a fluid.
type JunctionBox = {
    width: float;
    length: float;
    shape: JunctionShape;
    kind: JunctionKind;
}

/// Outlet is any structure which intakes or daylights a fluid from a pipe
/// network. Dimensions of an FES / Headwall should be able to be inferred from
/// the size of a connected pipe.
type Outlet = 
    | FES
    | Headwall

/// Any node within a gravity pipe network. This includes all manholes and
/// headwalls.
type DrainStructure =
    | JunctionBox of JunctionBox
    | Outlet of Outlet

/// Payable is the wrapper for all pay-item types. Each type is a *kind* of item
///  and not a specific item itself.
type Payable =
    | Pipe of Pipe
    | Structure of DrainStructure
    | Demolition of Payable