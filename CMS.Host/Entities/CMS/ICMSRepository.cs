using Volo.Abp.Domain.Repositories;

namespace CMS.Entities.CMS;

public interface ICMSRepository : IRepository<CMS, Guid>
{
    Task<List<CMS>> GetAll();
    Task<CMS> FindByIdAsync(Guid id);
    Task<CMS> FindByNameAsync(string pageName);
    Task<CMS> InsertOrUpdateCMSContent(CMS cms);
}