using AutoMapper;
using CMS.Services.Dtos;

namespace CMS;

public class CMSBlazorAutoMapperProfile : Profile
{
    public CMSBlazorAutoMapperProfile()
    {
        CreateMap<CMSDto, CreateUpdateCMSDto>();
    }
}
