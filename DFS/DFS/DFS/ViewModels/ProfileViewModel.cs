using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using static DFS.Models.LoginResponse;

namespace DFS.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private String _trainerName;
        public String TrainerName
        {
            get
            {
                return _trainerName;
            }
            set
            {
                _trainerName = value;
                RaisePropertyChanged(nameof(TrainerName));
            }
        }

        private String _serviceDesc;
        public String ServiceDesc
        {
            get
            {
                return _serviceDesc;
            }
            set
            {
                _serviceDesc = value;
                RaisePropertyChanged(nameof(ServiceDesc));
            }
        }

        private String _trainerSpeciality;
        public String TrainerSpeciality
        {
            get
            {
                return _trainerSpeciality;
            }
            set
            {
                _trainerSpeciality = value;
                RaisePropertyChanged(nameof(TrainerSpeciality));
            }
        }

        private String _trainingPlace;
        public String TrainingPlace
        {
            get
            {
                return _trainingPlace;
            }
            set
            {
                _trainingPlace = value;
                RaisePropertyChanged(nameof(TrainingPlace));
            }
        }

        private String _trainerExperience;
        public String TrainerExperience
        {
            get
            {
                return _trainerExperience;
            }
            set
            {
                _trainerExperience = value;
                RaisePropertyChanged(nameof(TrainerExperience));
            }
        }

        private String _trainerAccolades;
        public String TrainerAccolades
        {
            get
            {
                return _trainerAccolades;
            }
            set
            {
                _trainerAccolades = value;
                RaisePropertyChanged(nameof(TrainerAccolades));
            }
        }

        private String _trainerCert;
        public String TrainerCert
        {
            get
            {
                return _trainerCert;
            }
            set
            {
                _trainerCert = value;
                RaisePropertyChanged(nameof(TrainerCert));
            }
        }

        private Boolean _isReviewsVisible;
        public Boolean IsReviewsVisible
        {
            get
            {
                return _isReviewsVisible;
            }
            set
            {
                _isReviewsVisible = value;
                RaisePropertyChanged(nameof(IsReviewsVisible));
            }
        }

        private Boolean _isServiceVisible;
        public Boolean IsServiceVisible
        {
            get
            {
                return _isServiceVisible;
            }
            set
            {
                _isServiceVisible = value;
                RaisePropertyChanged(nameof(IsServiceVisible));
            }
        }

        private Boolean _isProfileVisible;
        public Boolean IsProfileVisible
        {
            get
            {
                return _isProfileVisible;
            }
            set
            {
                _isProfileVisible = value;
                RaisePropertyChanged(nameof(IsProfileVisible));
            }
        }

        private Boolean _isEditable;
        public Boolean IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                _isEditable = value;
                RaisePropertyChanged(nameof(IsEditable));
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


        private Color _profileColor;
        public Color ProfileColor
        {
            get
            {
                return _profileColor;
            }
            set
            {
                _profileColor = value;
                RaisePropertyChanged(nameof(ProfileColor));
            }
        }

        private Color _serviceColor;
        public Color ServiceColor
        {
            get
            {
                return _serviceColor;
            }
            set
            {
                _serviceColor = value;
                RaisePropertyChanged(nameof(ServiceColor));
            }
        }

        private Color _reviewColor;
        public Color ReviewColor
        {
            get
            {
                return _reviewColor;
            }
            set
            {
                _reviewColor = value;
                RaisePropertyChanged(nameof(ReviewColor));
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }

        private ObservableCollection<Models.LoginResponse.Services> _serviceListData;
        public ObservableCollection<Models.LoginResponse.Services> ServiceListData
        {
            get { return _serviceListData; }
            set
            {
                _serviceListData = value;
                RaisePropertyChanged(nameof(ServiceListData));
            }
        }

        private ObservableCollection<Models.LoginResponse.Reviews> _reviewListData;
        public ObservableCollection<Models.LoginResponse.Reviews> ReviewListData
        {
            get { return _reviewListData; }
            set
            {
                _reviewListData = value;
                RaisePropertyChanged(nameof(ReviewListData));
            }
        }

        public ICommand ServiceCommand { get; set; }
        public ICommand ReviewCommand { get; set; }
        public ICommand ProfileCommand { get; set; }


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
            set
            {
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

        private ObservableCollection<string> gallery;
        public ObservableCollection<string> Gallery
        {
            get { return gallery; }
            set
            {
                gallery = value;
                RaisePropertyChanged(nameof(Gallery));
            }
        }

        private void DisplayReviewList(ObservableCollection<Reviews> mainData)
        {
            ObservableCollection<Reviews> reviews = new ObservableCollection<Reviews>();

            foreach (var item in mainData)
            {
                Reviews review = new Reviews();
                review.Comment = item.Comment;
                review.Name = item.Name;
                review.TraineeEmailId = item.TraineeEmailId;
                review.TrainerEmailId = item.TrainerEmailId;
                review.FirstImageSource = "unselected.png";
                review.SecondImageSource = "unselected.png";
                review.ThirdImageSource = "unselected.png";
                review.FourthImageSource = "unselected.png";
                review.FifthImageSource = "unselected.png";

                Double starRating = Convert.ToDouble(item.Rating) / 2;

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

            ReviewListData = reviews;

        }

        public ProfileViewModel()
        {
            ServiceListData = new ObservableCollection<Models.LoginResponse.Services>();
            ReviewListData = new ObservableCollection<Reviews>();



            // When Trainer checked his/her own profile
            if (App.SelectedView == "Trainer")
            {
                // Review data process
                DisplayReviewList(App.LoginResponse.reviews);

                IsEditable = true;
                DisconnectVisible = false;
                TrainerName = App.LoginResponse.basicInfo.Name;
                TrainingPlace = App.LoginResponse.basicInfo.State;
                TrainerAccolades = App.LoginResponse.professionalInfo.Accolades;
                TrainerExperience = App.LoginResponse.professionalInfo.Experience;
                TrainerSpeciality = App.LoginResponse.professionalInfo.Speciality;

                try
                {

                    if (App.LoginResponse.basicInfo.ImageUrl != null && App.LoginResponse.basicInfo.ImageUrl != "NA" && App.LoginResponse.basicInfo.ImageUrl != "defaultIcon.png")
                    {
                        String url = App.LoginResponse.basicInfo.ImageUrl != null ? App.LoginResponse.basicInfo.ImageUrl : "defaultIcon.png";

                        ImageSource = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(url) };
                    }
                }catch(Exception ex)
                {

                }
                //PlaceHolderImageSource = "defaultIcon.png";
                ServiceListData = App.LoginResponse.professionalInfo.services;

                TrainerCert = "";

                foreach (var item in App.LoginResponse.professionalInfo.certifications)
                {
                    TrainerCert += item.Certification + " | ";
                }

                if(TrainerCert.Contains(" | "))
                {
                    TrainerCert = TrainerCert.Substring(0, TrainerCert.Length - 2);
                }

                if (App.LoginResponse.basicInfo.InstaGramImages != null && App.LoginResponse.basicInfo.InstaGramImages != "")
                {

                    string s = App.LoginResponse.basicInfo.InstaGramImages;
                    string[] imageurl = s.Split(',').Select(sValue => sValue.Trim()).ToArray();
                    if (imageurl.Count() > 0)
                    {
                        GalleryVisible = true;
                        InstaVisible = false;
                        DisconnectVisible = true;

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
                        GalleryVisible = true;
                        InstaVisible = false;
                        DisconnectVisible = true;

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
                        GalleryVisible = false;
                        InstaVisible = true;
                        DisconnectVisible = false;
                    }
                }
            }

            // When profile is checked by trainee
            else
            {

                // Review data process
                DisplayReviewList(App.TrainerData.reviews);

                IsEditable = false;
                TrainerName = App.TrainerData.basicInfo.Name;
                TrainingPlace = App.TrainerData.basicInfo.State;
                TrainerAccolades = App.TrainerData.professionalInfo.Accolades;
                TrainerExperience = App.TrainerData.professionalInfo.Experience;
                TrainerSpeciality = App.TrainerData.professionalInfo.Speciality;

                try
                {
                    if (App.TrainerData.basicInfo.ImageUrl != null && App.TrainerData.basicInfo.ImageUrl != "NA" && App.TrainerData.basicInfo.ImageUrl != "defaultIcon.png")
                    {

                        String url = App.TrainerData.basicInfo.ImageUrl != null ? App.TrainerData.basicInfo.ImageUrl : "defaultIcon.png";


                        ImageSource = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(url) };
                    }
                }
                catch (Exception ex)
                {

                }

                if (App.TrainerData.basicInfo.InstaGramImages != null)
                {

                    string s = App.TrainerData.basicInfo.InstaGramImages;
                    string[] imageurl = s.Split(',').Select(sValue => sValue.Trim()).ToArray();

                    if (imageurl.Count() > 0)
                    {

                        GalleryVisible = true;
                        InstaVisible = false;
                        DisconnectVisible = false;

                        var list = new List<string>();
                        foreach (var item in imageurl)
                        {
                            list.Add(item);
                        }
                        Gallery = new ObservableCollection<string>(list);
                    }
                }

                if (App.TrainerData.professionalInfo.services.Any())
                    ServiceListData = App.TrainerData.professionalInfo.services;



                TrainerCert = "";

                foreach (var item in App.TrainerData.professionalInfo.certifications)
                {
                    TrainerCert += item.Certification + " | ";
                }

                if (TrainerCert.Contains(" | "))
                {
                    TrainerCert = TrainerCert.Substring(0, TrainerCert.Length - 2);
                }
            }

            SelectedIndex = 0;

            HandleSwipe();

            ServiceCommand = new Command(() => HandleServiceSelection());
            ReviewCommand = new Command(() => HandleReviewSelection());
            ProfileCommand = new Command(() => HandleProfileSelection());
        }

        public void HandleSwipe()
        {
            if (SelectedIndex == 0)
            {
                HandleProfileSelection();
            }
            else if (SelectedIndex == 1)
            {
                HandleServiceSelection();
            }
            else
            {
                HandleReviewSelection();
            }
        }

        public void HandleServiceSelection()
        {
            SelectedIndex = 1;
            IsServiceVisible = true;
            IsReviewsVisible = false;
            IsProfileVisible = false;

            ServiceColor = Color.LimeGreen;
            ReviewColor = Color.White;
            ProfileColor = Color.White;
        }

        public void HandleReviewSelection()
        {
            SelectedIndex = 2;
            IsServiceVisible = false;
            IsReviewsVisible = true;
            IsProfileVisible = false;

            ServiceColor = Color.White;
            ReviewColor = Color.LimeGreen;
            ProfileColor = Color.White;
        }

        public async void UpdateInstagramMedia()
        {
            var result =await App.TodoManager.UpdateInstagramMedia(App.InstaAccessToken);
            if (result.Equals("Success"))
            {
                if (App.InstagramMedia != null && App.InstagramMedia.data != null)
                {
                    GalleryVisible = true;
                    InstaVisible = false;
                    DisconnectVisible = true;

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
                    GalleryVisible = false;
                    InstaVisible = true;
                    DisconnectVisible = false;
                }
            }
        }

        public void HandleProfileSelection()
        {
            SelectedIndex = 0;
            IsServiceVisible = false;
            IsReviewsVisible = false;
            IsProfileVisible = true;

            ServiceColor = Color.White;
            ReviewColor = Color.White;
            ProfileColor = Color.LimeGreen;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
