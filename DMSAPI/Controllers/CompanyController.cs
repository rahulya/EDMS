using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DMSAPI.BusinessLogic.Company;
using DMSAPI.Models;
using DMSAPI.Services;
using DMSAPI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository<tblCompanyGroup> _ICompanyRepository;
        private readonly ICompanyRepository<tblCompanyDatabase> _ICompanyDatabaseRepository;
        private IWebHostEnvironment _env;
        private readonly CompanyBL _companyBL;
        public CompanyController(ICompanyRepository<tblCompanyGroup> companyRepository, CompanyBL companyBL, ICompanyRepository<tblCompanyDatabase> ICompanyDatabaseRepository, IWebHostEnvironment env)
        {
            this._ICompanyRepository = companyRepository;
            this._companyBL = companyBL;
            this._ICompanyDatabaseRepository = ICompanyDatabaseRepository;
            _env = env;
        }
        [HttpGet]
        [Route("CompanyGroupList")]
        public async Task<ActionResult<IEnumerable<tblCompanyGroup>>> GetAllCompany()
        {
            return await _ICompanyRepository.GetAll();

            //  return await _efCoreRepositoryProduct.GetAllProduct();
        }
        [HttpPost]
        [Route("SaveCompanyGroup")]
        public async Task<IActionResult> SaveCompany([FromBody] tblCompanyGroup companyGroup)
        {
            companyGroup.GroupCreateDate = DateTime.Now;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_ICompanyRepository != null)
                {
                    await _ICompanyRepository.SaveCompany(companyGroup);
                }
                return CreatedAtAction("GetAllCompany", new { id = companyGroup.Id }, companyGroup);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("CreateCompanyDatabase")]
        public async Task<IActionResult> CreateCompany([FromBody] tblCompanyDatabase tblCompanyDatabase)
        {
            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_ICompanyRepository != null)
                {
                    await _ICompanyDatabaseRepository.SaveCompany(tblCompanyDatabase);
                    string IsSuccess= _companyBL.CreateDatabase(tblCompanyDatabase.CompanyDatabaseCode);
                    if (IsSuccess !=null)
                    {
                        string CompanyDatabaseCode = new String(Path.GetFileNameWithoutExtension(tblCompanyDatabase.CompanyDatabaseCode).Take(10).ToArray()).Replace(" ", "-");
                        string patht = this._env.ContentRootPath + "\\" + CompanyDatabaseCode;

                        if (!Directory.Exists(patht))
                            Directory.CreateDirectory(patht);

                    }

                    
                }
                return CreatedAtAction("GetAllCompany", new { id = tblCompanyDatabase.Id }, tblCompanyDatabase);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        [Route("UserWiseComDbList")]
        public async Task<IActionResult> EditProduct([FromBody] ParameterOne parameterOne)
        {
            try
            {
                // ResultType result = new ResultType();
                var t1 = Task.Run(() => _companyBL.GetCompanyDatabaseList(parameterOne));
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

    }
}