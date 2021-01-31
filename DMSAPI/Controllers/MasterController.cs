using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DMSAPI.Models;
using DMSAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        DbConnectionClass  _dbConnection;
        //IHttpContextAccessor accessor;
        // private readonly IGroupRepository<tblUserGroup> _IGroupRepository;
        private readonly DMSDbContext _dMSDbContext;
        public MasterController( IOptions<DbConnectionClass> dbConnection,DMSDbContext dMSDbContext)
        {
         //   this._IGroupRepository = groupRepository;
            _dbConnection = dbConnection.Value;
            _dMSDbContext=dMSDbContext;
          
        }

        public  string DbConnectionString()
        {
                string DbName, DbServerName, DbServerUser, DbServerPassword;
                DbServerName =_dbConnection.DBServerName;
                DbServerUser = _dbConnection.DBServerUser;
                DbServerPassword = _dbConnection.DBServerPassword;          
            if (HttpContext.Session.GetString("Database") != null)
            {
                DbName = HttpContext.Session.GetString("Database");

            }
            else
            {
                DbName = "";
            }
            string conString = "Data Source=" + DbServerName + ";Initial Catalog=" + DbName + ";User ID=" + DbServerUser + ";Password=" + DbServerPassword;
            return conString;
           

        }

      //  [Route("UserList")]

            public class Product
            {
                public string ProductDesc { get; set; }
            }

       // [Route("GroupList")]
        //public async Task<ActionResult<IEnumerable<tblUserGroup>>> GetAllGroup()
        //{

            //HttpContext.Session.SetString("Database", "IMSDatabase");
            //List<Product> lstObjects = new List<Product>();
            //string query = @"select ProductDesc from product";
          
            //using (SqlConnection con = new SqlConnection())
            //{
            //    using (SqlCommand cmd = con.CreateCommand())
            //    {
            //        con.Open();
            //        cmd.CommandTimeout = con.ConnectionTimeout;
            //        cmd.CommandType = System.Data.CommandType.Text;
            //        cmd.CommandText = query;
            //        SqlDataReader dr = cmd.ExecuteReader();
                  
            //        Product obj = null;
            //        while (dr.Read())
            //        {
            //            obj = new Product();
            //            obj.ProductDesc = dr["ProductDesc"].ToString();
                        
            //            lstObjects.Add(obj);
            //        }
            //      //  lstObjects;


            //    }
               
            //}


          
           // return await _IGroupRepository.GetAll();

            //return await _IGroupRepository.GetAll();

            // return await _efCoreRepositoryProduct.GetAllProduct();
      //  }
        
     

    }
}