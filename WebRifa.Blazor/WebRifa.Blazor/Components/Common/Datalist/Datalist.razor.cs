using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebRifa.Blazor.Components.Common.Datalist;
public partial class Datalist {
    [Parameter]
    public string Placeholder { get; set; } = "Pesquisar...";

    public string SearchTerm { get; set; } = string.Empty;
    public Item? SelectedItem { get; set; }
    public string SelectedItemValue { 
        get {
            return SelectedItem?.Value ?? string.Empty;
        }
        set { }
    }
    
    public List<Item> Items { get; set; } = new() {
        new("item1ID", "Item 1"),
        new("item2ID", "Item 2"),
        new("item3ID", "Item 3"),
        new("item4ID", "Item 4"),
    };

    void OnInputKeyUp(ChangeEventArgs args)
    {
        SearchTerm = args?.Value?
            .ToString()?
            .ToLowerInvariant() ?? string.Empty;
    }

    void OnClickItem(Item item)
    {
        SelectedItem = item;
    }

    List<Item> GetFilteredList() =>
        Items.Where(i =>
            i.Value
                .ToLowerInvariant()
                .Contains(SearchTerm))
            .ToList();
}

public record Item(string Key, string Value);