using Volo.Abp.Application.Dtos;

namespace CMS.Services.Dtos;

public class CMSDto : EntityDto<Guid>
{
    public string PageName { get; set; }
    public string PageContent { get; set; }
}