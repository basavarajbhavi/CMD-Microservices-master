using CMD.API.Appointments.Controllers;
using CMD.Business.Appointments.Implementations;
using CMD.Business.Appointments.Interfaces;
using CMD.Model.Appointments;
using CMD.Repository.Appointments.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace FeedbackUnitTest
{
    [TestClass]
    public class FeedbackUnitTest
    {
        FeedbackRepository obj;
        //unit test initialise for Business Layer
        IFeedbackService ob;
        [TestInitialize]
        public void ObjectCreation()
        {
            obj = new FeedbackRepository();
            ob = new FeedbackService(obj);
        }
        #region Data(Repository)
        [TestMethod]
        public void GetFeedback_ReturnFeedbackId()
        {
            int id = 3;
            var data = obj.GetFeedback(id);
            Assert.IsNotNull(data);
        }
        [TestMethod]
        // [ExpectedException(typeof(NullReferenceException))]
        public void GetFeedback_ReturnNoFeedbackId()
        {
            int id = 1;
            var data = obj.GetFeedback(id);
            Assert.IsNull(data);
        }
        #endregion
        # region Controller
        [TestMethod]
        public void GetFeedbackWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IFeedbackService>();
            mockRepository.Setup(x => x.GetFeedback(3))
                .Returns(new FeedBack { Id = 3 });
            var controller = new FeedbackController(mockRepository.Object);
            // Act
            IHttpActionResult actionResult = controller.GetFeedback(3);
            var contentResult = actionResult as OkNegotiatedContentResult<FeedBack>;
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.Id);
        }
        [TestMethod]
        //[ExpectedException(typeof(NotFoundResult))]
        public void GetFeedbackNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IFeedbackService>();
            var controller = new FeedbackController(mockRepository.Object);
            // Act
            IHttpActionResult actionResult = controller.GetFeedback(10);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        #endregion
        #region business
        [TestMethod]
        public void GetFeedback_ShouldReturnFeedbacks()
        {
            var result = ob.GetFeedback(3);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetFeedback_ShouldReturnNothingForNegetiveAppointmentIds() // Negetive i.e., <= 0
        {
            //Act
            var results = obj.GetFeedback(-1);
            //Assert
            Assert.AreEqual(results, null);
        }
        #endregion
    }
}
