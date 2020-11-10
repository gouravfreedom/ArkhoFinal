using System;
namespace DFS.Models
{
    public class SubmitOtpModel
    {
        public string emailID { get; set; }

        public string OTP { get; set; }

        public string newPassword { get; set; }

        public string profile { get; set; }

        public SubmitOtpModel(String _email, string _otp, string _newPass)
        {
            emailID = _email;
            OTP = _otp;
            newPassword = _newPass;
            profile = App.SelectedView;
        }
    }
}
