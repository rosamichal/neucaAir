using NeucaAir.Domain.Discounts;
using NeucaAir.Domain.Entities;
using NeucaAir.Domain.Services;
using NeucaAir.Domain.ValueObjects;

namespace NeucaAir.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tenant1 = new Tenant("Michał", new DateOnly(1988, 4, 8), TenantGroup.B);
            var tenant2 = new Tenant("Jan", new DateOnly(2000, 10, 3), TenantGroup.A);

            var flightId = new FlightId("ABC12345XYZ");
            var departureTime = new DateTimeOffset(2024, 10, 3, 12, 30, 00, new TimeSpan(1, 0, 0));
            var flight = new Flight(flightId, "Warszawa", "Afryka", departureTime, departureTime.DayOfWeek, 30_00);

            var discountService = new DiscountService();
            var discountCriteria = new List<DiscountCriteriaBase>() { new BirthdayDiscount(), new AfricaThursdayDiscount() };
            var ticketService = new TicketService(discountService, discountCriteria);

            var ticket1 = ticketService.BuyTicket(flight, tenant1);
            var ticket2 = ticketService.BuyTicket(flight, tenant2);

            WriteTicket(ticket1);
            WriteTicket(ticket2);
        }

        private static void WriteTicket(Ticket ticket)
        {
            Console.WriteLine($"""
                                Imię: {ticket.Tenant.Name} (grupa {ticket.Tenant.Group})
                                Lot {ticket.Flight.FlightId.Id} Z: {ticket.Flight.From} DO: {ticket.Flight.To}
                                Data lotu: {ticket.Flight.DepartureTime.ToString()}, {ticket.Flight.DayOfDeparture}
                                Cena podstawowa: {ticket.Flight.BasePriceInCent / 100:.00} EUR
                                Cena po rabatach: {ticket.FinalPriceInCent / 100:.00} EUR
                                Zastosowane zniżki: {string.Join(", ", ticket.GetAppliedDiscounts().Select(d => d.ToString()))}
                                ---------------------------------------------------------------------
                                """);
        }
    }
}
