namespace Dunger.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public decimal Summs { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;
    }
}