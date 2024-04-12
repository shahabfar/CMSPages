using CMS.Entities.CMS;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CMS.Data;

public class EfCoreCMSRepository : EfCoreRepository<CMSDbContext, Entities.CMS.CMS, Guid>, ICMSRepository
{
    public EfCoreCMSRepository(IDbContextProvider<CMSDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Entities.CMS.CMS>> GetAll()
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.ToListAsync();
    }

    public async Task<Entities.CMS.CMS> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Entities.CMS.CMS> FindByNameAsync(string pageName)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(author => author.PageName == pageName);
    }

    public async Task<Entities.CMS.CMS> InsertOrUpdateCMSContent(Entities.CMS.CMS cms)
    {
        throw new NotImplementedException();
    }
}