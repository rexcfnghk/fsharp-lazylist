module FSharp.LazyList.Tests

open FSharp.LazyList
open Hedgehog
open Swensen.Unquote
open Xunit

[<Fact>]
let ``toList round trips to original list`` () =
    Property.check <| property {
        let! list = Range.linear 1 1000 |> Gen.list <| Gen.alphaNum
        let sut = List.fold (flip LazyList.cons) LazyList.empty list
        LazyList.toList sut =! list
    }
    
[<Fact>]
let ``toSeq round trips to original seq`` () =
    Property.check <| property {
        let! seq = Range.linear 1 1000 |> Gen.list <| Gen.alphaNum
        let sut = Seq.fold (flip LazyList.cons) LazyList.empty seq
        LazyList.toSeq sut
        |> Seq.toList
        |> (=!) seq
    }
