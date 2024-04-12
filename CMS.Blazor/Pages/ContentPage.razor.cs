using CMS.Services.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Syncfusion.Blazor.RichTextEditor;

namespace CMS.Pages;

public partial class ContentPage
{
    [Parameter]
    public string Id { get; set; }
    private CMSDto CMSDto { get; set; }

    SfRichTextEditor RteObj;

    protected override async Task OnInitializedAsync()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
        await LoadCMSDtoAsync();
    }

    private async void HandleLocationChanged(object sender, LocationChangedEventArgs e)
    {
        // We have to do this in order to Blazor detects the state change and re-render the component.
        await LoadCMSDtoAsync();
    }

    private async Task LoadCMSDtoAsync()
    {
        if (!Guid.TryParse(Id, out var id))
        {
            NavigationManager.NavigateTo("/");
            return;
        }

        CMSDto = await CMSAppService.GetAsync(id);
        StateHasChanged();
    }

    private async Task SaveContent()
    {
        CMSDto.PageContent = await RteObj.GetXhtmlAsync();
        await CMSAppService.UpdateAsync(CMSDto.Id, new CreateUpdateCMSDto { PageName = CMSDto.PageName, PageContent = CMSDto.PageContent });
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= HandleLocationChanged;
    }
}