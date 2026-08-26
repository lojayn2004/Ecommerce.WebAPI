using Ecommerce.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Domain.Models.Orders
{
    [Owned]
    public class OrderAddress
    {
    
        public string City { get; set; } = default!;

        public string Street { get; set; } = default!;

        public string Country { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

       

    }
}
