using System;
using System.Collections.Generic;

namespace DFS.Models
{
    public class GetTimeSlotResponse
    {
        public TimeSlots TimeSlots { get; set; }
    }

    public class Service
    {
        public string serviceName { get; set; }
        public String charges { get; set; }
        public string chargingPeriod { get; set; }
        public string serviceId { get; set; }
        public string workLocaton { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    public class TimeSlot
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string day { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public object remarks { get; set; }
        public string addByEmailID { get; set; }
        public string trainerEmailId { get; set; }
        public string serviceId { get; set; }
        public object slotId { get; set; }
        public bool IsStarVisible { get; set; }
        public List<Service> services { get; set; }
    }

    public class TimeSlots
    {
        public string emailID { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public object addByEmailID { get; set; }
        public List<TimeSlot> timeSlot { get; set; }
    }
}
