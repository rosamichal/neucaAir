using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NeucaAir.Domain.ValueObjects
{
    public record FlightId
    {
        public string Id { get; }

        public FlightId(string id)
        {
            if (!IsValid(id))
            {
                throw new ArgumentException("Invalid Flight Id format.");
            }
            Id = id;
        }

        private bool IsValid(string id)
        {
            return Regex.IsMatch(id, @"^[A-Z]{3}\d{5}[A-Z]{3}$");
        }
    }
}
