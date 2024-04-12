using AutoMapper;
using CMS.Services.Dtos;

namespace CMS.ObjectMapping;

public class CMSAutoMapperProfile : Profile
{
    public CMSAutoMapperProfile()
    {
        CreateMap<CMSDto, Entities.CMS.CMS>();
        CreateMap<Entities.CMS.CMS, CMSDto>();
        CreateMap<CreateUpdateCMSDto, Entities.CMS.CMS>();
    }
}
