using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Models
{
    public class ResultType
    {       
            public string Message { get; set; }
            public bool IsSuccess { get; set; }
            public object Data { get; set; }
            public int TotalCount { get; set; }
       
    }
}
