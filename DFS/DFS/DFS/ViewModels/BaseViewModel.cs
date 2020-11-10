using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DFS
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            if (App.LoginResponse.basicInfo.ImageUrl != null && App.LoginResponse.basicInfo.ImageUrl != "NA" && App.LoginResponse.basicInfo.ImageUrl != "defaultIcon.png")
            {
                //String url = App.LoginResponse.basicInfo.ImageUrl != null ? App.LoginResponse.basicInfo.ImageUrl : "defaultIcon.png";
                //string[] values = url.Split(new string[] { "http" }, StringSplitOptions.None);

                //String finalUrl;

                //if (values.Length > 2)
                //{
                //    String tempUrl = "http" + values[values.Length - 1];

                //    finalUrl = tempUrl.Substring(0, tempUrl.Length - 5);
                //}
                //else
                //{
                //    finalUrl = "http" + values[values.Length - 1];
                //}


                ImageSource = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(App.LoginResponse.basicInfo.ImageUrl) };
            }

            //PlaceHolderImageSource = "defaultIcon.png";
        }

        private string title = string.Empty;
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string subtitle = string.Empty;
        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public const string SubtitlePropertyName = "Subtitle";
        public string Subtitle
        {
            get { return subtitle; }
            set { SetProperty(ref subtitle, value); }
        }

        private string icon = null;
        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;
            }
        }

        bool isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            private set { SetProperty(ref isNotBusy, value); }
        }

        private bool canLoadMore = true;
        /// <summary>
        /// Gets or sets if we can load more.
        /// </summary>
        public const string CanLoadMorePropertyName = "CanLoadMore";
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value); }
        }

        private UriImageSource _imageSource { get; set; } //= App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        public UriImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        private string _placeHolderImageSource { get; set; } //= App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        public string PlaceHolderImageSource
        {
            get
            {
                return _placeHolderImageSource;
            }
            set
            {
                _placeHolderImageSource = value;
                OnPropertyChanged(nameof(PlaceHolderImageSource));
            }
        }


        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

