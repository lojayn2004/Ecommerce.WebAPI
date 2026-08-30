using AutoMapper;
using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.Payment;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.ServicesAbstractions;
using Ecommerce.Application.Specifications;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Basket;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.Services
{
    public class PaymentService(IBasketRepository _basketRepo, 
        IUnitOfWork _unitOfWork,
        IPaymentGatway _paymentGateway,
        IOptions<StripeOptions> _options,
        IMapper _mapper): IPaymentService
    {
        public async  Task<Result<CustomerBasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null)
                return Result<CustomerBasketDto>.Failure(ErrorType.NotFound, $"Basket With Id {basketId} Is Not Found");

            
            if (basket.BasketItems.Count == 0)
                return Result<CustomerBasketDto>.Failure(ErrorType.NotFound, $"Basket With Id {basketId} Doesnot Have Items");

            

            var deliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetById(basket.DelievryMethodId.Value);

            if (deliveryMethod == null)
                return Result<CustomerBasketDto>.Failure(ErrorType.NotFound, $"Delivery Method {basket.DelievryMethodId} Not Found");

            basket.ShippingPrice = deliveryMethod.Price;

            try
            {
                var orderItems = await CreateOrderFromBasket(basket);

                var subTotal = orderItems.Sum(o => o.Price * o.Quantity);
                var totalAmount = subTotal + basket.ShippingPrice;
                if (string.IsNullOrEmpty(basket.PaymentIntentId))
                {
                   var result =   await _paymentGateway.CreatePaymentIntentAsync(totalAmount.Value, _options.Value.DefaultCurrency);

                    basket.PaymentIntentId = result.PaymentIntentId;
                    basket.ClientSecret = result.ClientSecret;
                }
                else
                {
                    await _paymentGateway.UpdatePaymentIntentAsync(basket.PaymentIntentId, totalAmount.Value);
                }

                await  _basketRepo.CreateOrUpdateBasketAsync(basket);

                return Result<CustomerBasketDto>.Success(_mapper.Map<CustomerBasketDto>(basket));
            }
            catch (ProductNotFoundException ex)
            {
                return Result<CustomerBasketDto>.Failure(ErrorType.NotFound, ex.Message);
            }

                     

        }

        public async Task PaymentFailedAsync(string paymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await  orderRepo.GetById(new OrderPaymentIntentSpecs(paymentIntentId));

            if (order == null) return ;

            order.MarkPaymentFailed();
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task PaymentSuccededAsync(string paymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetById(new OrderPaymentIntentSpecs(paymentIntentId));

            if (order == null) return;

            order.MarkPaymentRecieved();
            await _unitOfWork.SaveAllChangesAsync();
        }

        private async Task<IEnumerable<OrderItem>> CreateOrderFromBasket(CustomerBasket customerBasketDto)
        {
            var basketItems = customerBasketDto.BasketItems;
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            var orderItems = new List<OrderItem>();
            foreach (var item in basketItems)
            {
                var product = await productRepo.GetById(item.Id);
               
                if (product == null)
                    throw new ProductNotFoundException($"Product With Id {product.Id} Not Found");
                
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
