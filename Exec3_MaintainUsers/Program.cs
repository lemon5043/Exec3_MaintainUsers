using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISpan.Utility;

namespace Exec3_MaintainUsers
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//Insert("小白", "white123", "password!", new DateTime(1997, 01, 15), 185);
				//SelectAll();
				//Update("小白z", "white1234", "password", new DateTime(1996, 01, 15), 186, 1);
				Delete(2);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"操作失敗，原因:{ex.Message}");
			}
		}

		static void Insert(string name, string account, string password, DateTime dateOfBirth, int height)
		{
			string connString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
			string sql = @"INSERT INTO Users
						(name, account, password, dateOfBirth, height)
						VALUES(@Name, @Account, @password, @dateOfBirth, @height)";
			var dbHelper = new SqlDbHelper("default");


			var parameters = new SqlParameterBuilder()
				.AddVarchar("@Name", 50, name)
				.AddVarchar("@Account", 50, account)
				.AddVarchar("@password", 50, password)
				.AddDateTime("@dateOfBirth", dateOfBirth)
				.AddInt("@height", height)
				.Build();

			dbHelper.ExecuteNonQuery(sql, parameters);
			Console.WriteLine("資料已新增");
		}

		static void SelectAll()
		{
			string sql = @"select * from users";
			var dbHelper = new SqlDbHelper("default");

			var parameters = new SqlParameterBuilder()
					.AddInt("id", 0)
					.Build();

				DataTable news = dbHelper.Select(sql, parameters);
				foreach (DataRow row in news.Rows)
				{
					int id = row.Field<int>("id");
					string name = row.Field<string>("name");
					string account = row.Field<string>("account");
					string password = row.Field<string>("password");
					System.DateTime dateOfBirth = row.Field<DateTime>("dateOfBirth");
					int height = row.Field<int>("height");
					Console.WriteLine(
						$"ID = {id}, name = {name}, account = {account}, password = {password}, dateOfBirth = {dateOfBirth}, height = {height}");
				}
			
		}

		static void Update(string name, string account, string password, DateTime dateOfBirth, int height, int Id)
		{
			string sql = @"UPDATE Users
						SET name = @name,
						account = @account,
						password = @password,
						dateOfBirth = @dateOfBirth,
						height = @height
						where Id = @Id";
			var dbHelper = new SqlDbHelper("default");

			var parameters = new SqlParameterBuilder()
					.AddVarchar("@Name", 50, name)
					.AddVarchar("@Account", 50, account)
					.AddVarchar("@password", 50, password)
					.AddDateTime("@dateOfBirth", dateOfBirth)
					.AddInt("@height", height)
					.AddInt("@Id", Id)
					.Build();

				dbHelper.ExecuteNonQuery(sql, parameters);
				Console.WriteLine("紀錄已更新");
		}

		static void Delete(int Id)
		{
			string sql = @"DELETE 
						from Users
						where Id = @Id";
			var dbHelper = new SqlDbHelper("default");

			var parameters = new SqlParameterBuilder()
				.AddInt("ID", Id)
				.Build();

				dbHelper.ExecuteNonQuery(sql, parameters);
				Console.WriteLine("紀錄已刪除");
		}
	}
}
