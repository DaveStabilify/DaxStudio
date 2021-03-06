﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DaxStudio.Common
{
    public class CmdLineArgs
    {
        private Application _app;
        public CmdLineArgs(Application app)
        {
            _app = app;
        }
        public  int Port
        {
            get {
                if (_app.Properties.Contains(AppProperties.PortNumber))
                    return (int)_app.Properties[AppProperties.PortNumber];
                return 0;
            }
            set
            {
                if (_app.Properties.Contains(AppProperties.PortNumber))
                    _app.Properties[AppProperties.PortNumber] = value;
                _app.Properties.Add(AppProperties.PortNumber, value);
            }
        }

        public string FileName
        {
            get
            {
                if (_app.Properties.Contains(AppProperties.FileName))
                    return (string)_app.Properties[AppProperties.FileName];
                return string.Empty;
            }
            set
            {
                if (_app.Properties.Contains(AppProperties.FileName))
                    _app.Properties[AppProperties.FileName] = value;
                _app.Properties.Add(AppProperties.FileName, value);
            }
        }

        public bool LoggingEnabledByCommandLine
        {
            get
            {
                if (_app.Properties.Contains(AppProperties.LoggingEnabledByCommandLine))
                    return (bool)_app.Properties[AppProperties.LoggingEnabledByCommandLine];
                return false;
            }
            set
            {
                if (_app.Properties.Contains(AppProperties.LoggingEnabledByCommandLine))
                    _app.Properties[AppProperties.LoggingEnabledByCommandLine] = value;
                _app.Properties.Add(AppProperties.LoggingEnabledByCommandLine, value);
            }
        }

        public bool LoggingEnabledByHotKey
        {
            get
            {
                if (_app.Properties.Contains(AppProperties.LoggingEnabledByHotKey))
                    return (bool)_app.Properties[AppProperties.LoggingEnabledByHotKey];
                return false;
            }
            set
            {
                if (_app.Properties.Contains(AppProperties.LoggingEnabledByHotKey))
                    _app.Properties[AppProperties.LoggingEnabledByHotKey] = value;
                _app.Properties.Add(AppProperties.LoggingEnabledByHotKey, value);
            }
        }

        public bool LoggingEnabled { get {
                return LoggingEnabledByCommandLine || LoggingEnabledByHotKey;
            }
        }

        public bool TriggerCrashTest {
            get
            {
                if (_app.Properties.Contains(AppProperties.CrashTest))
                    return (bool)_app.Properties[AppProperties.CrashTest];
                return false;
            }
            set
            {
                if (_app.Properties.Contains(AppProperties.CrashTest))
                    _app.Properties[AppProperties.CrashTest] = value;
                _app.Properties.Add(AppProperties.CrashTest, value);
            }
        }
    }
}
