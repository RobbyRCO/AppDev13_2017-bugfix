using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Internship.Api.Controllers;
using Internship.Api.Models;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Internship.Api.Tests.Controllers
{
    [TestClass]

    public class StagecoördinatorControllerTest
    {
        private TestableStagecoördinatorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = TestableStagecoördinatorController.CreateInstance();
        }

        public void Get_ReturnsAllStagecoördinatorsFromRepository()
        {
            //Arrange

            var allStagecoördinators = new List<Stagecoördinator>()
            {
                new Stagecoördinator {Id = 1},
                new Stagecoördinator {Id = 2}
            };
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.GetAll()).Returns(() => allStagecoördinators);

            //Act
            var returnedStagecoördinator = _controller.Get() as OkNegotiatedContentResult<IEnumerable<Stagecoördinator>>;

            //Assert
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(returnedStagecoördinator.Content.ToList(), Is.EquivalentTo(allStagecoördinators));
        }

        [Test]
        public void Get_ValidId_ReturnsStagecoördinatorIfItExists()
        {
            //Arrange
            var stagecoördinator = new StagecoördinatorBuilder().WithId().Build();
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(stagecoördinator);

            //Act
            var okResult = _controller.Get(stagecoördinator.Id) as OkNegotiatedContentResult<Stagecoördinator>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Get(stagecoördinator.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(stagecoördinator));
        }

        [Test]
        public void Get_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Stagecoördinator>(null);

            //Act
            var okResult = _controller.Get(new Random().Next(1, int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
        }


        [Test]
        public void GetHomeData_ValidId_ReturnsStageCoördinatorHomePageData()
        {
            //Arrange
            var stagecoördinator = new StagecoördinatorBuilder().WithId().Build();
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns(stagecoördinator);

            //Act

            var returnedHomePageData = _controller.GetHomeData(stagecoördinator.Id) as OkNegotiatedContentResult<Stagecoördinator>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.GetHomePageData(stagecoördinator.Id), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(stagecoördinator));  
        }

        [Test]
        public void GetHomeData_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns<Stagecoördinator>(null);

            //Act
            var okResult = _controller.GetHomeData(new Random().Next(1, int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.GetHomePageData(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_ValidId_ReturnsStagecoördinatorWithUserAccount()
        {
            //Arrange
            var stagecoördinator = new StagecoördinatorBuilder().WithId().WithUseraccount().Build();
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.GetStagecoördinatorWithUserAccount(stagecoördinator.UserAccountId)).Returns(stagecoördinator);

            //Act

            var returnedHomePageData = _controller.Get(stagecoördinator.UserAccountId) as OkNegotiatedContentResult<Stagecoördinator>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(
                repo => repo.GetStagecoördinatorWithUserAccount(stagecoördinator.UserAccountId), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(stagecoördinator));
        }


        [Test]
        public void Post_ValidStagecoördinatorIsSavedInRepository()
        {
            //Arange
            var stagecoördinator = new StagecoördinatorBuilder().Build();
            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.Post(It.IsAny<Stagecoördinator>())).Returns(() =>
            {
                stagecoördinator.Id = new Random().Next();
                return stagecoördinator;
            });

            //Act
            var actionResult = _controller.Post(stagecoördinator) as CreatedAtRouteNegotiatedContentResult<Stagecoördinator>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Post(stagecoördinator), Times.Once);
            Assert.That(actionResult.Content, Is.EqualTo(stagecoördinator)); //---
            Assert.That(actionResult.Content.Id, Is.GreaterThan(0));
            Assert.That(actionResult.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(actionResult.RouteValues.Count, Is.EqualTo(2));
            Assert.That(actionResult.RouteValues["controller"], Is.EqualTo("Stagecoördinator"));
            Assert.That(actionResult.RouteValues["id"], Is.EqualTo(actionResult.Content.Id));
        }

        [Test]
        public void Post_InValidStagecoördinatorModelStateCausesBadRequest()
        {
            //Arange
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badActionResult = _controller.Post(new StagecoördinatorBuilder().WithNumber().Build()) as BadRequestResult;

            //Assert
            Assert.That(badActionResult, Is.Not.Null);
        }

        [Test]
        public void Put_ExistingStagecoördinatorIsSavedInRepository()
        {
            //Arrange
            var stagecoördinator = new StagecoördinatorBuilder().WithId().Build();

            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.Get(stagecoördinator.Id)).Returns(() => stagecoördinator);

            //Act
            var okResult = _controller.Put(stagecoördinator.Id, stagecoördinator) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Get(stagecoördinator.Id), Times.Once);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Update(stagecoördinator), Times.Once);
        }

        [Test]
        public void Put_NonExistingStagecoördinatorReturnsNotFound()
        {
            //Aragnge
            _controller.StagecoördinatorRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var stagecoördinator = new StagecoördinatorBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.Put(stagecoördinator.Id, stagecoördinator) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Get(stagecoördinator.Id), Times.Once);
            _controller.StagecoördinatorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Stagecoördinator>()), Times.Never);
        }

        [Test]
        public void Put_InValidStagecoördinatorModelStateCausesBadRequest()
        {
            //Arrange
            var stagecoördinator = new StagecoördinatorBuilder().WithNumber().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.Put(stagecoördinator.Id, stagecoördinator) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndStagecoördinatorIdCausesBadRequest()
        {
            //Arange
            var stagecoördinator = new StagecoördinatorBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.Put(10, stagecoördinator) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Delete_ExistingStagecoördinatorIsDeletedFromRepository()
        {
            //Arrange
            var stagecoördinator = new StagecoördinatorBuilder().WithId().Build();

            _controller.StagecoördinatorRepositoryMock.Setup(repo => repo.Get(stagecoördinator.Id)).Returns(() => stagecoördinator);

            //Act
            var action = _controller.Delete(stagecoördinator.Id) as OkResult;

            //Assert
            Assert.That(action, Is.Not.Null);
            _controller.StagecoördinatorRepositoryMock.Verify(r => r.Get(stagecoördinator.Id), Times.Once);
            _controller.StagecoördinatorRepositoryMock.Verify(r => r.Delete(stagecoördinator.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingStagecoördinatorReturnsNotFound()
        {
            //Arrange
            _controller.StagecoördinatorRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            int someId = new Random().Next();
            //Act
            var action = _controller.Delete(someId) as NotFoundResult;

            //Assert
            Assert.That(action, Is.Not.Null);
        }

        // -

        class TestableStagecoördinatorController : StagecoördinatorController
        {
            public Mock<IStagecoördinatorRepository> StagecoördinatorRepositoryMock { get; }



            public TestableStagecoördinatorController(Mock<IStagecoördinatorRepository> stagecoördinatorRepositoryMock) : base(stagecoördinatorRepositoryMock.Object)
            {
                StagecoördinatorRepositoryMock = stagecoördinatorRepositoryMock;
            }

            public static TestableStagecoördinatorController CreateInstance()
            {
                var stagecoördinatorRepository = new Mock<IStagecoördinatorRepository>();
                return new TestableStagecoördinatorController(stagecoördinatorRepository);
            }
        }

        class StagecoördinatorBuilder
        {
            private Stagecoördinator _stagecoördinator;
            private Random _random;

            public StagecoördinatorBuilder()
            {
                _stagecoördinator = new Stagecoördinator()
                {
                  
                };
                _random = new Random();
            }

            public Stagecoördinator Build()
            {
                return _stagecoördinator;
            }

            public StagecoördinatorBuilder WithNumber()
            {
                _stagecoördinator.Nummer = Guid.NewGuid().ToString();
                return this;
            }

            public StagecoördinatorBuilder WithId()
            {
                _stagecoördinator.Id = _random.Next(int.MaxValue);
                return this;
            }

            public StagecoördinatorBuilder WithUseraccount()
            {
                _stagecoördinator.UserAccount = new UserAccount
                {
                    Id = Guid.NewGuid().ToString()
                };
                _stagecoördinator.UserAccountId = _stagecoördinator.UserAccount.Id;
                return this;
            }
        }



    }
}
