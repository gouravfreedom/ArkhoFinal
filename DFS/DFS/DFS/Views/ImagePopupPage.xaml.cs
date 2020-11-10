using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DFS.Views
{
    public partial class ImagePopupPage : PopupPage
    {
        double startScale, currentScale, xOffset, yOffset, StartX, StartY, x, y;
        public ImagePopupPage(string imageSource)
        {
            InitializeComponent();
            viewModel.ImageSource = imageSource;
        }

        /*private void Image_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartX = (1 - x) * image.Width;
                    StartY = (1 - y) * image.Height;
                    break;
                case GestureStatus.Running:
                    x = Clamp(1 - (StartX + e.TotalX) / Width, 0, 1);
                    y = Clamp(1 - (StartY + e.TotalY) / Height, 0, 1);

                    image.AnchorX = x;
                    image.AnchorY = y;

                    image.TranslationX = image.AnchorX;
                    image.TranslationY = image.AnchorY;
                    break;
            }
        }

        private void Image_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = image.Scale;
                image.AnchorX = 0;
                image.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max (1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = image.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (image.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = image.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (image.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * image.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * image.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                image.TranslationX = targetX.Clamp (-image.Width * (currentScale - 1), 0);
                image.TranslationY = targetY.Clamp (-image.Height * (currentScale - 1), 0);

                // Apply scale factor.
                image.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = image.TranslationX;
                yOffset = image.TranslationY;
            }
        }

        private T Clamp<T>(T value, T minimum, T maximum) where T : IComparable
        {
            if (value.CompareTo(minimum) < 0)
                return minimum;
            else if (value.CompareTo(maximum) > 0)
                return maximum;
            else
                return value;
        }*/
    }
}
