using NeucaAir.Domain.ValueObjects;

namespace NeucaAir.Domain.Entities
{
    public sealed class Flight
    {
        public FlightId FlightId { get; }
        public string From { get; }
        public string To { get; }
        public DateTimeOffset DepartureTime { get; }
        public DayOfWeek DayOfDeparture => DepartureTime.DayOfWeek;
        public int BasePriceInCent { get; }

        public Flight(FlightId flightId, string from, string to, DateTimeOffset departureTime, int basePriceInCent)
        {
            FlightId = flightId;
            From = from;
            To = to;
            DepartureTime = departureTime;
            BasePriceInCent = basePriceInCent;
        }
    }
}
