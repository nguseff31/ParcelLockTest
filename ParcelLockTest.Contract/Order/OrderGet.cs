#pragma warning disable CS8618
namespace ParcelLockTest.Contract.Order;

public class OrderGet {
    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    public ICollection<string> Products { get; set; } = new List<string>();

    public decimal Price { get; set; }

    public string ParcelLockNumber { get; set; }

    public string Phone { get; set; }

    public string FullName { get; set; }

    public enum OrderStatus {
        Registered = 1,
        Accepted = 2,
        CourierTravel = 3,
        ParcelLock = 4,
        Delivered = 5,
        Canceled = 6
    }
}
