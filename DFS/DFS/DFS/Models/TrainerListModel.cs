using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DFS.Models
{
    public class TrainerListModel
    {
        public TrainerListModel()
        {
        }

        [JsonProperty("Trainee")]
        public ObservableCollection<Trainee> trainee { get; set; }

        public class Trainee
        {
            [JsonProperty("email")]
            public String Email { get; set; }

            [JsonProperty("name")]
            public String Name { get; set; }

            [JsonProperty("rating")]
            public Double Rating { get; set; }

            [JsonProperty("address")]
            public String Address { get; set; }

            [JsonProperty("imageUrL")]
            public String ImageUrL { get; set; }

            [JsonProperty("sportsInterest")]
            public String SportsInterest { get; set; }

            [JsonProperty("status")]
            public String Status { get; set; }

            [JsonProperty("country")]
            public String Country { get; set; }

            [JsonProperty("state")]
            public String State { get; set; }

            public String FirstImageSource { get; set; }

            public String SecondImageSource { get; set; }

            public String ThirdImageSource { get; set; }

            public String FourthImageSource { get; set; }

            public String FifthImageSource { get; set; }

            [JsonProperty("services")]
            public ObservableCollection<Services> services { get; set; }
        }

        public class Services
        {
            [JsonProperty("serviceName")]
            public String ServiceName { get; set; }

            [JsonProperty("charges")]
            public String Charges { get; set; }

            [JsonProperty("serviceId")]
            public String ServiceId { get; set; }

            [JsonProperty("chargingPeriod")]
            public String ChargingPeriod { get; set; }

            [JsonProperty("workLocaton")]
            public String WorkLocaton { get; set; }

            [JsonProperty("teamSize")]
            public String TeamSize { get; set; }

            [JsonProperty("schedule")]
            public List<Schedule> schedules { get; set; }
        }

        public class Schedule
        {
            [JsonProperty("day")]
            public String Day { get; set; }

            [JsonProperty("month")]
            public String Month { get; set; }

            [JsonProperty("year")]
            public String Year { get; set; }

            [JsonProperty("startTime")]
            public String StartTime { get; set; }

            [JsonProperty("endTime")]
            public String EndTime { get; set; }

            [JsonProperty("weekDay")]
            public String WeekDay { get; set; }

            [JsonProperty("scheduleType")]
            public String ScheduleType { get; set; }

            [JsonProperty("serviceRefId")]
            public String ServiceRefId { get; set; }
        }

        public class TraineeList
        {
            public String Email { get; set; }

            public String Name { get; set; }

            public String Address { get; set; }

            public UriImageSource ImageUrL { get; set; }

            public String SportsInterest { get; set; }

            public String Status { get; set; }

            public String Country { get; set; }

            public String State { get; set; }

        }


    }
}
