using System;
using System.Collections.Generic;
using System.Text;
using Web.Models;
using Xunit;

namespace BackEndTests
{
    public class EmailConfigurationTests
    {
        [Fact]
        public void TestEmailAddressAtCharacter()
        {
            EmailConfiguration emailTest = new EmailConfiguration();
            emailTest.NetworkCredName = "test";
            Assert.NotNull(emailTest.NetworkCredName);
        }

        [Fact]
        public void TestEmailAddressIsNull()
        {
            Email emailTest = new Email();
            Assert.Null(emailTest.EmailAddress);
        }

        [Fact]
        public void TestEmailAddressIsNotNull()
        {
            Email emailTest = new Email();
            emailTest.EmailAddress = "bob@bcit.ca";
            Assert.NotNull(emailTest.EmailAddress);
        }
    }
}
