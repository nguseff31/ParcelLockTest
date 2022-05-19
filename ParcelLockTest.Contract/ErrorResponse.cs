#pragma warning disable CS8618
namespace ParcelLockTest.Contract;

public class ErrorResponse {
    public string Message { get; set; }

    public int ErrorCode { get; set; }

    public string? StackTrace { get; set; }
}
