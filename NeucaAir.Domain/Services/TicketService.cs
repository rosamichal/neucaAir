using NeucaAir.Domain.Discounts;
using NeucaAir.Domain.Entities;

namespace NeucaAir.Domain.Services
{
    public class TicketService
    {
        private readonly IDiscountService _discountService;
        private readonly IEnumerable<DiscountCriteriaBase> _availableCriteria;

        public TicketService(IDiscountService discountService, IEnumerable<DiscountCriteriaBase> availableCriteria)
        {
            _discountService = discountService;
            _availableCriteria = availableCriteria;
        }

        public Ticket BuyTicket(Flight flight, Tenant tenant)
        {
            var ticket = new Ticket(flight, tenant);
            _discountService.ApplyDiscounts(ticket, _availableCriteria);

            return ticket;
        }
    }
}
