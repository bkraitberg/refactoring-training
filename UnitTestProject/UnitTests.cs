using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Refactoring;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_TuscDoesNotThrowAnException()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("Jason\r\nsfa\r\n1\r\nYes\r\n8\r\n\r\n"))
                {
                    Console.SetIn(reader);

                    Tusc.Start();
                }
            }
        }

        [TestMethod]
        public void Test_InvalidUserIsNotAccepted()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("Joel\r\n"))
                {
                    Console.SetIn(reader);

                    Tusc.Start();
                }

                Assert.IsTrue(writer.ToString().Contains("You entered an unknown user"));
            }
        }

        [TestMethod]
        public void Test_InvalidPasswordIsNotAccepted()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("Jason\r\nsfb\r\n"))
                {
                    Console.SetIn(reader);

                    Tusc.Start();
                }

                Assert.IsTrue(writer.ToString().Contains("You entered an invalid password"));
            }
        }

        [TestMethod]
        public void Test_UserCanCancelPurchase()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                using (var reader = new StringReader("Jason\r\nsfa\r\n1\r\nNo\r\n8\r\n\r\n"))
                {
                    Console.SetIn(reader);

                    Tusc.Start();
                }

                Assert.IsTrue(writer.ToString().Contains("Purchase cancelled"));

            }
        }
    }
}
