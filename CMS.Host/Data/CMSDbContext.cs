using CMS.Entities.CMS;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace CMS.Data;

public class CMSDbContext : AbpDbContext<CMSDbContext>
{
    public CMSDbContext(DbContextOptions<CMSDbContext> options)
        : base(options)
    {
    }

    public DbSet<Entities.CMS.CMS> CMSs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own entities here */
        builder.Entity<Entities.CMS.CMS>(b =>
        {
            b.ToTable("App" + "CMSs");
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.PageName).IsRequired().HasMaxLength(CMSConsts.MaxPageNameLength);
            b.Property(x => x.PageContent).HasMaxLength(CMSConsts.MaxPageNameLength);
        });
    }
}
