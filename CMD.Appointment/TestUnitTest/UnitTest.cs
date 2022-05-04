using CMD.API.Appointments.Controllers;
using CMD.Business.Appointments.Implementations;
using CMD.DTO.Appointments;
using CMD.Model.Appointments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace TestUnitTest
{
    [TestClass]
    public class UnitTest
    {

        TestController controller = null;
        TestService obj = null;



        [TestInitialize]
        public void Initializer()
        {



            obj = new TestService();
            controller = new TestController(obj);
        }




        [TestCleanup]
        public void Cleaner()
        {
            obj = null;
            controller = null;
        }

        [TestMethod]
        public void GetTests_ShouldReturnAllTestNamesPresentInTheDataBase()
        {
            //Act
            IHttpActionResult actionResult = this.controller.GetTests();
            var contentResult = actionResult as OkNegotiatedContentResult<ICollection<Test>>;
            //Assert
            Assert.IsNotNull(contentResult);
        }



        //[TestMethod]
        //public void AddTest_AppointmentIdAndTest_ReturnsCreatedLocation()
        //{

        // //arrange
        // Test test = new Test { Id = 4, Name = "Cholesterol" };



        // //Act
        // IHttpActionResult actionResult = this.controller.AddTest(15, test);
        // var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<TestReport>;



        // //Assert
        // Assert.AreEqual($"addtest/{15}",createdResult.RouteName);
        // Assert.AreEqual(4, createdResult.RouteValues["Id"]);
        //}





        [TestMethod]
        public void GetRecommendedTest_AppointmentId_ReturnOkStatusWithListOfTest()
        {
            //Act
            IHttpActionResult recommendedTests = this.controller.GetRecommendedTest(15);
            var actionResult = recommendedTests as OkNegotiatedContentResult<ICollection<Test>>;



            //Assert
            Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Content);
        }



        [TestMethod]
        public void GetRecommendedTest_InvalidAppointmentId_ReturnNotFoundStatus()
        {
            //Act
            IHttpActionResult recommendedTest = this.controller.GetRecommendedTest(0);



            //Assert
            Assert.IsInstanceOfType(recommendedTest, typeof(NotFoundResult));
        }



        //[TestMethod]
        //public void GetTestReports_ReturnsNotFoundIfAddedNothing()
        //{
        // // Act
        // IHttpActionResult testReports = this.controller.GetTestReports();



        // // Assert
        // Assert.IsInstanceOfType(testReports, typeof(NotFoundResult));
        //}



        [TestMethod]
        public void GetTestReports_ReturnsTestReportsDTOIfAddedByDoctor()
        {
            //Act
            IHttpActionResult testReports = this.controller.GetTestReports();
            var actionResult = testReports as OkNegotiatedContentResult<ICollection<TestReportDTO>>;



            //Assert
            Assert.IsNotNull(actionResult);



        }



        [TestMethod]
        public void RemoveTest_ValidTestReportIdValidAppointMentId_ReturnsOkStatus()
        {
            //Arrange
            TestReport testReport = obj.AddTest(new Test { Id = 6, Name = "CT Scan" }, 15);



            // Act
            IHttpActionResult actionResult = this.controller.RemoveTest(testReport.Id, 15);



            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<TestReport>));
        }
    }
}
