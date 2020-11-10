using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DFS.ViewModels
{
    public class TrainerListViewModel : INotifyPropertyChanged
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

        private String _searchText;
        public String SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));

                FilterList();
            }
        }

        private ObservableCollection<Models.TrainerListModel.Trainee> _listViewData;
        public ObservableCollection<Models.TrainerListModel.Trainee> ListViewData
        {
            get { return _listViewData; }
            set
            {
                _listViewData = value;
                RaisePropertyChanged(nameof(ListViewData));
            }
        }

        private ObservableCollection<Models.TrainerListModel.Trainee> _mainListData;
        public ObservableCollection<Models.TrainerListModel.Trainee> MainListData
        {
            get { return _mainListData; }
            set
            {
                _mainListData = value;
                RaisePropertyChanged(nameof(MainListData));
            }
        }

        public Task Initialization { get; private set; }

        public TrainerListViewModel()
        {
            _isServiceInProgress = true;

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var response = await App.TodoManager.FetchTrainerList();
            ListViewData = new ObservableCollection<Models.TrainerListModel.Trainee>();
            MainListData = response.trainee;
            FilterList();
            IsServiceInProgress = false;
        }

        private void FilterList()
        {
            if (SearchText == "" || SearchText == null)
            {
                ListViewData = MainListData;
            }
            else
            {
                ListViewData = new ObservableCollection<Models.TrainerListModel.Trainee>();
                foreach (var item in MainListData)
                {
                    //if (
                    if (item.SportsInterest != null && item.SportsInterest != "" && item.SportsInterest.ToUpper().Contains(SearchText.ToUpper()))
                    {
                        ListViewData.Add(item);
                    }
                }
            }

            DisplayReviewList(ListViewData);
        }

        private void DisplayReviewList(ObservableCollection<Models.TrainerListModel.Trainee> mainData)
        {
            ObservableCollection<Models.TrainerListModel.Trainee> reviews = new ObservableCollection<Models.TrainerListModel.Trainee>();

            foreach (var item in mainData)
            {
                Models.TrainerListModel.Trainee review = new Models.TrainerListModel.Trainee();
                review.ImageUrL = item.ImageUrL;
                review.Address = item.Address;
                review.Country = item.Country;
                review.Email = item.Email;
                review.Name = item.Name;
                review.services = item.services;
                review.SportsInterest = item.SportsInterest;
                review.State = item.State;
                review.Status = item.Status;

                Double starRating = item.Rating;

                if (starRating > 0 && starRating < 1)
                {
                    review.FirstImageSource = "semi_selected.png";
                }
                else if (starRating > 0.99 && starRating < 1.5)
                {
                    review.FirstImageSource = "selected.png";
                }
                else if (starRating > 1.49 && starRating < 2.0)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "semi_selected.png";
                }
                else if (starRating > 1.99 && starRating < 2.5)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                }
                else if (starRating > 2.49 && starRating < 3.0)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                    review.ThirdImageSource = "semi_selected.png";
                }
                else if (starRating > 2.99 && starRating < 3.5)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                    review.ThirdImageSource = "selected.png";
                }
                else if (starRating > 3.49 && starRating < 4.0)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                    review.ThirdImageSource = "selected.png";
                    review.FourthImageSource = "semi_selected.png";
                }
                else if (starRating > 3.99 && starRating < 4.5)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                    review.ThirdImageSource = "selected.png";
                    review.FourthImageSource = "selected.png";
                }
                else if (starRating > 4.49 && starRating < 5.0)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                    review.ThirdImageSource = "selected.png";
                    review.FourthImageSource = "selected.png";
                    review.FifthImageSource = "semi_selected.png";
                }
                else if (starRating > 4.9)
                {
                    review.FirstImageSource = "selected.png";
                    review.SecondImageSource = "selected.png";
                    review.ThirdImageSource = "selected.png";
                    review.FourthImageSource = "selected.png";
                    review.FifthImageSource = "selected.png";
                }

                reviews.Add(review);
            }

            ListViewData = reviews;

        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
