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
    public class tblCustomerDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerDocumentId { get; set; }
        public string PhotocopyOfLalpurjaDoc { get; set; }
        public string ? PhotocopyOfLalpurjaCode { get; set; }
        public string TaxClearanceDoc { get; set; }
        public string ?  TaxClearanceCode { get; set; }
        public string CitizenshipDoc { get; set; }
        public string ? CitizenshipCode { get; set; }
        public string NaapiNaksaWithKittaNoDoc { get; set; }
        public string NaapiNaksaWithKittaNo { get; set; }
        public string HouseDesginMapDoc { get; set; }
        public string ? HouseDesginMapCode { get; set; }
        public bool IssueTemporayCertification { get; set; }
        public bool PermamentCertification { get; set; }
        public bool CompletionCertification { get; set; }
        public bool ApprovalOfWardChair { get; set; }

        public bool ApprovalOfWardChairLackOfAccessOfRoad { get; set; }

        public int CustomerId { get; set; }

        //[FromForm(Name = "file")]
        //public IFormFile Filesource { get; set; }

        [FromForm(Name = "photoCopyOfLalpurjaFile")]
        public IFormFile PhotoCopyOfLalpurjaFile { get; set; }

        [FromForm(Name = "taxClearanceFile")]
        public IFormFile TaxClearanceFile { get; set; }


        [FromForm(Name = "citizenshipDocFile")]
        public IFormFile CitizenshipDocFile { get; set; }


        [FromForm(Name = "naapiNaskaDocFile")]
        public IFormFile NaapiNaskaDocFile { get; set; }

        [FromForm(Name = "houseDesignMapDocFile")]
        public IFormFile HouseDesignMapDocFile { get; set; }

        public string ActionType { get; set; }

       

        //[FromForm(Name = "taxClearanceFile")]
        //public IFormFile TaxClearanceFile { get; set; }


    }
}
