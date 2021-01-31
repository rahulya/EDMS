using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Database
{
    public class tblCustomerDocumentViewModel
    {
        public int CustomerDocumentId { get; set; }
        public string PhotocopyOfLalpurjaDoc { get; set; }
        public string PhotocopyOfLalpurjaCode { get; set; }
        public string TaxClearanceDoc { get; set; }
        public string TaxClearanceCode { get; set; }
        public string CitizenshipDoc { get; set; }
        public string CitizenshipCode { get; set; }
        public string NaapiNaksaWithKittaNoDoc { get; set; }
        public string NaapiNaksaWithKittaNo { get; set; }
        public string HouseDesginMapDoc { get; set; }
        public string HouseDesginMapCode { get; set; }
    }
}
