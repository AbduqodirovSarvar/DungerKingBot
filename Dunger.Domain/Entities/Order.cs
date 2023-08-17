﻿namespace Dunger.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public bool IsPaid { get; set; } = false;
        public bool IsDone { get; set; } = false;
        public string Address { get; set; } = string.Empty;
        public string? LocationUrl { get; set; }
        public int FilialId { get; set; }
        public Filial? Filial { get; set; }
        public double? Reyting { get; set; }
        public decimal TotalSumms { get; set; }
        public ICollection<OrderMenu> Menus { get; set; } = new HashSet<OrderMenu>();
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedTime { get; set; }
        public DateTime? DeliveredTime { get; set; }
    }
}