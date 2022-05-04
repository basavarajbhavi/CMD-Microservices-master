using CMD.API.Appointments.Controllers;
using CMD.Business.Appointments.Implementations;
using CMD.Business.Appointments.Interfaces;
using CMD.DTO.Appointments;
using CMD.Model.Appointments;
using CMD.Repository.Appointments.Implementations;
using CMD.Repository.Appointments.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace RecommendationUnitTest
{
    [TestClass]
    public class UnitTest
    {
        IRecommendationService obj;
        RecommendationRepository obj2;
        [TestInitialize]
        public void ObjectCreation()
        {
            obj2 = new RecommendationRepository();
            obj = new RecommendationService(obj2);
        }
        [TestCleanup]
        public void ObjectDeletion()
        {
            obj = null;
        }
        [TestMethod]
        public void RemoveRecommedations_ShouldRemoveRecommendations()
        {
            var moq = new Mock<RecommendationRepository>();
            moq.Setup(r => r.RemoveRecommendation(It.IsAny<int>())).Returns(true);
            Action action = () =>
            {
                var service = new RecommendationService(moq.Object);
                Assert.IsTrue(service.RemoveRecommendation(1));
            };
        }
        [TestMethod]
        public void DeleteReturnsOk()
        {
            var mockRepository = new Mock<IRecommendationService>();
            var controller = new RecommendationController(mockRepository.Object);
            IHttpActionResult actionResult = controller.Remove(3);
        }
        [TestMethod]
        public void AddRecommendation_ShouldAddRecommendation()
        {
            RecommendationDTO prescription = new RecommendationDTO { AppointmentId = 5, DoctorId = 2 };
            var res = obj.AddRecommendtaion(prescription);
            Assert.IsNotNull(res);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IRecommendationService>();
            var controller = new RecommendationController(mockRepository.Object);
            // Act
            IHttpActionResult actionResult = controller.AddRecommendation(new RecommendationDTO { AppointmentId=1, DoctorId=1 });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Recommendation>;
            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }
    }
}
