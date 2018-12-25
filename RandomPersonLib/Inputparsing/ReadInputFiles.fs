﻿module internal ReadInputFiles

open System.IO
open System.Text
open RandomPersonLib
open CountryFiles
open GenericFiles
open PostalCodeCityStatesGen
open PersonData
open Util

[<NoEquality;NoComparison>]
type InputFiles = {
    generic:     GenericFiles
    denmark:     CountryFiles
    finland:     CountryFiles
    iceland:     CountryFiles
    netherlands: CountryFiles
    norway:      CountryFiles
    sweden:      CountryFiles
    usa:         CountryFiles
}

let readInputFiles () =
    let generic = {
        passwords             = File.ReadAllLines("data/Generic/passwords.txt", Encoding.UTF8);
    }

    let denmark = {
        generalData          = readDataFromJsonFile<PersonData> "data/Denmark/denmark.json";
        addresses1           = File.ReadAllLines("data/Denmark/Gader i København.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/Denmark/DK.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Denmark);
    }

    let finland = {
        generalData          = readDataFromJsonFile<PersonData> "data/Finland/finland.json";
        addresses1           = File.ReadAllLines("data/Finland/Streets in Finland.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/Finland/FI.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Finland);
    }

    let iceland = {
        generalData          = readDataFromJsonFile<PersonData> "data/Iceland/iceland.json";
        addresses1           = File.ReadAllLines("data/Iceland/Streets in Iceland.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/Iceland/postnumer.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Iceland);
    }

    let netherlands = {
        generalData          = readDataFromJsonFile<PersonData> "data/Netherlands/netherlands.json";
        addresses1           = File.ReadAllLines("data/Netherlands/Addresses in NL.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/Netherlands/NL.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Netherlands);
    }

    let norway = {
        generalData          = readDataFromJsonFile<PersonData> "data/Norway/norway.json";
        addresses1           = File.ReadAllLines("data/Norway/Gater i Oslo.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/Norway/Postnummerregister-ansi.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Norway);
    }

    let sweden = {
        generalData          = readDataFromJsonFile<PersonData> "data/Sweden/sweden.json";
        addresses1           = File.ReadAllLines("data/Sweden/Gator i Stockholm.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/Sweden/SE.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Sweden);
    }

    let usa = {
        generalData          = readDataFromJsonFile<PersonData> "data/USA/usa.json";
        addresses1           = File.ReadAllLines("data/USA/Streets in Sullivan.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("data/USA/US.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.USA);
    }

    {
        generic = generic;
        denmark = denmark;
        finland = finland;
        iceland = iceland;
        netherlands = netherlands;
        norway = norway;
        sweden = sweden;
        usa = usa;
    }
