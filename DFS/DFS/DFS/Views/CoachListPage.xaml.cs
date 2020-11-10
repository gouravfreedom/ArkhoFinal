using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class CoachListPage : ContentPage
    {
        ViewModels.TrainerListViewModel trainerListViewModel;
        public CoachListPage()
        {
            InitializeComponent();

            BindingContext = trainerListViewModel = new ViewModels.TrainerListViewModel();
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var trainee = e.SelectedItem as Models.TrainerListModel.Trainee;

            if (trainee.Email == null)
            {
                return;
            }

            trainerListViewModel.IsServiceInProgress = true;

            String message = await App.TodoManager.Login(new Models.LoginRequestModel("INT", trainee.Email, "Trainer", "qwertyqazxcvbnm"));
            if (message == "Success")
            {
                //App.TrainerData.professionalInfo.services = trainee.services;

                ObservableCollection<Models.LoginResponse.Services> services = new ObservableCollection<Models.LoginResponse.Services>();

                foreach (var serviceItem in trainee.services)
                {
                    Models.LoginResponse.Services service = new Models.LoginResponse.Services();
                    service.Charges = serviceItem.Charges;
                    service.ChargingPeriod = serviceItem.ChargingPeriod;
                    service.ServiceName = serviceItem.ServiceName;
                    service.TeamSize = serviceItem.TeamSize;
                    service.WorkLocaton = serviceItem.WorkLocaton;
                    service.ServiceId = serviceItem.ServiceId;

                    List<Models.LoginResponse.Schedule> schedules = new List<Models.LoginResponse.Schedule>();

                    foreach (var scheduleItem in serviceItem.schedules)
                    {
                        Models.LoginResponse.Schedule schedule = new Models.LoginResponse.Schedule();
                        schedule.Day = scheduleItem.Day;
                        schedule.EndTime = scheduleItem.EndTime;
                        schedule.Month = scheduleItem.Month;
                        schedule.ScheduleType = scheduleItem.ScheduleType;
                        schedule.StartTime = scheduleItem.StartTime;
                        schedule.WeekDay = scheduleItem.WeekDay;
                        schedule.Year = scheduleItem.Year;
                        schedule.ServiceRefId = scheduleItem.ServiceRefId;

                        schedules.Add(schedule);
                    }
                    service.schedules = schedules;

                    services.Add(service);
                }

                App.TrainerData.professionalInfo.services = services;

                await this.Navigation.PushAsync(new TrainerProfilePage());
            }

            trainerListViewModel.IsServiceInProgress = false;

            if (sender is ListView lv) lv.SelectedItem = null;

        }
    }
}
