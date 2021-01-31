using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DMSAPI.BusinessLogic.User;
using DMSAPI.Models;
using DMSAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<tblUser> _IUserRepository;

        private readonly IUserRepository<tblUserGroup> _IUserRepositoryUserGroup;
        private readonly DMSDbContext _Context;
        private readonly UserBL _userBL;

        public UserController(IUserRepository<tblUser>  userRepository, DMSDbContext dMSDbContext, IUserRepository<tblUserGroup> IUserRepositoryUserGroup,UserBL userBL)
        {
            this._IUserRepository = userRepository;
            this._IUserRepositoryUserGroup = IUserRepositoryUserGroup;
            _Context = dMSDbContext;
            this._userBL = userBL;
        }
        //[HttpGet]
        //[Route("UserList")]
        //public async Task<ActionResult<IEnumerable<tblUser>>> GetAllUser()
        //{
        //    return await _IUserRepository.GetAll();

        //    // return await _efCoreRepositoryProduct.GetAllProduct();
        //}
        [HttpGet]
        [Route("UserList")]
        public async Task<IActionResult> getBranchMappedList()
        {
            try
            {
               // ResultType result = new ResultType();
                var t1 = Task.Run(() => _userBL.GetUserList());
                await Task.WhenAll(t1);
                
                var data = new 
                {
                    Data = await t1,
                  
                };
                return Ok(t1.Result);
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
        }




        [HttpGet]
        [Route("UserGroupList")]
        public async Task<ActionResult<IEnumerable<tblUserGroup>>> GetAllUserGroupAll()
        {
            return await _IUserRepositoryUserGroup.GetUserGroupAll();

        }

        [HttpPost]
        [Route("SaveUserGroupData")]
        public async Task<IActionResult> SaveUserData([FromBody] tblUserGroup userGroup)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_IUserRepository != null)
                {
                    await _IUserRepositoryUserGroup.Add(userGroup);
                }
                return CreatedAtAction("GetAllUserGroupAll", new { id = userGroup.Id }, userGroup);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        [HttpPost]
        [Route("SaveUserData")]
        public async Task<IActionResult> SaveUserData([FromBody] tblUser user)
        {
            //user.Date = DateTime.Now;
             string newPassoword= Encrypt(user.Password);
            user.Password = newPassoword;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_IUserRepository != null)
                {
                    await _IUserRepository.Add(user);
                }
                return CreatedAtAction("getBranchMappedList", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("LoginIn")]

        public async Task<IActionResult> LoginIn([FromBody] Login login)
        {
           
            string newPassoword = Encrypt(login.Password);
            login.Password = newPassoword;
             try
                {
                    var getLoginDetail = await _IUserRepository.LoginNew(login);
                    if (getLoginDetail == null)
                    {
                        return Ok(getLoginDetail);
                    }
                    login.UserRoleId = getLoginDetail.UserRoleId;
                    login.UserRoleName = getLoginDetail.UserRoleName;
                    login.CompanyGroupId = getLoginDetail.CompanyGroupId;
                    login.UserId = getLoginDetail.UserId;
                   

                var aa = _Context.tblUser_TblDatabase.Where(x => x.UserId == login.UserId).FirstOrDefault();
                if (aa==null)
                {
                    login.HasCompany =0;
                }
                else
                {
                    login.HasCompany = 1;
                }
              
                return CreatedAtAction("getBranchMappedList", new { id = login }, login);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            
           
        }

        [HttpGet]
        [Route("GetUserGroup/{proGroDesc}")]
        public async Task<IActionResult> GetProductGroup(string proGroDesc)
        {
            try
            {
                var userGroupList = await _IUserRepositoryUserGroup.getUserGroupListForAutocomplete(proGroDesc);
                if (userGroupList == null)
                {
                    return NotFound();
                }

                return Ok(userGroupList);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("GetCompanyGroup/{proGroDesc}")]
        public async Task<IActionResult> getCompGrupListForAutocomplete(string proGroDesc)
        {
            try
            {
                var companyGroupList = await _IUserRepositoryUserGroup.getCompGrupListForAutocomplete(proGroDesc);
                if (companyGroupList == null)
                {
                    return NotFound();
                }

                return Ok(companyGroupList);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}