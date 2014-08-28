using ServiceReminder.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ServiceReminder.ViewModels
{
    public class ReminderListItem
    {
        public int Id;
        public string VehiclePhoto { get; set; }
        public string Name { get; set; }
        public string NextServiceDate { get; set; }

    }

    public class HomePageViewModel : ObservableObject
    {
        public List<ReminderListItem> ReminderList { get; set; }

        public void PopulateReminderList()
        {
            ReminderList = new List<ReminderListItem>();
               
               foreach (var item in new ReminderItemDatabase().GetItems())
	           {
                   ReminderList.Add(new ReminderListItem { Id = item.Id, VehiclePhoto = item.VehicleType + ".png", Name=item.Name, NextServiceDate = item.NextServiceDate.ToString("d") });
		 
               }
        }    
    }
}
