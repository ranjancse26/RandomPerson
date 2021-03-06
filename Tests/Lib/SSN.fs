﻿namespace Tests

open System
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open MathUtil
open SSN

[<TestClass>]
type ``generateSSN should`` () =

    [<TestMethod>]
    member __.``return a correct SSN for Danish male`` () =
        let country = Country.Denmark
        let birthdate = DateTime(1933, 7, 31)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(DenmarkSSNParameters.IndividualNumberStart,
                                                             DenmarkSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DenmarkSSNParameters.ChecksumStart, DenmarkSSNParameters.ChecksumLength))

        Assert.AreEqual(DenmarkSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("31", d)
        Assert.AreEqual("07", m)
        Assert.AreEqual("33", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 399)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd checksum)

    [<TestMethod>]
    member __.``return a correct SSN for Danish female`` () =
        let country = Country.Denmark
        let birthdate = DateTime(1962, 2, 3)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(DenmarkSSNParameters.IndividualNumberStart,
                                                             DenmarkSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DenmarkSSNParameters.ChecksumStart, DenmarkSSNParameters.ChecksumLength))

        Assert.AreEqual(DenmarkSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("03", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("62", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499 || 900 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven checksum)

    [<TestMethod>]
    member __.``return a correct SSN for Dutch person`` () =
        let country = Country.Netherlands
        let birthdate = DateTime(1962, 2, 3)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let individualNumber = Convert.ToInt32(ssn.Substring(NetherlandsSSNParameters.IndividualNumberStart,
                                                             NetherlandsSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NetherlandsSSNParameters.ChecksumStart, NetherlandsSSNParameters.ChecksumLength))

        Assert.AreEqual(NetherlandsSSNParameters.SsnLength, ssn.Length)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 99999999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return a correct SSN for Finnish male`` () =
        let country = Country.Finland
        let birthdate = DateTime(1925, 8, 7)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.Substring(FinlandSSNParameters.CenturySignStart, FinlandSSNParameters.CenturySignLength)
        let individualNumber = Convert.ToInt32(ssn.Substring(FinlandSSNParameters.IndividualNumberStart,
                                                             FinlandSSNParameters.IndividualNumberLength))
        let checksum = ssn.Substring(FinlandSSNParameters.ChecksumStart, FinlandSSNParameters.ChecksumLength)
        let checksumPattern = "^(\d|[A-Y])$"
        let checksumRegex = Regex checksumPattern

        Assert.AreEqual(FinlandSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("07", d)
        Assert.AreEqual("08", m)
        Assert.AreEqual("25", y)
        Assert.AreEqual("-", centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Finnish female`` () =
        let country = Country.Finland
        let birthdate = DateTime(1977, 2, 15)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.Substring(FinlandSSNParameters.CenturySignStart, FinlandSSNParameters.CenturySignLength)
        let individualNumber = Convert.ToInt32(ssn.Substring(FinlandSSNParameters.IndividualNumberStart,
                                                             FinlandSSNParameters.IndividualNumberLength))
        let checksum = ssn.Substring(FinlandSSNParameters.ChecksumStart, FinlandSSNParameters.ChecksumLength)
        let checksumPattern = "^(\d|[A-Y])$"
        let checksumRegex = Regex checksumPattern

        Assert.AreEqual(FinlandSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("77", y)
        Assert.AreEqual("-", centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Icelandic male`` () =
        let country = Country.Iceland
        let birthdate = DateTime(1945, 10, 28)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(IcelandSSNParameters.IndividualNumberStart,
                                                             IcelandSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(IcelandSSNParameters.ChecksumStart, IcelandSSNParameters.ChecksumLength))
        let centurySign = ssn.Substring(IcelandSSNParameters.CenturySignStart, IcelandSSNParameters.CenturySignLength)

        Assert.AreEqual(IcelandSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("28", d)
        Assert.AreEqual("10", m)
        Assert.AreEqual("45", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber < 100)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.AreEqual("9", centurySign)

    [<TestMethod>]
    member __.``return a correct SSN for Icelandic female`` () =
        let country = Country.Iceland
        let birthdate = DateTime(1912, 1, 2)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(IcelandSSNParameters.IndividualNumberStart,
                                                             IcelandSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(IcelandSSNParameters.ChecksumStart, IcelandSSNParameters.ChecksumLength))
        let centurySign = ssn.Substring(IcelandSSNParameters.CenturySignStart, IcelandSSNParameters.CenturySignLength)

        Assert.AreEqual(IcelandSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("02", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("12", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber < 100)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.AreEqual("9", centurySign)

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian male`` () =
        let country = Country.Norway
        let birthdate = DateTime(1954, 3, 21)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(NorwaySSNParameters.IndividualNumberStart,
                                                             NorwaySSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NorwaySSNParameters.ChecksumStart, NorwaySSNParameters.ChecksumLength))

        Assert.AreEqual(NorwaySSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("21", d)
        Assert.AreEqual("03", m)
        Assert.AreEqual("54", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian female`` () =
        let country = Country.Norway
        let birthdate = DateTime(1990, 5, 14)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(NorwaySSNParameters.IndividualNumberStart,
                                                             NorwaySSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NorwaySSNParameters.ChecksumStart, NorwaySSNParameters.ChecksumLength))

        Assert.AreEqual(NorwaySSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("14", d)
        Assert.AreEqual("05", m)
        Assert.AreEqual("90", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Swedish male`` () =
        let country = Country.Sweden
        let birthdate = DateTime(1952, 12, 25)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        
        let individualNumber = Convert.ToInt32(ssn.Substring(SwedenSSNParameters.oldSsnParams.IndividualNumberStart,
                                                             SwedenSSNParameters.oldSsnParams.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(SwedenSSNParameters.oldSsnParams.ChecksumStart, SwedenSSNParameters.oldSsnParams.ChecksumLength))

        Assert.AreEqual(SwedenSSNParameters.oldSsnParams.SsnLength, ssn.Length)
        Assert.AreEqual("25", d)
        Assert.AreEqual("12", m)
        Assert.AreEqual("52", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Swedish female`` () =
        let country = Country.Sweden
        let birthdate = DateTime(1982, 11, 30)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        
        let individualNumber = Convert.ToInt32(ssn.Substring(SwedenSSNParameters.oldSsnParams.IndividualNumberStart,
                                                             SwedenSSNParameters.oldSsnParams.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(SwedenSSNParameters.oldSsnParams.ChecksumStart, SwedenSSNParameters.oldSsnParams.ChecksumLength))

        Assert.AreEqual(SwedenSSNParameters.oldSsnParams.SsnLength, ssn.Length)
        Assert.AreEqual("30", d)
        Assert.AreEqual("11", m)
        Assert.AreEqual("82", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven individualNumber)
