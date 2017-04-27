using System;
using NUnit.Framework;
using OOP_Eksamensopgave_2017;

namespace OOP_Eksamensopgave_2017.Tests
{
    [TestFixture]
    public class UserTest
    {
        [TestCase("Lukas")]
        [TestCase("bob")]
        public void Firstname_ValidInput_PassesIfNoException(string firstname)
        {
            User Testuser = new User("Testuser", "Tester", "test", "test@test.dk");

            Assert.DoesNotThrow(() => Testuser.Firstname = firstname);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Firstname_InvalidInput_PassesIfException(string firstname)
        {
            User Testuser = new User("Testuser", "Tester", "test", "test@test.dk");

            Assert.Throws<ArgumentNullException>(() => Testuser.Firstname = firstname);
        }
    }
}
