﻿using CMD.Business.Appointments.Interfaces;
using CMD.DTO.Appointments;
using CMD.Model.Appointments;
using CMD.Repository.Appointments.Implementations;
using CMD.Repository.Appointments.Interfaces;

namespace CMD.Business.Appointments.Implementations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository repo;

        public RecommendationRepository Obj2 { get; }

        public RecommendationService()
        {
            this.repo = new RecommendationRepository();
        }

        public RecommendationService(RecommendationRepository obj2)
        {
            Obj2=obj2;
        }

        public bool RemoveRecommendation(int id)
        {
            return repo.RemoveRecommendation(id);
        }

        public RecommendationDTO AddRecommendtaion(RecommendationDTO recoDTO)
        {
            var reco = new Recommendation
            {
                AppointmentId = recoDTO.AppointmentId,
                DoctorId = recoDTO.DoctorId,
                RecommendedDoctor = recoDTO.DoctorName,
            };
            var recommendation = repo.AddRecommendtaion(reco);
            var result = new RecommendationDTO
            {
                RecommendationId = recommendation.RecommedationId,
                DoctorId = recommendation.DoctorId,
                DoctorName = recommendation.RecommendedDoctor,
                AppointmentId = recommendation.AppointmentId,
            };
            return result;
        }


        //public List<DoctorInfoDTO> GetDoctors()
        //{
        //    return repo.GetDoctors();
        //}
    }
}
