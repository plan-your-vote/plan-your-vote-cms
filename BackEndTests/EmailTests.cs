using Microsoft.Extensions.Options;
using System;
using Web.Controllers;
using Web.Models;
using Xunit;


namespace BackEndTests
{
    public class EmailTests
    {
        private readonly IOptions<EmailConfiguration> mockEmailConfiguration;

        [Fact]
        public void TestEmailAddressAtCharacter()
        {
            Email emailTest = new Email();
            emailTest.EmailAddress = "bob@bcit.ca";
            Assert.Contains("@", emailTest.EmailAddress);
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

        [Fact]
        public void Input_Valid_Vaues()
        {
            EmailController controller = new EmailController(mockEmailConfiguration);
            string emailAddress = "test@email.com";
            string subject = "subject";
            string message = "message";
            Email email = new Email();
            Assert.True(controller.SendEmail(emailAddress, subject, message));

        }
    }
}
