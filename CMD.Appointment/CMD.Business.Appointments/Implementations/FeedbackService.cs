using CMD.Business.Appointments.Interfaces;
using CMD.Model.Appointments;
using CMD.Repository.Appointments.Implementations;
using CMD.Repository.Appointments.Interfaces;

namespace CMD.Business.Appointments.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository repo;

        public FeedbackService()
        {
            this.repo = new FeedbackRepository();
        }

        public FeedbackService(FeedbackRepository obj)
        {
            Obj=obj;
        }

        public FeedbackRepository Obj { get; }

        public FeedBack GetFeedback(int id)
        {
            var result = repo.GetFeedback(id);
            return result;
        }
    }
}
