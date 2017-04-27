// <copyright file="UserTest.cs">Copyright ©  2017</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using OOP_Eksamensopgave_2017;

namespace OOP_Eksamensopgave_2017.Tests
{
    /// <summary>This class contains parameterized unit tests for User</summary>
    [PexClass(typeof(User))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class UserTest
    {
    }
}
