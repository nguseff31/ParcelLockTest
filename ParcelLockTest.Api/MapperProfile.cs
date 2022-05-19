using AutoMapper;
using ParcelLockTest.Api.Entities;
using ParcelLockTest.Contract.Order;
using ParcelLockTest.Contract.ParcelLock;

namespace ParcelLockTest.Api;

public class MapperProfile : Profile {
    public MapperProfile() {
        CreateMap<OrderDto, OrderGet>();
        CreateMap<ParcelLockDto, ParcelLockGet>();
        CreateMap<OrderCreate, OrderDto>();
        CreateMap<OrderUpdate, OrderDto>();
    }
}
