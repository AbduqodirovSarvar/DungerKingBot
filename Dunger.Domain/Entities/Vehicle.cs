namespace Dunger.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Deliver> Delivers { get; set; } = new HashSet<Deliver>();
    }
}
