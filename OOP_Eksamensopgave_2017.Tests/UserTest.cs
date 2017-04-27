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
        /// <summary>Test stub for CompareTo(User)</summary>
        [PexMethod]
        internal int CompareToTest([PexAssumeUnderTest]User target, User other)
        {
            int result = target.CompareTo(other);
            return result;
            // TODO: add assertions to method UserTest.CompareToTest(User, User)
        }

        /// <summary>Test stub for CompareTo(Object)</summary>
        [PexMethod]
        internal int CompareToTest01([PexAssumeUnderTest]User target, object obj)
        {
            int result = target.CompareTo(obj);
            return result;
            // TODO: add assertions to method UserTest.CompareToTest01(User, Object)
        }

        /// <summary>Test stub for .ctor(String, String, String, String)</summary>
        [PexMethod]
        internal User ConstructorTest(
            string firstname,
            string lastname,
            string username,
            string email
        )
        {
            User target = new User(firstname, lastname, username, email);
            return target;
            // TODO: add assertions to method UserTest.ConstructorTest(String, String, String, String)
        }

        /// <summary>Test stub for get_Email()</summary>
        [PexMethod]
        internal string EmailGetTest([PexAssumeUnderTest]User target)
        {
            string result = target.Email;
            return result;
            // TODO: add assertions to method UserTest.EmailGetTest(User)
        }

        /// <summary>Test stub for set_Email(String)</summary>
        [PexMethod]
        internal void EmailSetTest([PexAssumeUnderTest]User target, string value)
        {
            target.Email = value;
            // TODO: add assertions to method UserTest.EmailSetTest(User, String)
        }

        /// <summary>Test stub for Equals(Object)</summary>
        [PexMethod]
        internal bool EqualsTest([PexAssumeUnderTest]User target, object obj)
        {
            bool result = target.Equals(obj);
            return result;
            // TODO: add assertions to method UserTest.EqualsTest(User, Object)
        }

        /// <summary>Test stub for get_Firstname()</summary>
        [PexMethod]
        internal string FirstnameGetTest([PexAssumeUnderTest]User target)
        {
            string result = target.Firstname;
            return result;
            // TODO: add assertions to method UserTest.FirstnameGetTest(User)
        }

        /// <summary>Test stub for set_Firstname(String)</summary>
        [PexMethod]
        internal void FirstnameSetTest([PexAssumeUnderTest]User target, string value)
        {
            target.Firstname = value;
            // TODO: add assertions to method UserTest.FirstnameSetTest(User, String)
        }

        /// <summary>Test stub for GetHashCode()</summary>
        [PexMethod]
        internal int GetHashCodeTest([PexAssumeUnderTest]User target)
        {
            int result = target.GetHashCode();
            return result;
            // TODO: add assertions to method UserTest.GetHashCodeTest(User)
        }

        /// <summary>Test stub for get_Id()</summary>
        [PexMethod]
        internal int IdGetTest([PexAssumeUnderTest]User target)
        {
            int result = target.Id;
            return result;
            // TODO: add assertions to method UserTest.IdGetTest(User)
        }

        /// <summary>Test stub for get_Lastname()</summary>
        [PexMethod]
        internal string LastnameGetTest([PexAssumeUnderTest]User target)
        {
            string result = target.Lastname;
            return result;
            // TODO: add assertions to method UserTest.LastnameGetTest(User)
        }

        /// <summary>Test stub for set_Lastname(String)</summary>
        [PexMethod]
        internal void LastnameSetTest([PexAssumeUnderTest]User target, string value)
        {
            target.Lastname = value;
            // TODO: add assertions to method UserTest.LastnameSetTest(User, String)
        }

        /// <summary>Test stub for ToString()</summary>
        [PexMethod]
        internal string ToStringTest([PexAssumeUnderTest]User target)
        {
            string result = target.ToString();
            return result;
            // TODO: add assertions to method UserTest.ToStringTest(User)
        }

        /// <summary>Test stub for get_Username()</summary>
        [PexMethod]
        internal string UsernameGetTest([PexAssumeUnderTest]User target)
        {
            string result = target.Username;
            return result;
            // TODO: add assertions to method UserTest.UsernameGetTest(User)
        }

        /// <summary>Test stub for set_Username(String)</summary>
        [PexMethod]
        internal void UsernameSetTest([PexAssumeUnderTest]User target, string value)
        {
            target.Username = value;
            // TODO: add assertions to method UserTest.UsernameSetTest(User, String)
        }
    }
}
