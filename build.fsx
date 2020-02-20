#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.DotNet
open Fake.Core

let buildDir = "./build/"

Target.create "Clean" (fun _ ->
    Shell.cleanDir buildDir
)

Target.create "BuildApp" (fun _ ->
    DotNet.build id  "FSharp.LazyList.sln"
)

Target.create "Test" (fun _ ->
    DotNet.test id "FSharp.LazyList.sln"
)

Target.create "Default" (fun _ ->
    Trace.trace "Hello World from FAKE"
)

open Fake.Core.TargetOperators

"Clean"
    ==> "BuildApp"
    ==> "Test"
    ==> "Default"

Target.runOrDefault "Default"