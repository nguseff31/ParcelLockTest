using Microsoft.AspNetCore.Mvc;
using ParcelLockTest.Contract.Order;

namespace ParcelLockTest.Api.Services.Interfaces;

public interface IOrderService {
    Task<OrderGet?> Get(int id);
    Task<int> Create(OrderCreate model);
    Task<ActionResult<OrderGet>> Update(int id, [FromBody] OrderUpdate model);

    /// <summary>
    /// Cancel order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<OrderGet> Cancel(int id);
}
