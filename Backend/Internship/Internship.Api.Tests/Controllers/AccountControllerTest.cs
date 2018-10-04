using System.Web.Http.Results;
using Internship.Api.Controllers;
using Internship.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Internship.Api.Models;

namespace Internship.Api.Tests.Controllers
{
    [TestClass]
    class AccountControllerTest
    {



        private TestableAccountController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = TestableAccountController.CreateInstance();
        }


        [Test]
        public void Get_ReturnsUserAccountIfItExists()
        {
            //Arrange
            var userAccount = new AccountBuilder().WithId().Build();
            _controller.UserAccountRepositoryMock.Setup(repo => repo.Get(It.IsAny<string>())).Returns(userAccount);

            //Act
            var okResult = _controller.Get(userAccount.Id) as OkNegotiatedContentResult<UserAccount>;

            //Assert
          
           Assert.That(okResult, Is.Not.Null);
            _controller.UserAccountRepositoryMock.Verify(repo => repo.Get(userAccount.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(userAccount));
        
        }


       



        //---
        class TestableAccountController : AccountController
        {
            public Mock<IUserAccountRepository> UserAccountRepositoryMock { get; }



            public TestableAccountController(Mock<IUserAccountRepository> userAccountRepositoryMock) : base(userAccountRepositoryMock.Object)
            {
                UserAccountRepositoryMock = userAccountRepositoryMock;
            }

            public static TestableAccountController CreateInstance()
            {
                var accountRepository = new Mock<IUserAccountRepository>();
                return new TestableAccountController(accountRepository);
            }
        }

        class AccountBuilder
        {
            private UserAccount _user;

            public AccountBuilder()
            {
                _user = new UserAccount()
                {
                    UserName = "voornaam.achternaam@pxl.be",
                    Email = "voornaam.achternaam@pxl.be",
                    PasswordHash = "ANwEhgfqkNnxRIMt0qAz8su82mS8pZbULwhvJaceqc92HYKYZp82tsqMHDcnasV5vQ==",
                    EmailConfirmed = true
                };
            }

            public UserAccount Build()
            {
                return _user;
            }

 

            public AccountBuilder WithId()
            {
                _user.Id = "dca94895-33c7-44df-9e81-20a059df32ae";
                return this;
            }
        }
    }
}
