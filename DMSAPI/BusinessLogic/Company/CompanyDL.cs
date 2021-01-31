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
using System.Text;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Company
{
    public class CompanyDL
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        ConnectionString _connn;

        public CompanyDL(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor, ConnectionString connectionString)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
            this._connn = connectionString;



        }

        internal List<tblCompany_DatabaseViewModel> GetCompanyDatabaseList(ParameterOne parameterOne)
        {
            List<tblCompany_DatabaseViewModel> lstObjects = new List<tblCompany_DatabaseViewModel>();
            var httpContext = accessor.HttpContext;
            httpContext.Session.SetString("Database", "SystemDatabase");
            using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandText = "sp_GetUserWiseCompanyDb";
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = parameterOne.UserId;
                    cmd.Parameters.Add("@companyGroupId", SqlDbType.Int).Value = parameterOne.CompanyGroupId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();

                    tblCompany_DatabaseViewModel obj = null;
                    while (dr.Read())
                    {
                        obj = new tblCompany_DatabaseViewModel();
                        obj.tblUserDatabaseId = Convert.ToInt32(dr["tblUserDatabaseId"]);
                        obj.UserId = Convert.ToInt32(dr["UserId"]);
                        obj.companyDatabaseId = Convert.ToInt32(dr["companyDatabaseId"]);
                        obj.CompanyDatabaseCode = (string)dr["CompanyDatabaseCode"];
                        obj.CompanyGroupId =Convert.ToInt32( dr["CompanyGroupId"]);
                        obj.CompanyName = (string)dr["CompanyName"];
                        obj.StartDate = (string)dr["StartDate"];
                        obj.EndDate = (string)dr["EndDate"];
                 
                        lstObjects.Add(obj);
                    }
                    //result.Data = lstObjects;
                }
            }
            return lstObjects;
        }
        internal string CreateDatabase(string DatabaseName)
        {
            try
            {

                ResultType resultType = new ResultType();
                var httpContext = accessor.HttpContext;
                httpContext.Session.SetString("Database", "SystemDatabase");

                string strsql = "Create Database " + DatabaseName;

                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = strsql;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        resultType.Message = "Create Sucessfully";
                        createTable(DatabaseName);
                        //result.Data = lstObjects;
                    }
                }
                return resultType.Message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
        public void createTable(string DatabaseName)
        {
            var httpContext = accessor.HttpContext;
            httpContext.Session.SetString("Database", "'"+ DatabaseName + "'");
           // StringBuilder strSql = new StringBuilder();

            string SqlQry = @"create table tblCustomer(
                    Id  INT NOT NULL IDENTITY(1,1) PRIMARY KEY,                  
                    FirstName varchar(50 ) not null,
                    Middle varchar(50),
                    LastName varchar(50) ,
                    KittaNo varchar(100),
                    CitizenshipNo int not null,
                    Address varchar(100) not null,
                    PhoneNo varchar(10),
                    FatherName varchar(50) not null,
                    GrandFatherName varchar(50) not null,
                    EmailAddress varchar(50),
                    MobileNo varchar(20) ,    
                    CompanyId int ,
                    UserId int,
                    IsFileSave bit
                    )
                    create table tblCustomerDocument(
                    customerDocumentId INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
                    [PhotocopyOfLalpurjaDoc] [varchar](max) NULL,
                    [PhotocopyOfLalpurjaCode] [varchar](20) NULL,
                    [TaxClearanceDoc] [varchar](max) NULL,
                    [TaxClearanceCode] [varchar](20) NULL,
                    [CitizenshipDoc] [varchar](max) NULL,
                    [CitizenshipCode] [varchar](20) NULL,
                    [NaapiNaksaWithKittaNoDoc] [varchar](max) NULL,
                    [NaapiNaksaWithKittaNo] [varchar](50) NULL,
                    [HouseDesginMapDoc] [varchar](max) NULL,
                    [HouseDesginMapCode] [varchar](max) NULL,
                    [IssueTemporayCertification] [bit] NULL,
                    [PermamentCertification] [bit] NULL,
                    [CompletionCertification] [bit] NULL,
                    [ApprovalOfWardChair] [bit] NULL,	
                    [ApprovalOfWardChairLackOfAccessOfRoad] [bit] NULL,
                     CustomerId int FOREIGN KEY REFERENCES tblCustomer(Id))

                        CREATE TABLE tblDocumentFormRight(
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [ControlName] [nvarchar](50) NULL,
	                    [ControlVisible] [bit] NULL,
	                    [Mandatory] [bit] NULL
	                    )
                go
                CREATE proc [dbo].[Sp_CreateCustomerDocument]
                (
                @PhotocopyOfLalpurjaDoc nvarchar(max),
                @PhotocopyOfLalpurjaCode nvarchar(20),
                @TaxClearanceDoc nvarchar(max),
                @TaxClearanceCode nvarchar(20),
                @CitizenshipDoc nvarchar(max),
                @CitizenshipCode nvarchar(20),
                @NaapiNaksaWithKittaNoDoc nvarchar(max),
                @NaapiNaksaWithKittaNo nvarchar(20),
                @HouseDesginMapDoc nvarchar(max),
                @HouseDesginMapCode nvarchar(20),
                @IssueTemporayCertification bit,
                @PermamentCertification bit,
                @CompletionCertification bit,
                @ApprovalOfWardChair bit,
                @ApprovalOfWardChairLackOfAccessOfRoad bit,
                @CustomerId int,
                @IsFileSave bit,
                @ActionType NVARCHAR(20) = ''
                )
                as 
                begin
                  IF @ActionType = 'Insert'  
                  begin
                  insert into tblCustomerDocument(PhotocopyOfLalpurjaDoc,PhotocopyOfLalpurjaCode,TaxClearanceDoc,TaxClearanceCode,CitizenshipDoc,CitizenshipCode,NaapiNaksaWithKittaNoDoc
                  ,NaapiNaksaWithKittaNo,HouseDesginMapDoc,HouseDesginMapCode,IssueTemporayCertification,PermamentCertification,CompletionCertification,ApprovalOfWardChair,
                  ApprovalOfWardChairLackOfAccessOfRoad,CustomerId)  VALUES (@PhotocopyOfLalpurjaDoc,@PhotocopyOfLalpurjaCode,@TaxClearanceDoc,@TaxClearanceCode,@CitizenshipDoc,@CitizenshipCode,@NaapiNaksaWithKittaNoDoc
                  ,@NaapiNaksaWithKittaNo,@HouseDesginMapDoc,@HouseDesginMapCode,@IssueTemporayCertification,@PermamentCertification,@CompletionCertification,@ApprovalOfWardChair,
                  @ApprovalOfWardChairLackOfAccessOfRoad,@CustomerId)

                  update tblCustomer set IsfileSave=@IsFileSave where Id=@CustomerId
                  end

                end
                GO
                create proc [dbo].[sp_getCustomerDocumentByCustomerID]
                (@customerid int)
                as
                begin
                select customerDocumentId,PhotocopyOfLalpurjaDoc,TaxClearanceDoc,CitizenshipDoc,NaapiNaksaWithKittaNoDoc,HouseDesginMapDoc from tblCustomerDocument where CustomerId=@customerid
                end
                GO
                CREATE PROCEDURE [dbo].[sp_GetCustomerList]
                AS
                select c.Id as customerId,c.FirstName,c.middle,c.LastName,c.KittaNo,c.CitizenshipNo,c.CompanyId,c.UserId,c.IsFileSave  from tblCustomer c
                GO ";       
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connn.DbConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandText = SqlQry;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                       
                      
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            


        }
    }
}
