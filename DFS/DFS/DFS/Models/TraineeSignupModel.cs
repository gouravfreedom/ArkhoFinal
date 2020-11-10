using System;
using System.Collections.Generic;

namespace DFS.Models
{
    public class TraineeSignupModel
    {
        public String profile { get; set; }

        public String paypalId { get; set; }

        public String signUpMetod { get; set; }

        public String password { get; set; }

        public String email { get; set; }

        public String imagePayload { get; set; }

        public BasicInfo basicInfo { get; set; }

        public ProfessionalInfo professionalInfo {get;set;}

        public class BasicInfo {

            public int id { get; set; } 

            public String name { get; set; }

            public String valueAdded { get; set; }

            public String title { get; set; }

            public String dateOfBirth { get; set; }

            public String mobileNumber { get; set; }

            public String weight { get; set; }

            public String height { get; set; }

            public String anyMedicalCondition { get; set; }

            public String sportsInterest { get; set; }

            public String gender { get; set; }

            public String country { get; set; }

            public String state { get; set; }

            public String address { get; set; }

            public String imageUrl { get; set; }

            public String phoneNumber { get; set; }

            public String instaGramId { get; set; }

            public String latitude { get; set; }

            public String longitude { get; set; }

            public String instaGramImages { get; set; }
        }

        public class ProfessionalInfo
        {
            public String speciality { get; set; }

            public String experience { get; set; }

            public String accolades { get; set; }

            public List<Services> services { get; set; }

            public List<Certifications> certifications { get; set; }

        }

        public class Services
        {
            public String serviceName { get; set; }

            public Double charges { get; set; }

            public String chargingPeriod { get; set; }

            public String workLocaton { get; set; }

            public String teamSize { get; set; }

            public List<Schedule> schedule { get; set; }

        }

        public class Schedule
        {
            public String day { get; set; }

            public String month { get; set; }

            public String year { get; set; }

            public String scheduleType { get; set; }

            public String startTime { get; set; }

            public String endTime { get; set; }

            public String weekDay { get; set; }

        }

        public class Certifications
        {
            public String certification { get; set; }

        }

    }
}
