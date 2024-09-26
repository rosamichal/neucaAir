using NeucaAir.Domain.Discounts;
using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Services
{
    public class DiscountService : IDiscountService
    {
        private const int MinimumPriceInCent = 20_00;

        public void ApplyDiscounts(Ticket ticket, IEnumerable<DiscountCriteriaBase> criteria)
        {
            var finalPriceInCents = ticket.Flight.BasePriceInCent;
            foreach (var criterion in criteria)
            {
                if (criterion.IsEligible(ticket) && finalPriceInCents - criterion.DiscountAmountInCent >= MinimumPriceInCent)
                {
                    finalPriceInCents -= criterion.DiscountAmountInCent;
                    ticket.AddAppliedDiscount(criterion);
                }
            }
        }
    }
}
