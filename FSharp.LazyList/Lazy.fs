module Lazy

let returnLazy = Lazy.CreateFromValue

let ( |Lazy| ) (x: Lazy<_>) = x.Force ()

let map f (Lazy x) =
    lazy (f x)
    
let ( <!> ) = map

let apply (Lazy f) (Lazy x) =
    lazy (f x)
    
let ( <*> ) = apply

let bind<'a, 'b> (f: 'a -> Lazy<'b>) (Lazy x) =
    f x
    
type LazyBuilder () =
    member _.Bind (x, f) = bind f x
    member _.Return x = returnLazy x
    member _.ReturnFrom x = x
    member _.Zero () = returnLazy ()
    
let lazyExp = LazyBuilder ()