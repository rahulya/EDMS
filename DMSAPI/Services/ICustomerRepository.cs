using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Services
{
    public interface ICustomerRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> SaveCustoemr(T entity);
    }
}
