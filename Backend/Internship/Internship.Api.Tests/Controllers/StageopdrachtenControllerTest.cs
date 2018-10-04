using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using System.Web.Http.Results;
using Internship.Api.Controllers;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;

namespace Internship.Api.Tests.Controllers
{
    [TestClass]
    class StageopdrachtenControllerTest
    {
        private TestableStageopdrachtenController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = TestableStageopdrachtenController.CreateInstance();
        }

        [Test]
        public void Get_ReturnsAllStageopdrachtenFromRepository()
        {
            //Arrange

            var allStageOpdrachten = new List<Stageopdracht>()
            {
                new StageopdrachtenBuilder().WithId().Build(),
                new StageopdrachtenBuilder().WithId().Build(),
            };
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.GetAll()).Returns(() => allStageOpdrachten);

            //Act
            var returnedStageopdrachten = _controller.Get() as OkNegotiatedContentResult<IEnumerable<Stageopdracht>>;

            //Assert
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(returnedStageopdrachten.Content.ToList(), Is.EquivalentTo(allStageOpdrachten));
        }

        [Test]
        public void Get_ReturnsStageopdrachtIfItExists()
        {
            //Arrange
            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(stageopdracht);

            //Act
            var okResult = _controller.Get(stageopdracht.Id) as OkNegotiatedContentResult<Stageopdracht>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Get(stageopdracht.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(stageopdracht));
        }

        [Test]
        public void Get_ReturnsNotFoundIfItDoesNotExists()
        {
            //Arrange
            _controller.StageopdrachtenRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            //Act
            var someId = new Random().Next();
            var notFoundResult = _controller.Get(someId) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Get(someId), Times.Once);
        }

        [Test]
        public void GetStageopdrachtenWithFavorites_ReturnsStageopdrachtIfItExists()
        {
            //Arrange
            var stageopdracht = new StageopdrachtenBuilder().WithId().WithFavorites().Build();
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.GetWithFavorites(stageopdracht.Id)).Returns(stageopdracht);

            //Act
            var okResult = _controller.GetStageopdrachtenWithFavorites(stageopdracht.Id) as OkNegotiatedContentResult<Stageopdracht>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetWithFavorites(stageopdracht.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(stageopdracht));
        }

        [Test]
        public void GetStageopdrachtenWithFavorites_ReturnsNotFoundIfItDoesNotExists()
        {
            //Arrange
            _controller.StageopdrachtenRepositoryMock.Setup(r => r.GetWithFavorites(It.IsAny<int>())).Returns(() => null);
            //Act
            var someId = new Random().Next();
            var notFoundResult = _controller.GetStageopdrachtenWithFavorites(someId) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetWithFavorites(someId), Times.Once);
        }

        [Test]
        public void GetGoedgekeurdeStageopdrachten_ReturnsAllApprovedStageopdrachtenFromRepository()
        {
            //Arrange

            var allStageOpdrachten = new List<Stageopdracht>()
            {
               new StageopdrachtenBuilder().WithId().Build(),
                new StageopdrachtenBuilder().WithId().Build()
            };
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.GetAllGoedgekeurdeStageopdrachten()).Returns(() => allStageOpdrachten);

            //Act
            var returnedStageopdrachten = _controller.GetGoedgekeurdeStageopdrachten() as OkNegotiatedContentResult<IEnumerable<Stageopdracht>>;

            //Assert
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetAllGoedgekeurdeStageopdrachten(), Times.Once);
            Assert.That(returnedStageopdrachten.Content.ToList(), Is.EquivalentTo(allStageOpdrachten));
        }

        [Test]
        public void GetDetailStageopdrachtLectorAndStagecoordinator_ValidId_ReturnsAllStageopdrachtenDetailsFromRepository()
        {
            //Arrange
            var stageopdracht = new StageopdrachtenBuilder().WithContactpersoon().WithBedrijfspromotor().Build();
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.GetDetailStageopdracht(stageopdracht.Id)).Returns(() => stageopdracht);

            //Act
            var returnedStageopdracht = _controller.GetDetailStageopdrachtLectorAndStagecoordinator(stageopdracht.Id) as OkNegotiatedContentResult<Stageopdracht>;

            //Assert
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetDetailStageopdracht(stageopdracht.Id), Times.Once);
            Assert.That(returnedStageopdracht.Content, Is.EqualTo(stageopdracht));
        }

        [Test]
        public void GetDetailStageopdrachtLectorAndStagecoordinator_InvalidId_ReturnsAllStageopdrachtenDetailsFromRepository()
        {
            //Arrange
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.GetDetailStageopdracht(It.IsAny<int>())).Returns(() => null);

            //Act
            var actionResult = _controller.GetDetailStageopdrachtLectorAndStagecoordinator(new Random().Next(1,int.MaxValue)) as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetDetailStageopdracht(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Post_ValidStageopdrachtlIsSavedInRepository()
        {
            //Arange
            var stageopdracht = new StageopdrachtenBuilder().Build();
            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.Post(It.IsAny<Stageopdracht>())).Returns(() =>
            {
                stageopdracht.Id = new Random().Next();
                return stageopdracht;
            });

            //Act
            var actionResult = _controller.Post(stageopdracht) as CreatedAtRouteNegotiatedContentResult<Stageopdracht>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Post(stageopdracht), Times.Once);
            Assert.That(actionResult.Content, Is.EqualTo(stageopdracht)); //---
            Assert.That(actionResult.Content.Id, Is.GreaterThan(0));
            Assert.That(actionResult.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(actionResult.RouteValues.Count, Is.EqualTo(2));
            Assert.That(actionResult.RouteValues["controller"], Is.EqualTo("Stageopdrachten"));
            Assert.That(actionResult.RouteValues["id"], Is.EqualTo(actionResult.Content.Id));
        }

        [Test]
        public void Post_InValidStageopdrachtModelStateCausesBadRequest()
        {
            //Arange
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badActionResult = _controller.Post(new StageopdrachtenBuilder().WithContactpersoon().Build()) as BadRequestResult;

            //Assert
            Assert.That(badActionResult, Is.Not.Null);
        }

        [Test]
        public void Put_ExistingStageopdrachtIsSavedInRepository()
        {
            //Arrange
            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();

            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.Get(stageopdracht.Id)).Returns(() => stageopdracht);

            //Act
            var okResult = _controller.Put(stageopdracht.Id, stageopdracht) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Get(stageopdracht.Id), Times.Once);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Update(stageopdracht), Times.Once);
        }

        [Test]
        public void Put_NonExistingStageopdrachtReturnsNotFound()
        {
            //Aragnge
            _controller.StageopdrachtenRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.Put(stageopdracht.Id, stageopdracht) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Get(stageopdracht.Id), Times.Once);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.Update(It.IsAny<Stageopdracht>()), Times.Never);
        }

        [Test]
        public void Put_InValidStageopdrachtModelStateCausesBadRequest()
        {
            //Arrange
            var stagevoorstel = new StageopdrachtenBuilder().WithContactpersoon().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.Put(stagevoorstel.Id, stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndStageopdrachtIdCausesBadRequest()
        {
            //Arange
            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.Put(10, stageopdracht) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        //START
        [Test]
        public void PutFavorites_ExistingStageopdrachtAndFavoritesIsSavedInRepository()
        {
            //Arrange
            var stageopdracht = new StageopdrachtenBuilder().WithId().WithFavorites().Build();

            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.GetWithFavorites(stageopdracht.Id)).Returns(() => stageopdracht);

            //Act
            var okResult = _controller.PutFavorites(stageopdracht.Id, new Random().Next(1,int.MaxValue), stageopdracht) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetWithFavorites(stageopdracht.Id), Times.Once);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.UpdateFavorites(stageopdracht, It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void PutFavorites_NonExistingStageopdrachtReturnsNotFound()
        {
            //Aragnge
            _controller.StageopdrachtenRepositoryMock.Setup(s => s.GetWithFavorites(It.IsAny<int>())).Returns(() => null);

            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.PutFavorites(stageopdracht.Id, new Random().Next(1, int.MaxValue), stageopdracht) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.GetWithFavorites(stageopdracht.Id), Times.Once);
            _controller.StageopdrachtenRepositoryMock.Verify(repo => repo.UpdateFavorites(stageopdracht, It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void PutFavorites_InValidStageopdrachtModelStateCausesBadRequest()
        {
            //Arrange
            var stagevoorstel = new StageopdrachtenBuilder().WithContactpersoon().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.PutFavorites(stagevoorstel.Id, new Random().Next(1, int.MaxValue), stagevoorstel) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void PutFavorites_MismatchBetweenUrlIdAndStageopdrachtIdCausesBadRequest()
        {
            //Arange
            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.PutFavorites(10, new Random().Next(1, int.MaxValue), stageopdracht) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }
        //STOP

        [Test]
        public void Delete_ExistingStageopdrachtenIsDeletedFromRepository()
        {
            //Arrange
            var stageopdracht = new StageopdrachtenBuilder().WithId().Build();

            _controller.StageopdrachtenRepositoryMock.Setup(repo => repo.Get(stageopdracht.Id)).Returns(() => stageopdracht);

            //Act
            var action = _controller.Delete(stageopdracht.Id) as OkResult;

            //Assert
            Assert.That(action, Is.Not.Null);
            _controller.StageopdrachtenRepositoryMock.Verify(r => r.Get(stageopdracht.Id), Times.Once);
            _controller.StageopdrachtenRepositoryMock.Verify(r => r.Delete(stageopdracht.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingStageopdrachtenReturnsNotFound()
        {
            //Arrange
            _controller.StageopdrachtenRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            int someId = new Random().Next();
            //Act
            var action = _controller.Delete(someId) as NotFoundResult;

            //Assert
            Assert.That(action, Is.Not.Null);
        }

        class TestableStageopdrachtenController : StageopdrachtenController
        {
            public Mock<IStageopdrachtenRepository> StageopdrachtenRepositoryMock { get; }

            public TestableStageopdrachtenController(Mock<IStageopdrachtenRepository> stageopdrachtenRepositoryMock)
                : base(stageopdrachtenRepositoryMock.Object)
            {
                StageopdrachtenRepositoryMock = stageopdrachtenRepositoryMock;
            }

            public static TestableStageopdrachtenController CreateInstance()
            {
                var stageopdrachtenReposiroty = new Mock<IStageopdrachtenRepository>();
                return new TestableStageopdrachtenController(stageopdrachtenReposiroty);
            }
        }

        class StageopdrachtenBuilder
        {
            private Stageopdracht _stageopdracht;
            private Random _random;

            public StageopdrachtenBuilder()
            {
                _stageopdracht = new Stageopdracht()
                {
                    Opdrachtgever = new Bedrijf
                    {
                        Bedrijfsnaam = "Cegeka"
                    }
                };
                _random = new Random();
            }

            public Stageopdracht Build()
            {
                return _stageopdracht;
            }

            public StageopdrachtenBuilder WithContactpersoon()
            {
                _stageopdracht.Contactpersoon = new Contactpersoon
                {
                    Voornaam = "Henk",
                    Achternaam = "Jenkins"
                };
           
                return this;
            }

            public StageopdrachtenBuilder WithId()
            {
                _stageopdracht.Id = _random.Next(int.MaxValue);
                return this;
            }

            public StageopdrachtenBuilder WithFavorites()
            {
                _stageopdracht.StudentFavorieten = new List<Student>
                {
                    new Student(),
                    new Student()
                };
                return this;
            }

            public StageopdrachtenBuilder WithBedrijfspromotor()
            {
                _stageopdracht.Bedrijfspromotor = new Bedrijfspromotor();
                return this;
            }

        }

    }

}
