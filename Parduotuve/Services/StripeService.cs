using Stripe.Checkout;

namespace Parduotuve.Services;

public class StripeService : IStripeService
{
    public async Task<string> CreateCheckoutSessionAsync(IEnumerable<SessionLineItemOptions> items, Dictionary<string, string> metadata, string successUrl, string cancelUrl)
    {
        var options = new SessionCreateOptions
        {
            LineItems = items.ToList(),
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            Metadata = metadata
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }
}