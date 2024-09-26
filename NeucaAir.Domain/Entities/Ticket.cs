using NeucaAir.Domain.Discounts;

namespace NeucaAir.Domain.Entities
{
    public sealed class Ticket
    {
        public Guid TenantId { get; }
        public Flight Flight { get; }
        public Tenant Tenant { get; }
        public int FinalPriceInCent => Flight.BasePriceInCent - _appliedDiscounts.Sum(d => d.DiscountAmountInCent);

        private List<DiscountCriteriaBase> _appliedDiscounts = new List<DiscountCriteriaBase>();

        public Ticket(Flight flight, Tenant tenant)
        {
            Flight = flight;
            Tenant = tenant;
        }

        public void AddAppliedDiscount(DiscountCriteriaBase discount)
        {
            _appliedDiscounts.Add(discount);
        }

        public IReadOnlyList<DiscountCriteriaBase> GetAppliedDiscounts() => 
            Tenant.CanStoreDiscounts() ? _appliedDiscounts.AsReadOnly() : new List<DiscountCriteriaBase>().AsReadOnly();
    }
}
