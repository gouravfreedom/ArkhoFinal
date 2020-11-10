using System;
namespace DFS.Models
{
    public class CreateOtpModel
    {
        public string emailID { get; set; }

        public string profile { get; set; }

        public CreateOtpModel(String _email)
        {
            emailID = _email;
            profile = App.SelectedView;
        }
    }
}
