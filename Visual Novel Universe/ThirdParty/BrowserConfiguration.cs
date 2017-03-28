using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace Visual_Novel_Universe.ThirdParty
{
    public static class BrowserConfiguration
    {
        private static void SetBrowserFeatureControlKey(string Feature, string AppName, uint Value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                string.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", Feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key?.SetValue(AppName, Value, RegistryValueKind.DWord);
            }
        }

        public static void SetBrowserFeatureControl()
        {
            string FileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            
            if (string.Compare(FileName, "devenv.exe", StringComparison.OrdinalIgnoreCase) == 0
                || string.Compare(FileName, "XDesProc.exe", StringComparison.OrdinalIgnoreCase) == 0)
                return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", FileName, GetBrowserEmulationMode());
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", FileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", FileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", FileName, 1);
        }

        private static uint GetBrowserEmulationMode()
        {
            int BrowserVersion = 7;
            using (var IeKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                RegistryRights.QueryValues))
            {
                if (IeKey != null)
                {
                    object version = IeKey.GetValue("svcVersion");
                    if (null == version)
                    {
                        version = IeKey.GetValue("Version");
                        if (null == version)
                            throw new ApplicationException("Microsoft Internet Explorer is required!");
                    }
                    int.TryParse(version.ToString().Split('.')[0], out BrowserVersion);
                }
            }

            uint mode = 10000;

            switch (BrowserVersion)
            {
                case 7:
                    mode = 7000;
                    break;
                case 8:
                    mode = 8000;
                    break;
                case 9:
                    mode = 9000;
                    break;
            }

            return mode;
        }

    }
}
