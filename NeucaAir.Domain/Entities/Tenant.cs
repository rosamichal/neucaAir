namespace NeucaAir.Domain.Entities
{
    public enum TenantGroup
    {
        A,
        B
    }

    public sealed class Tenant
    {
        public Guid TenantId { get; }
        public TenantGroup Group { get; }
        public string Name { get; }
        public DateOnly Birthday { get; }

        public Tenant(string name, DateOnly birthday, TenantGroup group)
        {
            TenantId = Guid.NewGuid();
            Group = group;
            Name = name;
            Birthday = birthday;
        }

        public bool CanStoreDiscounts() => Group == TenantGroup.A;
    }
}
