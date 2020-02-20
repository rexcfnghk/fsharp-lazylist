#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.DotNet.MSBuild
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.DotNet
open Fake.Core

let buildDir = "./build/"
let testDir = "./test/"

// Default
Target.create "Clean" (fun _ ->
    Shell.cleanDirs [buildDir; testDir]
)

Target.create "BuildApp" (fun _ ->
    !! "src/app/**/*.fsproj"
    |> MSBuild.runRelease id buildDir "Build"
    |> Trace.logItems "AppBuild-Output: ")

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