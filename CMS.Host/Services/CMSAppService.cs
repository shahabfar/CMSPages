using CMS.Entities.CMS;
using CMS.Services.Dtos;
using Volo.Abp.Application.Services;

namespace CMS.Services;

/* Inherit your application services from this class. */
public class CMSAppService : ApplicationService, ICMSAppService
{
    private readonly ICMSRepository _cmsRepository;
    private readonly CMSManager _cmsManager;

    public CMSAppService(ICMSRepository cmsRepository, CMSManager cmsManager)
    {
        _cmsRepository = cmsRepository;
        _cmsManager = cmsManager;
    }

    public async Task<CMSDto> GetAsync(Guid id)
    {
        var cms = await _cmsRepository.GetAsync(id);
        return ObjectMapper.Map<Entities.CMS.CMS, CMSDto>(cms);
    }

    public async Task<List<CMSDto>> GetAll()
    {
        var cmsList = await _cmsRepository.GetListAsync();
        return ObjectMapper.Map<List<Entities.CMS.CMS>, List<CMSDto>>(cmsList);
    }

    public async Task<CMSDto> CreateAsync(CreateUpdateCMSDto input)
    {
        var cms = await _cmsManager.CreateAsync(input.PageName, input.PageContent);
        await _cmsRepository.InsertAsync(cms);
        return ObjectMapper.Map<Entities.CMS.CMS, CMSDto>(cms);
    }

    public async Task UpdateAsync(Guid id, CreateUpdateCMSDto input)
    {
        var cms = await _cmsRepository.GetAsync(id);

        if (cms.PageName != input.PageName)
            await _cmsManager.ChangeNameAsync(cms, input.PageName);

        if (!string.IsNullOrEmpty(input.PageContent))
            await _cmsManager.ChangeContentAsync(cms, input.PageContent);
        await _cmsRepository.UpdateAsync(cms);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _cmsRepository.DeleteAsync(id);
    }
}