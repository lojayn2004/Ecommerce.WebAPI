

namespace Ecommerce.Application.Dtos.ResultPattern
{
   

    public record Result<T> 
    {
        public bool IsSuccess { get; private set; } = true;

        public ErrorType ErrorType { get; private set; } = ErrorType.None;

        public string Message { get; private set; }

        public T? Data { get; }




        private Result(ErrorType error, string message)
        {
            ErrorType = error;
            Message = message;
            IsSuccess = false;
        }
        private Result(T value)  => Data = value;
        
        public static Result<T> Success(T value) => new Result<T>(value);

        public static Result<T> Failure(ErrorType error, string message) => new Result<T>(error, message);
    }


}


