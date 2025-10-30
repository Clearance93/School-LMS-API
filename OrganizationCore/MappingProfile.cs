using AutoMapper;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationModels;
using OrganizationModels.Model;

namespace OrganizationCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Teachers, TeachersDto>().ReverseMap();
            CreateMap<Learners, LearnersDto>().ReverseMap();
            CreateMap<Students, StudentDto>().ReverseMap();

            CreateMap<Guests, GuestsDto>().ReverseMap();
            CreateMap<Admins, AdminDto>().ReverseMap();
            CreateMap<StuffMembers, StuffMemberDto>().ReverseMap();

            CreateMap<CreateOrganizationDto, OrganizationSetup>()
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<ApplicationUser, Admins>()
                .ForMember(dest => dest.AdminId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuperAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<OrganizationSetup, OrganizationSetupDto>().ReverseMap().ReverseMap();

            CreateMap<UserDto, Admins>()
                .ForMember(dest => dest.AdminId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuperAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateOrganizationDto, OrganizationSetup>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateUserDto, ApplicationUser>()
                .ForMember(dest => dest.Id,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                            opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.UpdatedAt,
                            opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateAdminDto, Admins>()
            .ForMember(dest => dest.OrganizationSetupId,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuperAdmin, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateAdminDto, Admins>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateTeacherDto, Teachers>()
                .ForMember(dest => dest.TeacherId,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateTeacherDto, Teachers>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateLearnerDto, Learners>()
                .ForMember(dest => dest.LearnerId,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateLearnerDto, Learners>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateGuestDto, Guests>()
                .ForMember(dest => dest.GuestId,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateGuestDto, Guests>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateStuffMemberDto, StuffMembers>()
                .ForMember(dest => dest.StuffMemberId,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateStuffMemberDto, StuffMembers>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateStudentDto, Students>()
                .ForMember(dest => dest.StudentId,
                           opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted,
                           opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap().ReverseMap();

            CreateMap<UpdateStudentDto, Students>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();
        }
    }
}
