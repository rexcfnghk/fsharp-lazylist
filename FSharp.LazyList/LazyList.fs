[<RequireQualifiedAccess>]
module LazyList

open Lazy
open Utilities

[<StructuredFormatDisplay("{AsString}")>]
type LazyList<'a> =
    | Empty
    | LazyList of head: Lazy<'a> * tail: LazyList<'a>

let empty = Empty

let mkLazyList x xs = LazyList (x, xs)

let singleton x = x |> Lazy.returnLazy |> flip mkLazyList empty

let cons x xs = LazyList (Lazy.returnLazy x, xs)

let concat xs ys =
    let rec concatImpl acc xs ys =
        match (xs, ys) with
        | Empty, Empty -> acc
        | Empty, ys -> concatImpl acc ys empty 
        | LazyList (Lazy x, xs), ys -> concatImpl (cons x acc) xs ys
    concatImpl empty xs ys
    
let ( ++ ) = concat

let rec map f = function
    | Empty -> Empty
    | LazyList (x, xs) -> mkLazyList (f x) (map f xs)
    
let ( <!> ) = map

let rec apply fLazyList xLazyList =
    match (fLazyList, xLazyList) with
    | LazyList (Lazy f, fs), LazyList (Lazy x, xs) ->
        mkLazyList (lazy (f x)) (apply fs xs)
    | _ -> Empty
    
let rec foldr f acc = function
    | Empty -> acc
    | LazyList (Lazy x, xs) -> f x (foldr f acc xs)
    
let rec foldl f acc = function
    | Empty -> acc
    | LazyList (Lazy x, xs) -> foldl f (f acc x) xs
    
let rec bind f = function
    | Empty -> Empty
    | LazyList (Lazy x, xs) -> f x ++ bind f xs

let toList xs = foldl (fun acc x -> x :: acc) [] xs
