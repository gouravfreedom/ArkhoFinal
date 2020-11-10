using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
//using DFS.Views;
using Xamarin.Forms;

namespace DFS.ViewModels
{
    public class TraineeProfileViewModel : INotifyPropertyChanged
    {
        private String _traineeName;
        public String TraineeName
        {
            get { return _traineeName; }
            set
            {
                _traineeName = value;
                RaisePropertyChanged(nameof(TraineeName));
            }
        }

        private String _traineeInterest;
        public String TraineeInterest
        {
            get { return _traineeInterest; }
            set
            {
                _traineeInterest = value;
                RaisePropertyChanged(nameof(TraineeInterest));
            }
        }

        public String TraineeGoals
        {
            get { return _traineeGoals; }
            set
            {
                _traineeGoals = value;
                RaisePropertyChanged(nameof(TraineeGoals));
            }
        }

        private String _traineeGoals;

        private UriImageSource _imageSource { get; set; } // = App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        public UriImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        //private string _placeHolderImageSource = App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        //public string PlaceHolderImageSource
        //{
        //    get
        //    {
        //        return _placeHolderImageSource;
        //    }
        //    set
        //    {
        //        _placeHolderImageSource = value;
        //        RaisePropertyChanged(nameof(PlaceHolderImageSource));
        //    }
        //}

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

        private bool galeryVisible;
        public bool GalleryVisible
        {
            get
            {
                return galeryVisible;
            }
            set {
                galeryVisible = value;
                RaisePropertyChanged(nameof(GalleryVisible));
            }
        }

        private bool instaVisible;
        public bool InstaVisible
        {
            get
            {
                return instaVisible;
            }
            set
            {
                instaVisible = value;
                RaisePropertyChanged(nameof(InstaVisible));
            }
        }

        private bool _disconnectVisible;
        public bool DisconnectVisible
        {
            get
            {
                return _disconnectVisible;
            }
            set
            {
                _disconnectVisible = value;
                RaisePropertyChanged(nameof(DisconnectVisible));
            }
        }

        private ObservableCollection<string> gallery;
        public ObservableCollection<string> Gallery {
            get { return gallery; }
            set {
                gallery = value;
                RaisePropertyChanged(nameof(Gallery));
            }
        }

        public ICommand CalanderCommand
        {
            get { return new Command(CalanderHandler); }
        }

        public TraineeProfileViewModel()
        {
            //Models.LoginResponse.SyncLoginResponse syncLoginResponse = App.DatabaseManager.SyncLoginResponse("Trainee");

            TraineeName = App.LoginResponse.basicInfo.Name;
            TraineeGoals = App.LoginResponse.basicInfo.SportsInterest;
            TraineeInterest = App.LoginResponse.basicInfo.SportsInterest;

            try
            {
                if (App.LoginResponse.basicInfo.ImageUrl != null && App.LoginResponse.basicInfo.ImageUrl != "NA" && App.LoginResponse.basicInfo.ImageUrl != "defaultIcon.png")
                {
                    String url = App.LoginResponse.basicInfo.ImageUrl != null ? App.LoginResponse.basicInfo.ImageUrl : "defaultIcon.png";

                    ImageSource = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(url) };
                }
            }
            catch (Exception ex)
            {

            }

            if (App.LoginResponse.basicInfo.InstaGramImages != null && App.LoginResponse.basicInfo.InstaGramImages != "")
            {

                string s = App.LoginResponse.basicInfo.InstaGramImages;
                string[] imageurl = s.Split(',').Select(sValue => sValue.Trim()).ToArray();

                if (imageurl.Count() > 0)
                {
                    DisconnectVisible = true;
                    GalleryVisible = true;
                    InstaVisible = false;

                    var list = new List<string>();
                    foreach (var item in imageurl)
                    {
                        list.Add(item);
                    }
                    Gallery = new ObservableCollection<string>(list);
                }
            }
            else
            {
                if (App.InstagramMedia != null && App.InstagramMedia.data != null)
                {
                    DisconnectVisible = true;
                    GalleryVisible = true;
                    InstaVisible = false;

                    var list = new List<string>();
                    foreach (var item in App.InstagramMedia.data)
                    {
                        var media = item.media_url;
                        list.Add(media);
                    }
                    Gallery = new ObservableCollection<string>(list);
                }

                else
                {
                    DisconnectVisible = false;
                    GalleryVisible = false;
                    InstaVisible = true;
                }
            }
        }

        void CalanderHandler(object obj)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new TraineeCalanderPage());
            MessagingCenter.Send<TraineeProfileViewModel>(this, "CalenderPage");
        }

        public async void UpdateInstagramMedia()
        {
            var result = await App.TodoManager.UpdateInstagramMedia(App.InstaAccessToken);
            if (result.Equals("Success"))
            {
                if (App.InstagramMedia != null && App.InstagramMedia.data != null)
                {
                    DisconnectVisible = true;
                    GalleryVisible = true;
                    InstaVisible = false;

                    var list = new List<string>();
                    foreach (var item in App.InstagramMedia.data)
                    {
                        var media = item.media_url;
                        list.Add(media);
                    }
                    Gallery = new ObservableCollection<string>(list);
                }
                else
                {
                    DisconnectVisible = false;
                    GalleryVisible = false;
                    InstaVisible = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
