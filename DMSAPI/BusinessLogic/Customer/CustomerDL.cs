
using DMSAPI.BusinessLogic.Database;
using DMSAPI.Models;
using DMSAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Customer
{
    public class CustomerDL
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        ConnectionString _connn;

        public CustomerDL(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor, ConnectionString connectionString)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
            this._connn = connectionString;
        }

        internal List<tblCustomer> GetCustomerList()
        {
            List<tblCustomer> lstObjects = new List<tblCustomer>();
            var httpContext = accessor.HttpContext;
            ResultType result = new ResultType();
            // httpContext.Session.SetString("Database", "SystemDatabase");
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = "sp_GetCustomerList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();

                    tblCustomer obj = null;
                    while (dr.Read())
                    {
                        obj = new tblCustomer();
                        obj.Id = Convert.ToInt32(dr["customerId"]);
                        obj.FirstName = (string)dr["FirstName"];
                        obj.Middle = (string)dr["middle"];
                        obj.LastName = (string)dr["LastName"];
                        obj.KittaNo = (string)dr["KittaNo"];
                        obj.CitizenshipNo =(string)dr["CitizenshipNo"];
                        obj.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                        obj.UserId = Convert.ToInt32(dr["UserId"]);
                        obj.IsFileSave =Convert.ToBoolean(dr["IsFileSave"]);
                        lstObjects.Add(obj);
                    }
                   
                }
            }
            return lstObjects;

        }
        internal string CreateCustomer(tblCustomer customer)
        {
            try
            {

                ResultType resultType = new ResultType();
                var httpContext = accessor.HttpContext;
                httpContext.Session.SetString("Database", "" + customer.DatabaseName + "");

                string strsql = @"insert into tblCustomer (FirstName,Middle,LastName,KittaNo,CitizenshipNo,Address,PhoneNo,FatherName,GrandFatherName,EmailAddress,MobileNo,CompanyId,UserId,IsFileSave)values
                                (@FirstName, @Middle, @LastName, @KittaNo, @CitizenshipNo, @Address, @PhoneNo, @FatherName, @GrandFatherName, @EmailAddress, @MobileNo, @CompanyId, @UserId,@IsFileSave)";

                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = strsql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.FirstName;
                        cmd.Parameters.Add("@Middle", SqlDbType.VarChar).Value = customer.Middle;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.LastName;
                        cmd.Parameters.Add("@KittaNo", SqlDbType.VarChar).Value = customer.KittaNo;
                        cmd.Parameters.Add("@CitizenshipNo", SqlDbType.VarChar).Value = customer.CitizenshipNo;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = customer.Address;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = customer.PhoneNo;
                        cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = customer.FatherName;
                        cmd.Parameters.Add("@GrandFatherName", SqlDbType.VarChar).Value = customer.GrandFatherName;
                        cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = customer.EmailAddress;
                        cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customer.MobileNo;
                        cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = customer.CompanyId;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = customer.UserId;
                        cmd.Parameters.Add("@IsFileSave", SqlDbType.Int).Value = customer.IsFileSave;
                        cmd.ExecuteNonQuery();
                        resultType.Message = "Create Sucessfully";

                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        internal void SetDatabseName(string databaseName)
        {

            var httpContext = accessor.HttpContext;
            _dbConnection.Database = databaseName;
            httpContext.Session.SetString("Database", "" + databaseName + "");


        }
        internal List<CustomerViewModel> GetCustomerName(int CustomerId)
        {
            List<CustomerViewModel> lstObjects = new List<CustomerViewModel>();
            ResultType result = new ResultType();
            string query = @"select FirstName CustomerName from tblcustomer where id='" + CustomerId + "'";
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    CustomerViewModel obj = null;
                    while (dr.Read())
                    {
                        obj = new CustomerViewModel();
                        obj.CustomerName = (string)dr["CustomerName"];
                        lstObjects.Add(obj);
                    }                    
                }
            }
            return lstObjects;
        }
        internal string CreateCustomerDocument(tblCustomerDocument  tblCustomerDocument)
        {
            try
            {
                ResultType resultType = new ResultType();           
                string strsql = "Sp_CreateCustomerDocument";
                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = strsql;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ActionType", SqlDbType.VarChar).Value = tblCustomerDocument.ActionType;
                        cmd.Parameters.Add("@PhotocopyOfLalpurjaDoc", SqlDbType.VarChar).Value = tblCustomerDocument.PhotocopyOfLalpurjaDoc;
                        cmd.Parameters.Add("@PhotocopyOfLalpurjaCode", SqlDbType.VarChar).Value = tblCustomerDocument.PhotocopyOfLalpurjaCode;
                        cmd.Parameters.Add("@TaxClearanceDoc", SqlDbType.VarChar).Value = tblCustomerDocument.TaxClearanceDoc;
                        cmd.Parameters.Add("@TaxClearanceCode", SqlDbType.VarChar).Value = tblCustomerDocument.TaxClearanceCode;
                        cmd.Parameters.Add("@CitizenshipDoc", SqlDbType.VarChar).Value = tblCustomerDocument.CitizenshipDoc;
                        cmd.Parameters.Add("@CitizenshipCode", SqlDbType.VarChar).Value = tblCustomerDocument.CitizenshipCode;
                        cmd.Parameters.Add("@NaapiNaksaWithKittaNoDoc", SqlDbType.VarChar).Value = tblCustomerDocument.NaapiNaksaWithKittaNoDoc;
                        cmd.Parameters.Add("@NaapiNaksaWithKittaNo", SqlDbType.VarChar).Value = tblCustomerDocument.NaapiNaksaWithKittaNo;
                        cmd.Parameters.Add("@HouseDesginMapDoc", SqlDbType.VarChar).Value = tblCustomerDocument.HouseDesginMapDoc == null ? "" : tblCustomerDocument.HouseDesginMapDoc;
                        cmd.Parameters.Add("@HouseDesginMapCode", SqlDbType.VarChar).Value = tblCustomerDocument.HouseDesginMapCode ==null?"" : tblCustomerDocument.HouseDesginMapCode;
                        cmd.Parameters.Add("@IssueTemporayCertification", SqlDbType.Bit).Value = tblCustomerDocument.IssueTemporayCertification;
                        cmd.Parameters.Add("@PermamentCertification", SqlDbType.Bit).Value = tblCustomerDocument.PermamentCertification;
                        cmd.Parameters.Add("@CompletionCertification", SqlDbType.Bit).Value = tblCustomerDocument.CompletionCertification;
                        cmd.Parameters.Add("@ApprovalOfWardChair", SqlDbType.Bit).Value = tblCustomerDocument.ApprovalOfWardChair;
                        cmd.Parameters.Add("@ApprovalOfWardChairLackOfAccessOfRoad", SqlDbType.Bit).Value = tblCustomerDocument.ApprovalOfWardChairLackOfAccessOfRoad;
                        cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value =Convert.ToInt32( tblCustomerDocument.CustomerId);
                        cmd.Parameters.Add("@IsFileSave", SqlDbType.Bit).Value = 1;
                        cmd.ExecuteNonQuery();
                        resultType.Message = "Create Sucessfully";

                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal List<tblCustomerDocumentViewModel> GetCustomerDocByCustomerID(int customerID)
        {
            try
            {
                List<tblCustomerDocumentViewModel> lstObjects = new List<tblCustomerDocumentViewModel>();
              //  var httpContext = accessor.HttpContext;
                ResultType result = new ResultType();
                // httpContext.Session.SetString("Database", "SystemDatabase");
                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = "sp_getCustomerDocumentByCustomerID";
                        cmd.Parameters.Add("@customerid", SqlDbType.Int).Value = Convert.ToInt32(customerID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();

                        tblCustomerDocumentViewModel obj = null;
                        while (dr.Read())
                        {
                            obj = new tblCustomerDocumentViewModel();
                            obj.CustomerDocumentId = Convert.ToInt32(dr["CustomerDocumentId"]);
                            obj.PhotocopyOfLalpurjaDoc = (string)dr["PhotocopyOfLalpurjaDoc"];
                            obj.TaxClearanceDoc = (string)dr["TaxClearanceDoc"];
                            obj.CitizenshipDoc = (string)dr["CitizenshipDoc"];
                            obj.NaapiNaksaWithKittaNoDoc = (string)dr["NaapiNaksaWithKittaNoDoc"];
                            obj.HouseDesginMapDoc = (string)dr["HouseDesginMapDoc"];                          
                            lstObjects.Add(obj);
                        }

                    }
                }
                return lstObjects;
            }
            catch (Exception ex)
            {

                throw ex;
            }          
        }
        internal List<tbldocumentright> Getdocumentright()
        {
            List<tbldocumentright> lstObjects = new List<tbldocumentright>();
            var httpContext = accessor.HttpContext;
            ResultType result = new ResultType();
            string query = @"select Id,ControlName,ControlVisible,Mandatory from tbldocumentformright";
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    tbldocumentright obj = null;
                    while (dr.Read())
                    {
                        obj = new tbldocumentright();
                        obj.Id =Convert.ToInt32( dr["Id"]);
                        obj.ControlName = (string)dr["ControlName"];
                        obj.ControlVisible = (Boolean)dr["ControlVisible"];
                        obj.Mandatory = (Boolean)dr["Mandatory"];                      
                        lstObjects.Add(obj);
                    }
                }
            }
            return lstObjects;
        }

        internal string saveDocumentRight(List<tbldocumentright> tbldocumentright)
        {
            try
            {
               
                ResultType resultType = new ResultType();

                string strsql = @"update tbldocumentformright set ControlVisible=@ControlVisible,Mandatory=@Mandatory where Id=@Id";
               
               
                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        conn.Open();
                        foreach (var item in tbldocumentright)
                        {
                          
                            cmd.CommandTimeout = conn.ConnectionTimeout;
                            cmd.CommandText = strsql;
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;
                            cmd.Parameters.Add("@ControlVisible", SqlDbType.Bit).Value = item.ControlVisible;
                            cmd.Parameters.Add("@Mandatory", SqlDbType.Bit).Value = item.Mandatory;

                            cmd.ExecuteNonQuery();
                        }                                        
                       
                        resultType.Message = "Create Sucessfully";

                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal List<tblCustomer> GetUpdateCustomerData(int Id)
        {
            List<tblCustomer> lstObjects = new List<tblCustomer>();
            var httpContext = accessor.HttpContext;
            ResultType result = new ResultType();
            // httpContext.Session.SetString("Database", "SystemDatabase");
            string query = @"select Id as CustomerId,FirstName,Middle,LastName,KittaNo,CitizenshipNo,Address,PhoneNo,FatherName,GrandFatherName,EmailAddress,MobileNo,CompanyId,
                            UserId,IsFileSave from tblCustomer where Id=@Id";
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value =Id;
                    SqlDataReader dr = cmd.ExecuteReader();

                    tblCustomer obj = null;
                    while (dr.Read())
                    {
                        obj = new tblCustomer();
                        obj.Id = Convert.ToInt32(dr["CustomerId"]);
                        obj.FirstName = (string)dr["FirstName"];
                        obj.Middle = (string)dr["Middle"];
                        obj.LastName = (string)dr["LastName"];
                        obj.KittaNo = (string)dr["KittaNo"];
                        obj.CitizenshipNo = (string)dr["CitizenshipNo"];
                        obj.Address = (string)dr["Address"];
                        obj.PhoneNo = (string)dr["PhoneNo"];
                        obj.FatherName = (string)dr["FatherName"];
                        obj.GrandFatherName = (string)dr["GrandFatherName"];
                        obj.EmailAddress = (string)dr["EmailAddress"];
                        obj.MobileNo = (string)dr["MobileNo"];
                        obj.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                        obj.UserId = Convert.ToInt32(dr["UserId"]);
                        obj.IsFileSave = Convert.ToBoolean(dr["IsFileSave"]);
                        lstObjects.Add(obj);
                    }
                }
            }
            return lstObjects;
        }


        internal string UpdateCustomer(tblCustomer customer)
        {
            try
            {

                ResultType resultType = new ResultType();
                var httpContext = accessor.HttpContext;
              //  httpContext.Session.SetString("Database", "" + customer.DatabaseName + "");

                string strsql = @"update tblcustomer set FirstName=@FirstName,Middle=@Middle,LastName=@LastName,KittaNo=@KittaNo,CitizenshipNo=@CitizenshipNo,
                        Address=@Address,PhoneNo=@PhoneNo,FatherName=@FatherName,GrandFatherName=@GrandFatherName,EmailAddress=@EmailAddress,MobileNo=@MobileNo where Id=@Id";

                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = strsql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = customer.Id;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.FirstName;
                        cmd.Parameters.Add("@Middle", SqlDbType.VarChar).Value = customer.Middle;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.LastName;
                        cmd.Parameters.Add("@KittaNo", SqlDbType.VarChar).Value = customer.KittaNo;
                        cmd.Parameters.Add("@CitizenshipNo", SqlDbType.VarChar).Value = customer.CitizenshipNo;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = customer.Address;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = customer.PhoneNo;
                        cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = customer.FatherName;
                        cmd.Parameters.Add("@GrandFatherName", SqlDbType.VarChar).Value = customer.GrandFatherName;
                        cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = customer.EmailAddress;
                        cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customer.MobileNo;
                      //  cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = customer.CompanyId;
                       // cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = customer.UserId;
                       // cmd.Parameters.Add("@IsFileSave", SqlDbType.Int).Value = customer.IsFileSave;
                        cmd.ExecuteNonQuery();
                        resultType.Message = "Update Sucessfully";
                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        internal List<tblCustomerDocument> GetUpdateCustomerDocumentData(int Id)
        {
            List<tblCustomerDocument> lstObjects = new List<tblCustomerDocument>();
            var httpContext = accessor.HttpContext;
            ResultType result = new ResultType();
            // httpContext.Session.SetString("Database", "SystemDatabase");
            string query = @"select customerDocumentId,PhotocopyOfLalpurjaDoc,TaxClearanceDoc,CitizenshipDoc,NaapiNaksaWithKittaNoDoc,HouseDesginMapDoc,IssueTemporayCertification
                    ,CompletionCertification,ApprovalOfWardChair,ApprovalOfWardChairLackOfAccessOfRoad,PermamentCertification from tblCustomerDocument where CustomerId=@Id";
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    SqlDataReader dr = cmd.ExecuteReader();

                    tblCustomerDocument obj = null;
                    while (dr.Read())
                    {
                        obj = new tblCustomerDocument();
                        obj.CustomerDocumentId = Convert.ToInt32(dr["customerDocumentId"]);
                        obj.PhotocopyOfLalpurjaDoc = (string)dr["PhotocopyOfLalpurjaDoc"];
                        obj.TaxClearanceDoc = (string)dr["TaxClearanceDoc"];
                        obj.CitizenshipDoc = (string)dr["CitizenshipDoc"];
                        obj.NaapiNaksaWithKittaNoDoc = (string)dr["NaapiNaksaWithKittaNoDoc"];
                        obj.HouseDesginMapDoc = (string)dr["HouseDesginMapDoc"];
                        obj.IssueTemporayCertification = Convert.ToBoolean(dr["IssueTemporayCertification"]);
                        obj.CompletionCertification = Convert.ToBoolean(dr["CompletionCertification"]);
                        obj.ApprovalOfWardChair = Convert.ToBoolean(dr["ApprovalOfWardChair"]);
                        obj.ApprovalOfWardChairLackOfAccessOfRoad = Convert.ToBoolean(dr["ApprovalOfWardChairLackOfAccessOfRoad"]);
                        obj.PermamentCertification = Convert.ToBoolean(dr["PermamentCertification"]);
                      
                        lstObjects.Add(obj);
                    }
                }
            }
            return lstObjects;
        }

        internal string DeleteCustomerData(int Id)
        {
            try
            {

                ResultType resultType = new ResultType();
                var httpContext = accessor.HttpContext;
                //  httpContext.Session.SetString("Database", "" + customer.DatabaseName + "");

                string strsql = @"delete from tblCustomerDocument where CustomerId=@Id
                                delete from tblCustomer where Id=@Id";

                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = strsql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                       
                        cmd.ExecuteNonQuery();
                        resultType.Message = "Delete Sucessfully";
                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal string saveFormEntry(tblFormEntry tblFormEntry)
        {
            try
            {

                ResultType resultType = new ResultType();

                string strsql = @"insert into tblFormEntry (Entrymodule,FieldName,FieldType,TotalWidth,Mandotaryopt,DateFormat)
                    values(@Entrymodule,@FieldName,@FieldType,@TotalWidth,@Mandotaryopt,@DateFormat)";


                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        conn.Open();                     
                            cmd.CommandTimeout = conn.ConnectionTimeout;
                            cmd.CommandText = strsql;
                            cmd.CommandType = CommandType.Text;                          
                            cmd.Parameters.Add("@Entrymodule", SqlDbType.VarChar).Value = tblFormEntry.Entrymodule;
                            cmd.Parameters.Add("@FieldName", SqlDbType.VarChar).Value = tblFormEntry.FieldName;
                            cmd.Parameters.Add("@FieldType", SqlDbType.VarChar).Value = tblFormEntry.FieldType;
                            cmd.Parameters.Add("@TotalWidth", SqlDbType.VarChar).Value = tblFormEntry.TotalWidth;
                            cmd.Parameters.Add("@Mandotaryopt", SqlDbType.Bit).Value = tblFormEntry.Mandotaryopt;
                            cmd.Parameters.Add("@DateFormat", SqlDbType.VarChar).Value = tblFormEntry.DateFormat ==null? "null": tblFormEntry.DateFormat;

                            cmd.ExecuteNonQuery();                    
                           resultType.Message = "Create Sucessfully";

                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal List<tblFormEntry> GetFormEntryList()
        {
            List<tblFormEntry> lstObjects = new List<tblFormEntry>();
            var httpContext = accessor.HttpContext;
            ResultType result = new ResultType();
            string query = @"select FormId,Entrymodule,FieldName,FieldType,TotalWidth,Mandotaryopt,isnull(DateFormat,'') DateFormat from tblFormEntry";
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    tblFormEntry obj = null;
                    while (dr.Read())
                    {
                        obj = new tblFormEntry();
                        obj.FormId = Convert.ToInt32(dr["FormId"]);
                        obj.Entrymodule = (string)dr["Entrymodule"];
                        obj.FieldName = (String)dr["FieldName"];
                        obj.FieldType = (String)dr["FieldType"];
                        obj.TotalWidth = (String)dr["TotalWidth"];
                        obj.Mandotaryopt = (Boolean)dr["Mandotaryopt"];
                        obj.DateFormat = (string)dr["DateFormat"];
                        lstObjects.Add(obj);
                    }
                }
            }
            return lstObjects;
        }

        internal string CreateCustomerDocumentUdf(CustomerDocumentUdf item)
        {
            try
            {
                ResultType resultType = new ResultType();
                string strsql = "Sp_CreateCustomerDocumentUdf";
                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                      

                        conn.Open();
                        int lenth = item.DocumentFile.Count;
                        for (int i = 0;i< item.DocumentFile.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandTimeout = conn.ConnectionTimeout;
                            cmd.CommandText = strsql;
                            cmd.CommandType = CommandType.StoredProcedure;                          
                            cmd.Parameters.Add("@FormId", SqlDbType.VarChar).Value = item.FormId[i];                         
                            cmd.Parameters.Add("@DocumentFileName", SqlDbType.VarChar).Value = item.DocumentFile[i].FileName;
                            cmd.Parameters.Add("@DocumentFileCode", SqlDbType.VarChar).Value = "100" + "_" + item.CustomerId;
                            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(item.CustomerId);
                            cmd.Parameters.Add("@ActionType", SqlDbType.VarChar).Value = item.ActionType;
                            cmd.Parameters.Add("@IsFileSave", SqlDbType.Bit).Value = 1;
                            cmd.ExecuteNonQuery();
                        }

                        //foreach (var itemm in item.DocumentFile)
                        //{
                        //    cmd.Parameters.Clear();
                        //    cmd.CommandTimeout = conn.ConnectionTimeout;
                        //    cmd.CommandText = strsql;
                        //    cmd.CommandType = CommandType.StoredProcedure;

                        //    foreach (var newitem in item.FormId)
                        //    {
                        //        cmd.Parameters.Add("@FormId", SqlDbType.VarChar).Value = newitem;
                        //    }
                          
                        //    cmd.Parameters.Add("@DocumentFileName", SqlDbType.VarChar).Value = itemm.FileName;
                        //    cmd.Parameters.Add("@DocumentFileCode", SqlDbType.VarChar).Value = "100" + "_" + item.CustomerId;
                        //    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(item.CustomerId);

                        //    cmd.Parameters.Add("@ActionType", SqlDbType.VarChar).Value = item.ActionType;
                        //    cmd.Parameters.Add("@IsFileSave", SqlDbType.Bit).Value = 1;
                        //    cmd.ExecuteNonQuery();
                        //}
                       
                                                                  
                        resultType.Message = "Create Sucessfully";

                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
