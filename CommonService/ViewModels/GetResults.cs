using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class GetResults
    {
        public GetResults(bool isSuccess = false, string message="", object result=null, int total=0)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = result;
            Total = total;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Total { get; set; }
    }

    //public class GetResults<T>
    //{
    //    public object Result { get; set; }
    //    public int Total { get; set; }
    //    public string[] SearchableFields { get; set; }
    //}
}
