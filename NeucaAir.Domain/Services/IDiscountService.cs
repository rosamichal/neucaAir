using NeucaAir.Domain.Discounts;
using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Services
{
    public interface IDiscountService
    {
        void ApplyDiscounts(Ticket ticket, IEnumerable<DiscountCriteriaBase> criteria);
    }
}
