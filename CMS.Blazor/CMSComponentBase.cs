using CMS.Localization;
using Volo.Abp.AspNetCore.Components;

namespace CMS;

public abstract class CMSComponentBase : AbpComponentBase
{
    protected CMSComponentBase()
    {
        LocalizationResource = typeof(CMSResource);
    }
}
