using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Database
{
    public class tbl_User
    {
        
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int UserGroupID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int UserRoleID { get; set; }
        public int CompanyGroupID { get; set; }

        public string UserRoleName { get; set; }

        public string UserGroupName { get; set; }
        public string CompanyGroupName { get; set; }

    }
}
