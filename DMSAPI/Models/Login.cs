using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CompanyCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }

        public int CompanyGroupId { get; set; }

        public int UserId { get; set; }

        [NotMapped]
        public int HasCompany { get; set; }

    }
}
