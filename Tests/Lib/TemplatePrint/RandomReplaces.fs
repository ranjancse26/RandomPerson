﻿namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomReplaces
open System.Text.RegularExpressions

[<TestClass>]
type ``replaceRandomInt should`` () =

    let randomIntRegex = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomInt randomIntRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(10 <= randomPart && randomPart <= 100)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,0)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomInt randomIntRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(-10 <= randomPart && randomPart <= 0)

[<TestClass>]
type ``replaceRandomIntWithStep should`` () =

    let randomIntWithStepSizeRegex = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomIntWithStep randomIntWithStepSizeRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,5,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,5,0)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomIntWithStep randomIntWithStepSizeRegex remaining 

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = -10 || randomPart = -5 || randomPart = 0)

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 20, 100)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomIntWithStep randomIntWithStepSizeRegex remaining 

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)

[<TestClass>]
type ``replaceRandomFloat should`` () =

    let randomFloatRegex = Regex "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 1000, 100000)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float, 1000, 100000)}, married: Random(switch,true,false)"

        let returnString = replace randomFloat randomFloatRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(1000.0 <= randomPart && randomPart <= 100000.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-1000, 0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-1000, 0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloat randomFloatRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(-1000.0 <= randomPart && randomPart <= 0.0)

[<TestClass>]
type ``replaceRandomSwitch should`` () =

    let randomSwitchRegex = Regex "#{Random\((?:switch,+)\s?(?:\s*([\w']+),?){2,}\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, true, false)} in a string with either true or false`` () =
        let remaining = "Married: #{Random(switch, true, false)}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "true" || randomPart = "false")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, yes, no, maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch, yes, no, maybe)}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,yes,no,maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch,yes,no,maybe)}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,one,two,three,four)} in a string with either one,one; two; three or four`` () =
        let remaining = "Married: #{Random(switch,one,two,three,four)}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)

        match randomPart with
        | "one" | "two" | "three" | "four" -> Assert.IsTrue(true)
        | _                                -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,one, two, three, four)} in a string with either one, two, three or four`` () =
        let remaining = "Married: #{Random(switch,one, two, three, four)}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)

        match randomPart with
        | "one" |"two" | "three" | "four" -> Assert.IsTrue(true)
        | _                               -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, 'one', 'two')} in a string with either 'one'' or 'two'`` () =
        let remaining = "Married: #{Random(switch, 'one', 'two')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart.[0] = "'one'" ||  randomPart.[0] = "'two'")

    [<TestMethod>]
    member __.``find and replace #{Random(switch, 'one', 'two')} in a string with either 'one' or 'two'`` () =
        let remaining = "Married: #{Random(switch, 'one', 'two')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "'one'" || randomPart = "'two'")
