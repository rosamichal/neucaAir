using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Discounts
{
    public abstract class DiscountCriteriaBase
    {
        public virtual int DiscountAmountInCent => 5_00;
        public abstract bool IsEligible(Ticket ticket);
    }
}
