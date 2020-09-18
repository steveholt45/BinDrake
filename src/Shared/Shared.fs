namespace Shared
open System
[<Struct>]
type Coordinates = { X: int; Y: int; Z: int }

[<Struct>]
type Dim =
    {
        Width: int
        Height: int
        Length: int
    }

type CalculationMode =
    | MinimizeLength
    | MinimizeHeight

type Container = { Dim: Dim; Coord: Coordinates; Weight : int }
type Item = { Dim: Dim; Weight : int; Id: string; Tag: string ; NoTop:bool; KeepTop:bool; KeepBottom : bool}
type ContainerTriplet = Container list
type ItemPut = { Item: Item; Coord: Coordinates }
type PutResult = ValueTuple<ContainerTriplet list ,  ItemPut ValueOption> ValueOption
[<Struct>]
type ItemsWithCost = { Items: Item list; Cost: float }
type PutItem = Container -> Item -> int -> PutResult
type StackItem = ValueTuple<Container list , Item list , ItemPut list>
[<Struct>]
type Counter = { Value : int }
type CalcResult = {
    ItemsPut : ItemPut list
    ContainerVol : int
    ItemsUnput : Item list
    PutVolume : int
    Container : Container
    EmptyContainers : Container list
}

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
type ICounterApi =
    {
        initialCounter : unit -> Async<Counter>
        run  : CalculationMode -> Container -> Item list -> float -> float -> Async<CalcResult>
    }

