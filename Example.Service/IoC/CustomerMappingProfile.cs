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
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(opt => opt.Ignore());

            // from the request to the client user model.
            CreateMap<AddCustomerRequest, Client.Models.User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => "Joe"))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "MyCurrentPassword"))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}