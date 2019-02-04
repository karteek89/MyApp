using System;
using System.Linq;
using System.Web;
using MyApp.Contract;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MyApp.Models;
using MyApp.Extensions;

namespace MyApp.Services
{
    public class UserFormService : IUserFormService
    {
        private readonly string _connectionString;
        public UserFormService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }

        public Task<IList<UserForm>> GetAllForms()
        {
            var query = string.Format("SELECT * FROM [dbo].[UserForm]");
            return Task.Run(() =>
            {
                var result = ExecuteQuery(query).DataTableToList<UserForm>();
                return result;
            });
        }

        public Task<int> GetActiveFormsCount(int userId)
        {
            var query = string.Format("SELECT COUNT(1) FROM [dbo].[UserForm] WHERE [IsActive] = 1 AND [UserId] = {0}", userId);
            return Task.Run(() => ExecuteScalar(query));
        }

        public Task<UserForm> GetFormById(int formId)
        {
            var query = string.Format("SELECT * FROM [dbo].[UserForm] WHERE [IsActive] = 1 AND [Id] = {0}", formId);
            return Task.Run(() =>
            {
                var result = ExecuteQuery(query).DataTableToList<UserForm>();
                return result.FirstOrDefault();
            });
        }

        public Task<int> DisableActiveForm(int userId)
        {
            try
            {
                string query = string.Format("UPDATE [dbo].[UserForm] SET [IsActive] = 0 WHERE [UserId]= @UserId");
                var sqlParameters = new List<SqlParameter>(){
                    new SqlParameter {  ParameterName = "@UserId", DbType = DbType.Int32, Value = userId }
                };

                return Task.Run(() => ExecuteNonQuery(query, sqlParameters));
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> CreateNewFrom(UserForm userForm)
        {
            try
            {
                string query = "INSERT INTO [dbo].[UserForm] ([UserId],[Field1],[Field2],[Field3],[Field4],[Field5],[Field6],[Field7],[Field8],[Field9],[Field10],[IsActive],[CreatedOn]) " +
                   "VALUES (@UserId, @Field1, @Field2, @Field3, @Field4, @Field5, @Field6, @Field7, @Field8, @Field9, @Field10, @IsActive, @CreatedOn)";

                var sqlParameters = new List<SqlParameter>(){
                    new SqlParameter {  ParameterName = "@UserId", DbType = DbType.Int32, Value = userForm.UserId },
                    new SqlParameter {  ParameterName = "@Field1", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field1 ?? "" },
                    new SqlParameter {  ParameterName = "@Field2", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field2 ?? "" },
                    new SqlParameter {  ParameterName = "@Field3", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field3 ?? "" },
                    new SqlParameter {  ParameterName = "@Field4", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field4 ?? "" },
                    new SqlParameter {  ParameterName = "@Field5", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field5 ?? "" },
                    new SqlParameter {  ParameterName = "@Field6", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field6 ?? "" },
                    new SqlParameter {  ParameterName = "@Field7", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field7 ?? "" },
                    new SqlParameter {  ParameterName = "@Field8", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field8 ?? "" },
                    new SqlParameter {  ParameterName = "@Field9", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field9 ?? "" },
                    new SqlParameter {  ParameterName = "@Field10", SqlDbType = SqlDbType.VarChar, Size = 50, Value = userForm.Field10 ?? ""},
                    new SqlParameter {  ParameterName = "@IsActive", SqlDbType = SqlDbType.Bit, Size = 50, Value = true },
                    new SqlParameter {  ParameterName = "@CreatedOn", SqlDbType = SqlDbType.DateTime, Size = 50, Value = DateTime.Now }
                };

                return Task.Run(() => ExecuteNonQuery(query, sqlParameters));
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        #region Private functions

        private DataTable ExecuteQuery(string query)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        private int ExecuteScalar(string query)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        private int ExecuteNonQuery(string query, IList<SqlParameter> parameters = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);
                }

                conn.Open();
                return (int)cmd.ExecuteNonQuery();
            }
        }

        #endregion

    }
}