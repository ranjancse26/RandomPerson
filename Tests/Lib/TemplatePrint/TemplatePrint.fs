﻿namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open CommonTemplatePrint
open OrdinaryReplaces
open RandomReplaces
open TestData

[<TestClass>]
type ``parseOrdinaryReplaces should`` () =

    [<TestMethod>]
    member __.``return a string with #{SSN} replaced by SSN`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "SSN: #{SSN}"

        let expectedString = sprintf "SSN: %s" person.SSN

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{FirstName.ToLower()} replaced by the FirstName in all lower caps`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "First name: #{FirstName.ToLower()}"

        let expectedString = sprintf "First name: %s" (person.FirstName.ToLower())

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{LastName.ToUpper()} replaced by the FirstName in all upper caps`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "Last name: #{LastName.ToUpper()}"

        let expectedString = sprintf "Last name: %s" (person.LastName.ToUpper())

        Assert.AreEqual(expectedString, replaced)

[<TestClass>]
type ``cleanupNumber should`` () =

    [<TestMethod>]
    member __.``return "123" when given " 123"`` () =
        let clean = cleanupValue " 123"

        Assert.AreEqual("123", clean)

    [<TestMethod>]
    member __.``return "123" when given "123 "`` () =
        let clean = cleanupValue "123 "

        Assert.AreEqual("123", clean)

[<TestClass>]
type ``replaceRandomSwitch should`` () =

    let randomSwitchPattern = "#{Random\(\s?switch\s?,(\s?['\w\,\\\/]+\s?,)+\s?['\w\,\\\/]+\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, true, false)} in a string with either true or false`` () =
        let remaining = "Married: #{Random(switch, true, false)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, true, false)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "true" || randomPart = "false")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, yes, no, maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch, yes, no, maybe)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, yes, no, maybe)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,yes,no,maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch,yes,no,maybe)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,yes,no,maybe)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,one,two,three,four)} in a string with either one, two, three or four`` () =
        let remaining = "Married: #{Random(switch,one,two,three,four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,one,two,three,four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "one" || randomPart = "two" || randomPart = "three" || randomPart = "four")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,one\,one,two,three,four)} in a string with either one,one; two; three or four`` () =
        let remaining = "Married: #{Random(switch,one\,one,two,three,four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,one\,one,two,three,four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPartForNonSpecial = returnString.Split(',').[0].Split(' ').[1]
        let randomPartForSpecial    = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)

        match randomPartForNonSpecial with
        | "two" | "three" | "four" -> Assert.IsTrue(true)
        | _ -> Assert.IsTrue(randomPartForSpecial.[0] = "one" && randomPartForSpecial.[1] = "one")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, one\,one\, two, two, three, four)} in a string with either one,one, two; two; three or four`` () =
        let remaining = "Married: #{Random(switch, one\,one\, two, two, three, four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, one\,one\, two, two, three, four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPartForNonSpecial = returnString.Split(',').[0].Split(' ').[1]
        let randomPartForSpecial    = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)

        match randomPartForNonSpecial with
        | "two" | "three" | "four" -> Assert.IsTrue(true)
        | _ -> Assert.IsTrue(randomPartForSpecial.[0] = "one" && randomPartForSpecial.[1] = "one" && randomPartForSpecial.[2] = "two")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, 'one\,one\', 'two\, two')} in a string with either 'one,one' or 'two, two'`` () =
        let remaining = "Married: #{Random(switch, 'one'\,'one\', 'two'\, 'two')}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, 'one'\,'one\', 'two'\, 'two')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue((randomPart.[0] = "'one'" && randomPart.[1] = "'one'") ||  (randomPart.[0] = "'two'" && randomPart.[1] = "'two'"))

    [<TestMethod>]
    member __.``find and replace #{Random(switch, 'one', 'two')} in a string with either 'one' or 'two'`` () =
        let remaining = "Married: #{Random(switch, 'one', 'two')}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, 'one', 'two')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "'one'" || randomPart = "'two'")
