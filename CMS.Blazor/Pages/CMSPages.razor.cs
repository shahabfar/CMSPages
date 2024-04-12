using Blazorise;
using Blazorise.DataGrid;
using CMS.Services.Dtos;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.UI.Navigation;

namespace CMS.Pages;

public partial class CMSPages
{
    private IReadOnlyList<CMSDto> CMSList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateUpdateCMSDto NewCMS { get; set; }

    private Guid EditingCMSId { get; set; }
    private CreateUpdateCMSDto EditingCMS { get; set; }

    private Modal CreateCMSModal { get; set; }
    private Modal EditCMSModal { get; set; }

    private Validations CreateValidationsRef;

    private Validations EditValidationsRef;

    public CMSPages()
    {
        NewCMS = new CreateUpdateCMSDto();
        EditingCMS = new CreateUpdateCMSDto();
    }

    private async Task GetAllCMSAsync()
    {
        CMSList = await CMSAppService.GetAll();
        TotalCount = CMSList.Count;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CMSDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetAllCMSAsync();
        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateCMSModal()
    {
        CreateValidationsRef.ClearAll();

        NewCMS = new CreateUpdateCMSDto();
        CreateCMSModal.Show();
    }

    private void CloseCreateCMSModal()
    {
        CreateCMSModal.Hide();
    }

    private void OpenEditCMSModal(CMSDto cms)
    {
        EditValidationsRef.ClearAll();

        EditingCMSId = cms.Id;
        EditingCMS = ObjectMapper.Map<CMSDto, CreateUpdateCMSDto>(cms);
        EditCMSModal.Show();
    }

    private async Task DeleteCMSAsync(CMSDto cms)
    {
        var confirmMessage = L["CMSDeletionConfirmationMessage", cms.PageName];
        if (!await Message.Confirm(confirmMessage))
            return;

        await CMSAppService.DeleteAsync(cms.Id);
        await GetAllCMSAsync();
        await RefreshMenuItemsAsync();
    }

    private void CloseEditCMSModal()
    {
        EditCMSModal.Hide();
    }

    private async Task CreateCMSAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            await CMSAppService.CreateAsync(NewCMS);
            await GetAllCMSAsync();
            CreateCMSModal.Hide();
            await RefreshMenuItemsAsync();
        }
    }

    private async Task UpdateCMSAsync()
    {
        if (await EditValidationsRef.ValidateAll())
        {
            await CMSAppService.UpdateAsync(EditingCMSId, EditingCMS);
            await GetAllCMSAsync();
            EditCMSModal.Hide();
            await RefreshMenuItemsAsync();
        }
    }

    private async Task RefreshMenuItemsAsync()
    {
        // Get the CMS menu
        var mainMenu = await MenuManager.GetMainMenuAsync();
        var cmsMenu = mainMenu.FindMenuItem("CMS");

        // Clear the existing menu items
        cmsMenu.Items.Clear();

        // Add the new menu items
        foreach (var item in CMSList)
        {
            cmsMenu.AddItem(
                new ApplicationMenuItem(
                    $"CMS.{item.PageName}",
                    item.PageName,
                    url: $"/content/{item.Id}"
                )
            );
        }

        NavigationManager.NavigateTo(NavigationManager.Uri, true);
    }
}