using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcelLockTest.Api.Common;
using ParcelLockTest.Api.Data;
using ParcelLockTest.Api.Entities;
using ParcelLockTest.Api.Services.Interfaces;
using ParcelLockTest.Contract.Order;

namespace ParcelLockTest.Api.Services;

public class OrderService : IOrderService {
    readonly ParcelLockContext _context;
    readonly IMapper _mapper;

    public OrderService(ParcelLockContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderGet?> Get(int id) {
        var result = await _context.Orders.Where(o => o.Id == id)
            .ProjectTo<OrderGet>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        if (result == null) {
            throw Errors.Order.NotFound(id);
        }

        return result;
    }

    public async Task<int> Create(OrderCreate model) {
        var isParcelLockActive = await _context.ParcelLocks
            .Where(p => p.Number == model.ParcelLockNumber)
            .Select(p => (bool?)p.IsActive)
            .FirstOrDefaultAsync();
        if (!isParcelLockActive.HasValue) {
            throw Errors.Order.ParcelLockNotFound(model.ParcelLockNumber);
        }

        if (!isParcelLockActive.Value) {
            throw Errors.Order.ParcelLockNotActive(model.ParcelLockNumber);
        }

        var order = _mapper.Map<OrderDto>(model);
        _context.Add(order);
        await _context.SaveChangesAsync();
        return order.Id;
    }

    public async Task<ActionResult<OrderGet>> Update(int id, [FromBody] OrderUpdate model) {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) {
            throw Errors.Order.NotFound(id);
        }

        _mapper.Map(model, order);
        return _mapper.Map<OrderGet>(order);
    }

    /// <summary>
    /// Cancel order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OrderGet> Cancel(int id) {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) {
            throw Errors.Order.NotFound(id);
        }

        order.SetStatus(OrderStatus.Canceled);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderGet>(order);
    }
}
