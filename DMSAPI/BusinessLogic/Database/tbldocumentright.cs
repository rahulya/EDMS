using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.BusinessLogic.Database
{
    public class tbldocumentright
    {
            public int Id { get; set; }
            public string ControlName { get; set; }
            public bool ControlVisible { get; set; }
            public bool Mandatory { get; set; }
    }
}
