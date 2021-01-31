using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.ViewModels
{
    public class CompanyDatabaseViewModel
    {
      
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int tblUserDatabaseId { get; set; }
           
            public int UserId { get; set; }          
            public int companyDatabaseId { get; set; }           
            public string CompanyDatabaseCode { get; set; }
            public int CompanyGroupId { get; set; }
            public string CompanyName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

        
    }
}
