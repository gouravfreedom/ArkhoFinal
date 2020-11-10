using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DFS.Models;
using Xamarin.Forms;
using XamForms.Controls;

namespace DFS.Views
{
    public partial class CalenderPage : ContentPage
    {
        ViewModels.CalenderViewModel calenderViewModel;
        //DateTime selectedDate;
        public CalenderPage(LoginResponse.Services schedules)
        {
            InitializeComponent();

            BindingContext = calenderViewModel = new ViewModels.CalenderViewModel(schedules);

            calendar.MinDate = DateTime.Now;

            if (App.SelectedView == "Trainer")
            {
                payButton.IsVisible = false;
            }

            //MessagingCenter.Subscribe<ViewModels.CalenderViewModel, DateTime>(this, "DateSelected", (sender, _selectedDate) =>
            //{
            //    //await this.Navigation.PushAsync(new TimeSelectionPopup());
            //    TimeView.IsVisible = true;
            //    OpaqueView.IsVisible = true;

            //    selectedDate = _selectedDate;

            //});

        }

        //void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{
        //    // don't do anything if we just de-selected the row.
        //    if (e.SelectedItem == null) return;

        //    // Deselect the item.
        //    if (sender is ListView lv) lv.SelectedItem = null;

        //}

        async void Handle_Calender(object sender, System.EventArgs e)
        {

            int count = 0;

            for (int index = 0; index < calenderViewModel.Attendances.Count; index++)
            {
                if (calenderViewModel.Attendances[index].BackgroundColor == Color.Green)
                {
                    count = count + 1;

                }
            }

            if (count == 0)
            {
                await DisplayAlert("Alert", "Please select minimum 1 day.", "Ok");
            }
            else
            {
                var result = await DisplayAlert("Alert", "Are you sure, you want to pay for " + count + " days?", "Accept", "Cancel");

                if (result)
                {

                    SetTimeSlotsRequestModel setTimeSlots = new SetTimeSlotsRequestModel();
                    setTimeSlots.emailID = App.TrainerData.Email;
                    setTimeSlots.addByEmailID = App.LoginResponse.Email;

                    List<Models.SetTimeSlotsRequestModel.TimeSlot> timeSlots = new List<Models.SetTimeSlotsRequestModel.TimeSlot>();

                    foreach (var item in calenderViewModel.Attendances)
                    {

                        if (item.BackgroundColor == Color.Green)
                        {

                            Models.SetTimeSlotsRequestModel.TimeSlot timeSlot = new Models.SetTimeSlotsRequestModel.TimeSlot();
                            timeSlot.day = item.Date.Day.ToString();
                            timeSlot.month = item.Date.Month.ToString();
                            timeSlot.year = item.Date.Year.ToString();

                            int index = calenderViewModel.Attendances.IndexOf(item);

                            timeSlot.startTime = calenderViewModel.Schedules[index].StartTime;
                            timeSlot.endTime = calenderViewModel.Schedules[index].EndTime;
                            timeSlot.serviceReferneceId = calenderViewModel.Schedules[index].ServiceRefId;
                            timeSlot.serviceId = calenderViewModel.ServiceId;
                            timeSlot.remarks = "Appointment";

                            timeSlots.Add(timeSlot);
                        }
                    }



                    setTimeSlots.timeSlot = timeSlots;

                    var slotResponseModel = await App.TodoManager.SetTimeSlot(setTimeSlots);
                    if (slotResponseModel != null && !string.IsNullOrEmpty(slotResponseModel.status.status) && slotResponseModel.status.status.ToLower().Equals("success"))
                    {

                        var paymentRequest = new PaymentRequest
                        {
                            BusinessTransaction = new BusinessTransaction
                            {
                                PayeeId = App.TrainerData.Email,
                                PayerId = App.LoginResponse.Email,
                                Amount = (count * Convert.ToDouble(calenderViewModel.Charges)) + "",
                                EventId = slotResponseModel.status.slotId
                            }
                        };
                        var paymentResponse = await App.TodoManager.StartPayment(paymentRequest);
                        if (paymentResponse != null && paymentResponse.TransactionStatus != null && !string.IsNullOrEmpty(paymentResponse.TransactionStatus.processURL))
                        {
                            await this.Navigation.PushAsync(new PaymentPage(paymentResponse.TransactionStatus.processURL));
                        }
                    }
                }

            }
        }
    }


    //void Hide_Calender(object sender, System.EventArgs e)
    //{
    //    TimeView.IsVisible = false;
    //    OpaqueView.IsVisible = false;
    //}


}
