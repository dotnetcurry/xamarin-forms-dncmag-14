

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ServiceReminder.iOS.ReminderService;

[assembly: Xamarin.Forms.Dependency(typeof(iOSReminderService))]
namespace ServiceReminder.iOS.ReminderService
{
    public class iOSReminderService : IReminderService
    {
        public void Remind(DateTime dateTime, string title, string message)
        {
            var notification = new UILocalNotification();

            //---- set the fire date (the date time in which it will fire)
            notification.FireDate =dateTime;

            //---- configure the alert stuff
            notification.AlertAction = title;
            notification.AlertBody = message;

            //---- modify the badge
            notification.ApplicationIconBadgeNumber = 1;

            //---- set the sound to be the default sound
            notification.SoundName = UILocalNotification.DefaultSoundName;

            //---- schedule it
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
    }
}