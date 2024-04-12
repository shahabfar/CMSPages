using CMS.Blazor.Menus;
using CMS.Localization;
using CMS.MultiTenancy;
using CMS.Services;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace CMS.Menus;

public class CMSMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public CMSMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
            await ConfigureMainMenuAsync(context);

        else if (context.Menu.Name == StandardMenus.User)
            await ConfigureUserMenuAsync(context);
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CMSResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                CMSMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );

        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        var cmsMenu = new ApplicationMenuItem(
            "CMS",
            l["Menu:CMS"],
            icon: "fa fa-database"
        );

        cmsMenu.AddItem(
            new ApplicationMenuItem(
                "CMS.CMSPages",
                l["Menu:CMSPages"],
                url: "/pages"
            )
        );

        var appService = context.ServiceProvider.GetRequiredService<ICMSAppService>();
        var items = await appService.GetAll();
        foreach (var item in items)
        {
            cmsMenu.AddItem(
                new ApplicationMenuItem(
                    $"CMS.{item.PageName}",
                    item.PageName,
                    url: $"/content/{item.Id}"
                )
            );
        }

        context.Menu.AddItem(cmsMenu);
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
