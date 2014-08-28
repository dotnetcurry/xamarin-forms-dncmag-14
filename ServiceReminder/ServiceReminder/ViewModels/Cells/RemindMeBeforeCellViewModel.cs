using ServiceReminder.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ServiceReminder.ViewModels
{
    public class RemindMePickerCellViewModel : ObservableObject, IPickerCellViewModel
    {
        private const string OneDayBefore = "One day before";
        private const string OneWeekBefore = "One week before";
        private const string OneMonthBefore = "One month before";
        public RemindMePickerCellViewModel()
        {
            Items = new ObservableCollection<string>();
            Items.Add(OneDayBefore);
            Items.Add(OneWeekBefore);
            Items.Add(OneMonthBefore);

            selectionChangedCommand = new Command((index) =>
            {
                if (App.SelectedModel == null)
                    return;
                var selectedIndex = Convert.ToInt32(index);
                    var selectedItem = Items[selectedIndex];
                    if (selectedItem == OneDayBefore)
                    App.SelectedModel.NextReminder = App.SelectedModel.NextServiceDate.AddDays(-1);
                    if (selectedItem == OneWeekBefore)
                    App.SelectedModel.NextReminder = App.SelectedModel.NextServiceDate.AddDays(-7);
                    if (selectedItem == OneMonthBefore)
                    App.SelectedModel.NextReminder = App.SelectedModel.NextServiceDate.AddMonths(-1);
            });
        }

        public int? GetSelectedIndex()
        {
            if (App.SelectedModel != null)
            {
                // This is called during edit ensure right item is selected in the picker
                var date = App.SelectedModel.NextReminder.Subtract(App.SelectedModel.NextServiceDate);
                if (date.TotalDays == -1)
                    return  0;
                 if (date.TotalDays == -7)
                    return  1;
                 if (date.TotalDays == -31)
                    return  3;
            }
            return 0;
        }

        private string pickerText = "Remind Me Before";

        public string PickerText
        {
            get { return pickerText; }
            set { pickerText = value; OnPropertyChanged(); }
        }

        private string pickerTitle = "Select Reminder";

        public string PickerTitle
        {
            get { return pickerTitle; }
            set { pickerTitle = value; OnPropertyChanged(); }
        }

        private Command selectionChangedCommand;

        public Command SelectionChangedCommand
        {
            get { return selectionChangedCommand; }
            set { selectionChangedCommand = value; OnPropertyChanged(); }
        }

        private IList<string> items;

        public IList<string> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(); }
        }
    }
}
