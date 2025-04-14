using Microsoft.AspNetCore.Mvc;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Repositories;
using Stripe;
using Stripe.Checkout;

namespace Parduotuve.Controllers
{
    [Route("api/stripehooks")]
    public class StripeWebHookController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public StripeWebHookController(IConfiguration configuration, IOrderRepository orderRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var secret = _configuration.GetValue<string>("StripeWebHookSecret");

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    secret,
                    throwOnApiVersionMismatch: false
                );

                if (stripeEvent.Type == Stripe.EventTypes.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var options = new SessionGetOptions();
                    options.AddExpand("line_items");

                    var service = new SessionService();
                    var sessionWithLineItems = await service.GetAsync(session.Id, options);
                    StripeList<LineItem> lineItems = sessionWithLineItems.LineItems;

                    string hash = String.Empty;
                    if(!sessionWithLineItems.Metadata.TryGetValue("paymentHash", out hash))
                    {
                        return BadRequest();
                    }
                    Order? order = await _orderRepository.GetOrderById(hash);
                    if(order == null)
                    {
                        return BadRequest();
                    }
                    order.IsCompleted = true;
                    await _orderRepository.UpdateOrder(order);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
