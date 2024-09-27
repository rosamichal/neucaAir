using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Discounts
{
    public sealed class AfricaThursdayDiscount : DiscountCriteriaBase
    {
        public override bool IsEligible(Ticket ticket)
        {
            return ticket.Flight.To == "Afryka" 
                && ticket.Flight.DepartureTime.DayOfWeek == DayOfWeek.Thursday;
        }

        public override string ToString()
        {
            return "Zniżka Afryka w czwartek";
        }
    }
}
