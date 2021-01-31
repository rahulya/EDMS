using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Database
{
    public class CustomerDocumentUdf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  CustomerDocumentUdfId { get; set; }

        [FromForm(Name = "formId")]
        public List<int>  FormId { get; set; }
        public string ? DocumentFileName { get; set; }
        public string ? DocumentFileCode { get; set; }

        public string ActionType { get; set; }

        public int CustomerId { get; set; }



        [FromForm(Name = "documentFile")]
        public List< IFormFile> DocumentFile { get; set; }
        // public List<documentFile> DocumentFile { get; set; }
    }

    public class documentFile
    {

        [FromForm(Name = "documentFile")]
        public IFormFile DocumentFile { get; set; }
    }
}
