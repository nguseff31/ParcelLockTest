using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace ParcelLockTest.Api.Entities;

[Table("Order")]
public class OrderDto {
    [Key]
    public int Id { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    [Required]
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<string> Products { get; set; } = new();

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string ParcelLockNumber { get; set; }

    [ForeignKey(nameof(ParcelLockNumber))]
    public ParcelLockDto ParcelLock { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string FullName { get; set; }

    public void SetStatus(OrderStatus newStatus) {
        if (newStatus == Status) return;

        if (StatusModel[Status].Contains(newStatus)) {
            Status = newStatus;
        }
        else {
            throw new ArgumentException($"Cannot change order status from {Status} to {newStatus}");
        }
    }

    // странно, что в задании не требуется эта логика и заказ можно либо создать, либо отменить
    static Dictionary<OrderStatus, HashSet<OrderStatus>> StatusModel = new() {
        { OrderStatus.Registered, new HashSet<OrderStatus> { OrderStatus.Accepted, OrderStatus.Canceled } },
        { OrderStatus.Accepted, new HashSet<OrderStatus> { OrderStatus.ParcelLock, OrderStatus.Canceled } },
        { OrderStatus.Accepted, new HashSet<OrderStatus> { OrderStatus.CourierTravel, OrderStatus.Canceled } },
        { OrderStatus.Accepted, new HashSet<OrderStatus> { OrderStatus.Delivered, OrderStatus.Canceled } },
        { OrderStatus.Canceled, new HashSet<OrderStatus>() }
    };
}


