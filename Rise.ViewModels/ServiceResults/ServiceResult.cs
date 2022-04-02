using Rise.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rise.ViewModels.ServiceResults
{
    public class ServiceResult <T> 
    {
        public T Data { get; set; }
        public string ResultExplanation { get; set; }

        public bool IsSuccess { get; set; } = false;

        public ServiceResult(T data,string resultExp, bool isSuccess)
        {
            Data = data;
            ResultExplanation = resultExp;
            IsSuccess = isSuccess;
        }
        public ServiceResult(T data, string resultExp)
        {
            Data = data;
            ResultExplanation = resultExp;
            IsSuccess = true;
        }
    }
}
