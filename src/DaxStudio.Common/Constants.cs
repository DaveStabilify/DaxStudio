﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaxStudio.Common
{
    public static class Constants
    {
        
        //public const string AppDataSettingsFolder = @"%APPDATA%\DaxStudio";
        //public const string LogFolder = @"%APPDATA%\DaxStudio\log\";
        public const string ExcelLogFileName = "DaxStudioExcel-{Date}.log";
        public const string StandaloneLogFileName = "DaxStudio-{Date}.log";
        //public const string AutoSaveIndexPath = @"%APPDATA%\DaxStudio\AutoSaveMasterIndex.json";
        //public const string AutoSaveFolder = @"%APPDATA%\DaxStudio\AutoSaveFiles";
        //public const string AvalonDockLayoutFile = @"%APPDATA%\DaxStudio\WindowLayouts\Custom.xml";
        public const string AvalonDockDefaultLayoutFile = @"DaxStudio.UI.Resources.AvalonDockLayout-Default.xml";

        public const System.Windows.Input.Key LoggingHotKey1 = System.Windows.Input.Key.LeftShift;
        public const System.Windows.Input.Key LoggingHotKey2 = System.Windows.Input.Key.RightShift;
        public const string LoggingHotKeyName = "Shift";
        public const int ExcelUIStartupTimeout = 10000;

        public const string FormatString = "FormatString";
        public const string LocaleId = "LocaleId";
        public const string IsUnique = "IsUnique";
        public const string AllowDbNull = "AllowDBNull";

        public const string StatusBarTimerFormat = "mm\\:ss\\.f";

        public const int AutoSaveIntervalMs = 10000; // autosave every 30 seconds
        public const string RefreshSessionQuery = "/* DAX Studio Internal */\nEVALUATE ROW(\"DAX Studio Session Refresh\",0)";

        public const string InternalQueryHeader = "/* DAX Studio Internal */";
        public const string IsoDateMask = "yyyy-MM-dd HH:mm:ss{0}000";
        public const string IsoDateFormat = "yyyy-MM-ddTHH:mm:ssZ";

        public const int MaxRecentFiles = 25;
        public const int MaxRecentServers = 25;
        public const int MaxMruSize = 25;
        //public const int TraceStartTimeoutSeconds = 30;
    }
}
