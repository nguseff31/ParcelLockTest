using Microsoft.AspNetCore.Mvc;
using ParcelLockTest.Api.Services.Interfaces;
using ParcelLockTest.Contract.Order;

namespace ParcelLockTest.Api.Controllers;

[ApiController]
[Route("v1/orders")]
public class OrderController : ControllerBase {
    readonly IOrderService _service;

    public OrderController(IOrderService service) {
        _service = service;
    }

    /// <summary>
    /// Детали заказа
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderGet>> Get(int id) {
        var result = await _service.Get(id);
        if (result == null) {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] OrderCreate model) {
        var orderId = await _service.Create(model);
        var uri = new Uri(Url.Action("Get", new { id = orderId }) ?? string.Empty);
        return Created(uri, orderId);
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Edit(int id, [FromBody] OrderUpdate model) {
        await _service.Update(id, model);
        return NoContent();
    }

    /// <summary>
    /// Отменить заказ
    /// </summary>
    /// <param name="id">Номер заказа</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderGet>> Delete(int id) {
        var order = await _service.Cancel(id);
        return Ok(order);
    }
}
