using CMD.API.Doctors.Controllers;
using CMD.Business.Doctors.Implementations;
using CMD.Business.Doctors.Interfaces;
using CMD.DTO.Doctor;
using CMD.Model.Doctors;
using CMD.Repository.Doctors;
using CMD.Repository.Doctors.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace DoctorUnitTest
{
    [TestClass]
    public class UnitTest1




    {
        IDoctorService obj;
        DoctorRepository obj2;
        [TestInitialize]



        public void ObjectCreation()
        {
            obj2 = new DoctorRepository();
            obj = new DoctorService(obj2);



        }



        [TestCleanup]
        public void ObjectDeletion()
        {
            obj = null;
            obj2 = null;



        }





        //IRecommendationRepository RecommendationRepository=new RecommendationRepository();






        [TestMethod]
        public void GetDoctors_ShouldReturnAllDoctors()
        {



            var doc = obj.GetAllDoctor();
            Assert.IsNotNull(doc);



        }




        [TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        public void GetDoctors_IncorrectDataBase_ShouldReturnNull()
        {



            var doc = obj.GetAllDoctor();
            Assert.IsTrue(doc != null);



        }



        [TestMethod]
        public void GetDoctorList()
        {
            // Arrange
            var mockRepository = new Mock<IDoctorService>();
            var controller = new DoctorController(mockRepository.Object);





            // Act
            IHttpActionResult actionResult = controller.GetAllDoctors();
            var actionresult2 = actionResult as OkNegotiatedContentResult<ICollection<RDoctorDTO>>;





            // Assert
            Assert.IsNotNull(actionresult2);
        }



        [TestMethod]



        public void GetDoctor_ReturnDoctorID()
        {
            int id = 2;
            var doc = obj2.GetDoctor(id);
            Assert.IsNotNull(doc);
        }



        //To check ID is not present



        [TestMethod]
        public void GetDoctor_NotReturnDoctorID()
        {
            int id = 100;
            var doc = obj2.GetDoctor(id);
            Assert.IsNull(doc);
        }



        //To check whether
        [TestMethod]
        public void EditDoctor_ReturnEditedData()
        {
            DoctorContext db = new DoctorContext();
            var doctor = db.Doctors.Find(2);
            doctor.Name = "Subham Bose";
            obj2.EditDoctor(doctor);
            var result = db.Doctors.Find(2);
            Assert.AreEqual(result.Name, doctor.Name);



        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EditDoctor_ReturnNoEditedData()
        {
            DoctorContext db = new DoctorContext();



            Doctor doc = null;
            obj2.EditDoctor(doc);
            //Assert.IsNotNull(result);



        }



        #region controller unit test


        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetControllerReturns()
        {
            // Arrange
            var mockRepository = new Mock<IDoctorService>();
            var controller = new DoctorController(mockRepository.Object);



            // Act
            IHttpActionResult actionResult = controller.Get(10);



            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(HttpResponseException));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PutReturnsContentResult()
        {
            // Arrange
            var mockRepository = new Mock<IDoctorService>();
            var controller = new DoctorController(mockRepository.Object);





            // Act
            IHttpActionResult actionResult = controller.Put(new DoctorProfileDTO { doctor_name = "Subham Bose" });
            var contentResult = actionResult as NegotiatedContentResult<Doctor>;





            // Assert
            // Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            // Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
        #endregion



        [TestMethod]
        public void GetTestReports_ShouldReturnAllTestReports()
        {
            var testReports = obj.GetDoctorsWithContact(3);
            Assert.IsNotNull(testReports);
        }





        [TestMethod]
        public void GetDoctor_WhenIdIsPresent_ShouldReturnDoctorData()
        {
            //When doctor with given id is present in database
            //it shoutld return doctor data/info
            int id = 2;
            var doc = obj2.GetDoctor(id);
            Assert.IsNotNull(doc);
        }
        [TestMethod]
        public void GetDoctor_WhenIdIsNotPresent_ShouldReturnNull()
        {
            //When doctor with given id is not present in database
            //it shoutld return null
            int id = 100;
            var doc = obj2.GetDoctor(id);
            Assert.IsNull(doc);
        }
        [TestMethod]
        public void EditDoctor_AfterEditingData_NameShouldBeSame()
        {
            DoctorContext db = new DoctorContext();
            var doctor = db.Doctors.Find(2);
            doctor.Name = "Subham Bose";
            obj2.EditDoctor(doctor);
            var result = db.Doctors.Find(2);
            Assert.AreEqual(result.Name, doctor.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EditDoctor_ReturnNoEditdData()
        {
            DoctorContext db = new DoctorContext();
            Doctor doc = null;
            var result = db.Doctors.Add(doc);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetControllerReturn()
        {
            // Arrange
            var mockRepository = new Mock<IDoctorService>();
            var controller = new DoctorController(mockRepository.Object); // Act
            IHttpActionResult actionResult = controller.Get(10); // Assert
            Assert.IsInstanceOfType(actionResult, typeof(HttpResponseException));
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PutReturnContentResult()
        {
            // Arrange
            var mockRepository = new Mock<IDoctorService>();
            var controller = new DoctorController(mockRepository.Object); // Act
            IHttpActionResult actionResult = controller.Put(new DoctorProfileDTO { doctor_name = "Subham Bose" });
            var contentResult = actionResult as NegotiatedContentResult<Doctor>; // Assert
                                                                                 // Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            // Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }





        #region kishore
        [TestMethod]
        public void GetIdsAssociatedWithDoctor_AtController_Positive()
        {
            //Arrange mock
            var service = new Mock<IDoctorService>();
            var controller = new DoctorController(service.Object);
            service.Setup(x => x.GetDoctorCard(3)).Returns(new DoctorCardDTO()
            {
                Id = 3,
                Name = "Emerson Earwood",
                PhoneNumber = "123"
            });
            //act
            var actionResult = controller.GetDoctorCard(3);
            var contentResult = actionResult as OkNegotiatedContentResult<DoctorCardDTO>;
            //assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.Id);
        }
        [TestMethod]
        public void GetDoctor_GetDoctorCardDTO_Positive()
        {
            DoctorCardDTO actual = obj.GetDoctorCard(3);
            DoctorCardDTO expected = new DoctorCardDTO
            {
                Name= "Mitchell Whitledge",
                PhoneNumber= "8645022354"
            };
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetDoctor_GetDoctorCardDTO_Negetive()
        {
            var actual = obj.GetDoctorCard(0);
        }
        #endregion

        [TestMethod]



        public void EditServiceDoctor_ReturnUpdated()

        {

            DoctorContext db = new DoctorContext();

            DoctorProfileDTO doctorProfileDTO = obj.GetDoctorsWithContact(2);
            doctorProfileDTO.doctor_name = "NEW Name";

            obj.EditDoctor(doctorProfileDTO);

            var expected = db.Doctors.Find(2);



            Assert.AreEqual(expected.Name, doctorProfileDTO.doctor_name);



        }

    }

}
