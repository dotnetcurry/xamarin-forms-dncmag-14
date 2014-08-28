using ServiceReminder.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ServiceReminder.ViewModels
{
    public class ServiceIntervalPickerCellViewModel : ObservableObject, IPickerCellViewModel
    {
        public const string Monthly = "Monthly";
        public const string Quarterly = "Quarterly";
        public const string HalfYearly = "Half Yearly";
        private const string Yearly = "Yearly";


        public ServiceIntervalPickerCellViewModel()
        {
            Items = new ObservableCollection<string>();
            Items.Add(Monthly);
            Items.Add(Quarterly);
            Items.Add(HalfYearly);
            Items.Add(Yearly);

            SelectionChangedCommand = new Command((index) =>
            {
                if (App.SelectedModel == null)
                    return;

                var selectedIndex = Convert.ToInt32(index);
                CalculateNextServiceDate(selectedIndex);

                // DEMO  USAGE OF MessagingCenter
                MessagingCenter.Send<ServiceIntervalPickerCellViewModel>(this, "serviceIntervalChanged");
            });
        }

        static int? CalculateIndex()
        {
            if (App.SelectedModel == null)
                return 0;

            //TODO: THERE'S A BUG - SELECT SERVICE INTERVAL FIRST THEN SELECT LAST SERVICE DATE- FIX LATER
            var duration = App.SelectedModel.NextServiceDate.Subtract(App.SelectedModel.LastServiceDate);

            //approx calculations (only for demo)

            if (duration.TotalDays <= 31)
                return 0;
            else if (duration.TotalDays <= 92)
                return 1;
            else if (duration.TotalDays <= 184)
                return 2;
            else if (duration.TotalDays <= 366)
                return 3;
            return 0;
        }

        public int? GetSelectedIndex()
        {
            // Get SelectedIndex makes sense only for existing items
            return CalculateIndex();
        }

        public static void CalculateNextServiceDate(int? selectedIndex = null)
        {
            if (!selectedIndex.HasValue)
                selectedIndex = CalculateIndex();

            App.SelectedModel.NextServiceDate = App.SelectedModel.LastServiceDate;
           
            if (selectedIndex == 0)
                App.SelectedModel.NextServiceDate = App.SelectedModel.NextServiceDate.AddMonths(1);
            if (selectedIndex == 1)
                App.SelectedModel.NextServiceDate = App.SelectedModel.NextServiceDate.AddMonths(3);
            if (selectedIndex == 2)
                App.SelectedModel.NextServiceDate = App.SelectedModel.NextServiceDate.AddMonths(6);
            if (selectedIndex == 3)
                App.SelectedModel.NextServiceDate = App.SelectedModel.NextServiceDate.AddMonths(12);
        }



        private string pickerText = "Service Interval:";

        public string PickerText
        {
            get { return pickerText; }
            set { pickerText = value; OnPropertyChanged(); }
        }

        private string pickerTitle = "Select Interval";

        public string PickerTitle
        {
            get { return pickerTitle; }
            set { pickerTitle = value; OnPropertyChanged(); }
        }

        private static Command selectionChangedCommand;

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
