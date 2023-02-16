using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using HSNP.Interfaces;
using HSNP.Models;
using HSNP.Services;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HSNP.Helpers;

namespace HSNP.Database
{
	[SQLite.Preserve(AllMembers = true)]
	public class DataStore
	{
		public readonly SQLiteAsyncConnection _database;
		private readonly string nameSpace = "HSNP.Models.";
		public static readonly bool ResetDatabaseOnStart = false;
		string dbPath = DependencyService.Get<Interfaces.IFileHelper>().GetLocalFilePath("HSNP.Database.db3");
		public DataStore()
		{
		   // _database = DependencyService.Get<ISQLite>().GetConnection();
			_database = new SQLiteAsyncConnection(dbPath);
			_database.CreateTableAsync<Setting>().Wait();
			_database.CreateTableAsync<User>().Wait();
			_database.CreateTableAsync<Visitor>().Wait();
			_database.CreateTableAsync<Visit>().Wait();
			_database.CreateTableAsync<Destination>().Wait();
            _database.CreateTableAsync<SystemCodeDetailView>().Wait();
            _database.CreateTableAsync<Badge>().Wait();
            _database.CreateTableAsync<VisitorTag>().Wait();


        }

		//public void DeleteUserData()
		//{
		//	this._database.DeleteAllAsync<Setting>().Wait();
			
		//}

		//public void DeleteSettings()
		//{
		//	this._database.DeleteAllAsync<Setting>().Wait();
		//	this._database.DeleteAllAsync<SubLocation>().Wait();
			
		//}

		public virtual void DeleteTableData<TEntity>() where TEntity : class
		{
			this._database.DeleteAllAsync<TEntity>().Wait();
		}
		public virtual void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class
		{
			this._database.InsertOrReplaceAsync(entity);
		}

		public virtual void Create<TEntity>(TEntity entity) where TEntity : class
		{
			this._database.InsertAsync(entity);
		}

		public virtual void Delete<TEntity>(TEntity entity) where TEntity : class
		{
			this._database.DeleteAsync(entity);
		}

		public virtual void Delete<TEntity>(int id) where TEntity : class
		{
			this._database.DeleteAsync<TEntity>(id);
		}

		public virtual void Manage<TEntity>(TEntity entity) where TEntity : class
		{
		}

		public virtual void Update<TEntity>(TEntity entity) where TEntity : class
		{
			this._database.UpdateAsync(entity);
		}
		public async Task<User> GetDefaultUser()
		{
			var user = await _database.Table<User>().FirstOrDefaultAsync();
			return user;
		}

		public async Task<User> GetUserByIdNumber(string email)
		{
			var user = await _database.Table<User>().FirstOrDefaultAsync();
			if(!string.IsNullOrEmpty(email))
			 user = await _database.Table<User>().FirstOrDefaultAsync(t => t.Email == email);

            return user;
		}
		
		public List<object> GetTable(string tableName)
		{
			// tableName =  + tableName;
			object[] obj = new object[] { };
			var type = Type.GetType(nameSpace + tableName);
			TableMapping map = new TableMapping(type);
			string query = "SELECT * FROM [" + tableName + "]";
			return _database.QueryAsync(map, query, obj).Result;
		}

		public List<Object> GetTableRows(string tableName, string column, string value,string column2="",string value2="")
		{
			
			Object[] obj = new Object[] { };
			var type = Type.GetType(nameSpace + tableName);
			TableMapping map = new TableMapping(type);
			string query = "SELECT * FROM [" + tableName + "] WHERE " + column + " = '" + value + "'";
			if(column2 != "" && value2 != "")
			{
				query += " AND " + column2 + " = '" + value2 + "'"; 
			}
			return _database.QueryAsync(map, query, obj).Result;
		}

		public int GetTableRowsCount(string tableName, string column, string value, string column2 = "", string value2 = "")
		{
			Object[] obj = new Object[] { };
			var type = Type.GetType(nameSpace + tableName);
			TableMapping map = new TableMapping(type);
			string query = "SELECT * FROM [" + tableName + "] WHERE " + column + " = '" + value + "'";
			if (column2 != "" && value2 != "")
			{
				query += " AND " + column2 + " = '" + value2 + "'";
			}
			return _database.QueryAsync(map, query, obj).Result.Count;
		}


		public T GetTableRow<T>(string tableName, string column, string value)
		{
			object[] obj = new object[] { };
			TableMapping map = new TableMapping(Type.GetType(nameSpace + tableName));
			string query = "SELECT * FROM " + tableName + " WHERE " + column + " = '" + value + "'";
			//return _database.QueryAsync(map, query, obj).Cast<T>().Single();
			return (T)_database.QueryAsync(map, query, obj).Result.FirstOrDefault();
		}
       

    }
}