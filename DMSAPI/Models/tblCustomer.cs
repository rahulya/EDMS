using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Models
{
    public class tblCustomer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Middle { get; set; }
        public string LastName { get; set; }
        public string KittaNo { get; set; }
        public string CitizenshipNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }  
        public int CompanyId { get; set; }
        public int UserId { get; set; }

        public string DatabaseName { get; set; }
       public bool IsFileSave { get; set; }
    }

}
