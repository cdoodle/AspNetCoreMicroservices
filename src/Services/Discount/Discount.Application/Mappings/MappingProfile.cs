using AutoMapper;
using Discount.Application.Models;
using Discount.Core.Entities;

namespace Discount.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<UpdatedCouponDto, Coupon>();
            //CreateMap<CreateCouponDto, Coupon>().ForMember(d => d.Id, o => o.Ignore()); ;
            CreateMap<CreateCouponDto, Coupon>();
        }
    }
}
