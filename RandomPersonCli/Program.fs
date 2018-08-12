﻿open System
open System.Runtime.CompilerServices
open CmdLineArgumentParsing
open CliEnums
open InteractiveMode
open ListMode
open TemplateMode
open ValidateMode

[<assembly: InternalsVisibleTo("Tests")>]
do()

let handleException (ex: Exception) =
    printfn "%s" ex.Message
    Environment.Exit 1

[<EntryPoint>]
let main argv =
    try
        let args = argv |> Array.toList
        let options = parseArgs args defaultOptions

        match options.mode with
        | Mode.Interactive -> interactiveMode
                                  options.settingsFilePath
        | Mode.List        -> listMode
                                  options.settingsFilePath
                                  options.amount
                                  options.nationality
                                  options.outputType
                                  options.fileFormat
                                  options.outputFilePath
        | Mode.Template    -> templateMode
                                  options.settingsFilePath
                                  options.amount
                                  options.nationality
        | Mode.Validation  -> validateMode
                                  options.ssn
                                  options.nationality
        | _ -> invalidArg "options.mode" "That mode does not exist."
    
        0
    with
    | _ as ex -> handleException ex; 1
