
using Ecommerce.Application.Dtos.Auth;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;


        public int DeliveryMethodId { get; set; } = default!;

        [Required]
        public AddressDto DeliveryAddress { get; set; } = default!;
    }
}
