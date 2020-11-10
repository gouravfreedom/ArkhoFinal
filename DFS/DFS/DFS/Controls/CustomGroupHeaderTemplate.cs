using System;
using Xamarin.Forms;

namespace DFS
{
    public class CustomGroupHeaderTemplate : DataTemplateSelector
    {
        public DataTemplate HeaderTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return HeaderTemplate;
        }
    }
}
