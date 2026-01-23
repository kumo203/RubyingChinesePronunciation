namespace _pinyin_ruby.Services;

public interface IHistoryService
{
    Task InitializeAsync();
    Task<List<ConversionItem>> GetHistoryAsync();
    Task AddToHistoryAsync(string inputText);
    Task RemoveFromHistoryAsync(int id);
    Task ClearHistoryAsync();
}
