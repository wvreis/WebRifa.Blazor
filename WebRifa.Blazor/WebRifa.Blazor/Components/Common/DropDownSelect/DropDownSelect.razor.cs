using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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

    int currentFocusIndexItem = default;

    bool ShoulddShowItems { get; set; }

    void OnInputKeyUp(ChangeEventArgs args)
    {
        SearchTerm = args?.Value?
            .ToString()?
            .ToLowerInvariant() ?? string.Empty;
    }

    public async Task SelectItem(TItem item)
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

    async Task OnDropDownOnKeyUp(KeyboardEventArgs args)
    {
        bool shouldIncrement = 
            args.Key == "ArrowDown" && 
            currentFocusIndexItem < GetFilteredList().Count() - 1;
        
        if (shouldIncrement) {
            ShowItems();
            currentFocusIndexItem++;
        }

        bool shouldDecrement =
            args.Key == "ArrowUp" &&
            currentFocusIndexItem > 0;

        if (shouldDecrement) {
            ShowItems();
            currentFocusIndexItem--;
        }

        if (args.Key == "Enter") {
            var item = GetItemFromIndex(currentFocusIndexItem);
            await SelectItem(item);
        }
    }

    void OnMouseOverIndex(int index) =>
        currentFocusIndexItem = index;

    string GetCSSClassInputFocus() =>
        ShoulddShowItems ? "show" : string.Empty;

    string GetPropValue(TItem item)
    {
        return item?
            .GetType()?
            .GetProperty(PropertyName)?
            .GetValue(item)?
            .ToString() ?? string.Empty;
    }

    string GetFocusCSSClass(int index) =>
        index == currentFocusIndexItem ?
        "input-focus" :
        string.Empty;

    int GetIndexFromItem(TItem item)
    {
        return GetFilteredList().IndexOf(item);
    }

    TItem GetItemFromIndex(int index)
    {
        return GetFilteredList()
            .Single(i => GetFilteredList().IndexOf(i) == index);
    }

    List<TItem> GetFilteredList() =>
        Items?
    .Where(i =>
       GetPropValue(i)
           .ToLowerInvariant()
           .Contains(SearchTerm))
       .ToList() ?? [];
}