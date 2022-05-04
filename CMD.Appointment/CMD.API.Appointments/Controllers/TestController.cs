using CMD.Business.Appointments.Interfaces;
using CMD.DTO.Appointments;
using CMD.Model.Appointments;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace CMD.API.Appointments.Controllers
{
    public class TestController : ApiController
    {
        protected ITestService manager;
        public TestController(ITestService manager)
        {
            this.manager = manager;
        }

        [Route("Test/{appointmnetId}")]
        [HttpPost]
        [ResponseType(typeof(TestReport))]
        public IHttpActionResult AddTest(int appointmnetId, Test test)
        {
            var createdTest = this.manager.AddTest(test, appointmnetId);
            return Created($"addTest/{appointmnetId}", createdTest);
        }

        [HttpGet]
        [Route("Test")]
        [ResponseType(typeof(ICollection<Test>))]
        public IHttpActionResult GetTests()
        {
            var tests = manager.GetAllTests();
            return Ok(tests);
        }

        [Route("RecommendedTest/{appointmentId}")]
        [HttpGet]
        [ResponseType(typeof(ICollection<Test>))]
        public IHttpActionResult GetRecommendedTest(int appointmentId)
        {
            var tests = this.manager.GetRecommendedTests(appointmentId);
            if (!tests.Any())
            {
                return NotFound();
            }
            return Ok(tests);
        }

        [Route("Test/testReportid/{testReportId}/appointmentid/{appointmentId}")]
        [HttpDelete]
        [ResponseType(typeof(TestReport))]
        public IHttpActionResult RemoveTest(int testReportId, int appointmentId)
        {
            var result = manager.DeleteTest(appointmentId, testReportId);
            return Ok(result);
        }

        [Route("TestReports")]
        [HttpGet]
        [ResponseType(typeof(ICollection<TestReportDTO>))]
        public IHttpActionResult GetTestReports()
        {
            var testReports = this.manager.GetTestReports();
            if (testReports.Count == 0)
            {
                return NotFound();
            }
            return Ok(testReports);
        }
    }
}
