using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ServiceReminder.Models
{
    public class ReminderItem : ObservableObject
    {
        public ReminderItem()
        {
                
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }

        public string VehicleType { get; set; }

        public bool IsReminderEnabled { get; set; }

        public DateTime NextReminder { get; set; }

        public DateTime LastServiceDate { get; set; }

        public DateTime NextServiceDate { get; set; }

        public string Notes { get; set; }

    }

}
