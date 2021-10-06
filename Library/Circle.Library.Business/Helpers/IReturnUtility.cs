﻿using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Business.Helpers
{
    public interface IReturnUtility
    {
        Task<ResponseMessage<T>> Success<T>(MessageDefinitions messageCode);
        Task<ResponseMessage<T>> SuccessWithData<T>(MessageDefinitions messageCode, T data);
        ResponseMessage<T> SuccessData<T>(T data);
        Task<ResponseMessage<T>> NoDataFound<T>(MessageDefinitions messageCode);
        Task<ResponseMessage<T>> Fail<T>(MessageDefinitions messageCode);
    }
}