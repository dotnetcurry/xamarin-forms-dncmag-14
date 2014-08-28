using ServiceReminder.Cells;
using ServiceReminder.Data;
using ServiceReminder.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ServiceReminder.ViewModels
{
    public class EditReminderPageViewModel : PageViewModelBase
    {

        VehicleListPicker vehicleListPicker;
        public EditReminderPageViewModel()
        {
           
            // If SelectedModel is null then the screen is Add else Edit
            if (App.SelectedModel == null)
            {
                
                this.Title = "Add Service Reminder";
                App.SelectedModel = new Models.ReminderItem();
                App.SelectedModel.LastServiceDate =  DateTime.Now;
                App.SelectedModel.NextServiceDate = DateTime.Now.AddMonths(1);
                App.SelectedModel.IsReminderEnabled = true;
                App.SelectedModel.NextReminder = App.SelectedModel.NextServiceDate.AddDays(-1);
                LastServiceDate = DateTime.Now;
               
                VehicleType = "Car";
            }
            else
            {
                this.Title = "Edit Service Reminder";
            }

            // DEMO usage of Custom Page instead of Picker
            ChooseVehichleTypeCommand = new Command(async () => {

                vehicleListPicker = new VehicleListPicker();
                if (Navigation != null)
                    await Navigation.PushAsync(vehicleListPicker);
            });

            // Demo usage of MessagingCenter
            MessagingCenter.Subscribe<ServiceIntervalPickerCellViewModel>(this, "serviceIntervalChanged", (sender) => {

                OnPropertyChanged("NextServiceDate");
            });
        }

        async public Task<bool> Save()
        {
            if (App.SelectedModel != null && !string.IsNullOrEmpty(App.SelectedModel.Name) && !string.IsNullOrEmpty(App.SelectedModel.RegistrationNumber))
            {
                new ReminderItemDatabase().SaveItem(App.SelectedModel);
                var remiderService = DependencyService.Get<IReminderService>();
                if(remiderService!=null && App.SelectedModel.IsReminderEnabled)
                    //TODO: Add 
                remiderService.Remind(DateTime.Now.AddSeconds(25), "Vehicle Service Alert", App.SelectedModel.Name + " is due for service on: " + App.SelectedModel.NextServiceDate.ToShortDateString());
            }
            else
                return false;

            return true;
        }

        public void OnAppearing()
        {
            if (vehicleListPicker != null)
                VehicleType = vehicleListPicker.SelectedVehicle;
            // if nothing was selected default to car
            if (string.IsNullOrEmpty(VehicleType))
                VehicleType = "Car";
        }

        public string Name
        {
            get { return App.SelectedModel.Name; }
            set
            {
                App.SelectedModel.Name = value;
                OnPropertyChanged();
            }
        }

        public string RegistrationNo
        {
            get { return App.SelectedModel.RegistrationNumber; }
            set { App.SelectedModel.RegistrationNumber = value; OnPropertyChanged(); }
        }


        public DateTime LastServiceDate
        {
            get { return App.SelectedModel.LastServiceDate; }
            set { App.SelectedModel.LastServiceDate = value;
            ServiceIntervalPickerCellViewModel.CalculateNextServiceDate(); 
                OnPropertyChanged("NextServiceDate"); OnPropertyChanged(); }
        }


        public string NextServiceDate
        {
            get { return App.SelectedModel.NextServiceDate.ToString("d"); }
        }


        public string VehicleType 
        {
            get { return App.SelectedModel.VehicleType; }
            set 
            { 
                App.SelectedModel.VehicleType = value;
                VehiclePhoto = VehicleType + ".png";
                OnPropertyChanged(); 
            }
        }


        public bool IsReminder
        {
            get { return App.SelectedModel.IsReminderEnabled; }
            set { App.SelectedModel.IsReminderEnabled = value; OnPropertyChanged(); }
        }


        private string vehiclePhoto;        

        public string VehiclePhoto
        {
            get { return vehiclePhoto; }
            set { vehiclePhoto = value; OnPropertyChanged(); }
        }

        private Command chooseVehichleTypeCommand;

        public Command ChooseVehichleTypeCommand
        {
            get { return chooseVehichleTypeCommand; }
            set { chooseVehichleTypeCommand = value; OnPropertyChanged(); }
        }
        
	
    }

    

}
