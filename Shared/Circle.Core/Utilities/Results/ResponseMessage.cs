﻿namespace Circle.Core.Utilities.Results
{
    public class ResponseMessage<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string DeveloperMessage { get; set; }
        public int StatusCode { get; set; }

        //Static Factory Method
        public static ResponseMessage<T> Success(T data)
        {
            return new ResponseMessage<T> { Data = data, StatusCode = 200, IsSuccess = true };
        }

        public static ResponseMessage<T> Success()
        {
            return new ResponseMessage<T> { StatusCode = 200, IsSuccess = true };
        }

        public static ResponseMessage<T> Fail(string error)
        {
            return new ResponseMessage<T> 
            { 
                StatusCode = 500, 
                IsSuccess = false ,
                Message = error
            };
        }

        public static ResponseMessage<T> NoDataFound(string error)
        {
            return new ResponseMessage<T> 
            { 
                StatusCode = 404, 
                IsSuccess = false ,
                Message = error
            };
        }
    }
}
