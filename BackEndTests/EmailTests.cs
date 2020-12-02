using Microsoft.Extensions.Options;
using System;
using Web.Controllers;
using PlanYourVoteLibrary2;
using Xunit;


namespace BackEndTests
{
    public class EmailTests
    {
         public IOptions<EmailConfiguration> mockEmailConfiguration;

        [Fact]
        public void TestEmailAddressAtCharacter()
        {
            Email emailTest = new Email();
            emailTest.EmailAddress = "bob@bcit.ca";
            Assert.Contains("@", emailTest.EmailAddress);
        }

        [Fact]
        public void TestEmailAddressNoEmptySpace()
        {
            Email emailTest = new Email();
            Assert.DoesNotContain(" ", emailTest.EmailAddress); 
        }

        [Fact]
        public void TestEmailAddressIsNotNull()
        {
            Email emailTest = new Email();
            emailTest.EmailAddress = "bob@bcit.ca";
            Assert.NotNull(emailTest.EmailAddress);
        }

        [Fact]
        public void Email_SubjectNotNull()
        {
           
            Email emailTest = new Email();
            emailTest.Subject = "not null subject";
            Assert.NotNull(emailTest.Subject);
        }
        

        [Fact]
        public void Email_Meessage_NotNull()
        {
            Email emailTest = new Email();
            emailTest.Message = "not null message";
            Assert.NotNull(emailTest.Message);
        }
    }
}
