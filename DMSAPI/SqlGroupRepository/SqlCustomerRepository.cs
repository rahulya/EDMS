using DMSAPI.Models;
using DMSAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.SqlGroupRepository
{
    public class SqlCustomerRepository<T> : ICustomerRepository<T> where T : class
    {
        private readonly DMSDbContext _Context;
        public  SqlCustomerRepository(DMSDbContext dMSDbContext)
        {
            this._Context = dMSDbContext;
        }
        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> SaveCustoemr(T entity)
        {
            try
            {

                _Context.Set<T>().Add(entity);
                await _Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
