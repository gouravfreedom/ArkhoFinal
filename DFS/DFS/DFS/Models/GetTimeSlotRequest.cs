using System;
namespace DFS.Models
{
    public class GetTimeSlotRequest
    {
        public GetTimeSlotRequest()
        {
            status = "Completed";
        }

        public string emailID { get; set; }
        public String status { get; set; }
    }
}
