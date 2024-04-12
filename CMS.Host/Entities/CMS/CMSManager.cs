using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace CMS.Entities.CMS;

public class CMSManager : DomainService
{
    private readonly ICMSRepository _cmsRepository;

    public CMSManager(ICMSRepository cmsRepository)
    {
        _cmsRepository = cmsRepository;
    }

    public async Task<CMS> CreateAsync(string pageName, string pageContent = "")
    {
        Check.NotNullOrWhiteSpace(pageName, nameof(pageName));

        var existingPage = await _cmsRepository.FindByNameAsync(pageName);
        if (existingPage != null)
            throw new CMSPageAlreadyExistsException(pageName);

        return new CMS(GuidGenerator.Create(), pageName, pageContent);
    }

    public async Task ChangeNameAsync(CMS cms, string newName)
    {
        Check.NotNull(cms, nameof(cms));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingPage = await _cmsRepository.FindByNameAsync(newName);
        if (existingPage != null && existingPage.Id != cms.Id)
            throw new CMSPageAlreadyExistsException(newName);

        cms.ChangeName(newName);
    }

    public async Task ChangeContentAsync(CMS cms, string newContent)
    {
        Check.NotNull(cms, nameof(cms));
        Check.NotNullOrWhiteSpace(newContent, nameof(newContent));

        var existingPage = await _cmsRepository.FindByNameAsync(newContent);
        if (existingPage != null && existingPage.Id != cms.Id)
            throw new CMSPageAlreadyExistsException(newContent);

        cms.ChangeContent(newContent);
    }

}