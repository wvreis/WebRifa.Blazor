using Microsoft.AspNetCore.Components;

namespace WebRifa.Blazor.Components.Common.DropDownSelect;
public partial class DropDownSelect<TItem> {
    [Parameter, EditorRequired]
    public List<TItem>? Items { get; set; }

    [Parameter, EditorRequired]
    public string PropertyName { get; set; } = string.Empty;

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

    void OnClickItem(TItem item)
    {
        SelectedItem = item;
        SelectedItemValue = GetPropValue(item);

        ToggleItemsShow();
    }

    void ToggleItemsShow()
    {
        ShoulddShowItems = !ShoulddShowItems;
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