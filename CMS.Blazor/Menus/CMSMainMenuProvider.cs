//using Volo.Abp.AspNetCore.Components.Web.LeptonXLiteTheme.Themes.LeptonXLite.Navigation;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.UI.Navigation;

//namespace CMS.Menus;

//[ExposeServices(typeof(MainMenuProvider))]
//public class CMSMainMenuProvider : MainMenuProvider
//{
//    private readonly IMenuManager _menuManager;

//    public CMSMainMenuProvider(IMenuManager menuManager/*, IObjectMapper<AbpAspNetCoreComponentsWebAssemblyLeptonXLiteThemeModule> objectMapper*/)
//        : base(menuManager)
//    {
//        _menuManager = menuManager;
//    }

//    public override async Task<MenuViewModel> GetMenuAsync()
//    {
//        var menu = await _menuManager.GetMainMenuAsync();

//        var result = new MenuViewModel
//        {
//            Menu = menu,
//            Items = menu.Items.Select(CreateMenuItemViewModel).ToList()
//        };

//        result.SetParents();

//        return result;
//    }

//    private MenuItemViewModel CreateMenuItemViewModel(ApplicationMenuItem applicationMenuItem)
//    {
//        var viewModel = new MenuItemViewModel
//        {
//            MenuItem = applicationMenuItem,
//            Items = new List<MenuItemViewModel>()
//        };

//        foreach (var item in applicationMenuItem.Items)
//        {
//            viewModel.Items.Add(CreateMenuItemViewModel(item));
//        }

//        return viewModel;
//    }

//    public async Task AddCMSMenuItemAsync()
//    {
//        var menu = await _menuManager.GetMainMenuAsync();

//        var cmsMenu = menu.FindMenuItem("CMS");

//        if (cmsMenu != null)
//        {
//            // Create a new menu item for CMS menu
//            var newItem = new ApplicationMenuItem(
//                "CMS.NewMenuItem",
//                "New Menu Item",
//                url: "/new-menu-item"
//            );

//            // Add the new menu item to the CMS menu
//            cmsMenu.AddItem(newItem);

//            // Update the menu
//            //await _menuManager.UpdateAsync(menu);
//        }
//    }
//}
