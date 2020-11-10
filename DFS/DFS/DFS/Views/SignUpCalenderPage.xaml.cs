using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DFS.Views
{
    public partial class SignUpCalenderPage : ContentPage
    {
        public SignUpCalenderPage(ViewModels.SignupViewModel signupViewModel)
        {
            InitializeComponent();


            BindingContext = signupViewModel;

            DateTime futuerDate = DateTime.Now;


            calendar.MinDate = futuerDate;

            futuerDate = futuerDate.AddDays(14);

            calendar.MaxDate = futuerDate;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            // don't do anything if we just de-selected the row.
            if (e.SelectedItem == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ViewModels.SignupViewModel>(this, "MoveBack", async (arg1) =>
            {
                await this.Navigation.PopAsync();
            });

            MessagingCenter.Subscribe<ViewModels.SignupViewModel, String>(this, "Alert", async (arg1, message) =>
            {
                await DisplayAlert("Alert", message, "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<ViewModels.SignupViewModel, String>(this, "Alert");
            MessagingCenter.Unsubscribe<ViewModels.SignupViewModel>(this, "MoveBack");

        }

    }
}
