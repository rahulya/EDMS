using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Database
{
   

    public class tblCompany_DatabaseViewModel
    {
        public int tblUserDatabaseId { get; set; }
        public int UserId { get; set; }
        public int companyDatabaseId { get; set; }
        public string CompanyDatabaseCode { get; set; }
        public int CompanyGroupId { get; set; }
        public string CompanyName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
