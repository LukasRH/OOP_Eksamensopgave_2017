using NUnit.Framework;
using OOP_ExamClassLibary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Commands;

namespace OOP_ExamClassLibary.Tests
{
    [TestFixture()]
    public class UserTests
    {
        //Localpart TestCases
        [TestCase("foobar@foo.dk")]
        [TestCase(".FooBar@foo.dk")]
        [TestCase("FOO.BAR@foo.dk")]
        [TestCase("foo-bar@foo.dk")]
        [TestCase("foo_bar@foo.dk")]
        [TestCase("-foobar0@foo.dk")]
        [TestCase("FooBar13@foo.dk")]
        [TestCase("foo0.bar38@foo.dk")]
        //Domain TestCases
        [TestCase("foobar@Foo.dk")]
        [TestCase("foobar@foo.bar.DK")]
        [TestCase("foobar@foo-bar.dk")]
        [TestCase("foobar@foo0.dk")]
        [TestCase("foobar@Foo15.dk")]
        [TestCase("foobar@0-Bar14.DK")]
        [TestCase("null@null.null")]
        public void UserTest_EmailValidationValidEmail_PassesIfValidEmail(string email)
        {
            Assert.DoesNotThrow(() =>
            {
                var user = new User("Foo","Bar","foobar",email);
            });
        }

        //Local part TestCases
        [TestCase("foo(bar)@foo.dk")]
        [TestCase("foo#bar@foo.dk")]
        [TestCase("foo@8bar@foo.dk")]
        [TestCase("@bar.dk")]
        [TestCase(" @bar.dk")]
        //Domain TestCases
        [TestCase("foo@.bar.dk")]
        [TestCase("foo@bar")]
        [TestCase("foo@#bar.dk")]
        [TestCase("foo@.dk")]
        [TestCase("foo@bar.")]
        [TestCase("foo@")]
        public void UserTest_EmailValidationInvalidEmail_PassIfInvalidEmail(string email)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var user = new User("Foo", "Bar", "foobar", email);
            });
        }

        [TestCase("foo")]
        [TestCase("foo09")]
        [TestCase("foo_bar")]
        public void UserTest_UsernameValidationValidUsername_PassIfValid(string username)
        {
            Assert.DoesNotThrow(() =>
            {
                new User("Foo", "Bar", username, "foo@bar.dk");
            });
        }

        [TestCase("Foo")]
        [TestCase("foo09 ")]
        [TestCase("foo#bar")]
        [TestCase("Foo.")]
        [TestCase("")]
        [TestCase(null)]
        public void UserTest_UsernameValidationInvalidUsername_PassIfInvalid(string username)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new User("Foo", "Bar", username, "foo@bar.dk");
            });
        }

        [TestCase("foo")]
        [TestCase("Foo")]
        [TestCase("foo-bar")]
        public void UserTest_FirstnameValidationValidFirstname_PassIfValid(string firstname)
        {
            Assert.DoesNotThrow(() =>
            {
                new User(firstname, "Bar", "foobar", "foo@bar.dk");
            });
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("f00")]
        [TestCase("f##")]
        public void UserTest_FirstnameValidationInvalidFirstname_PassIfInvalid(string firstname)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new User(firstname, "Bar", "foobar", "foo@bar.dk");
            });
        }

        [TestCase("bar")]
        [TestCase("Bar")]
        [TestCase("foo bar")]
        public void UserTest_LastnameValidationValidLastname_PassIfValid(string lastname)
        {
            Assert.DoesNotThrow(() =>
            {
                new User("foo", lastname, "foobar", "foo@bar.dk");
            });
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("f00")]
        [TestCase("f##")]
        public void UserTest_LastnameValidationInvalidLastname_PassIfInvalid(string lastname)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new User("foo", lastname, "foobar", "foo@bar.dk");
            });
        }

        [TestCase("1","foo","bar","foobar","foo@bar.dk","1000")]
        public void UserTest_TestArrayConstructor_Passes(params string[] data)
        {
            Assert.DoesNotThrow(() =>
            {
                new User(data);
            });
        }

        [Test]
        public void UserTest_GetFirstname()
        {
            var user = new User("Foo", "Bar", "foobar", "foo@foo.net");
            Assert.AreEqual("Foo", user.Firstname);
        }

        [Test]
        public void UserTest_GetLastname()
        {
            var user = new User("Foo", "Bar", "foobar", "foo@foo.net");
            Assert.AreEqual("Bar", user.Lastname);
        }

        [Test]
        public void UserTest_GetUsername()
        {
            var user = new User("Foo", "Bar", "foobar", "foo@foo.net");
            Assert.AreEqual("foobar", user.Username);
        }

        [Test]
        public void UserTest_GetEmail()
        {
            var user = new User("Foo", "Bar", "foobar", "foo@foo.net");
            Assert.AreEqual("foo@foo.net", user.Email);
        }

        [Test]
        public void UserTest_GetBalance()
        {
            var user = new User("Foo", "Bar", "foobar", "foo@foo.net", 100);
            Assert.AreEqual(100, user.Balance);
        }

        [Test]
        public void UserTest_GetID()
        {
            var user = new User("Foo", "Bar", "foobar", "foo@foo.net");
            Assert.Greater(user.Id, 1);
        }

        [Test]
        public void UserTest_EqualsToSameUser_PassIfSame()
        {
            var user = new User("foo","bar","foobar","foo@bar.dk");
            Assert.IsTrue(user.Equals(user));
        }

        [Test]
        public void UserTest_EqualsToSimularUser_PassIfDiffrent()
        {
            var user = new User("foo", "bar", "foobar", "foo@bar.dk");
            var user2 = new User("foo","bar","foobar", "foo@bar.dk");
            Assert.IsFalse(user.Equals(user2));
        }

        [Test]
        public void UserTest_EqualsToProduct_PassIfDiffrent()
        {
            var user = new User("foo", "bar", "foobar", "foo@bar.dk");
            var product = new Product("test", 20);
            Assert.IsFalse(user.Equals(product));
        }

        [Test]
        public void UserTest_CompareToSameUser_PassIfSame()
        {
            var user = new User("foo", "bar", "foobar", "foo@bar.dk");
            Assert.IsTrue(user.CompareTo((object) user) == 0);
        }

        public void UserTest_CompareToProduct_PassIfNotSame()
        {
            var user = new User("foo", "bar", "foobar", "foo@bar.dk");
            var product = new Product("test", 20);
        }
    }
}