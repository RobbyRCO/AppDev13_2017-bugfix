using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Internship.Api.Controllers;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Internship.Api.Tests.Controllers
{
    [TestClass]
    public class StagevoorstelControllerTest
    {
        private TestableStagevoorstelController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = TestableStagevoorstelController.CreateInstance();
        }

        public void Get_ReturnsAllStagevoorstellenFromRepository()
        {
            //Arrange

            var allStageVoorstellen = new List<Stagevoorstel>()
            {
                new Stagevoorstel {Id = 1},
                new Stagevoorstel {Id = 2}
            };
            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.GetAll()).Returns(() => allStageVoorstellen);

            //Act
            var returnedStagevoorstellen = _controller.Get() as OkNegotiatedContentResult<IEnumerable<Stagevoorstel>>;

            //Assert
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(returnedStagevoorstellen.Content.ToList(), Is.EquivalentTo(allStageVoorstellen));
        }

        [Test]
        public void Get_ReturnsStagevoorstelIfItExists()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();
            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(stagevoorstel);

            //Act
            var okResult = _controller.Get(stagevoorstel.Id) as OkNegotiatedContentResult<Stagevoorstel>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(stagevoorstel));
        }

        [Test]
        public void GetDetailInformation_ValidId_ReturnsStagevoorstelDetails()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithId().WithReview().WithStageOpdracht().WithBedrijfspromotor().WithContactpersoon().Build();
            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.GetDetail(stagevoorstel.Id))
                .Returns(stagevoorstel);

            //Act
            var actionResult =
                _controller.GetDetailInformation(stagevoorstel.Id) as OkNegotiatedContentResult<Stagevoorstel>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Content, Is.EqualTo(stagevoorstel));
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.GetDetail(stagevoorstel.Id), Times.Once);
        }

        [Test]
        public void GetDetailInformation_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.GetDetail(It.IsAny<int>()))
                .Returns<Stagevoorstel>(null);

            //Act
            var actionResult =
                _controller.GetDetailInformation(new Random().Next(1,Int32.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
        }

        [Test]
        public void Post_ValidStagevoorstelIsSavedInRepository()
        {
            //Arange
            var stagevoorstel = new StagevoorstelBuilder().Build();
            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Post(It.IsAny<Stagevoorstel>())).Returns(() =>
            {
                stagevoorstel.Id = new Random().Next();
                return stagevoorstel;
            });

            //Act
            var actionResult = _controller.Post(stagevoorstel) as CreatedAtRouteNegotiatedContentResult<Stagevoorstel>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Post(stagevoorstel), Times.Once);
            Assert.That(actionResult.Content, Is.EqualTo(stagevoorstel)); //---
            Assert.That(actionResult.Content.Id, Is.GreaterThan(0));
            Assert.That(actionResult.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(actionResult.RouteValues.Count, Is.EqualTo(2));
            Assert.That(actionResult.RouteValues["controller"], Is.EqualTo("Stagevoorstellen"));
            Assert.That(actionResult.RouteValues["id"], Is.EqualTo(actionResult.Content.Id));
        }

        [Test]
        public void Post_InValidStagevoorstelModelStateCausesBadRequest()
        {
            //Arange
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badActionResult = _controller.Post(new StagevoorstelBuilder().WithNoDate().Build()) as BadRequestResult;

            //Assert
            Assert.That(badActionResult, Is.Not.Null);
        }

        [Test]
        public void Put_ExistingStagevoorstelIsSavedInRepository()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Get(stagevoorstel.Id)).Returns(() => stagevoorstel);

            //Act
            var okResult = _controller.Put(stagevoorstel.Id, stagevoorstel) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Update(stagevoorstel), Times.Once);
        }

        [Test]
        public void Put_NonExistingStagevoorstelReturnsNotFound()
        {
            //Aragnge
            _controller.StagevoorstelRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.Put(stagevoorstel.Id, stagevoorstel) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Update(It.IsAny<Stagevoorstel>()), Times.Never);
        }

        [Test]
        public void Put_InValidStagevoorstelModelStateCausesBadRequest()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithNoDate().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.Put(stagevoorstel.Id, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndstagevoorstelIdCausesBadRequest()
        {
            //Arange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.Put(10, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutReview_ExistingStagevoorstel_StagevoorstelReviewSavedInRepository()
        {
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Get(stagevoorstel.Id)).Returns(() => stagevoorstel);

            //Act
            var okResult = _controller.PutReview(stagevoorstel.Id, stagevoorstel) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.UpdateReview(stagevoorstel), Times.Once);
        }

        [Test]
        public void PutReview_NonExistingStagevoorstelReturnsNotFound()
        {
            //Aragnge
            _controller.StagevoorstelRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.PutReview(stagevoorstel.Id, stagevoorstel) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.UpdateReview(It.IsAny<Stagevoorstel>()), Times.Never);
        }

        [Test]
        public void PutReview_InValidStagevoorstelModelStateCausesBadRequest()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithNoDate().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.PutReview(stagevoorstel.Id, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutReview_MismatchBetweenUrlIdAndstagevoorstelIdCausesBadRequest()
        {
            //Arange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.PutReview(10, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutToekennenLector_ExistingStagevoorstel_StagevoorstelReviewSavedInRepository()
        {
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Get(stagevoorstel.Id)).Returns(() => stagevoorstel);

            //Act
            var okResult = _controller.PutToekennenLector(stagevoorstel.Id, stagevoorstel) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.UpdateToekennenLector(stagevoorstel), Times.Once);
        }

        [Test]
        public void PutToekennenLector_NonExistingStagevoorstelReturnsNotFound()
        {
            //Aragnge
            _controller.StagevoorstelRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.PutToekennenLector(stagevoorstel.Id, stagevoorstel) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.UpdateToekennenLector(It.IsAny<Stagevoorstel>()), Times.Never);
        }

        [Test]
        public void PutToekennenLector_InValidStagevoorstelModelStateCausesBadRequest()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithNoDate().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.PutToekennenLector(stagevoorstel.Id, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutToekennenLector_MismatchBetweenUrlIdAndstagevoorstelIdCausesBadRequest()
        {
            //Arange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.PutToekennenLector(10, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutUpdateStatus_ExistingStagevoorstel_StagevoorstelReviewSavedInRepository()
        {
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Get(stagevoorstel.Id)).Returns(() => stagevoorstel);

            //Act
            var okResult = _controller.PutUpdateStatus(stagevoorstel.Id, stagevoorstel) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.UpdateStatus(stagevoorstel), Times.Once);
        }

        [Test]
        public void PutUpdateStatus_NonExistingStagevoorstelReturnsNotFound()
        {
            //Aragnge
            _controller.StagevoorstelRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.PutUpdateStatus(stagevoorstel.Id, stagevoorstel) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(repo => repo.UpdateStatus(It.IsAny<Stagevoorstel>()), Times.Never);
        }

        [Test]
        public void PutUpdateStatus_InValidStagevoorstelModelStateCausesBadRequest()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithNoDate().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.PutUpdateStatus(stagevoorstel.Id, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutUpdateStatus_MismatchBetweenUrlIdAndstagevoorstelIdCausesBadRequest()
        {
            //Arange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.PutUpdateStatus(10, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Delete_ExistingStagevoorstellenIsDeletedFromRepository()
        {
            //Arrange
            var stagevoorstel = new StagevoorstelBuilder().WithId().Build();

            _controller.StagevoorstelRepositoryMock.Setup(repo => repo.Get(stagevoorstel.Id)).Returns(() => stagevoorstel);

            //Act
            var action = _controller.Delete(stagevoorstel.Id) as OkResult;

            //Assert
            Assert.That(action, Is.Not.Null);
            _controller.StagevoorstelRepositoryMock.Verify(r => r.Get(stagevoorstel.Id), Times.Once);
            _controller.StagevoorstelRepositoryMock.Verify(r => r.Delete(stagevoorstel.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingStagevoorstellenReturnsNotFound()
        {
            //Arrange
            _controller.StagevoorstelRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            int someId = new Random().Next();
            //Act
            var action = _controller.Delete(someId) as NotFoundResult;

            //Assert
            Assert.That(action, Is.Not.Null);
        }

        class TestableStagevoorstelController : StagevoorstellenController
        {
            public Mock<IStagevoorstelRepository> StagevoorstelRepositoryMock { get; }

            public TestableStagevoorstelController(Mock<IStagevoorstelRepository> stagevoorstelRepositoryMock) : base(stagevoorstelRepositoryMock.Object)
            {
                StagevoorstelRepositoryMock = stagevoorstelRepositoryMock;
            }

            public static TestableStagevoorstelController CreateInstance()
            {
                var stagevoorstelRepository = new Mock<IStagevoorstelRepository>();
                return new TestableStagevoorstelController(stagevoorstelRepository);
            }
        }

        class StagevoorstelBuilder
        {
            private Stagevoorstel _stagevoorstel;
            private Random _random;

            public StagevoorstelBuilder()
            {
                _stagevoorstel = new Stagevoorstel()
                {
                    TimeStamp = DateTime.Now
                };
                _random = new Random();
            }

            public Stagevoorstel Build()
            {
                return _stagevoorstel;
            }

            public StagevoorstelBuilder WithNoDate()
            {
                
                _stagevoorstel.TimeStamp = DateTime.MinValue;
                return this;
            }

            public StagevoorstelBuilder WithId()
            {
                _stagevoorstel.Id = _random.Next(int.MaxValue);
                return this;
            }

            public StagevoorstelBuilder WithReview()
            {
                _stagevoorstel.Review = new Review();
                return this;
            }

            public StagevoorstelBuilder WithStageOpdracht()
            {
                _stagevoorstel.Stageopdracht = new Stageopdracht();
                return this;
            }

            public StagevoorstelBuilder WithBedrijfspromotor()
            {
                _stagevoorstel.Stageopdracht.Bedrijfspromotor = new Bedrijfspromotor();
                return this;
            }

            public StagevoorstelBuilder WithContactpersoon()
            {
                _stagevoorstel.Stageopdracht.Contactpersoon = new Contactpersoon();
                return this;
            }
        }
    }
}
