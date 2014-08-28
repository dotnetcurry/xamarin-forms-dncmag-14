using ServiceReminder.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
#if WINDOWS_PHONE
using Windows.Storage;
#endif
namespace ServiceReminder.Data
{
    public class ReminderItemDatabase
    {

        static string DatabasePath
        {
            get
            {
                var sqliteFilename = "ServiceReminder.db3";
                #if __IOS__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
                var path = Path.Combine(libraryPath, sqliteFilename);
                #else
                #if __ANDROID__
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, sqliteFilename);
                #else
                // WinPhone
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
                #endif
                #endif
                return path;
            }
        }

        static object locker = new object();

        static SQLiteConnection database = new SQLiteConnection(databasePath: DatabasePath);

        public ReminderItemDatabase()
        {
            // create the tables
            database.CreateTable<ReminderItem>();
        }

        public IEnumerable<ReminderItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<ReminderItem>() select i).ToList();
            }
        }

        public ReminderItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<ReminderItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem(ReminderItem item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<ReminderItem>(id);
            }
        }
    }
}
