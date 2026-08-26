using AutoMapper;
using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.DeliveryMethods;
using Ecommerce.Application.Dtos.Order;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Application.ServicesAbstractions;
using Ecommerce.Application.Specifications;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Services
{
    public class OrderService(
        IUnitOfWork _unitOfWork, 
        IBasketService _basketService,
        IMapper _mapper,
        UserManager<ApplicationUser> _userManager): IOrderService
    {
        public async Task<Result<OrderDto>> CreateOrder(CreateOrderDto createOrderDto, string userEmail)
        {
            var basketResult = await _basketService.GetBasketAsync(createOrderDto.BasketId);
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetById(createOrderDto.DeliveryMethodId);

            if (basketResult == null)
                return Result<OrderDto>.Failure(ErrorType.NotFound, $"Basket With Id {createOrderDto.BasketId} Not Found");
            var basket = basketResult.Data;
            if(basket == null || basket.BasketItems.Count == 0)
                return Result<OrderDto>.Failure(ErrorType.NotFound, $"Basket With Id {createOrderDto.BasketId} Doesnot Have Items");


            if (deliveryMethod == null)
                return Result<OrderDto>.Failure(ErrorType.NotFound, $"Delivery Method {createOrderDto.DeliveryMethodId} Not Found");


            var shippingAddress = _mapper.Map<OrderAddress>(createOrderDto.DeliveryAddress);

            
            var orderItems = await CreateOrderFromBasket(basket!);

            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            var order = new Order(userEmail, shippingAddress, deliveryMethod, orderItems, subTotal);
           
            _unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await _unitOfWork.SaveAllChangesAsync();
            if (result < 1) 
                return null; // TODO: CHange to meanigful object

            await _basketService.DeleteBasketAsync(createOrderDto.BasketId);

            var orderDto =  new OrderDto()
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTimeOffset.Now,
                UserEmail = userEmail,
                ShippingAddress = createOrderDto.DeliveryAddress,
                DeliveryMethod = deliveryMethod.ShortName,
                SubTotal = subTotal,
                DeliveryCost = deliveryMethod.Price,
                Total = order.GetTotal(),
                Items = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems)

            };
            return Result<OrderDto>.Success(orderDto);

        }

        public async Task<Result<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return Result<IEnumerable<DeliveryMethodDto>>.Success(_mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods));
        }

        public  async Task<Result<OrderDto>> GetOrderDetails(Guid orderId, string UserEmail)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetById(new UserOrderSpecification(UserEmail, orderId));
            if (order == null)
                return Result<OrderDto>.Failure(ErrorType.NotFound, $"Order With Id {orderId} Is not found");


            var orderDto = _mapper.Map<OrderDto>(order);
            return Result<OrderDto>.Success(orderDto);

        }

        public async Task<Result<IEnumerable<OrderDto>>> GetUserOrders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IEnumerable<OrderDto>>.Failure(ErrorType.Unauthorized, $"UnAuthorized User With Email {email}");
            var userOrders = await _unitOfWork.GetRepository<Order, Guid>().GetById(new UserOrderSpecification(email));
            var userOrdersDtos = _mapper.Map<IEnumerable<OrderDto>>(userOrders);
            return Result<IEnumerable<OrderDto>>.Success(userOrdersDtos);

        }
        private async Task<IEnumerable<OrderItem>> CreateOrderFromBasket(CustomerBasketDto customerBasketDto)
        {
            var basketItems = customerBasketDto.BasketItems;
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            var orderItems = new List<OrderItem>();
            foreach(var item in basketItems)
            {
                var product = await productRepo.GetById(item.Id);
                // TODO: ADD ERROR MESSAGE
                if (product == null)
                {
                    return [];
                }
                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl,
                    ProductId = product.Id
                });
            }
            return orderItems;
            
        }
    }
}
