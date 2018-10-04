using System;
using System.Collections.Generic;
using System.Linq;
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

    public class LectorControllerTest
    {

        private TestableLectorController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = TestableLectorController.CreateInstance();
        }

        public void Get_ReturnsAllLectorsFromRepository()
        {
            //Arrange

            var allLectors = new List<Lector>()
            {
                new Lector {Id = 1},
                new Lector {Id = 2}
            };
            _controller.LectorRepositoryMock.Setup(repo => repo.GetAll()).Returns(() => allLectors);

            //Act
            var returnedLectors = _controller.Get() as OkNegotiatedContentResult<IEnumerable<Lector>>;

            //Assert
            _controller.LectorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(returnedLectors.Content.ToList(), Is.EquivalentTo(allLectors));
        }

        [Test]
        public void Get_ValidId_ReturnsLector()
        {
            //Arrange
            var lector = new LectorBuilder().WithId().Build();
            _controller.LectorRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(lector);

            //Act
            var okResult = _controller.Get(lector.Id) as OkNegotiatedContentResult<Lector>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.Get(lector.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(lector));
        }

        [Test]
        public void Get_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.LectorRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Lector>(null);

            //Act
            var okResult = _controller.Get(new Random().Next(1,int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetHomeData_ValidId_ReturnsLectorHomePageData()
        {
            //Arrange
            var lector = new LectorBuilder().WithId().Build();
            _controller.LectorRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns(lector);

            //Act

            var returnedHomePageData = _controller.GetHomeData(lector.Id) as OkNegotiatedContentResult<Lector>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.GetHomePageData(lector.Id), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(lector));
        }

        [Test]
        public void GetHomeData_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.LectorRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns<Lector>(null);

            //Act
            var okResult = _controller.GetHomeData(new Random().Next(1, int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.GetHomePageData(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_ValidId_ReturnsLectorWithUserAccount()
        {
            //Arrange
            var lector = new LectorBuilder().WithId().WithUserAccount().Build();
            _controller.LectorRepositoryMock.Setup(repo => repo.GetLectorWithUserAccount(lector.UserAccountId)).Returns(lector);

            //Act

            var returnedHomePageData = _controller.Get(lector.UserAccountId) as OkNegotiatedContentResult<Lector>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.GetLectorWithUserAccount(lector.UserAccountId), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(lector));
        }

        [Test]
        public void Get_InvalidUserAccountId_ReturnsNotFound()
        {
            //Arrange
            _controller.LectorRepositoryMock.Setup(repo => repo.GetLectorWithUserAccount(It.IsAny<string>())).Returns<Lector>(null);

            //Act
            var okResult = _controller.Get(Guid.NewGuid().ToString()) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.GetLectorWithUserAccount(It.IsAny<string>()), Times.Once);
        }


        [Test]
        public void Post_ValidLectorIsSavedInRepository()
        {
            //Arange
            var lector = new LectorBuilder().Build();
            _controller.LectorRepositoryMock.Setup(repo => repo.Post(It.IsAny<Lector>())).Returns(() =>
            {
                lector.Id = new Random().Next();
                return lector;
            });

            //Act
            var actionResult = _controller.Post(lector) as CreatedAtRouteNegotiatedContentResult<Lector>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.Post(lector), Times.Once);
            Assert.That(actionResult.Content, Is.EqualTo(lector)); //---
            Assert.That(actionResult.Content.Id, Is.GreaterThan(0));
            Assert.That(actionResult.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(actionResult.RouteValues.Count, Is.EqualTo(2));
            Assert.That(actionResult.RouteValues["controller"], Is.EqualTo("Lector"));
            Assert.That(actionResult.RouteValues["id"], Is.EqualTo(actionResult.Content.Id));
        }

        [Test]
        public void Post_InValidLectorModelStateCausesBadRequest()
        {
            //Arange
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badActionResult = _controller.Post(new LectorBuilder().WithNoNumber().Build()) as BadRequestResult;

            //Assert
            Assert.That(badActionResult, Is.Not.Null);
        }

        [Test]
        public void Put_ExistingLectorIsSavedInRepository()
        {
            //Arrange
            var lector = new LectorBuilder().WithId().Build();

            _controller.LectorRepositoryMock.Setup(repo => repo.Get(lector.Id)).Returns(() => lector);

            //Act
            var okResult = _controller.Put(lector.Id, lector) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(repo => repo.Get(lector.Id), Times.Once);
            _controller.LectorRepositoryMock.Verify(repo => repo.Update(lector), Times.Once);
        }

        [Test]
        public void Put_NonExistingLectorReturnsNotFound()
        {
            //Aragnge
            _controller.LectorRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var lector = new LectorBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.Put(lector.Id, lector) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.LectorRepositoryMock.Verify(repo => repo.Get(lector.Id), Times.Once);
            _controller.LectorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Lector>()), Times.Never);
        }

        [Test]
        public void Put_InValidLectorModelStateCausesBadRequest()
        {
            //Arrange
            var lector = new LectorBuilder().WithNoNumber().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.Put(lector.Id, lector) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndLectorIdCausesBadRequest()
        {
            //Arange
            var lector = new LectorBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.Put(10, lector) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }


        [Test]
        public void Delete_ExistingLectorIsDeletedFromRepository()
        {
            //Arrange
            var lector = new LectorBuilder().WithId().Build();

            _controller.LectorRepositoryMock.Setup(repo => repo.Get(lector.Id)).Returns(() => lector);

            //Act
            var action = _controller.Delete(lector.Id) as OkResult;

            //Assert
            Assert.That(action, Is.Not.Null);
            _controller.LectorRepositoryMock.Verify(r => r.Get(lector.Id), Times.Once);
            _controller.LectorRepositoryMock.Verify(r => r.Delete(lector.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingLectorReturnsNotFound()
        {
            //Arrange
            _controller.LectorRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            int someId = new Random().Next();
            //Act
            var action = _controller.Delete(someId) as NotFoundResult;

            //Assert
            Assert.That(action, Is.Not.Null);
        }

        //---
        class TestableLectorController : LectorController
        {
            public Mock<ILectorRepository> LectorRepositoryMock { get; }

       

            public TestableLectorController(Mock<ILectorRepository> lectorRepositoryMock) : base(lectorRepositoryMock.Object)
            {
                LectorRepositoryMock = lectorRepositoryMock;
            }

            public static TestableLectorController CreateInstance()
            {
                var lectorRepository = new Mock<ILectorRepository>();
                return new TestableLectorController(lectorRepository);
            }
        }

        class LectorBuilder
        {
            private Lector _lector;
            private Random _random;

            public LectorBuilder()
            {
                _lector = new Lector()
                {
                   Voornaam = "David",
                   Achternaam = "Parren"
                };
                _random = new Random();
            }

            public Lector Build()
            {
                return _lector;
            }

            public LectorBuilder WithNoNumber()
            {
                _lector.Nummer = Guid.NewGuid().ToString();
                return this;
            }

            public LectorBuilder WithId()
            {
                _lector.Id = _random.Next(int.MaxValue);
                return this;
            }

            public LectorBuilder WithUserAccount()
            {
                _lector.UserAccount = new UserAccount
                {
                    Id = Guid.NewGuid().ToString()
                };
                _lector.UserAccountId = _lector.UserAccount.Id;
                return this;
            }
        }

    }
}
