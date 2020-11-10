using System;
namespace DFS.Models
{
    public class RatingRequestModel
    {
        public RatingRequestModel(int rating, string comment, string traineeEmailId, string trainerEmailId)
        {
            this.rating = rating;
            this.comment = comment;
            this.traineeEmailId = traineeEmailId;
            this.trainerEmailId = trainerEmailId;
        }

        public int rating { get; set; }

        public string comment { get; set; }

        public string traineeEmailId { get; set; }

        public string trainerEmailId { get; set; }
    }
}
