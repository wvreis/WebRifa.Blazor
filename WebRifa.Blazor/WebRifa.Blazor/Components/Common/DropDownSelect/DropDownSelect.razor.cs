using Microsoft.AspNetCore.Components;

namespace WebRifa.Blazor.Components.Common.DropDownSelect;
public partial class DropDownSelect<TItem> {
    [Parameter, EditorRequired]
    public List<TItem>? Items { get; set; }

    [Parameter, EditorRequired]
    public string PropertyName { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public EventCallback Callback { get; set; }

    [Parameter]
    public string Placeholder { get; set; } = "Item...";
    public string SearchTerm { get; set; } = string.Empty;
    public TItem? SelectedItem { get; set; }
    public string SelectedItemValue { get; set; } = string.Empty;

    bool ShoulddShowItems { get; set; }

    void OnInputKeyUp(ChangeEventArgs args)
    {
        SearchTerm = args?.Value?
            .ToString()?
            .ToLowerInvariant() ?? string.Empty;
    }

    public async Task OnClickItem(TItem item)
    {
        SelectedItem = item;
        SelectedItemValue = GetPropValue(item);
        await Callback.InvokeAsync(item);

        await HideItems();
    }

    void ShowItems()
    {
        ShoulddShowItems = true;
    }

    async Task HideItems()
    {
        if (string.IsNullOrEmpty(SelectedItemValue)) {
            SelectedItem = default;
            await Callback.InvokeAsync(null);
        }

        if (SelectedItem is null) {
            await Task.Delay(200);
        }

        ShoulddShowItems = false;
        StateHasChanged();
    }

    string GetShowldShowItemsCSSClass() =>
        ShoulddShowItems ? "show" : string.Empty;

    private string GetPropValue(TItem item)
    {
        return item?
            .GetType()?
            .GetProperty(PropertyName)?
            .GetValue(item)?
            .ToString() ?? string.Empty;
    }

    List<TItem> GetFilteredList() =>
        Items?
    .Where(i =>
       GetPropValue(i)
           .ToLowerInvariant()
           .Contains(SearchTerm))
       .ToList() ?? [];
}