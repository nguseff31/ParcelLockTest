using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcelLockTest.Api.Data;
using ParcelLockTest.Contract.ParcelLock;

namespace ParcelLockTest.Api.Controllers;

[ApiController]
[Route("parcel_locks")]
public class ParcelLockController : ControllerBase {
    readonly ParcelLockContext _context;
    readonly IMapper _mapper;

    public ParcelLockController(ParcelLockContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async IAsyncEnumerable<ParcelLockGet> GetAll() {
        var results = _context.ParcelLocks
            .OrderBy(p => p.Number)
            .ProjectTo<ParcelLockGet>(_mapper.ConfigurationProvider)
            .AsAsyncEnumerable();
        await foreach (var r in results) {
            yield return r;
        }
    }

    [HttpGet("{number}")]
    public async Task<ActionResult<ParcelLockGet>> Get(string number) {
        var result = await _context.ParcelLocks
            .ProjectTo<ParcelLockGet>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Number == number);
        if (result == null) {
            return NotFound();
        }
        return Ok(result);
    }
}
