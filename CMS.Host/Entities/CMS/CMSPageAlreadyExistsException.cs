using Volo.Abp;

namespace CMS.Entities.CMS;

public class CMSPageAlreadyExistsException : BusinessException
{
    public CMSPageAlreadyExistsException(string name)
        : base(DomainErrorCodes.CMSPageAlreadyExists)
    {
        WithData("name", name);
    }
}