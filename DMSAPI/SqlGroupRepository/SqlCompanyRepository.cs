using DMSAPI.Models;
using DMSAPI.Services;
using DMSAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.SqlGroupRepository
{
    public class SqlCompanyRepository<T> : ICompanyRepository<T> where T : class
    {
        private readonly DMSDbContext _Context;
        public SqlCompanyRepository(DMSDbContext dMSDbContext)
        {
            this._Context = dMSDbContext;
        }
        public async Task<List<T>> GetAll()
        {
            return await _Context.Set<T>().FromSqlRaw<T>(" exec sp_GetCompanyGroupList").ToListAsync();
        }

     
        public async Task<T> SaveCompany(T entity)
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
        public async Task<CompanyDatabaseViewModel> getUserWiseCompanyDatabaseList(int userId, int CompanyGroupId)
        {
            if (_Context != null)
            {
                return await(from ud in _Context.tblUser_TblDatabase

                             join cd in _Context.tblCompanyDatabase on ud.CompanyDatabaseId equals cd.Id
                             where cd.CompanyGroupId == userId && ud.UserId == CompanyGroupId

                             select new CompanyDatabaseViewModel
                             {
                                 tblUserDatabaseId=ud.Id,
                                 UserId=ud.UserId,
                                 companyDatabaseId=cd.Id,
                                 CompanyDatabaseCode=cd.CompanyDatabaseCode,
                                 CompanyGroupId=cd.CompanyGroupId,
                                 CompanyName=cd.CompanyName,
                                 StartDate=cd.StartDate,
                                 EndDate=cd.EndDate

                             }).FirstOrDefaultAsync();


            }
            return null;
        }
    }
}
