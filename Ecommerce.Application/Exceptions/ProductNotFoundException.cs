namespace Ecommerce.Application.Exceptions
{
    internal class ProductNotFoundException: NotFoundException
    {
        public ProductNotFoundException(string message): base(message) { }
    }
}
