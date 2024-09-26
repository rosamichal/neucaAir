using NeucaAir.Domain.ValueObjects;

namespace NeucaAir.Domain.Entities
{
    public sealed class Flight
    {
        public FlightId FlightId { get; }
        public string From { get; }
        public string To { get; }
        public DateTimeOffset DepartureTime { get; }
        public DayOfWeek DayOfDeparture { get; }
        public int BasePriceInCent { get; }

        public Flight(FlightId flightId, string from, string to, DateTimeOffset departureTime, DayOfWeek dayOfDeparture, int basePriceInCent)
        {
            FlightId = flightId;
            From = from;
            To = to;
            DepartureTime = departureTime;
            DayOfDeparture = dayOfDeparture;
            BasePriceInCent = basePriceInCent;
        }
    }
}
