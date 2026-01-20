using Pinyin.NET;
using System.Linq;

namespace _pinyin_ruby.Services;

public class PinyinService : IPinyinService
{
    public List<RubyToken> GetPinyin(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new List<RubyToken>();
        }

        var processor = new PinyinProcessor(PinyinFormat.WithToneMark);
        // Use withZhongWen: true to get the original text segments
        var pinyinResult = processor.GetPinyin(text, withZhongWen: true);

        var tokens = new List<RubyToken>();
        
        // Loop through SplitWords and Keys concurrently
        if (pinyinResult.SplitWords != null && pinyinResult.Keys != null)
        {
            var words = pinyinResult.SplitWords.ToList();
            var keys = pinyinResult.Keys.ToList();
            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];
                var pinyins = keys[i];
                
                // If pinyins list is not empty, use the first one.
                // Assuming empty pinyins means no pinyin available (like punctuation or english if not handled?)
                // Readme says "Windows" -> ["windows"]. So English returns itself as pinyin usually?
                // Let's check logic: if pinyin is same as word (case insensitive?), maybe it's not ruby?
                // But for simplicity, let's treat everything that has pinyin as ruby if it differs or if it's Chinese.
                // Actually, "Windows" -> "windows" implies we don't need ruby tag for it, or we do?
                // The user wants "Pinyin ruby web page". Usually English doesn't need ruby.
                // PinyinM.Net handles mixed text.
                
                string pinyin = (pinyins != null && pinyins.Count > 0) ? pinyins[0] : "";
                
                // Determine if we should show ruby
                // Simplistic check: if the word contains Chinese characters.
                // But PinyinProcessor handles segmenting.
                // If "Windows" -> "windows", showing "windows" above "Windows" is redundant.
                
                bool isChinese = IsChinese(word);
                
                tokens.Add(new RubyToken
                {
                    Text = word,
                    Pinyin = pinyin,
                    IsRuby = isChinese
                });
            }
        }
        
        return tokens;
    }

    private bool IsChinese(string text)
    {
        foreach (var c in text)
        {
            if (c >= 0x4e00 && c <= 0x9fbb) return true;
        }
        return false;
    }
}
