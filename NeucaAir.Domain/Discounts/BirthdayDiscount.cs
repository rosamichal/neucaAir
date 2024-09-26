using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Discounts
{
    public sealed class BirthdayDiscount : DiscountCriteriaBase
    {
        public override bool IsEligible(Ticket ticket)
        {
            return ticket.Tenant.Birthday.Date.Month == ticket.Flight.DepartureTime.Date.Month 
                && ticket.Tenant.Birthday.Date.Day == ticket.Flight.DepartureTime.Date.Day;
        }
    }
}
