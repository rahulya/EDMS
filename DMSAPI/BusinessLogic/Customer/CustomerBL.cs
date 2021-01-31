using DMSAPI.BusinessLogic.Database;
using DMSAPI.Models;
using DMSAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Customer
{
    public class CustomerBL
    {
        DbConnectionClass _dbConnection;
        IHttpContextAccessor accessor;
        ConnectionString _connn;
        CustomerDL _dll;
        public CustomerBL(IOptions<DbConnectionClass> dbConnection, IHttpContextAccessor accessor, ConnectionString connectionString, CustomerDL customerDL)
        {
            _dbConnection = dbConnection.Value;
            this.accessor = accessor;
            this._connn = connectionString;
            this._dll = customerDL;
        }
        public string CreateCustomer(tblCustomer customer)
        {
            return _dll.CreateCustomer(customer);
        }
        public List<tblCustomer> GetCustomerList()
        {
            return _dll.GetCustomerList();
        }

        public void SetDatabseName(string databaseName)
        {
            _dll.SetDatabseName(databaseName);
        }
        public List<CustomerViewModel> GetCustomerName(int CustomerId)
        {
            return _dll.GetCustomerName(CustomerId);
        }
        public string CreateCustomerDocument(tblCustomerDocument tblCustomerDocument, string CustomerName)
        {
            if (tblCustomerDocument.PhotoCopyOfLalpurjaFile != null)
            {
                tblCustomerDocument.PhotocopyOfLalpurjaDoc = tblCustomerDocument.PhotoCopyOfLalpurjaFile.FileName;
                tblCustomerDocument.PhotocopyOfLalpurjaCode = "100" + "_" + tblCustomerDocument.CustomerId;
            }
            if (tblCustomerDocument.TaxClearanceFile != null)
            {
                tblCustomerDocument.TaxClearanceDoc = tblCustomerDocument.TaxClearanceFile.FileName;
                tblCustomerDocument.TaxClearanceCode = "100" + "_" + tblCustomerDocument.CustomerId;
            }
            if (tblCustomerDocument.CitizenshipDocFile != null)
            {
                tblCustomerDocument.CitizenshipDoc = tblCustomerDocument.CitizenshipDocFile.FileName;
                tblCustomerDocument.CitizenshipCode = "100" + "_" + tblCustomerDocument.CustomerId;
            }
            if (tblCustomerDocument.NaapiNaskaDocFile.FileName != "")
            {
                tblCustomerDocument.NaapiNaksaWithKittaNoDoc = tblCustomerDocument.NaapiNaskaDocFile.FileName;
                tblCustomerDocument.NaapiNaksaWithKittaNo = "100" + "_" + tblCustomerDocument.CustomerId;
            }
            if (tblCustomerDocument.HouseDesignMapDocFile != null)
            {
                tblCustomerDocument.HouseDesginMapDoc = tblCustomerDocument.HouseDesignMapDocFile.FileName;
                tblCustomerDocument.HouseDesginMapCode = "100" + "_" + tblCustomerDocument.CustomerId;
            }


            return _dll.CreateCustomerDocument(tblCustomerDocument);
        }
        public List<tblCustomerDocumentViewModel> GetCustomerDocByCustomerID(int customerID)
        {
            return _dll.GetCustomerDocByCustomerID(customerID);
        }
        public List<tbldocumentright> Getdocumentright()
        {
            return _dll.Getdocumentright();
        }
        public string saveDocumentRight(List<tbldocumentright> tbldocumentright)
        {
            return _dll.saveDocumentRight(tbldocumentright);
        }
        public List<tblCustomer> GetUpdateCustomerData(int Id)
        {
            return _dll.GetUpdateCustomerData(Id);

        }
        public string UpdateCustomer(tblCustomer customer)
        {
            return _dll.UpdateCustomer(customer);
        }

        public List<tblCustomerDocument> GetUpdateCustomerDocumentData(int Id)
        {
            return _dll.GetUpdateCustomerDocumentData(Id);
        }
        public string DeleteCustomerData(int Id)
        {
            return _dll.DeleteCustomerData(Id);
        }
        public string saveFormEntry(tblFormEntry tblFormEntry)
        {

            if (tblFormEntry.FieldType=="1")
            {
                tblFormEntry.FieldType = "String";
            }
            else if (tblFormEntry.FieldType == "2")
            {
                tblFormEntry.FieldType = "Date";
            }
            else if (tblFormEntry.FieldType == "3")
            {
                tblFormEntry.FieldType = "File";
            }
            else if (tblFormEntry.FieldType == "4")
            {
                tblFormEntry.FieldType = "Number";
            }
            return _dll.saveFormEntry(tblFormEntry);
        }
        public List<tblFormEntry> GetFormEntryList()
        {
            return _dll.GetFormEntryList();
        }
        internal string CreateCustomerDocumentUdf(CustomerDocumentUdf customerDocumentUdf)
        {
           
         
            return _dll.CreateCustomerDocumentUdf(customerDocumentUdf);



        }
    }
}

