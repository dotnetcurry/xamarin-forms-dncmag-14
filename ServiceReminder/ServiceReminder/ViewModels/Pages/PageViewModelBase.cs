using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ServiceReminder.ViewModels
{
    public class PageViewModelBase : ObservableObject
    {
        public INavigation Navigation { get; set; }
        public const string TitlePropertyName = "Title";

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }


        private string icon = null;

        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { icon = value; OnPropertyChanged(); }
        }
    }
}
