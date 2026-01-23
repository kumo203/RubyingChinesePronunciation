using System.Text.Json;
using Microsoft.JSInterop;

namespace _pinyin_ruby.Services;

public class HistoryService : IHistoryService
{
    private readonly IJSRuntime _jsRuntime;
    private const string StorageKey = "pinyinRubyHistory";
    private const int MaxHistoryItems = 50;
    private List<ConversionItem> _history = new();

    public HistoryService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                _history = JsonSerializer.Deserialize<List<ConversionItem>>(json) ?? new();
            }
            else
            {
                _history = new();
            }
        }
        catch
        {
            _history = new();
        }
    }

    public async Task<List<ConversionItem>> GetHistoryAsync()
    {
        return await Task.FromResult(_history);
    }

    public async Task AddToHistoryAsync(string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
            return;

        // Check if identical text exists at the top of history
        if (_history.Count > 0 && _history[0].InputText == inputText.Trim())
            return;

        var newItem = new ConversionItem
        {
            InputText = inputText.Trim(),
            Timestamp = DateTime.Now,
            Id = _history.Count > 0 ? _history.Max(h => h.Id) + 1 : 1
        };

        _history.Insert(0, newItem);

        // Keep only the most recent 50 items
        if (_history.Count > MaxHistoryItems)
        {
            _history = _history.Take(MaxHistoryItems).ToList();
        }

        await SaveHistoryAsync();
    }

    public async Task RemoveFromHistoryAsync(int id)
    {
        _history.RemoveAll(h => h.Id == id);
        await SaveHistoryAsync();
    }

    public async Task ClearHistoryAsync()
    {
        _history.Clear();
        await SaveHistoryAsync();
    }

    private async Task SaveHistoryAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_history);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
        catch
        {
            // Silently handle localStorage errors
        }
    }
}
