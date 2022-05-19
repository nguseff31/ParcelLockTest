using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ParcelLockTest.Api.Data;
using ParcelLockTest.Api.Services.Interfaces;
using ParcelLockTest.Contract.ParcelLock;

namespace ParcelLockTest.Api.Services;

public class ParcelLockService : IParcelLockService {
    readonly ParcelLockContext _context;
    readonly IMapper _mapper;

    public ParcelLockService(ParcelLockContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ParcelLockGet?> Get(string number) {
        var result = await _context.ParcelLocks
            .ProjectTo<ParcelLockGet>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Number == number);
        return result;
    }

    public IAsyncEnumerable<ParcelLockGet> GetAll() {
        return _context.ParcelLocks
            .OrderBy(p => p.Number)
            .ProjectTo<ParcelLockGet>(_mapper.ConfigurationProvider)
            .AsAsyncEnumerable();
    }
}
