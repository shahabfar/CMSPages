using CMS.Services.Dtos;
using Volo.Abp.Application.Services;

namespace CMS.Services;

public interface ICMSAppService : IApplicationService
{
    Task<CMSDto> GetAsync(Guid id);

    Task<List<CMSDto>> GetAll();

    Task<CMSDto> CreateAsync(CreateUpdateCMSDto input);

    Task UpdateAsync(Guid id, CreateUpdateCMSDto input);

    Task DeleteAsync(Guid id);
}