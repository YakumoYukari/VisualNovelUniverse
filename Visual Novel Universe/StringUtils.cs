using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Visual_Novel_Universe
{
    public static class StringUtils
    {
        public static string SafeForUrl(this string s)
        {
            return HttpUtility.UrlEncode(s);
        }

        public static bool Equals(string s1, string s2, bool CaseSensitive = false)
        {
            return CaseSensitive
                ? string.Equals(s1, s2)
                : string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static void TrimAll(ref string[] List)
        {
            for (int i = 0; i < List.Length; i++)
            {
                List[i] = List[i].Trim();
            }
        }

        public static string ClearAllBracketGroups(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = ClearBrackets(s, '(', ')');
            s = ClearBrackets(s, '[', ']');
            s = ClearBrackets(s, '【', '】');
            //s = ClearBrackets(s, '{', '}');
            return s.Trim();
        }

        public static string ClearBrackets(string text, char OpenBracket, char CloseBracket)
        {
            const int MAX_LOOPS = 10;
            int loopcount = 0;

            int count = 0;
            int TargetIndex;
            while ((TargetIndex = text.IndexOf(OpenBracket)) >= 0)
            {
                loopcount++;
                if (text.IndexOf(CloseBracket) < 0 || loopcount > MAX_LOOPS) break;

                for (int i = TargetIndex; i < text.Length; i++)
                {
                    if (text[i] == OpenBracket) count++;
                    if (text[i] == CloseBracket) count--;
                    if (count != 0) continue;

                    text = text.Remove(TargetIndex, i - TargetIndex + 1);
                    break;
                }
            }
            return text;
        }

        public static string FixUrlCharacters(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text
                .Replace("&amp;", "&")
                .Replace("&quot;", "\"");
        }

        public static string ConvertHrefToText(string Text)
        {
            return Regex.Replace(Text, @"<\s*a\s.*?href\s*=\s*['""](.*?)['""].*?>(.*?)</\s*a\s*>", "$2: $1");
        }

        public static string TruncateString(string Input, int MaxLength, string Truncation = "…")
        {
            return Input.Length > MaxLength ? Input.Substring(0, MaxLength - Truncation.Length) + Truncation : Input;
        }

        public static string ConvertToPathSafe(string FolderName)
        {
            return FixUrlCharacters(FolderName)
                .Replace(".", "．")
                .Replace("/", "／")
                .Replace("\"", "'")
                .Replace("<", "＜")
                .Replace(">", "＞")
                .Replace("|", "｜")
                .Replace("*", "＊")
                .Replace("?", "？")
                .Replace(":", "：")
                .Trim();
        }

        public static string ConvertFromPathSafe(string FolderName)
        {
            return FolderName.Replace("．", ".")
                .Replace("／", "/")
                .Replace("'", "\"")
                .Replace("＜", "<")
                .Replace("＞", ">")
                .Replace("｜", "|")
                .Replace("＊", "*")
                .Replace("？", "?")
                .Replace("：", ":")
                .Trim();
        }
    }
}
