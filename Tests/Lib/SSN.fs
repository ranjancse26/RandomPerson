﻿namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open SSN

[<TestClass>]
type ``generateSSN should`` () =

    [<TestMethod>]
    member __.``return a correct SSN for Danish male`` () =
        let nationality = Nationality.Danish
        let birthdate = DateTime(1933, 7, 31)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random nationality birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(DanishSSNParameters.IndividualNumberStart,
                                                             DanishSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DanishSSNParameters.ChecksumStart, DanishSSNParameters.ChecksumLength))

        Assert.AreEqual(DanishSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("31", d)
        Assert.AreEqual("07", m)
        Assert.AreEqual("33", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 399)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd checksum)

    [<TestMethod>]
    member __.``return a correct SSN for Danish female`` () =
        let nationality = Nationality.Danish
        let birthdate = DateTime(1962, 2, 3)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random nationality birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(DanishSSNParameters.IndividualNumberStart,
                                                             DanishSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DanishSSNParameters.ChecksumStart, DanishSSNParameters.ChecksumLength))

        Assert.AreEqual(DanishSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("03", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("62", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499 || 900 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven checksum)

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian male`` () =
        let nationality = Nationality.Norwegian
        let birthdate = DateTime(1954, 3, 21)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random nationality birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.IndividualNumberStart,
                                                             NorwegianSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.ChecksumStart, NorwegianSSNParameters.ChecksumLength))

        Assert.AreEqual(NorwegianSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("21", d)
        Assert.AreEqual("03", m)
        Assert.AreEqual("54", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian female`` () =
        let nationality = Nationality.Norwegian
        let birthdate = DateTime(1990, 5, 14)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random nationality birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.IndividualNumberStart,
                                                             NorwegianSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.ChecksumStart, NorwegianSSNParameters.ChecksumLength))

        Assert.AreEqual(NorwegianSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("14", d)
        Assert.AreEqual("05", m)
        Assert.AreEqual("90", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)
