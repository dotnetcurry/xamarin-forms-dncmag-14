using ServiceReminder.Models;
using ServiceReminder.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ServiceReminder
{
	public class App
	{
		public static Page GetMainPage()
		{
            return new NavigationPage(new  HomePage());
		}

        public static ReminderItem SelectedModel { get; set; }

	}
}
