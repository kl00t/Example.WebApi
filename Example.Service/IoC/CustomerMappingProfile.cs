using AutoMapper;
using Example.Service.Services.Requests;

namespace Example.Service.IoC
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // from the data model to the service model.
            CreateMap<Data.Models.Customer, Models.Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            // from the request to the data model.
            CreateMap<AddCustomerRequest, Data.Models.Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}