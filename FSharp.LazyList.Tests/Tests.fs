module Tests

open Hedgehog
open Swensen.Unquote
open Xunit

[<Fact>]
let ``toList round trip to original list`` () =
    Property.check <| property {
        let! list = Range.linear 1 1000 |> Gen.list <| Gen.alphaNum
        let sut = List.fold (flip LazyList.cons) LazyList.empty list
        LazyList.toList sut =! list
    }
