using SQLite;

namespace HSNP.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}