

namespace Ecommerce.Application.Dtos.ResultPattern
{
    public record Result
    {
        public bool IsSuccess { get; private set; }
        public Error? Error { get; }

        public ErrorType ErrorType { get; private set; }

        public string Message { get; private set; }

        protected Result()
        {

        }

        protected Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(Error error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));

        public static Result Failure(ErrorType error, string message)
        {
            return new Result()
            {
                IsSuccess = false,
                ErrorType = error,
                Message = message
            }
            ;
        }

        public static implicit operator Result(Error error) => Failure(error);
    }


    public record Result<T> : Result
    {
        public T? Data { get; }

        private Result(T value) : base(true, null) => Data = value;
        private Result(Error error) : base(false, error) { }

        public static Result<T> Success(T value) => new Result<T>(value);

        public static implicit operator Result<T>(T value) => new(value);

        public static implicit operator Result<T>(Error error) => new(error);
    }


}


