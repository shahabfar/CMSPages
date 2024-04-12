using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace CMS.Entities.CMS;

public class CMS : BasicAggregateRoot<Guid>
{
    [Required]
    [StringLength(CMSConsts.MaxPageNameLength)]
    public string PageName { get; private set; }

    [Required]
    [StringLength(CMSConsts.MaxPageContentLength)]
    public string PageContent { get; private set; }

    protected CMS()
    {
    }

    public CMS(Guid id, [NotNull] string pageName, string pageContent)
        : base(id)
    {
        PageName = pageName;
        PageContent = pageContent;
    }

    public static CMS InsertOrUpdate(Guid id, string pageName, string pageContent)
    {
        var cms = new CMS
        {
            Id = id,
            PageName = Check.NotNullOrWhiteSpace(pageName, nameof(pageName), maxLength: CMSConsts.MaxPageNameLength),
            PageContent = Check.NotNullOrWhiteSpace(pageContent, nameof(pageContent), maxLength: CMSConsts.MaxPageContentLength)
        };

        return cms;
    }

    public void ChangeName(string newName)
    {
        PageName = newName;
    }

    public void ChangeContent(string newContent)
    {
        PageContent = newContent;
    }

}