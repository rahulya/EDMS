using DMSAPI.Models;
using DMSAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Services
{
    public interface ICompanyRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> SaveCompany(T entity);

        Task<CompanyDatabaseViewModel> getUserWiseCompanyDatabaseList(int userId,int CompanyGroupId);
        //Task<List<tblGroup>> getGroupListForAutocomplete(string prGrpDesc = "");
    }
}
