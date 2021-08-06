namespace Library.Tests

open System
open Library
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass() =

    [<TestMethod>]
    member this.TestMethodPassing() = Assert.IsTrue(true)

    [<TestMethod>]
    member this.TestInputHello() =
        let expected = "Hello World"
        let actual = Library.Say.hello "World"
        Assert.Equals(expected, actual)
