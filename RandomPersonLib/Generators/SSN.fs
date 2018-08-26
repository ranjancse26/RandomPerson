﻿module internal SSN

open System
open RandomPersonLib
open DanishSSNGeneration
open DutchSSNGeneration
open FinnishSSNGeneration
open IcelandicSSNGeneration
open NorwegianSSNGeneration
open SwedishSSNGeneration

let generateSSN (random: Random) (nationality: Nationality) (birthdate : DateTime) (gender: Gender) (isAnonymizingSSN: bool) (isRemovingHypensFromSSN: bool) =
    let ssn = match nationality with
              | Nationality.Danish    -> generateDanishSSN    random birthdate gender isAnonymizingSSN
              | Nationality.Dutch     -> generateDutchSSN     random                  isAnonymizingSSN
              | Nationality.Finnish   -> generateFinnishSSN   random birthdate gender isAnonymizingSSN
              | Nationality.Icelandic -> generateIcelandicSSN random birthdate        isAnonymizingSSN
              | Nationality.Norwegian -> generateNorwegianSSN random birthdate gender isAnonymizingSSN
              | Nationality.Swedish   -> generateSwedishSSN   random birthdate gender isAnonymizingSSN
              | _ -> invalidArg "nationality" "Illegal nationality."

    match isRemovingHypensFromSSN with
    | true  -> ssn.Replace("-", "")
    | false -> ssn
