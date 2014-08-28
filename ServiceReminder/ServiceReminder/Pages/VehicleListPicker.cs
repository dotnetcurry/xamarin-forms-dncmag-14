using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ServiceReminder.Pages
{
    public class Vehicle
    {
        public string Type { get; set; }
        public string Photo { get; set; }

        public string Detail { get; set; }

    }
    public class VehicleListPicker : ContentPage
    {

        public string SelectedVehicle {get;set;}
        public VehicleListPicker()
        {
            var vehicleTypes = new List<Vehicle>()
            {
                new Vehicle(){ Type = "Car", Photo = "Car.png" , Detail = "runs on 4 wheels"},
                new Vehicle(){ Type = "Bike", Photo = "Bike.png", Detail = "runs on 2 wheels"},
                new Vehicle(){ Type = "Other", Photo = "Other.png", Detail= "has no info"},
            };


            var listView = new ListView()
            {

                ItemsSource = vehicleTypes,

                ItemTemplate = new DataTemplate(()=>
                {

                    Label typeLabel = new Label();
                    typeLabel.SetBinding(Label.TextProperty, "Type");

                    Image image = new Image();
                    image.SetBinding(Image.SourceProperty, "Photo");

                    Label detailLabel = new Label();
                    detailLabel.SetBinding(Label.TextProperty,
                        new Binding("Detail", BindingMode.OneWay,
                                    null, null, "Vehicle {0}"));

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children = 
                            {
                                image,
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 0,
                                    Children = 
                                    {
                                        typeLabel,
                                        detailLabel
                                    }
                                }
                            }
                        }
                    };

                })

            };

            listView.ItemTapped += listView_ItemTapped;

            Label header = new Label
            {
                Text = "Choose Vehicle",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    listView
                }
            };
        }

        async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            SelectedVehicle = (e.Item as Vehicle).Type;
            await Navigation.PopAsync();
        }
    }
}
