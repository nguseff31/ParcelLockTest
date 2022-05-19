using ParcelLockTest.Contract.ParcelLock;

namespace ParcelLockTest.Api.Services.Interfaces;

public interface IParcelLockService {
    Task<ParcelLockGet?> Get(string number);
    IAsyncEnumerable<ParcelLockGet> GetAll();
}
