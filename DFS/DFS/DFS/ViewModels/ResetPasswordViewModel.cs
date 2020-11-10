using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace DFS.ViewModels
{
    public class ResetPasswordViewModel : INotifyPropertyChanged
    {
        private Boolean _isServiceInProgress;
        public Boolean IsServiceInProgress
        {
            get { return _isServiceInProgress; }
            set
            {
                _isServiceInProgress = value;
                RaisePropertyChanged(nameof(IsServiceInProgress));
            }
        }

        private Boolean _isOtpSent;
        public Boolean IsOtpSent
        {
            get { return _isOtpSent; }
            set
            {
                _isOtpSent = value;
                RaisePropertyChanged(nameof(IsOtpSent));
            }
        }

        private String _emailAddress;
        public String EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                RaisePropertyChanged(nameof(EmailAddress));
            }
        }

        private String _password;
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private String _otp;
        public String OTP
        {
            get
            {
                return _otp;
            }
            set
            {
                _otp = value;
                RaisePropertyChanged(nameof(OTP));
            }
        }

        public ICommand ResetCommand { get; set; }

        public ResetPasswordViewModel()
        {
            IsOtpSent = false;
            IsServiceInProgress = false;

            ResetCommand = new Command(ResetClicked);
        }

        private async void ResetClicked()
        {
            
            
            if(IsOtpSent)
            {
                if(EmailAddress == "" || EmailAddress == null || OTP == "" || OTP == null || Password == null || Password == "")
                {
                    MessagingCenter.Send<ViewModels.ResetPasswordViewModel, String>(this, "Alert", "Please provide required input.");
                    return;
                }

                IsServiceInProgress = true;
                String result = await App.TodoManager.SubmitOtpService(new Models.SubmitOtpModel(EmailAddress, OTP, Password));

                if (result == "Success")
                {
                    MessagingCenter.Send<ViewModels.ResetPasswordViewModel>(this, "Success");
                }
                else
                {
                    MessagingCenter.Send<ViewModels.ResetPasswordViewModel, String>(this, "Alert", "Something went wrong. Please try again");
                }


            }
            else
            {
                if (EmailAddress == "" || EmailAddress == null)
                {
                    MessagingCenter.Send<ViewModels.ResetPasswordViewModel, String>(this, "Alert", "Please provide required input.");
                    return;
                }

                IsServiceInProgress = true;
                String result = await App.TodoManager.CreateOtpService(new Models.CreateOtpModel(EmailAddress));

                if(result != "Success")
                {
                    MessagingCenter.Send<ViewModels.ResetPasswordViewModel, String>(this, "Alert", "Something went wrong. Please try again");
                }
                else
                {
                    IsOtpSent = true;
                }
            }

            IsServiceInProgress = false;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
