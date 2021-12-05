using Project.Business.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Middleware
{
    public class ServiceResult
    {
        public ServiceResult()
        {

        }
        public ServiceResult(ServiceResultCode resultCode)
        {
            if (resultCode == ServiceResultCode.Success)
                ResultMessage = "Successfully Done";
            if (resultCode == ServiceResultCode.Generic)
                ResultMessage = "Invalid Entry please correct your input and try again.";
            if (resultCode == ServiceResultCode.RecordNotFound)
                ResultMessage = "Record Not Found";
            if (resultCode == ServiceResultCode.AlreadyExists)
                ResultMessage = "Item already exists";
            ResultCode = resultCode;
        }

        public ServiceResult(ServiceResultCode resultCode, string resultMessage)
        {
            ResultCode = resultCode;
            ResultMessage = resultMessage;
        }


        public bool HasError => ResultCode != ServiceResultCode.Success;
        public string ResultMessage { get; set; } = string.Empty;
        public ServiceResultCode ResultCode { get; set; }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() : base()
        {
        }

        public ServiceResult(ServiceResultCode resultCode) : base(resultCode)
        {
            if (resultCode == ServiceResultCode.Success)
                ResultMessage = "Successfully Done";
            if (resultCode == ServiceResultCode.Generic)
                ResultMessage = "Invalid Entry please correct your input and try again.";
            if (resultCode == ServiceResultCode.RecordNotFound)
                ResultMessage = "Record Not Found";
            if (resultCode == ServiceResultCode.AlreadyExists)
                ResultMessage = "Item already exists";
            if (resultCode == ServiceResultCode.NullException)
                ResultMessage = "Entry was null";
            ResultCode = resultCode;
        }

        public ServiceResult(ServiceResultCode resultCode, string resultMessage) : base(resultCode, resultMessage)
        {
            ResultCode = resultCode;
            ResultMessage = resultMessage;
        }
        public ServiceResult(ServiceResultCode resultCode,string resultMessage,T data)
        {
            ResultCode= resultCode;
            ResultMessage = resultMessage;
            Data = data;
        }


        public ServiceResult(T data)
        {
            Data = data;
        }

        public ServiceResult(IEnumerable<T> list)
        {
            ListData = list;
        }

        public T Data { get; set; }
        public IEnumerable<T> ListData { get; set; }
    }

}
