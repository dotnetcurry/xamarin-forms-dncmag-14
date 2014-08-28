using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceReminder
{
    public interface IReminderService
    {
        void Remind(DateTime dateTime, string title, string message);
    }
}
