using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Discounts
{
    public sealed class AfricaThursdayDiscount : DiscountCriteriaBase
    {
        public override bool IsEligible(Ticket ticket)
        {
            return ticket.Flight.To == "Africa" 
                && ticket.Flight.DepartureTime.DayOfWeek == DayOfWeek.Thursday;
        }
    }
}
