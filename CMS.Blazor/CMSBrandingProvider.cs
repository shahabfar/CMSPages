using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CMS;

[Dependency(ReplaceServices = true)]
public class CMSBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CMS";
}
