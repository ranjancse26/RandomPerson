﻿namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Util
open MathUtil
open RandomUtil
open StringUtil

[<TestClass>]
type ``isOdd should`` () =

    [<TestMethod>]
    member __.``return true given an odd number`` () =
        let number = 201
        let result = isOdd number
        Assert.IsTrue(result)

    [<TestMethod>]
    member __.``return false given an even number`` () =
        let number = 200
        let result = isOdd number
        Assert.IsFalse(result)

[<TestClass>]
type ``isEven should`` () =

    [<TestMethod>]
    member __.``return true given an even number`` () =
        let number = 200
        let result = isEven number
        Assert.IsTrue(result)

    [<TestMethod>]
    member __.``return false given an odd number`` () =
        let number = 201
        let result = isEven number
        Assert.IsFalse(result)

[<TestClass>]
type ``randomIntBetween should`` () =

    [<TestMethod>]
    member __.``return an integer between 1 and 10, given 1 and 10`` () =
        let result = randomIntBetween 1 10
        Assert.IsTrue(1 <= result && result <= 10)

    [<TestMethod>]
    member __.``return an integer between -100 and 10, given -100 and 10`` () =
        let result = randomIntBetween -100 10
        Assert.IsTrue(-100 <= result && result <= 10)

[<TestClass>]
type ``randomIntBetweenWithStep should`` () =

    [<TestMethod>]
    member __.``return an integer that is either 0, 5 or 10`` () =
        let result = randomIntBetweenWithStep 0 5 10
        Assert.IsTrue(result = 0 || result = 5 || result = 10)

    [<TestMethod>]
    member __.``return an integer that is either 0, 25, 50, 75 or 100`` () =
        let result = randomIntBetweenWithStep 0 25 100
        Assert.IsTrue(result = 0 || result = 25 || result = 50 || result = 75 || result = 100)

[<TestClass>]
type ``randomFloatBetween should`` () =

    [<TestMethod>]
    member __.``return an random float between -20,0 and 20,0`` () =
        let result = randomFloatBetween -20.0 20.0
        Assert.IsTrue(-20.0 <= result && result < 20.0)

    [<TestMethod>]
    member __.``return an random float between 0,0 and 200,0`` () =
        let result = randomFloatBetween 0.0 200.0
        Assert.IsTrue(0.0 <= result && result < 200.0)

[<TestClass>]
type ``intFromChar should`` () =

    [<TestMethod>]
    member __.``return 1 given '1'`` () =
        let numberAsChar = '1'
        let number = intFromChar numberAsChar
        Assert.AreEqual(1, number)

    [<TestMethod>]
    member __.``return 0 given '0'`` () =
        let numberAsChar = '0'
        let number = intFromChar numberAsChar
        Assert.AreEqual(0, number)

    [<TestMethod>]
    member __.``return 9 given '9'`` () =
        let numberAsChar = '9'
        let number = intFromChar numberAsChar
        Assert.AreEqual(9, number)

    [<TestMethod>]
    member __.``return 5 given '5'`` () =
        let numberAsChar = '5'
        let number = intFromChar numberAsChar
        Assert.AreEqual(5, number)

[<TestClass>]
type ``incrementAtPosition should`` () =

    [<TestMethod>]
    member __.``return "1234" given "1233" and index 3`` () =
        let input = "1233"
        let incremented = input |> incrementAtPosition 3
        Assert.AreEqual("1234", incremented)

    [<TestMethod>]
    member __.``return "1234" given "1134" and index 1`` () =
        let input = "1134"
        let incremented = input |> incrementAtPosition 1
        Assert.AreEqual("1234", incremented)

    [<TestMethod>]
    member __.``return "1230" given "1239" and index 3`` () =
        let input = "1239"
        let incremented = input |> incrementAtPosition 3
        Assert.AreEqual("1230", incremented)

[<TestClass>]
type ``roundToNearest should`` () =

    [<TestMethod>]
    member __.``return 20,0 when given 1,0 as rounding and 19,6 as value`` () =
        let rounding = 1.0
        let value = 19.6
        let result = roundToNearest rounding value
        Assert.IsTrue(19.999 < result || result < 20.001)

    [<TestMethod>]
    member __.``return 50,0 when given 10,0 as rounding and 45,6 as value`` () =
        let rounding = 10.0
        let value = 45.6
        let result = roundToNearest rounding value
        Assert.IsTrue(49.999 < result || result < 50.001)

    [<TestMethod>]
    member __.``return -5,5 when given 0,5 as rounding and -5,4 as value`` () =
        let rounding = 0.5
        let value = -5.4
        let result = roundToNearest rounding value
        Assert.IsTrue(-5.501 < result || result < -5.499)
        
[<TestClass>]
type ``uppercase should`` () =

    [<TestMethod>]
    member __.``return "ASDF" when given "asdF"`` () =
        let result = uppercase "asdF"
        Assert.AreEqual("ASDF", result)

[<TestClass>]
type ``lowercase should`` () =

    [<TestMethod>]
    member __.``return "sadf" when given "SAdF"`` () =
        let result = lowercase "SAdF"
        Assert.AreEqual("sadf", result)

[<TestClass>]
type ``capitalize should`` () =

    [<TestMethod>]
    member __.``return "Sadf" when given "sadf"`` () =
        let result = capitalize "sadf"
        Assert.AreEqual("Sadf", result)

    [<TestMethod>]
    member __.``return "ToWN" when given "toWN"`` () =
        let result = capitalize "toWN"
        Assert.AreEqual("ToWN", result)

[<TestClass>]
type ``firstUppercaseRestLowercase should`` () =

    [<TestMethod>]
    member __.``return "Sadf" when given "saDF"`` () =
        let result = firstUppercaseRestLowercase "saDF"
        Assert.AreEqual("Sadf", result)

    [<TestMethod>]
    member __.``return "Town" when given "toWN"`` () =
        let result = firstUppercaseRestLowercase "toWN"
        Assert.AreEqual("Town", result)
