using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.CIUtils
{
    public class OperationResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public OperationResult(bool result, string message = "")
        {
            Success = result;
            Message = message;
        }
    }
}
