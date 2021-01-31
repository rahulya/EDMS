using DMSAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Services
{
    public interface IGroupRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<T> SaveAsync(T entity);

        Task<List<tblUserGroup>> getGroupListForAutocomplete(string prGrpDesc = "");

    }
}
