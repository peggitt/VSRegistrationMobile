﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HSNP.Constants
{
    
    public static class AppConstants
    {
        // Live
      //  public static string BaseApiAddress => "http://app.hsnpmis.or.ke:5500";
      //  public static string SecurityApiAddress => "http://app.hsnpmis.or.ke:5200";

        // Test
        public static string BaseApiAddress => "http://app.hsnpmis.or.ke:7300";
        public static string SecurityApiAddress => "http://app.hsnpmis.or.ke:7200";

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
