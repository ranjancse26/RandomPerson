namespace RandomPersonLib

open System
open System.Runtime.CompilerServices
open RandomPersonLib
open ReadInputFiles
open Validate
open TemplatePrint
open Util

[<assembly: InternalsVisibleTo("Tests")>]
do()

/// Interface for RandomPerson for use with C#.
type IRandomPerson =
    abstract member CreatePerson: Nationality -> Person
    abstract member CreatePerson: Nationality * RandomPersonOptions -> Person
    abstract member CreatePeople: int * Nationality -> Person seq
    abstract member CreatePeople: int * Nationality * RandomPersonOptions -> Person seq
    abstract member CreatePeopleTemplated: int * Nationality * string -> string seq
    abstract member CreatePeopleTemplated: int * Nationality * string * RandomPersonOptions -> string seq
    abstract member ValidateSSN: Nationality * string -> bool

/// A service class for generating random persons or validating SSNs.
type RandomPerson() =
    let i = readInputFiles ()
    let defaultOptions = RandomPersonOptions(false, false, false, false, false, false)

    let createPerson (nationality: Nationality, options: RandomPersonOptions, random: Random) =
        match nationality with
        | Nationality.Danish    -> Person(nationality, i.generic, i.danish,    options, random)
        | Nationality.Dutch     -> Person(nationality, i.generic, i.dutch,     options, random)
        | Nationality.Finnish   -> Person(nationality, i.generic, i.finnish,   options, random)
        | Nationality.Icelandic -> Person(nationality, i.generic, i.icelandic, options, random)
        | Nationality.Norwegian -> Person(nationality, i.generic, i.norwegian, options, random)
        | Nationality.Swedish   -> Person(nationality, i.generic, i.swedish,   options, random)
        | _ -> invalidArg "nationality" "Illegal nationality."

    /// Create a Person object given a nationality.
    member this.CreatePerson (nationality: Nationality) = this.CreatePerson (nationality, defaultOptions)

    /// Create a Person object given a nationality and an options object.
    member __.CreatePerson (nationality: Nationality, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        createPerson(nationality, options, random)

    /// Create a list of Person objects given a nationality.
    member this.CreatePeople (amount: int, nationality: Nationality) = this.CreatePeople (amount, nationality, defaultOptions)

    /// Create a list of Person objects given a nationality and an options object.
    member __.CreatePeople (amount: int, nationality: Nationality, options: RandomPersonOptions)  =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(nationality, options, random))

    /// Create a list of strings given a template string where certain values will be replaced.
    member this.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string) =
        this.CreatePeopleTemplated (amount, nationality, outputString, defaultOptions)

    /// Create a list of strings given a template string where certain values will be replaced.
    member __.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(nationality, options, random)) |> List.map (printForTemplateMode outputString)
        
    /// Validate an SSN for a given nationality.
    member __.ValidateSSN (nationality: Nationality, ssn: string) =
        match nationality with
        | Nationality.Danish    -> validateDK ssn
        | Nationality.Dutch     -> validateNL ssn
        | Nationality.Finnish   -> validateFI ssn
        | Nationality.Icelandic -> validateIC ssn
        | Nationality.Norwegian -> validateNO ssn
        | Nationality.Swedish   -> validateSE ssn
        | _ -> invalidArg "nationality" "Illegal nationality."

    interface IRandomPerson with
        
        /// Create a Person object given a nationality.
        member this.CreatePerson (nationality: Nationality) =
            this.CreatePerson (nationality)

        /// Create a Person object given a nationality.
        member this.CreatePerson (nationality: Nationality, options: RandomPersonOptions) =
            this.CreatePerson (nationality, options)

        /// Create a list of Person objects given a nationality.
        member this.CreatePeople (amount: int, nationality: Nationality) =
            this.CreatePeople (amount, nationality) |> List.toSeq

        /// Create a list of Person objects given a nationality.
        member this.CreatePeople (amount: int, nationality: Nationality, options: RandomPersonOptions) =
            this.CreatePeople (amount, nationality, options) |> List.toSeq

        /// Create a list of strings given a template string where certain values will be replaced.
        member this.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string) =
            this.CreatePeopleTemplated (amount, nationality, outputString) |> List.toSeq

        /// Create a list of strings given a template string where certain values will be replaced.
        member this.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string, options: RandomPersonOptions) =
            this.CreatePeopleTemplated (amount, nationality, outputString, options) |> List.toSeq

        /// Validate an SSN for a given nationality.
        member this.ValidateSSN (nationality: Nationality, ssn: string) =
            this.ValidateSSN (nationality, ssn)
