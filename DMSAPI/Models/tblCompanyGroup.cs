using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Models
{
    public class tblCompanyGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CompanyGroupName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int TotalUsers { get; set; }
        public DateTime ExpirayDateAD { get; set; }
        
        public bool IsBranchApplicable { get; set; }
        public int NoOfBranch { get; set; }
        public string GroupCode { get; set; }
        public DateTime GroupCreateDate { get; set; }

    }
}
