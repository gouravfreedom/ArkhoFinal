using System;
using Xamarin.Forms;

namespace DFS
{
    public class PersonDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LabelTemplate { get; set; }
        public DataTemplate LabelButtonTemplate { get; set; }
        public DataTemplate MinusButtonTemplate { get; set; }
        public DataTemplate EntryTemplate { get; set; }
        public DataTemplate PickerTemplate { get; set; }
        public DataTemplate DatePickerTemplate { get; set; }
        public DataTemplate ServiceTemplate { get; set; }
        public DataTemplate NumericTemplate { get; set; }
        public DataTemplate EditorTemplate { get; set; }
        public DataTemplate ButtonTemplate { get; set; }
        public DataTemplate OnlyMinusButtonTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var actualItem  = ((Models.SignupData)item);

            if (actualItem.InputType == "Label" && actualItem.IsAdditionAvailable)
            {
                return LabelButtonTemplate;
            }
            else if(actualItem.InputType == "Minus")
            {
                return MinusButtonTemplate;
            }
            else if (actualItem.InputType == "OnlyMinus")
            {
                return OnlyMinusButtonTemplate;
            }
            else if (actualItem.InputType == "Label")
            {
                return LabelTemplate;
            }
            else if (actualItem.InputType == "Entry" && actualItem.IsAdditionAvailable)
            {
                return NumericTemplate;
            }
            else if (actualItem.InputType == "Entry")
            {
                return EntryTemplate;
            }
            else if (actualItem.InputType == "Picker" && actualItem.IsAdditionAvailable)
            {
                return DatePickerTemplate;
            }
            else if (actualItem.InputType == "Picker")
            {
                return PickerTemplate;
            }
            else if (actualItem.InputType == "Service")
            {
                return ServiceTemplate;
            }
            else if (actualItem.InputType == "Editor")
            {
                return EditorTemplate;
            }
            else if (actualItem.InputType == "Button")
            {
                return ButtonTemplate;
            }


            return LabelTemplate;
        }
    }
}
