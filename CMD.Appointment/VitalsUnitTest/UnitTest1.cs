using CMD.API.Appointments.Controllers;
using CMD.Business.Appointments.Interfaces;
using CMD.DTO.Appointments;
using CMD.Model.Appointments;
using CMD.Repository.Appointments;
using CMD.Repository.Appointments.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace VitalsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        VitalRepository repo = new VitalRepository();



        [TestMethod]
        public void GetVitalByID_ReturnNotNull()
        {
            var temp = repo.getVitalById(1);
            Assert.IsNotNull(temp);
        }



        [TestMethod]
        public void GetVital_ReturnNotNull()
        {
            var temp = repo.getAllVitals();
            Assert.IsNotNull(temp);
        }


        [TestMethod]
        public void GetVitalByID_IDNotPresent_ShouldReturnNull()
        {
            var temp = repo.getVitalById(1000);
            Assert.IsNull(temp);
        }



        [TestMethod]
        public void EditVital_ReturnsEditedVital()
        {
            CMDContext db = new CMDContext();
            var Vital = db.Vitals.Find(1);
            Vital.ECG = 44;
            repo.updateVital(Vital);
            var result = db.Vitals.Find(1);
            Assert.AreEqual(Vital.Temperature, result.Temperature);



        }



        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetControllerReturn()
        {
            // Arrange
            var mockRepository = new Mock<IVitalService>();
            var controller = new VitalController(mockRepository.Object); // Act
            IHttpActionResult actionResult = controller.GetAllVitals(10); // Assert
            Assert.IsInstanceOfType(actionResult, typeof(HttpResponseException));
        }



        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PutReturnContentResult()
        {
            // Arrange
            var mockRepository = new Mock<IVitalService>();
            var controller = new VitalController(mockRepository.Object); // Act
            IHttpActionResult actionResult = controller.Put(1, new VitalDTO { respiration_rate = 30 });
            var contentResult = actionResult as NegotiatedContentResult<Vital>; // Assert
                                                                                // Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            // Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }
    }
}
