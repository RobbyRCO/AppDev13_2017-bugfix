using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Internship.Api.Controllers;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace Internship.Api.Tests.Controllers
{
    [TestFixture]
    class StageControllerTests
    {
        private TestableStageController _controller;
        private StageBuilder _stageBuilder = new StageBuilder();
        private RandomGenerator _random = new RandomGenerator();

        [SetUp]
        public void SetUp()
        {
            _controller = TestableStageController.CreateInstance();
        }

        [Test]
        public void GetAll_ReturnsAllStagesFromRepository()
        {
            //Arrange
            var stages = new List<Stage> { _stageBuilder.WithId().Build(), _stageBuilder.WithId().Build() };
            _controller._stageRepositoryMock.Setup(repo => repo.GetAll()).Returns(stages);

            //Act
            var result = _controller.GetAll() as OkNegotiatedContentResult<IEnumerable<Stage>>;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EquivalentTo(stages));
            _controller._stageRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public void Get_IdExists_ReturnsStage()
        {
            //Arrange
            var stage = _stageBuilder.WithId().Build();
            _controller._stageRepositoryMock.Setup(repo => repo.Get(stage.Id)).Returns(stage);

            //Act
            var result = _controller.Get(stage.Id) as OkNegotiatedContentResult<Stage>;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo(stage));
            _controller._stageRepositoryMock.Verify(repo => repo.Get(stage.Id), Times.Once);
        }

        [Test]
        public void Get_IdDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            _controller._stageRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Stage>(null);

            //Act
            var result = _controller.Get(_random.Next()) as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _controller._stageRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Post_ValidStage_SavesStageInDb()
        {
            //Arrange
            var stage = _stageBuilder.Build();
            _controller._stageRepositoryMock.Setup(repo => repo.Post(stage)).Returns(() =>
            {
                stage.Id = _random.Next();
                return stage;
            });

            //Act
            var result = _controller.Post(stage) as CreatedAtRouteNegotiatedContentResult<Stage>;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo(stage));
            Assert.That(result.Content.Id, Is.GreaterThan(0));
            Assert.That(result.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(result.RouteValues.Count, Is.EqualTo(2));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Stage"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(result.Content.Id));
            _controller._stageRepositoryMock.Verify(repo => repo.Post(stage), Times.Once);
        }

        [Test]
        public void Post_InvalidStage_ReturnsBadRequest()
        {
            //Arrange
            _controller.ModelState.AddModelError("StageOpdracht", "StageOpdracht is required");

            //Act
            var result = _controller.Post(_stageBuilder.WithoutOpdracht().Build()) as BadRequestResult;

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Put_ValidIdAndStage_SavesChangesInDb()
        {
            //Arrange
            var stage = _stageBuilder.WithId().Build();
            _controller._stageRepositoryMock.Setup(repo => repo.Get(stage.Id)).Returns(stage);
            _controller._stageRepositoryMock.Setup(repo => repo.Update(stage)).Returns(stage);

            //Act
            var result = _controller.Put(stage.Id, stage) as OkNegotiatedContentResult<Stage>;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo(stage));
            _controller._stageRepositoryMock.Verify(repo => repo.Update(stage), Times.Once);
            _controller._stageRepositoryMock.Verify(repo => repo.Get(stage.Id), Times.Once);
        }

        [Test]
        public void Put_StageNotFound_ReturnsNotFound()
        {
            //Arrange
            var stage = _stageBuilder.Build();
            _controller._stageRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Stage>(null);

            //Act
            var result = _controller.Put(stage.Id, stage) as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Put_InvalidStageModelState_ReturnsBadRequest()
        {
            //Arrange
            _controller.ModelState.AddModelError("StageOpdracht", "StageOpdracht is required");
            var stage = _stageBuilder.WithoutOpdracht().WithId().Build();

            //Act
            var result = _controller.Put(stage.Id, stage) as BadRequestResult;

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndStageId_ReturnsBadRequest()
        {
            //Arrange
            var stage = _stageBuilder.WithId(10).Build();

            //Act
            var result = _controller.Put(20, stage) as BadRequestResult;

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Delete_ValidId_ReturnsOkResult()
        {
            //Arrange
            var stage = _stageBuilder.WithId().Build();
            _controller._stageRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(stage);

            //Act
            var result = _controller.Delete(_random.Next()) as OkResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _controller._stageRepositoryMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _controller._stageRepositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Delete_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller._stageRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Stage>(null);

            //Act
            var result = _controller.Delete(_random.Next()) as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);

        }

        class TestableStageController : StageController
        {
            public Mock<IStageRepository> _stageRepositoryMock { get; }

            private TestableStageController(Mock<IStageRepository> _stageRepositoryMock) : base(_stageRepositoryMock.Object)
            {
                this._stageRepositoryMock = _stageRepositoryMock;
            }

            public static TestableStageController CreateInstance()
            {
                var stageRepoMock = new Mock<IStageRepository>();
                return new TestableStageController(stageRepoMock);
            }
        }

        class StageBuilder
        {
            private Stage stage;
            private Random rand;

            public StageBuilder()
            {
                rand = new Random();
                stage = new Stage
                {
                    Student = new Student(),
                    StageStatus = StageStatus.InBehandeling,
                    StartDatum = DateTime.Now,
                    EindDatum = DateTime.Now,
                    StageOpdracht = new Stageopdracht()
                };
            }

            public StageBuilder WithId(int id = -1)
            {
                if (id == -1)
                {
                    stage.Id = rand.Next(1, Int32.MaxValue);
                }
                else
                {
                    stage.Id = id;
                }
                return this;
            }

            public StageBuilder WithoutOpdracht()
            {
                stage.StageOpdracht = null;
                return this;
            }

            public Stage Build()
            {
                return stage;
            }
        }

        class RandomGenerator
        {
            private Random random;

            public RandomGenerator()
            {
                random = new Random();
            }

            public int Next()
            {
                return random.Next(1, int.MaxValue);
            }
        }
    }
}
