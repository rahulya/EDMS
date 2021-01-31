using DMSAPI.Models;
using DMSAPI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.SqlGroupRepository
{
    public class SqlUserRepository<T> : IUserRepository<T>where T : class
    {
        private readonly DMSDbContext _Context;
        public SqlUserRepository(DMSDbContext dMSDbContext)
        {
            this._Context = dMSDbContext;
        }
        public  async Task<T> Add(T entity)
        {
            try
            {
                _Context.Set<T>().Add(entity);
                await _Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<T>> GetAll()
        {
            return await _Context.Set<T>().FromSqlRaw<T>(" exec sp_GetUserList").ToListAsync();
        }

        public async Task<List<T>> GetUserGroupAll()
        {
            return await _Context.Set<T>().FromSqlRaw<T>(" exec sp_GetUserGroupList").ToListAsync();
        }

        public Task<T> Login(T entity)
        {
            throw new NotImplementedException();
        }


    

        public Task<T> SaveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Login> LoginNew(Login login)
        {
            if (_Context != null)
            {

                var password= await (from tu in _Context.tblUser where tu.Password== login.Password
                              select new Login
                              {
                                
                                  Password = tu.Password

                              }).FirstOrDefaultAsync();
                if (password==null)
                {
                    return password;
                }
                var UserName = await (from tu in _Context.tblUser
                                      where tu.UserName == login.UserName
                                      select new Login
                                      {

                                          UserName = tu.UserName

                                      }).FirstOrDefaultAsync();
                if (UserName == null)
                {
                    return UserName;
                }
                var compnayCode = await (from tu in _Context.tblUser join c in _Context.tblCompanyGroup on tu.CompanyGroupID equals c.Id
                                      where c.GroupCode == login.CompanyCode
                                      select new Login
                                      {

                                          CompanyCode = c.GroupCode

                                      }).FirstOrDefaultAsync();
                if (compnayCode == null)
                {
                    return compnayCode;
                }
                if (password !=null && UserName !=null && compnayCode !=null)
                {
                    return await (from tu in _Context.tblUser 
                    join r in _Context.tblUserRole on tu.UserRoleID equals r.id 
                    join c in _Context.tblCompanyGroup on tu.CompanyGroupID equals c.Id
                    where tu.UserName==login.UserName
                    select new Login
                    {
                                      
                        UserName = tu.UserName,
                        Password = tu.Password,
                        CompanyCode = c.GroupCode,
                        UserRoleId = tu.UserRoleID,
                        UserRoleName=r.Name,
                        CompanyGroupId=tu.CompanyGroupID,
                        UserId=tu.Id

                    }).FirstOrDefaultAsync();
                }
            }

            return null;
        }

           // return null;

            //return await (from tu in _Context.tblUser
            //                  join g in _Context.tblCompanyGroup on tu.CompanyGroupID equals g.Id
            //                  where tu.UserName == login.UserId && tu.Password == login.Password && g.GroupCode == login.CompanyCode
            //                  select new Login
            //                  {
            //                      CompanyCode = g.GroupCode,
            //                      UserId = tu.UserName,
            //                      Password = tu.Password

            //                  }).FirstOrDefaultAsync();


            // }
            // return null;
       // }

        public async Task<List<tblUserGroup>> getUserGroupListForAutocomplete(string proGroDesc)
        {
            if (_Context != null && proGroDesc == "AllProductGroup")
            {

                return await _Context.Set<tblUserGroup>().ToListAsync();
            }
            else
            {
                return await (from m in _Context.tblUserGroup
                              where m.Name.Contains(proGroDesc)
                              select m).ToListAsync();
            }

            // return null;
        }
        public async Task<List<tblCompanyGroup>> getCompGrupListForAutocomplete(string proGroDesc)
        {
            if (_Context != null && proGroDesc == "AllCompanyGroup")
            {

                return await _Context.Set<tblCompanyGroup>().ToListAsync();
            }
            else
            {
                return await (from m in _Context.tblCompanyGroup
                              where m.CompanyGroupName.Contains(proGroDesc)
                              select m).ToListAsync();
            }

            // return null;
        }
    }
}
