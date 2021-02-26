using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DMSAPI.BusinessLogic.Customer;
using DMSAPI.BusinessLogic.Database;
using DMSAPI.Models;
using DMSAPI.Services;
using DMSAPI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IWebHostEnvironment _env;
        private readonly DMSDbContext _Context;
        private readonly ICustomerRepository<tblCustomer> _customerRepository;
        private readonly CustomerBL _customerBL;
        public CustomerController(ICustomerRepository<tblCustomer> customerRepository, DMSDbContext _Context, CustomerBL customerBL, IWebHostEnvironment env)
        {
            this._customerRepository = customerRepository;
            this._Context = _Context;
            this._customerBL = customerBL;
            _env = env;

        }


        [HttpGet]
        [Route("SetDatabaseName/{DatabaseName}")]
        public async Task<IActionResult> SetDatabaseName(string DatabaseName)
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.SetDatabseName(DatabaseName));
                await Task.WhenAll(t1);
                var data = new
                {
                    // Data = await t1,

                };
                return Ok(t1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("CustomerList")]
        public async Task<IActionResult> GetCustomerList()
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.GetCustomerList());
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


        [HttpPost]
        [Route("SaveCustomerData")]
        public async Task<IActionResult> CustomerSave([FromBody] tblCustomer Customer)
        {
            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string FirstName = null;
                FirstName = new String(Path.GetFileNameWithoutExtension(Customer.FirstName).Take(10).ToArray()).Replace(" ", "-");

                //   string patht = this._env.ContentRootPath + "\\"+ Customer.DatabaseName + "\\" + FirstName;

                string patht = this._env.ContentRootPath + "\\RK15\\" + FirstName;


                if (!Directory.Exists(patht))
                    Directory.CreateDirectory(patht);

                var t1 = Task.Run(() => _customerBL.CreateCustomer(Customer));
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,

                };

                return Ok(Customer);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        [Route("CustomerDocumentSaveData")]
        public async Task<IActionResult> CustomerDocumentSave([FromForm]tblCustomerDocument document)
        {

            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<CustomerViewModel> lst = _customerBL.GetCustomerName(document.CustomerId);
                string CustomerName = lst[0].CustomerName;
                var t1 = Task.Run(() => _customerBL.CreateCustomerDocument(document,CustomerName));
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };

                string path = this._env.WebRootPath + "\\RK15\\" + CustomerName;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (document.PhotoCopyOfLalpurjaFile != null)
                {
                    using (Stream fileStream = new FileStream(Path.Combine(path, document.PhotoCopyOfLalpurjaFile.FileName), FileMode.Create))
                    {
                        await document.PhotoCopyOfLalpurjaFile.CopyToAsync(fileStream);
                    }
                }
                if (document.TaxClearanceFile.FileName !="")
                {
                    using (Stream fileStream = new FileStream(Path.Combine(path, document.TaxClearanceFile.FileName ), FileMode.Create))
                    {
                        await document.TaxClearanceFile.CopyToAsync(fileStream);
                    }
                }
                if (document.CitizenshipDocFile != null)
                {
                    using (Stream fileStream = new FileStream(Path.Combine(path, document.CitizenshipDocFile.FileName ), FileMode.Create))
                    {
                        await document.CitizenshipDocFile.CopyToAsync(fileStream);
                    }
                }
                if (document.NaapiNaskaDocFile != null)
                {
                    using (Stream fileStream = new FileStream(Path.Combine(path, document.NaapiNaskaDocFile.FileName), FileMode.Create))
                    {
                        await document.NaapiNaskaDocFile.CopyToAsync(fileStream);
                    }
                }

                if (document.HouseDesignMapDocFile != null)
                {
                    using (Stream fileStream = new FileStream(Path.Combine(path, document.HouseDesignMapDocFile.FileName ), FileMode.Create))
                    {
                        await document.HouseDesignMapDocFile.CopyToAsync(fileStream);
                    }
                }


                return Ok(document);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [Route("GetCustomerDocByCustomerID/{customerId}")]
        public async Task<IActionResult> GetCustomerDocByCustomerID(int customerId)
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.GetCustomerDocByCustomerID(customerId));
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
        [Route("DownloadFile/{fileName}")]
        public async Task<FileStream> DownloadFile(string fileName)
        {
            ;
          //  List<CustomerViewModel> lst = _customerBL.GetCustomerName(document.CustomerId);
           // string CustomerName = lst[0].CustomerName;
           
            //here inject database code in rk15 dyanmically.
            string path = this._env.WebRootPath + "\\RK15\\" + "Demo";

           
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            currentDirectory =  path;
            var file  =  Path.Combine(Path.Combine(currentDirectory), fileName);

            return new FileStream(file, FileMode.Open, FileAccess.Read);
        }

        public class GetUploadUrl
        {
            public string UrlPath { get; set; }
        }

        [HttpGet]
        [Route("UploadFileView/{fileName}")]
        public async Task<GetUploadUrl> UploadFileView(string fileName)
        {
            GetUploadUrl getUploadUrl = new GetUploadUrl();
            //  List<CustomerViewModel> lst = _customerBL.GetCustomerName(document.CustomerId);
            // string CustomerName = lst[0].CustomerName;


            string path = this._env.WebRootPath + "\\RK15\\" + "Demo";
            string  currentDirectory = path;

            var file = Path.Combine(Path.Combine(currentDirectory), fileName);

            string urlPath =  "RK15/" + "Santosh" + "/" + fileName;
             getUploadUrl.UrlPath = urlPath;

            return getUploadUrl;
        }

        [HttpGet]
        [Route("Documentright")]
        public async Task<IActionResult> Getdocumentright()
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.Getdocumentright());
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };
                return Ok(t1.Result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("saveDocumentRight")]
        public async Task<IActionResult> saveDocumentRight(List<tbldocumentright> tbldocumentright)
        {
            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            
                var t1 = Task.Run(() => _customerBL.saveDocumentRight(tbldocumentright));
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUpdateCustomerData/{Id}")]
        public async Task<IActionResult> GetUpdateCustomerData(int Id)
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.GetUpdateCustomerData(Id));
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
        [HttpPost]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] tblCustomer Customer)
        {
            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var t1 = Task.Run(() => _customerBL.UpdateCustomer(Customer));
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };
                return Ok(Customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetUpdateCustomerDocumentData/{Id}")]
        public async Task<IActionResult> GetUpdateCustomerDocumentData(int Id)
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.GetUpdateCustomerDocumentData(Id));
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

        [HttpDelete]
        [Route("DeleteCustomerData/{Id}")]
        public async Task<IActionResult> DeleteCustomerData(int Id)
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.DeleteCustomerData(Id));
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

        [HttpPost]
        [Route("SaveFormEntry")]
        public async Task<IActionResult> saveFormEntry([FromBody] tblFormEntry FormEntry )
        {
            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var t1 = Task.Run(() => _customerBL.saveFormEntry(FormEntry));
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };
                return Ok(FormEntry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetFormEntryList")]
        public async Task<IActionResult> GetFormEntryList()
        {
            try
            {
                var t1 = Task.Run(() => _customerBL.GetFormEntryList());
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };
                return Ok(t1.Result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("CustomerDocumentSaveDataUdf")]
        public async Task<IActionResult> CreateCustomerDocumentUdf([FromForm] CustomerDocumentUdf customerDocumentUdf)
        {

            ResultType resultType = new ResultType();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                

                List<CustomerViewModel> lst = _customerBL.GetCustomerName(customerDocumentUdf.CustomerId);
                string CustomerName = lst[0].CustomerName;
                var t1 = Task.Run(() => _customerBL.CreateCustomerDocumentUdf(customerDocumentUdf));
                await Task.WhenAll(t1);
                var data = new
                {
                    Data = await t1,
                };
                string path = this._env.WebRootPath + "\\RK15\\" + CustomerName;
                foreach (var item in customerDocumentUdf.DocumentFile)
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    if (item.FileName != null)
                    {
                        using (Stream fileStream = new FileStream(Path.Combine(path, item.FileName), FileMode.Create))
                        {
                            await item.CopyToAsync(fileStream);
                        }
                    }
                }
                return Ok(customerDocumentUdf);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //rahul uptdae

        }
    }
}