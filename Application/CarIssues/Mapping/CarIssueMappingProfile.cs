using Application.CarIssues.Commands;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace Application.CarIssues.Mapping
{
    public class CarIssueMappingProfile : Profile
    {
        public CarIssueMappingProfile()
        {

            CreateMap<Domain.Models.CarIssue, Dtos.CarIssueDto>()
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car.CarId));

            CreateMap<Dtos.CarIssueDto, Domain.Models.CarIssue>()
                .ForMember(dest => dest.Car, opt => opt.Ignore());

            CreateMap<Commands.UpdateCarIssueCommand, Domain.Models.CarIssue>()
                .ForMember(dest => dest.CarIssueId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Car, opt => opt.Ignore());
            
            CreateMap<CreateCarIssueCommand, CarIssue>()
                .ForMember(dest => dest.CarIssueId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Car, opt => opt.Ignore());

        }
    }
}