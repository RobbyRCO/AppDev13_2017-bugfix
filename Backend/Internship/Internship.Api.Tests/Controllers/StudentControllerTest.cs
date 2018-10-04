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

    public class StudentControllerTest
    {
        private TestableStudentController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = TestableStudentController.CreateInstance();
        }

        [Test]
        public void Get_ReturnsAllStudentenFromRepository()
        {
            //Arrange

            var allStudenten = new List<Student>()
            {
                new Student {Id = 1},
                new Student {Id = 2}
            };
            _controller.StudentRepositoryMock.Setup(repo => repo.GetAll()).Returns(() => allStudenten);

            //Act
            var returnedStudent = _controller.Get() as OkNegotiatedContentResult<IEnumerable<Student>>;

            //Assert
            _controller.StudentRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(returnedStudent.Content.ToList(), Is.EquivalentTo(allStudenten));
        }

        [Test]
        public void Get_ReturnsStudentIfItExists()
        {
            //Arrange
            var student = new StudentBuilder().WithId().Build();
            _controller.StudentRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(student);

            //Act
            var okResult = _controller.Get(student.Id) as OkNegotiatedContentResult<Student>;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StudentRepositoryMock.Verify(repo => repo.Get(student.Id), Times.Once);
            Assert.That(okResult.Content, Is.EqualTo(student));
        }



        [Test]
        public void GetHomeData_ReturnsStudentHomePageData()
        {
            //Arrange
            var student = new StudentBuilder().WithId().Build();
            _controller.StudentRepositoryMock.Setup(repo => repo.GetHomePageData(It.IsAny<int>())).Returns(student);

            //Act

            var returnedHomePageData = _controller.GetHomeData(student.Id) as OkNegotiatedContentResult<Student>;

            //Assert
            Assert.That(returnedHomePageData, Is.Not.Null);
            _controller.StudentRepositoryMock.Verify(repo => repo.GetHomePageData(student.Id), Times.Once);
            Assert.That(returnedHomePageData.Content, Is.EqualTo(student));
        }

        [Test]
        public void Get_ValidId_ReturnsStudentWithUserAccount()
        {
            //Arrange
            var student = new StudentBuilder().WithId().WithUserAccount().Build();
            _controller.StudentRepositoryMock.Setup(repo => repo.GetStudentWithUserAccount(student.UserAccountId))
                .Returns(student);

            //Act
            var actionResult = _controller.Get(student.UserAccountId) as OkNegotiatedContentResult<Student>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Content, Is.EqualTo(student));
            _controller.StudentRepositoryMock.Verify(repo => repo.GetStudentWithUserAccount(student.UserAccountId), Times.Once);
        }

        [Test]
        public void Get_InvalidId_ReturnsNotFound()
        {
            //Arrange
            _controller.StudentRepositoryMock.Setup(repo => repo.GetStudentWithUserAccount(It.IsAny<string>()))
                .Returns<Student>(null);

            //Act
            var actionResult = _controller.Get(Guid.NewGuid().ToString()) as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.StudentRepositoryMock.Verify(repo => repo.GetStudentWithUserAccount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Post_ValidStudentIsSavedInRepository()
        {
            //Arange
            var student = new StudentBuilder().Build();
            _controller.StudentRepositoryMock.Setup(repo => repo.Post(It.IsAny<Student>())).Returns(() =>
            {
                student.Id = new Random().Next();
                return student;
            });

            //Act
            var actionResult = _controller.Post(student) as CreatedAtRouteNegotiatedContentResult<Student>;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            _controller.StudentRepositoryMock.Verify(repo => repo.Post(student), Times.Once);
            Assert.That(actionResult.Content, Is.EqualTo(student)); //---
            Assert.That(actionResult.Content.Id, Is.GreaterThan(0));
            Assert.That(actionResult.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(actionResult.RouteValues.Count, Is.EqualTo(2));
            Assert.That(actionResult.RouteValues["controller"], Is.EqualTo("Student"));
            Assert.That(actionResult.RouteValues["id"], Is.EqualTo(actionResult.Content.Id));
        }

        [Test]
        public void Post_InValidStudentModelStateCausesBadRequest()
        {
            //Arange
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badActionResult = _controller.Post(new StudentBuilder().WithNoNumber().Build()) as BadRequestResult;

            //Assert
            Assert.That(badActionResult, Is.Not.Null);
        }

        [Test]
        public void Put_ExistingStudentIsSavedInRepository()
        {
            //Arrange
            var student = new StudentBuilder().WithId().Build();

            _controller.StudentRepositoryMock.Setup(repo => repo.Get(student.Id)).Returns(() => student);

            //Act
            var okResult = _controller.Put(student.Id, student) as OkResult;

            //Assert
            Assert.That(okResult, Is.Not.Null);
            _controller.StudentRepositoryMock.Verify(repo => repo.Get(student.Id), Times.Once);
            _controller.StudentRepositoryMock.Verify(repo => repo.Update(student), Times.Once);
        }

        [Test]
        public void Put_NonExistingStudentReturnsNotFound()
        {
            //Aragnge
            _controller.StudentRepositoryMock.Setup(s => s.Get(It.IsAny<int>())).Returns(() => null);

            var student = new StudentBuilder().WithId().Build();

            //Act
            var notFoundResult = _controller.Put(student.Id, student) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);

            _controller.StudentRepositoryMock.Verify(repo => repo.Get(student.Id), Times.Once);
            _controller.StudentRepositoryMock.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Never);
        }

        [Test]
        public void Put_InValidStudentModelStateCausesBadRequest()
        {
            //Arrange
            var student = new StudentBuilder().WithNoNumber().Build();

            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var badRequestResult = _controller.Put(student.Id, student) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }

        [Test]
        public void Put_MismatchBetweenUrlIdAndStudentIdCausesBadRequest()
        {
            //Arange
            var student = new StudentBuilder().WithId().Build();

            //Act
            var badRequestResult = _controller.Put(10, student) as BadRequestResult;

            //Assert
            Assert.That(badRequestResult, Is.Not.Null);
        }


        [Test]
        public void Delete_ExistingStudentIsDeletedFromRepository()
        {
            //Arrange
            var student = new StudentBuilder().WithId().Build();

            _controller.StudentRepositoryMock.Setup(repo => repo.Get(student.Id)).Returns(() => student);

            //Act
            var action = _controller.Delete(student.Id) as OkResult;

            //Assert
            Assert.That(action, Is.Not.Null);
            _controller.StudentRepositoryMock.Verify(r => r.Get(student.Id), Times.Once);
            _controller.StudentRepositoryMock.Verify(r => r.Delete(student.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingStudentReturnsNotFound()
        {
            //Arrange
            _controller.StudentRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
            int someId = new Random().Next();
            //Act
            var action = _controller.Delete(someId) as NotFoundResult;

            //Assert
            Assert.That(action, Is.Not.Null);
        }

        //---
        class TestableStudentController : StudentController
        {
            public Mock<IStudentRepository> StudentRepositoryMock { get; }



            public TestableStudentController(Mock<IStudentRepository> studentRepositoryMock) : base(studentRepositoryMock.Object)
            {
                StudentRepositoryMock = studentRepositoryMock;
            }

            public static TestableStudentController CreateInstance()
            {
                var studentRepository = new Mock<IStudentRepository>();
                return new TestableStudentController(studentRepository);
            }
        }

        class StudentBuilder
        {
            private Student _student;
            private Random _random;

            public StudentBuilder()
            {
                _student = new Student()
                {
                    Achternaam = "Schirock",
                    Voornaam = "Anissa"
                };
                _random = new Random();
            }

            public Student Build()
            {
                return _student;
            }

            public StudentBuilder WithNoNumber()
            {
                _student.Nummer = Guid.NewGuid().ToString();
                return this;
            }

            public StudentBuilder WithId()
            {
                _student.Id = _random.Next(int.MaxValue);
                return this;
            }

            public StudentBuilder WithUserAccount()
            {
                _student.UserAccount = new UserAccountBuilder().Build();
                return this;

            }
        }

        class UserAccountBuilder
        {
            private UserAccount userAccount;

            public UserAccountBuilder()
            {
                userAccount = new UserAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    PasswordHash = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = Guid.NewGuid().ToString()
                };
            }

            public UserAccount Build()
            {
                return userAccount;
            }
        }
    }
}
