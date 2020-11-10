using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DFS.Models;
using DFS.ViewModels;
using Xamarin.Forms;
using XamForms.Controls;

namespace DFS.Views
{
    public partial class TraineeCalanderPage : ContentPage
    {
        TraineeCalanderViewModel viewModel;
        public TraineeCalanderPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new TraineeCalanderViewModel();

            calendar.MinDate = DateTime.Now;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                viewModel.IsServiceInProgress = true;
                //var email = App.SelectedView.Equals("Trainer") ? App.TrainerData.Email : App.LoginResponse.Email;
                //var request = new GetTimeSlotRequest { emailID = email, month = DateTime.Now.Month.ToString(), year = DateTime.Now.Year.ToString() };
                //var response = await App.TodoManager.GetTimeSlots(request);
                //if (response != null && response.TimeSlots.timeSlot.Any())
                //{
                //    var bookedDates = new List<SpecialDate>();
                //    foreach (var item in response.TimeSlots.timeSlot)
                //    {
                //        var date = new DateTime(Convert.ToInt32(item.year), Convert.ToInt32(item.month), Convert.ToInt32(item.day));
                //        bookedDates.Add(new SpecialDate(date) { BackgroundColor = Color.Red, Selectable = true });
                //    }
                //    viewModel.Attendances = new ObservableCollection<SpecialDate>(bookedDates);
                //}

                viewModel.GetTimeSlots();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            viewModel.IsServiceInProgress = false;
        }


    }
}
