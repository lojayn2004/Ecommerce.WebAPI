

namespace Ecommerce.Application.Dtos.ResultPattern
{
    public enum ErrorType { NotFound, Validation, Unauthorized, NotCreated, NotDeleted}

    public record Error(ErrorType Type, string Description);

   
    public static class Errors
    {
        public static Error ProductNotFound { get; } = new(ErrorType.NotFound, "Product not found.");

        public static Error FailedBasketCreation { get; } = new(ErrorType.NotCreated, "Failed Basket Creation");

        public static Error BasketNotFound { get; } = new(ErrorType.NotFound, "Basket Not Found");


        public static Error DeleteBasketFailed { get; } = new(ErrorType.NotDeleted, "Failed Basket Deletion");

    
        public static Error CachedValueNotFound { get; } = new(ErrorType.NotFound, "Cached Value Not Found");
    
    
        public static Error InValidUserCredentials { get; } = new(ErrorType.Unauthorized, "Invalid User Credentials");


        public static Error DeliveryMethodNotFound { get; } = new(ErrorType.NotFound, "Delivery Method Not Found");

        public static Error UserCreationFailed { get; } = new(ErrorType.NotCreated, "User Creation Failed");
    }
    
    
}
