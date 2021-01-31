using DMSAPI.BusinessLogic.Database;
using DMSAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.User
{
    public class UserDL
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        ConnectionString _connn;

        public UserDL(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor, ConnectionString connectionString)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
            this._connn = connectionString;



        }
        
        internal List<tbl_User> GetUserList()
        {
            List<tbl_User> lstObjects = new List<tbl_User>();
            var httpContext = accessor.HttpContext;
            ResultType result = new ResultType();
            //string sqlQuery = @"sp_GetUserList";
            httpContext.Session.SetString("Database", "SystemDatabase");
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = "sp_GetUserList";
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    SqlDataReader dr = cmd.ExecuteReader();

                    tbl_User obj = null;
                    while (dr.Read())
                    {
                        obj = new tbl_User();
                        obj.Id = Convert.ToInt32( dr["Id"]);
                        obj.UserName = (string)dr["UserName"];
                        obj.FirstName = (string)dr["FirstName"];
                        obj.MiddleName = (string)dr["MiddleName"];
                        obj.LastName = (string)dr["LastName"];
                        obj.UserGroupName = (string)dr["userGroupName"];
                        obj.UserRoleName = (string)dr["userRoleName"];
                        obj.CompanyGroupName = (string)dr["CompanyGroupName"];
                        lstObjects.Add(obj);
                    }
                    //result.Data = lstObjects;
                }
            }
            return lstObjects;

        }
    }
   
}
