using Microsoft.Phone.Scheduler;
using ServiceReminder.WinPhone.ReminderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(WPReminderService))]

namespace ServiceReminder.WinPhone.ReminderService
{
    public class WPReminderService : IReminderService
    {
        public void Remind(DateTime dateTime, string title, string message)
        {

            string param1Value = title;
            string param2Value = message;
            string queryString = "";
            if (param1Value != "" && param2Value != "")
            {
                queryString = "?param1=" + param1Value + "&param2=" + param2Value;
            }
            else if (param1Value != "" || param2Value != "")
            {
                queryString = (param1Value != null) ? "?param1=" + param1Value : "?param2=" + param2Value;
            }
            Microsoft.Phone.Scheduler.Reminder reminder = new Microsoft.Phone.Scheduler.Reminder("ServiceReminder");
            reminder.Title = title;
            reminder.Content = message;
            reminder.BeginTime = dateTime;
            reminder.ExpirationTime = dateTime.AddDays(1);
            reminder.RecurrenceType = RecurrenceInterval.None;
            reminder.NavigationUri = new Uri("/MainPage.xaml" + queryString, UriKind.Relative);
            ;

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

        }
    }
}
