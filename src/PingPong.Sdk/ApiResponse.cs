using PingPong.Sdk.Models;

namespace PingPong.Sdk
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; private set; }
        
        public T Data { get; private set; }
        
        public ErrorDto ErrorDto { get; private set; }

        
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

        
        public static ApiResponse<T> Success(int statusCode, T data)
        {
            return new ApiResponse<T>()
            {
                StatusCode = statusCode,
                Data = data
            };
        }
        public static ApiResponse<T> Error(int statusCode, ErrorDto error)
        {
            return new ApiResponse<T>()
            {
                StatusCode = statusCode,
                ErrorDto = error
            };
        }
    }
}