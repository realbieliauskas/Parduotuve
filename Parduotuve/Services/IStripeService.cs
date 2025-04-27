using Stripe.Checkout;

namespace Parduotuve.Services;

public interface IStripeService
{
    Task<string> CreateCheckoutSessionAsync(IEnumerable<SessionLineItemOptions> items,
        Dictionary<string, string> metadata, string successUrl, string cancelUrl);
}