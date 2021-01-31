using DMSAPI.BusinessLogic.Database;
using DMSAPI.Models;
using DMSAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Company
{
    public class CompanyBL
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        ConnectionString _connn;
        CompanyDL _dll;
        public CompanyBL(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor, ConnectionString connectionString, CompanyDL companyDL)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
            this._connn = connectionString;
            this._dll = companyDL;
        }
        public string CreateDatabase(string DatabaseName)
        {
            return _dll.CreateDatabase(DatabaseName);
        }
        public List<tblCompany_DatabaseViewModel> GetCompanyDatabaseList(ParameterOne parameterOne)
        {
            return _dll.GetCompanyDatabaseList(parameterOne);
        }
    }
}
