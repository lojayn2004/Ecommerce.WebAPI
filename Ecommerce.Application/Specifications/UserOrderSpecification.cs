using Ecommerce.Domain.Models.Orders;


namespace Ecommerce.Application.Specifications
{
    internal class UserOrderSpecification: BaseSpecification<Order, Guid>
    {
        public UserOrderSpecification(string UserEmail) : base(o => o.UserEmail == UserEmail)
        {

            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }


        public UserOrderSpecification(string UserEmail, Guid orderId) : 
            base(o => o.UserEmail == UserEmail && o.Id == orderId)
        {

            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
