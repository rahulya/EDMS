using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Database
{
    public class tblFormEntry
    {
        public int FormId { get; set; }
        public string Entrymodule { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string TotalWidth { get; set; }
        public bool Mandotaryopt { get; set; }
        public string ? DateFormat { get; set; }
    }
}
