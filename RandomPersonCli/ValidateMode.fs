﻿module internal ValidateMode

open System
open RandomPersonLib
open CliUtil

let validate (lib: RandomPerson) (nationality: Nationality) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _         -> lib.ValidateSSN(nationality, readSSN) |> printfn "%b" |> loop

    loop ()

let validateMode (ssn: string) (nationality: Nationality) =
    let lib = RandomPerson()

    match ssn with
    | "" ->
        printHelp ()

        let rec mainloop() =
            if Console.KeyAvailable then
                match Console.ReadKey(true).KeyChar with
                | 'q' -> ()
                | 'd' -> validate lib Nationality.Danish    |> printHelp |> mainloop
                | 'D' -> validate lib Nationality.Dutch     |> printHelp |> mainloop
                | 'f' -> validate lib Nationality.Finnish   |> printHelp |> mainloop
                | 'i' -> validate lib Nationality.Icelandic |> printHelp |> mainloop
                | 'n' -> validate lib Nationality.Norwegian |> printHelp |> mainloop
                | 's' -> validate lib Nationality.Swedish   |> printHelp |> mainloop
                | _ -> mainloop()
            else
                mainloop ()

        mainloop()
    | _ -> lib.ValidateSSN(nationality, ssn) |> printfn "%b"
