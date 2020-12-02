using System;
using System.Collections.Generic;
using System.Text;
using PlanYourVoteLibrary2;
using Xunit;

namespace BackEndTests
{
    public class EmailConfigurationTests
    {
        [Fact]
        public void NetworkCredNameNotNull()
        {
            EmailConfiguration emailTest = new EmailConfiguration();
            emailTest.NetworkCredName = "test";
            Assert.NotNull(emailTest.NetworkCredName);
        }

        [Fact]
        public void NotNull_Network_User_Name()
        {
            EmailConfiguration emailConfigTest = new EmailConfiguration();
            emailConfigTest.NetworkCredUserName = "test";
            Assert.NotNull(emailConfigTest.NetworkCredUserName);
        }

        [Fact]
        public void True_Smtp_Enabled()
        {
            EmailConfiguration emailConfigTest = new EmailConfiguration();
            emailConfigTest.SmtpEnableSSL = true;
            Assert.True(emailConfigTest.SmtpEnableSSL);
        }
    }
}
