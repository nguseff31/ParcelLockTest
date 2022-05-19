#pragma warning disable CS8618
namespace ParcelLockTest.Contract;

public class DefaultValidationResponse : ErrorResponse {
    public DefaultValidationResponse() {
        Message = "Validation error";
        ErrorCode = 400;
    }
    public Dictionary<string, string[]> Errors { get; set; }
}
