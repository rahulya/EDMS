using DMSAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic
{

    //public class DbConnection
    //{
    //    public string DBServerName { get; set; }
    //    public string DBServerUser { get; set; }
    //    public string DBServerPassword { get; set; }

    //}
    public class  ConnectionString      
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        public ConnectionString(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
        }

        public string DbConnectionString()
        {
            var httpContext = accessor.HttpContext;
            string DbName, DbServerName, DbServerUser, DbServerPassword;
            DbServerName = _dbConnection.DBServerName;
            DbServerUser = _dbConnection.DBServerUser;
            DbServerPassword = _dbConnection.DBServerPassword;
            DbName=_dbConnection.Database;
            if (httpContext.Session.GetString("Database") != null && DbName != "")
            {
                DbName = httpContext.Session.GetString("Database");
            }
            else
            {
                //DbName = "";
            }
            string conString = "Data Source=" + DbServerName + ";Initial Catalog=" + DbName + ";User ID=" + DbServerUser + ";Password=" + DbServerPassword;
            return conString;


        }
    }
    

}
