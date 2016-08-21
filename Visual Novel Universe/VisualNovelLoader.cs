using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    public static class VisualNovelLoader
    {
        public static VisualNovel Load(string FolderPath)
        {
            try
            {
                using (var sw = new StreamReader(Path.Combine(FolderPath, ConfigurationManager.AppSettings["VnInfoFilename"])))
                {
                    var xmls = new XmlSerializer(typeof(VisualNovel));
                    return xmls.Deserialize(sw) as VisualNovel;
                }
            }
            catch (InvalidOperationException Ioe)
            {
                Logger.Instance.LogError($"Error loading VN {FolderPath}: {Ioe.Message}");
                return null;
            }
        }

        public static void Save(VisualNovel Vn, string FolderPath)
        {
            try
            {
                using (var sw = new StreamWriter(Path.Combine(FolderPath, ConfigurationManager.AppSettings["VnInfoFilename"])))
                {
                    var xmls = new XmlSerializer(typeof(VisualNovel));
                    xmls.Serialize(sw, Vn);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error saving VN: [{Vn.VnFolderPath}]: {e.Message}\n{e.StackTrace}");
            }
        }

        public static void Save(VisualNovel Vn)
        {
            try
            {
                using (var sw = new StreamWriter(Vn.VnInfoFilepath))
                {
                    var xmls = new XmlSerializer(typeof(VisualNovel));
                    xmls.Serialize(sw, Vn);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error saving VN: [{Vn.VnFolderPath}]: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}
