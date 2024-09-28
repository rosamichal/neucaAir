using Moq;

using NeucaAir.Domain.Discounts;
using NeucaAir.Domain.Entities;
using NeucaAir.Domain.Services;
using NeucaAir.Domain.ValueObjects;

namespace NeucaAir.Domain.Tests
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _discountService;

        public DiscountServiceTests()
        {
            _discountService = new DiscountService();
        }

        [Fact]
        public void ApplyDiscounts_NoDiscountsApplied_WhenNoCriteriaMet()
        {
            // Arrange
            var flight = new Flight(new FlightId("ABC12345XYZ"), string.Empty, string.Empty, DateTimeOffset.Now, 50_00);
            var tenant = new Tenant(string.Empty, new DateOnly(), TenantGroup.A);
            var ticket = new Ticket(flight, tenant);
            var criteria = new List<DiscountCriteriaBase>();

            // Act
            _discountService.ApplyDiscounts(ticket, criteria);

            // Assert
            Assert.Equal(50_00, ticket.FinalPriceInCent);
            Assert.Empty(ticket.GetAppliedDiscounts());
        }

        [Fact]
        public void ApplyDiscounts_DiscountApplied_WhenCriteriaMet()
        {
            // Arrange
            var flight = new Flight(new FlightId("ABC12345XYZ"), string.Empty, string.Empty, DateTimeOffset.Now, 50_00);
            var tenant = new Tenant(string.Empty, new DateOnly(), TenantGroup.A);
            var ticket = new Ticket(flight, tenant);

            var mockCriteria = new Mock<DiscountCriteriaBase>();
            mockCriteria.Setup(c => c.IsEligible(ticket)).Returns(true);
            mockCriteria.Setup(c => c.DiscountAmountInCent).Returns(5_00);

            var criteria = new List<DiscountCriteriaBase> { mockCriteria.Object };

            // Act
            _discountService.ApplyDiscounts(ticket, criteria);

            // Assert
            Assert.Equal(45_00, ticket.FinalPriceInCent);
            Assert.Single(ticket.GetAppliedDiscounts());
            Assert.Contains(mockCriteria.Object, ticket.GetAppliedDiscounts());
        }

        [Fact]
        public void ApplyDiscounts_DiscountNotApplied_WhenPriceBelowMinimum()
        {
            // Arrange
            var flight = new Flight(new FlightId("ABC12345XYZ"), string.Empty, string.Empty, DateTimeOffset.Now, 21_00);
            var tenant = new Tenant(string.Empty, new DateOnly(), TenantGroup.A);
            var ticket = new Ticket(flight, tenant);

            var mockCriteria = new Mock<DiscountCriteriaBase>();
            mockCriteria.Setup(c => c.IsEligible(ticket)).Returns(true);
            mockCriteria.Setup(c => c.DiscountAmountInCent).Returns(5_00);

            var criteria = new List<DiscountCriteriaBase> { mockCriteria.Object };

            // Act
            _discountService.ApplyDiscounts(ticket, criteria);

            // Assert
            Assert.Equal(21_00, ticket.FinalPriceInCent); 
            Assert.Empty(ticket.GetAppliedDiscounts());
        }

        [Fact]
        public void ApplyDiscounts_MultipleDiscountsApplied_WhenMultipleCriteriaMet()
        {
            // Arrange
            var flight = new Flight(new FlightId("ABC12345XYZ"), string.Empty, string.Empty, DateTimeOffset.Now, 50_00);
            var tenant = new Tenant(string.Empty, new DateOnly(), TenantGroup.A);
            var ticket = new Ticket(flight, tenant);

            var mockCriteria1 = new Mock<DiscountCriteriaBase>();
            mockCriteria1.Setup(c => c.IsEligible(It.IsAny<Ticket>())).Returns(true);
            mockCriteria1.Setup(c => c.DiscountAmountInCent).Returns(5_00);

            var mockCriteria2 = new Mock<DiscountCriteriaBase>();
            mockCriteria2.Setup(c => c.IsEligible(It.IsAny<Ticket>())).Returns(true);
            mockCriteria2.Setup(c => c.DiscountAmountInCent).Returns(5_00);

            var criteria = new List<DiscountCriteriaBase> { mockCriteria1.Object, mockCriteria2.Object };

            // Act
            _discountService.ApplyDiscounts(ticket, criteria);

            // Assert
            Assert.Equal(40_00, ticket.FinalPriceInCent);
            Assert.Equal(2, ticket.GetAppliedDiscounts().Count);
            Assert.Contains(mockCriteria1.Object, ticket.GetAppliedDiscounts());
            Assert.Contains(mockCriteria2.Object, ticket.GetAppliedDiscounts());
        }

        [Fact]
        public void ApplyDiscounts_StopApplyingDiscount_WhenMinimumPriceReached()
        {
            // Arrange
            var flight = new Flight(new FlightId("ABC12345XYZ"), string.Empty, string.Empty, DateTimeOffset.Now, 26_00);
            var tenant = new Tenant(string.Empty, new DateOnly(), TenantGroup.A);
            var ticket = new Ticket(flight, tenant);

            var mockCriteria1 = new Mock<DiscountCriteriaBase>();
            mockCriteria1.Setup(c => c.IsEligible(It.IsAny<Ticket>())).Returns(true);
            mockCriteria1.Setup(c => c.DiscountAmountInCent).Returns(5_00);

            var mockCriteria2 = new Mock<DiscountCriteriaBase>();
            mockCriteria2.Setup(c => c.IsEligible(It.IsAny<Ticket>())).Returns(true);
            mockCriteria2.Setup(c => c.DiscountAmountInCent).Returns(5_00);

            var criteria = new List<DiscountCriteriaBase> { mockCriteria1.Object, mockCriteria2.Object };

            // Act
            _discountService.ApplyDiscounts(ticket, criteria);

            // Assert
            Assert.Equal(21_00, ticket.FinalPriceInCent);
            Assert.Single(ticket.GetAppliedDiscounts());
            Assert.Contains(mockCriteria1.Object, ticket.GetAppliedDiscounts());
        }
    }
}
