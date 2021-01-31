using DMSAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Services
{
    public interface IUserRepository<T>where T:class
    {
        Task<List<T>> GetAll();

        Task<List<T>> GetUserGroupAll();
        Task<T> Add(T entity);
       
        Task<T> SaveAsync(T entity);
        Task<T> Login(T entity);

        Task<List<tblUserGroup>> getUserGroupListForAutocomplete(string prGrpDesc = "");

        Task<List<tblCompanyGroup>> getCompGrupListForAutocomplete(string prGrpDesc = "");
        Task<Login> LoginNew(Login login);


        //string LoginNew(string CompanyCode, string UserID, string Password);
    }
}
