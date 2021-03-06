﻿namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open FinlandSSNValidation

[<TestClass>]
type ``validateSSNForFinland should`` () =

    [<TestMethod>]
    member __.``return false for "070969-147B"`` () =
        let isReal, _ = validateSSNForFinland "070969-147B"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "070969-147A"`` () =
        let isReal, _ = validateSSNForFinland "070969-147A"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "250949-648X"`` () =
        let isReal, _ = validateSSNForFinland "250949-648X"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "190846-711H"`` () =
        let isReal, _ = validateSSNForFinland "190846-711H"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "410846-711H"`` () =
        let isReal, _ = validateSSNForFinland "410846-711H"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "010199-0044"`` () =
        let isReal, _ = validateSSNForFinland "010199-0044"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "010199-2502"`` () =
        let isReal, _ = validateSSNForFinland "010199-2502"
        Assert.IsTrue(isReal)
        