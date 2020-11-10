using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DFS.Models;
using Xamarin.Forms;
using XamForms.Controls;

namespace DFS.ViewModels
{
    public class TraineeCalanderViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<XamForms.Controls.SpecialDate> attendances;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String _timeSlot;
        public String TimeSlot
        {
            get
            {
                return _timeSlot;
            }
            set
            {
                _timeSlot = value;
                RaisePropertyChanged(nameof(TimeSlot));
            }
        }

        private String _emailId;
        public String EmailId
        {
            get
            {
                return _emailId;
            }
            set
            {
                _emailId = value;
                RaisePropertyChanged(nameof(EmailId));
            }
        }

        private String _date;
        public String Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        private String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private String _phoneNumber;
        public String PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
            }
        }

        private String _location;
        public String Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                RaisePropertyChanged(nameof(Location));
            }
        }

        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; RaisePropertyChanged(nameof(Attendances)); }
        }



        private bool _isServiceInProgress;
        public bool IsServiceInProgress
        {
            get
            {
                return _isServiceInProgress;
            }
            set
            {
                _isServiceInProgress = value;
                RaisePropertyChanged(nameof(IsServiceInProgress));
            }
        }

        private bool _isInfoVisible;
        public bool IsInfoVisible
        {
            get
            {
                return _isInfoVisible;
            }
            set
            {
                _isInfoVisible = value;
                RaisePropertyChanged(nameof(IsInfoVisible));
            }
        }

        private GetTimeSlotResponse _timeSlotResponse;
        public GetTimeSlotResponse TimeSlotResponse
        {
            get
            {
                return _timeSlotResponse;
            }
            set
            {
                _timeSlotResponse = value;
                RaisePropertyChanged(nameof(TimeSlotResponse));
            }
        }

        public TraineeCalanderViewModel()
        {

        }

        DateTime currentDate = DateTime.Now;

        public ICommand LeftNavigateCommand
        {
            get
            {
                return new Command(HandleAction);
            }
        }

        public ICommand RightNavigateCommand
        {
            get
            {
                return new Command(HandleAction1);
            }
        }

        public ICommand DateChosen
        {
            get
            {
                return new Command((obj) => {

                    DateTime dateTime = (DateTime)obj;

                    if (TimeSlotResponse != null)
                    {
                        foreach (var item in TimeSlotResponse.TimeSlots.timeSlot)
                        {
                            if (item.day.ToString() == dateTime.Day.ToString())
                            {
                                Date = item.day + "/" + item.month + "/" + item.year;
                                EmailId = App.SelectedView == "Trainee" ? item.trainerEmailId : item.addByEmailID;
                                TimeSlot = item.startTime + " - " + item.endTime;
                                Name = item.name;
                                PhoneNumber = item.phoneNumber;
                                Location = item.services.Count > 0 ? item.services[0].workLocaton : "";

                                IsInfoVisible = true;

                                break;
                            }
                        }
                    }


                });
            }
        }

        public ICommand CalenderSubmitCommand
        {
            get
            {
                return new Command((obj) => {
                    IsInfoVisible = false;
                });
            }
        }


        void HandleAction(object obj)
        {
            currentDate = currentDate.AddMonths(-1);
            GetTimeSlots();
        }

        private void HandleAction1(object obj)
        {
            currentDate = currentDate.AddMonths(1);
            GetTimeSlots();
        }

        public async void GetTimeSlots()
        {
            try
            {
                IsServiceInProgress = true;
                var email = App.SelectedView.Equals("Trainee") ? (App.TrainerData == null ? App.LoginResponse.Email : App.TrainerData.Email) : App.LoginResponse.Email;

                String currentMonth = currentDate.Month > 9 ? currentDate.Month.ToString() : "0" + (currentDate.Month.ToString());

                var request = new GetTimeSlotRequest { emailID = App.LoginResponse.Email};
                TimeSlotResponse = await App.TodoManager.GetTimeSlots(request);
                if (TimeSlotResponse != null && TimeSlotResponse.TimeSlots.timeSlot.Any())
                {
                    List<SpecialDate> bookedDates = new List<SpecialDate>();
                    foreach (var item in TimeSlotResponse.TimeSlots.timeSlot)
                    {
                        DateTime date = new DateTime(Convert.ToInt32(item.year), Convert.ToInt32(item.month), Convert.ToInt32(item.day));
                        bookedDates.Add(new SpecialDate(date) { BackgroundColor = Color.Red, Selectable = true });

                    }
                    Attendances = new ObservableCollection<SpecialDate>(bookedDates);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            IsServiceInProgress = false;
        }
    }
}
