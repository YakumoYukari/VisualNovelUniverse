using System;

namespace Visual_Novel_Universe
{
    public static class PathUtils
    {
        public static bool IsValidUrl(string Url)
        {
            Uri uriResult;
            return Uri.TryCreate(Url, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        }
        public static string GetUri(this string s)
        {
            return new UriBuilder(s).Uri.AbsoluteUri;
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
