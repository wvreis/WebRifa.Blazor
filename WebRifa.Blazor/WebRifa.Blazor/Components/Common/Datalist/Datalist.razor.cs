using Microsoft.AspNetCore.Components;

namespace WebRifa.Blazor.Components.Common.Datalist;
public partial class Datalist {
    [Parameter]
    public string Placeholder { get; set; } = "Item...";
    [Parameter]    
    public List<Item> Items { get; set; } = new();
    public string SearchTerm { get; set; } = string.Empty;
    public Item? SelectedItem { get; set; }
    public string SelectedItemValue { get; set; } = string.Empty;

    bool ShoulddShowItems { get; set; }



    void OnInputKeyUp(ChangeEventArgs args)
    {
        SearchTerm = args?.Value?
            .ToString()?
            .ToLowerInvariant() ?? string.Empty;
    }

    void OnClickItem(Item item)
    {
        SelectedItem = item;
        SelectedItemValue = item?.Value ?? string.Empty;
        ToggleItemsShow();
    }
    void ToggleItemsShow()
    {
        ShoulddShowItems = !ShoulddShowItems;
    }

    string GetShowldShowItemsCSSClass() =>
        ShoulddShowItems ? "show" : string.Empty;

    List<Item> GetFilteredList() =>
        Items.Where(i =>
            i.Value
                .ToLowerInvariant()
                .Contains(SearchTerm))
            .ToList();

}

public record Item(string Key, string Value);