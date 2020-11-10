using System;
using DFS;
using DFS.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]

namespace DFS.iOS
{
    public class CustomListViewRenderer : ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null)
            {
                // Configure the native control and subscribe to event handlers
                Control.AllowsSelection = false;
            }
        }

    }

}
