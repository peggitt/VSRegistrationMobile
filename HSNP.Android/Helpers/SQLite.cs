using SQLite;
using System;
using System.IO;
using HSNP.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(HSNP.Droid.Helpers.SQLite))]
namespace HSNP.Droid.Helpers
{
    public class SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "USR2021.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsPath, sqliteFilename);
            return new SQLiteConnection(path);
        }
    }
}