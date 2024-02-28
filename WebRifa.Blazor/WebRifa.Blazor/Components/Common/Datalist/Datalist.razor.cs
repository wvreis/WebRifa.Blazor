using Microsoft.AspNetCore.Components;

namespace WebRifa.Blazor.Components.Common.Datalist;
public partial class Datalist<TItem> {
    [Parameter, EditorRequired]
    public List<TItem> Items { get; set; } = new();

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
        SelectedItemValue = item?
            .GetType()?
            .GetProperty(PropertyName)?
            .GetValue(item)?
            .ToString() ?? string.Empty;

        ToggleItemsShow();
    }
    void ToggleItemsShow()
    {
        ShoulddShowItems = !ShoulddShowItems;
    }

    string GetShowldShowItemsCSSClass() =>
        ShoulddShowItems ? "show" : string.Empty;



    List<TItem> GetFilteredList() =>
       Items;
        //.Where(i =>
        //   i.Value
        //       .ToLowerInvariant()
        //       .Contains(SearchTerm))
        //   .ToList();

}