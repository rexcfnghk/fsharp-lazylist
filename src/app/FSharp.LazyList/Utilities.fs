[<AutoOpen>]
module Utilities

let flip f y x = f x y

let curry f x y = f (x, y)

let uncurry f (x, y) = f x y

let mkTuple x y = x, y