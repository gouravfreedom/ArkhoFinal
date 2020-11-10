using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DFS.Models;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class ScheduleList : ContentPage
    {
        int RatingIndex = 0;
        List<TimeSlot> timeSlot;

        string TraineeEmailId, TrainerEmailId;
        public ScheduleList()
        {
            InitializeComponent();

            OpaqueView.IsVisible = true;
            IndicatorView.IsVisible = true;
            RatingView.IsVisible = false;

            LoadingText.Text = "Loading...";

            Initialization = InitializeAsync();

        }

        void FirstImageSelected(object sender, System.EventArgs e)
        {
            RatingIndex = 1;
            FirstImage.Source = "selected.png";
            SecondImage.Source = "unselected.png";
            ThirdImage.Source = "unselected.png";
            FourthImage.Source = "unselected.png";
            FifthImage.Source = "unselected.png";
        }

        void SecondImageSelected(object sender, System.EventArgs e)
        {
            RatingIndex = 2;
            FirstImage.Source = "selected.png";
            SecondImage.Source = "selected.png";
            ThirdImage.Source = "unselected.png";
            FourthImage.Source = "unselected.png";
            FifthImage.Source = "unselected.png";
        }

        void ThirdImageSelected(object sender, System.EventArgs e)
        {
            RatingIndex = 3;
            FirstImage.Source = "selected.png";
            SecondImage.Source = "selected.png";
            ThirdImage.Source = "selected.png";
            FourthImage.Source = "unselected.png";
            FifthImage.Source = "unselected.png";
        }

        void FourthImageSelected(object sender, System.EventArgs e)
        {
            RatingIndex = 4;
            FirstImage.Source = "selected.png";
            SecondImage.Source = "selected.png";
            ThirdImage.Source = "selected.png";
            FourthImage.Source = "selected.png";
            FifthImage.Source = "unselected.png";
        }

        void FifthImageSelected(object sender, System.EventArgs e)
        {
            RatingIndex = 5;
            FirstImage.Source = "selected.png";
            SecondImage.Source = "selected.png";
            ThirdImage.Source = "selected.png";
            FourthImage.Source = "selected.png";
            FifthImage.Source = "selected.png";
        }

        public Task Initialization { get; private set; }

        private async Task InitializeAsync()
        {
            timeSlot = new List<TimeSlot>();

            var response = await App.TodoManager.GetTimeSlots(new Models.GetTimeSlotRequest { emailID = App.LoginResponse.Email });

            if (response != null)
            {
                TraineeEmailId = response.TimeSlots.emailID;
            }
            foreach (var item in response.TimeSlots.timeSlot)
            {
                int month = Convert.ToInt32(item.month);
                int day = Convert.ToInt32(item.day);

                if (month == DateTime.Now.Month && App.SelectedView == "Trainee")
                {
                    if (day <= DateTime.Now.Day)
                    {
                        item.IsStarVisible = true;
                    }
                }
                else if (month < DateTime.Now.Month && App.SelectedView == "Trainee")
                {
                    item.IsStarVisible = true;
                }

                timeSlot.Add(item);

            }

            ItemsListView.ItemsSource = timeSlot;

            OpaqueView.IsVisible = false;
            IndicatorView.IsVisible = false;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            var obj = ((TappedEventArgs)e).Parameter;

            var selectedObject = obj as TimeSlot;
            TrainerEmailId = selectedObject.trainerEmailId;

            RatingIndex = 0;
            FirstImage.Source = "unselected.png";
            SecondImage.Source = "unselected.png";
            ThirdImage.Source = "unselected.png";
            FourthImage.Source = "unselected.png";
            FifthImage.Source = "unselected.png";

            RatingView.IsVisible = true;
            OpaqueView.IsVisible = true;

        }

        async void Done_Clicked(object sender, System.EventArgs e)
        {
            RatingView.IsVisible = false;

            LoadingText.Text = "Submitting...";

            RatingRequestModel ratingRequestModel = new RatingRequestModel(RatingIndex * 2, CommentText.Text, TraineeEmailId, TrainerEmailId);

            String message = await App.TodoManager.SubmitRating(ratingRequestModel);

            if (message == "Success")
            {
                await DisplayAlert("Alert", "Rating submitted successfully.", "Ok");
            }
            else
            {
                await DisplayAlert("Alert", "Something went wrong. Please try again.", "Ok");
            }

            OpaqueView.IsVisible = false;
        }
    }
}
