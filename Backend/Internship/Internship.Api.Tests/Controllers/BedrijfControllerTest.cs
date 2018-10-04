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

    public class BedrijfControllerTest
    {

        private TestableBedrijfController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = TestableBedrijfController.CreateInstance();
        }

        public void Get_ReturnsAllBedrijvenFromRepository()
        {
            //Arrange

            var allBedrijven = new List<Bedrijf>()
            {
                new Bedrijf {Id = 1},
                new Bedrijf {Id = 2}
            };
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetAll()).Returns(() => allBedrijven);

            //Act
            var returnedBedrijf = _controller.Get() as OkNegotiatedContentResult<IEnumerable<Bedrijf>>;

            //Assert
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(returnedBedrijf.Content.ToList(), Is.EquivalentTo(allBedrijven));
        }

        [Test]
        public void Get_ValidId_ReturnsBedrijf()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithId().Build();
            _controller.BedrijfRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(bedrijf);

            //Act
            var okResult = _controller.Get(bedrijf.Id) as OkNegotiatedContentResult<Bedrijf>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.Get(bedrijf.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(bedrijf));
        }

        [Test]
        public void Get_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.BedrijfRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Bedrijf>(null);

            //Act
            var okResult = _controller.Get(new Random().Next(1,int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
        }


        [Test]
        public void GetHomeData_ValidId_ReturnsBedrijfHomePageData()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithId().Build();
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns(bedrijf);

            //Act

            var returnedHomePageData = _controller.GetHomeData(bedrijf.Id) as OkNegotiatedContentResult<Bedrijf>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetHomePageData(bedrijf.Id), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(bedrijf));
        }

        [Test]
        public void GetHomeData_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns<Bedrijf>(null);

            //Act
            var okResult = _controller.GetHomeData(new Random().Next(1, int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetHomePageData(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_ValidUserAccountId_ReturnsBedrijfAndUserAccount()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithId().WithUserAccount().Build();
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetBedrijfWithUserAccount(bedrijf.UserAccountId)).Returns(bedrijf);

            //Act

            var returnedHomePageData = _controller.Get(bedrijf.UserAccountId) as OkNegotiatedContentResult<Bedrijf>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetBedrijfWithUserAccount(bedrijf.UserAccountId), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(bedrijf));
        }

        [Test]
        public void Get_InvalidUserAccountId_ReturnsNotFound()
        {
            //Arrange
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetBedrijfWithUserAccount(It.IsAny<string>())).Returns<Bedrijf>(null);

            //Act
            var okResult = _controller.Get(Guid.NewGuid().ToString()) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetBedrijfWithUserAccount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetBedrijfWithBedrijfspromotorsAndContactpersonen_ValidId_ReturnsBedrijfWithDetails()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithId().WithBedrijfspromotor().WithContactpersoon().Build();
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetBedrijfWithBedrijfspromotorsAndContactpersonen(bedrijf.Id)).Returns(bedrijf);

            //Act

            var returnedHomePageData = _controller.GetBedrijfWithBedrijfspromotorsAndContactpersonen(bedrijf.Id) as OkNegotiatedContentResult<Bedrijf>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetBedrijfWithBedrijfspromotorsAndContactpersonen(bedrijf.Id), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(bedrijf));
        }

        [Test]
        public void GetBedrijfWithBedrijfspromotorsAndContactpersonen_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.BedrijfRepositoryMock.Setup(repo => repo.GetBedrijfWithBedrijfspromotorsAndContactpersonen(It.IsAny<int>())).Returns<Bedrijf>(null);

            //Act
            var okResult = _controller.GetBedrijfWithBedrijfspromotorsAndContactpersonen(new Random().Next(1,Int32.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.GetBedrijfWithBedrijfspromotorsAndContactpersonen(It.IsAny<int>()), Times.Once);
        }


        [Test]
        public void Post_ValidBedrijfIsSavedInRepository()
        {
            //Arange
            var bedrijf = new BedrijfBuilder().Build();
            _controller.BedrijfRepositoryMock.Setup(repo => repo.Post(It.IsAny<Bedrijf>())).Returns(() =>
            {
                bedrijf.Id = new Random().Next();
                return bedrijf;
            });

            //Act
            var actionResult = _controller.Post(bedrijf) as CreatedAtRouteNegotiatedContentResult<Bedrijf>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.Post(bedrijf), Times.Once);
            Assert.That(actionResult.Content, Is.EqualTo(bedrijf)); //---
            Assert.That(actionResult.Content.Id, Is.GreaterThan(0));
            Assert.That(actionResult.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(actionResult.RouteValues.Count, Is.EqualTo(2));
            Assert.That(actionResult.RouteValues["controller"], Is.EqualTo("Bedrijf"));
            Assert.That(actionResult.RouteValues["id"], Is.EqualTo(actionResult.Content.Id));
        }

        [Test]
        public void Post_InValidBedrijfModelStateCausesBadRequest()
        {
            //Arange
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badActionResult = _controller.Post(new BedrijfBuilder().WithNoName().Build()) as BadRequestResult;

            //Assert
            Assert.That(badActionResult, Is.Not.Null);
        }

        [Test]
        public void Put_ExistingBedrijfIsSavedInRepository()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithId().Build();

            _controller.BedrijfRepositoryMock.Setup(repo => repo.Get(bedrijf.Id)).Returns(() => bedrijf);

            //Act
            var okResult = _controller.Put(bedrijf.Id, bedrijf) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.Get(bedrijf.Id), Times.Once);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.Update(bedrijf), Times.Once);
        }

        [Test]
        public void Put_NonExistingBedrijReturnsNotFound()
        {
            //Aragnge
            _controller.BedrijfRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var bedrijf = new BedrijfBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.Put(bedrijf.Id, bedrijf) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.BedrijfRepositoryMock.Verify(repo => repo.Get(bedrijf.Id), Times.Once);
            _controller.BedrijfRepositoryMock.Verify(repo => repo.Update(It.IsAny<Bedrijf>()), Times.Never);
        }

        [Test]
        public void Put_InValidBedrijfModelStateCausesBadRequest()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithNoName().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.Put(bedrijf.Id, bedrijf) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndBedrijfIdCausesBadRequest()
        {
            //Arange
            var bedrijf = new BedrijfBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.Put(10, bedrijf) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }


        [Test]
        public void Delete_ExistingBedrijfIsDeletedFromRepository()
        {
            //Arrange
            var bedrijf = new BedrijfBuilder().WithId().Build();

            _controller.BedrijfRepositoryMock.Setup(repo => repo.Get(bedrijf.Id)).Returns(() => bedrijf);

            //Act
            var action = _controller.Delete(bedrijf.Id) as OkResult;

            //Assert
            Assert.That(action, Is.Not.Null);
            _controller.BedrijfRepositoryMock.Verify(r => r.Get(bedrijf.Id), Times.Once);
            _controller.BedrijfRepositoryMock.Verify(r => r.Delete(bedrijf.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingBedrijfReturnsNotFound()
        {
            //Arrange
            _controller.BedrijfRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            int someId = new Random().Next();
            //Act
            var action = _controller.Delete(someId) as NotFoundResult;

            //Assert
            Assert.That(action, Is.Not.Null);
        }


        //---
        class TestableBedrijfController : BedrijfController
        {
            public Mock<IBedrijfRepository> BedrijfRepositoryMock { get; }



            public TestableBedrijfController(Mock<IBedrijfRepository> bedrijfRepositoryMock) : base(bedrijfRepositoryMock.Object)
            {
                BedrijfRepositoryMock = bedrijfRepositoryMock;
            }

            public static TestableBedrijfController CreateInstance()
            {
                var bedrijfRepository = new Mock<IBedrijfRepository>();
                return new TestableBedrijfController(bedrijfRepository);
            }
        }

        class BedrijfBuilder
        {
            private Bedrijf _bedrijf;
            private Random _random;

            public BedrijfBuilder()
            {
                _bedrijf = new Bedrijf()
                {
                   Bedrijfsnaam = "Cegeka"
                };
                _random = new Random();
            }

            public Bedrijf Build()
            {
                return _bedrijf;
            }

            public BedrijfBuilder WithNoName()
            {
                _bedrijf.Id = _random.Next(int.MaxValue);
                return this;
            }

            public BedrijfBuilder WithId()
            {
                _bedrijf.Id = _random.Next(int.MaxValue);
                return this;
            }

            public BedrijfBuilder WithUserAccount()
            {
                _bedrijf.UserAccount = new UserAccount
                {
                    Id = Guid.NewGuid().ToString()
                };
                _bedrijf.UserAccountId = _bedrijf.UserAccount.Id;
                return this;
            }

            public BedrijfBuilder WithBedrijfspromotor()
            {
                _bedrijf.Bedrijfspromotors = new List<Bedrijfspromotor>
                {
                    new Bedrijfspromotor()
                };
                return this;
            }

            public BedrijfBuilder WithContactpersoon()
            {
                _bedrijf.Contactpersonen = new List<Contactpersoon>
                {
                    new Contactpersoon()
                };
                return this;
            }
        }


    }
}
