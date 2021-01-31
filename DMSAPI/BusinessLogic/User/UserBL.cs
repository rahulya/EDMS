using DMSAPI.BusinessLogic.Database;
using DMSAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.User
{

   
    public class UserBL
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        ConnectionString _connn;
        UserDL _dll;
        public UserBL(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor, ConnectionString connectionString,UserDL userDL)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
            this._connn = connectionString;
            this._dll = userDL;
        }
        internal List<tbl_User> GetUserList()
        {
            return _dll.GetUserList();
        }
    }
}
