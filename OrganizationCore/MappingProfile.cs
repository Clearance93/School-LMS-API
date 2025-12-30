using AutoMapper;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Communication;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.Settings;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationModels;
using OrganizationModels.Model;
using OrganizationModels.Model.Communication;
using OrganizationModels.Model.Settings;

namespace OrganizationCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            CreateMap<CreateUserDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<OrganizationSetup, OrganizationSetupDto>().ReverseMap();

            CreateMap<CreateOrganizationDto, OrganizationSetup>()
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateOrganizationDto, OrganizationSetup>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Admins, AdminDto>().ReverseMap();

            CreateMap<ApplicationUser, Admins>()
                .ForMember(dest => dest.AdminId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuperAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserDto, Admins>()
                .ForMember(dest => dest.AdminId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuperAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateAdminDto, Admins>()
                .ForMember(dest => dest.AdminId, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationSetupId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuperAdmin, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateAdminDto, Admins>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Teachers, TeachersDto>().ReverseMap();

            CreateMap<CreateTeacherDto, Teachers>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateTeacherDto, Teachers>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Learners, LearnersDto>().ReverseMap();

            CreateMap<CreateLearnerDto, Learners>()
                .ForMember(dest => dest.LearnerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateLearnerDto, Learners>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Students, StudentDto>().ReverseMap();

            CreateMap<CreateStudentDto, Students>()
                .ForMember(dest => dest.StudentId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateStudentDto, Students>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Guests, GuestsDto>().ReverseMap();

            CreateMap<CreateGuestDto, Guests>()
                .ForMember(dest => dest.GuestId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateGuestDto, Guests>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<StuffMembers, StuffMemberDto>().ReverseMap();

            CreateMap<CreateStuffMemberDto, StuffMembers>()
                .ForMember(dest => dest.StuffMemberId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateStuffMemberDto, StuffMembers>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Grade, GradeDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GradeWithStreamDto, Grade>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()).ReverseMap();

            CreateMap<GradeStream, StreamGradeDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GradeWithStreamDto, GradeStream>()
                .ForMember(dest => dest.StreamId, opt => opt.Ignore())
                .ForMember(dest => dest.GradeId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())      
                .ReverseMap();

            CreateMap<SchoolAdminSettings, SchoolAdminSettingsDto>().ReverseMap();

            CreateMap<SchoolAdminSettingsDto, SchoolAdminSettings>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ContactEmail, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CourseStreams, CourseStreamDto>().ReverseMap();

            CreateMap<SchoolSubjects, SchoolSubjectDto>().ReverseMap();

            CreateMap<SchoolSubjects, SchoolSubjectDto>()
                .ForMember(dest => dest.CourseStreamId, opt => opt.Ignore()).ReverseMap();

            CreateMap<ExamGradeScale, ExamGradeScaleDto>().ReverseMap();

            CreateMap<ExamGradeScaleDto, ExamGradeScale>()
                .ForMember(dest => dest.ExamGradeScaleId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<LibraryItem, LibraryItemsDto>().ReverseMap();

            CreateMap<LibraryItemsDto, LibraryItem>()
                .ForMember(dest => dest.LibraryId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegistrrationLink, GeneretingRegistrationLinkDto>().ReverseMap();

            CreateMap<RegistrrationLink, GeneretingRegistrationLinkDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Message, BroadcastMessageDto>().ReverseMap();

            CreateMap<Message, BroadcastMessageDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Message, BroadcastMessageDto>().ReverseMap();

            CreateMap<Message, MessagesDto>()
                .ForMember(dest => dest.TimeStamp, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Message, BroadcastMessageDto>().ReverseMap();

            CreateMap<Message, CreateMessageDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}