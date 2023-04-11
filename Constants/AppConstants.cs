using System;
using System.Collections.Generic;
using System.Text;

namespace HSNP.Constants
{
    
    public static class AppConstants
    {
        public static string BaseApiAddress => "http://197.254.7.126:7200";
        public static string SettingsServiceUrl => "http://197.254.7.126:7300";

        public const string DatabaseFilename = "HSNPSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
