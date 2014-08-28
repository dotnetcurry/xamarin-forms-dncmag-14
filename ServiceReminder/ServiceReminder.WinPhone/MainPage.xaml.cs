using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;


namespace ServiceReminder.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            Content = ServiceReminder.App.GetMainPage().ConvertPageToUIElement(this);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string param1Value = "";
            string param2Value = "";

            NavigationContext.QueryString.TryGetValue("param1", out param1Value);
            NavigationContext.QueryString.TryGetValue("param2", out param2Value);
            if (!string.IsNullOrEmpty(param1Value))
                MessageBox.Show(param1Value, param2Value, MessageBoxButton.OK);
        }
    }
}
