using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Utilities.Helpers
{
    public class AppException
    {
        public AppException(int statusCode, string errorMessage, string details = null)
        {
            IsSuccess = false;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Details = details; //null or ""
        }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }
    }
}
