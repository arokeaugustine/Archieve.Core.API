using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts
{
    public class ResponseModel<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }

    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }

}
