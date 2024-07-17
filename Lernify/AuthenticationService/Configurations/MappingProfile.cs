using AuthenticationService.Models.DTOs.User.Requests;
using AuthenticationService.Models.DTOs.User.Responses;
using AuthenticationService.Models.Entities;
using AuthenticationService.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping for RegisterDTO to User
        CreateMap<RegisterDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)))
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        // Mapping for LoginDTO to User (if needed)
        CreateMap<LoginDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // Mapping for User to UserResponseDTO
        CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));

        // Mapping for Role to RoleResponse
        CreateMap<Role, RoleResponseDTO>();
    }
}