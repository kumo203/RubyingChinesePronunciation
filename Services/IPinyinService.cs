namespace _pinyin_ruby.Services;

public interface IPinyinService
{
    List<RubyToken> GetPinyin(string text);
}
