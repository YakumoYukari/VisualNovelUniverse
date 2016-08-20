using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    public static class FolderTools
    {
        public static bool RenameVn(VisualNovel Vn, string NewName)
        {
            NewName = PathUtils.ConvertToPathSafe(NewName);
            return MoveVn(Vn, Path.Combine(Vn.BaseFolderPath, NewName));
        }

        private static bool MoveVn(VisualNovel Vn, string ToDir)
        {
            //Check that the drive letter is the same, because we can't move across volumes
            if (Vn.VnFolderPath.Substring(0,2) != ToDir.Substring(0, 2))
            {
                Logger.Instance.LogError($"Attempted to move VN across volumes: [{Vn.VnFolderPath}] to [{ToDir}]");
                return false;
            }

            bool Success = Move(Vn.VnFolderPath, ToDir);
            if (!Success) return false;

            Vn.VnFolderPath = ToDir;
            VisualNovelLoader.Save(Vn);
            return true;
        }

        private static bool Move(string FromDir, string ToDir)
        {
            if (FromDir == ToDir)
                return false;
            
            try
            {
                var FromDirInfo = new DirectoryInfo(FromDir);

                if (StringUtils.Equals(ToDir, FromDir))
                {
                    //Windows does not recognize case sensitivity in folder names
                    //So to capitalize a folder that is lowercase, we must do it indirectly
                    var ToDirInfo = new DirectoryInfo(ToDir + "_TEMP_DIRECTORY");
                    FromDirInfo.MoveTo(ToDirInfo.ToString());
                    ToDirInfo.MoveTo(ToDir);
                }
                else
                {
                    FromDirInfo.MoveTo(ToDir);
                }
                return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show($"Error renaming {FromDir} to {ToDir}. This is usually because the target folder already exists, and you have duplicates.");
                Console.WriteLine($"Failed trying to move [{FromDir}] to [{ToDir}]");
                Logger.Instance.LogError($"Error renaming folder ({FromDir} to {ToDir}): {e.Message}");
                return false;
            }
        }

        public static void EncapsulateAllFiles(string Dir)
        {
            var VNDirectoryFilenames = Directory.GetFiles(Dir);
            int ErrorCount = 0;

            foreach (string Filename in VNDirectoryFilenames)
            {
                string OriginalFile = Path.GetFileName(Filename);
                string NewFolderName = Path.GetFileNameWithoutExtension(Filename);
                NewFolderName = PathUtils.ConvertToPathSafe(NewFolderName);

                string NewPath = Path.Combine(Dir, NewFolderName);
                if (!Directory.Exists(NewPath))
                    Directory.CreateDirectory(NewPath);

                if (OriginalFile == null) continue;

                string ToDestination = Path.Combine(NewPath, OriginalFile);

                if (!Move(Filename, ToDestination))
                    ErrorCount++;
            }

            if (ErrorCount > 0)
            {
                MessageBox.Show($"{ErrorCount} errors have occurred while encapsulating files. This is usually because of an already existing VN.");
            }
        }

        public static void RenameAllFolders(IEnumerable<VisualNovel> VisualNovels, Language NameLanguage)
        {
            foreach (var Vn in VisualNovels)
            {
                if (!Vn.HasVnInfo) continue;
                if (NameLanguage == Language.Japanese && string.IsNullOrWhiteSpace(Vn.JapaneseName)) continue;
                if (Vn.FolderName != PathUtils.ConvertToPathSafe(NameLanguage == Language.English ? Vn.EnglishName : Vn.JapaneseName))
                    RenameVn(Vn, NameLanguage == Language.English ? Vn.EnglishName : Vn.JapaneseName);
            }
        }

        public static void CleanAllFolderNames(string Dir)
        {
            foreach (string Folder in Directory.GetDirectories(Dir))
            {
                if (File.Exists(Path.Combine(Folder, ConfigurationManager.AppSettings["VnInfoFilename"]))) continue;

                string FolderName = PathUtils.LastElementOfPath(Folder);

                string NoBrackets = StringUtils.ClearAllBracketGroups(FolderName);
                if (string.IsNullOrWhiteSpace(NoBrackets))
                    NoBrackets = FolderName;

                string CleanName = PathUtils.ConvertToPathSafe(NoBrackets);

                string Final = RemoveAllExtraInfo(CleanName);
                if (!string.IsNullOrWhiteSpace(Final))
                    CleanName = Final;

                string NewPath = Path.Combine(Dir, CleanName);

                Move(Folder, NewPath);
            }
        }

        public static string RemoveAllExtraInfo(string FolderName)
        {
            string Cleaned = FolderName;

            foreach (string Tag in Settings.Instance.TagsToClearFromFolderNames)
            {
                int Position = Cleaned.LastIndexOf(Tag, StringComparison.OrdinalIgnoreCase);
                if (Position > 0)
                {
                    Cleaned = Cleaned.Substring(0, Position);
                }
            }

            Cleaned = RemoveLeadingSerialNumbers(Cleaned);

            return Cleaned.Trim();
        }

        private static string RemoveLeadingSerialNumbers(string FolderName)
        {
            string Cleaned = FolderName;

            if (!Regex.IsMatch(Cleaned, @"\d{5}\d*")) return Cleaned;

            int FirstLetterPosition = 0;
            foreach (char c in Cleaned)
            {
                if (char.IsDigit(c))
                {
                    FirstLetterPosition++;
                }
                else
                {
                    break;
                }
            }

            Cleaned = Cleaned.Substring(FirstLetterPosition);
            return string.IsNullOrWhiteSpace(Cleaned) ? FolderName : Cleaned;
        }
    }
}
