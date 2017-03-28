using System;
using Core.Utils;

namespace Visual_Novel_Universe
{
    public static class PathUtils
    {
        public static bool IsValidUrl(string Url)
        {
            Uri UriResult;
            return Uri.TryCreate(Url, UriKind.Absolute, out UriResult)
                && UriResult.Scheme == Uri.UriSchemeHttp || UriResult.Scheme == Uri.UriSchemeHttps;
        }
        public static string GetUri(this string S)
        {
            return new UriBuilder(S).Uri.AbsoluteUri;
        }

        public static string ConvertToPathSafe(string FolderName)
        {
            return StringUtils.FixUrlCharacters(FolderName)
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

        public static string LastElementOfPath(string Filepath)
        {
            return Filepath.Substring(Filepath.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
        }

        public static string OneLevelUp(string Filepath)
        {
            int Index = Filepath.LastIndexOf(@"\", StringComparison.Ordinal);
            return Index < 0 ? Filepath : Filepath.Substring(0, Index);
        }
    }
}
