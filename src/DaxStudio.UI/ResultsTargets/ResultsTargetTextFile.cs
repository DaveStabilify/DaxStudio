﻿using ADOTabular.AdomdClientWrappers;
using DaxStudio.Interfaces;
using DaxStudio.UI.Extensions;
using DaxStudio.UI.Interfaces;
using DaxStudio.UI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DaxStudio.UI.Model
{
    [Export(typeof(IResultsTarget))]
    public class ResultsTargetTextFile : IResultsTarget
    {
        #region Standard Properties
        public string Name
        {
            get { return "File"; }
        }
        public string Group
        {
            get { return "Standard"; }
        }
        public bool IsDefault
        {
            get { return false; }
        }

        public bool IsEnabled
        {
            get { return true; }
        }

        public int DisplayOrder
        {
            get { return 300; }
        }


        public string Message
        {
            get { return "Results will be sent to a Text File"; }
        }

        public OutputTargets Icon
        {
            get { return OutputTargets.File; }
        }
        #endregion

        public Task OutputResultsAsync(IQueryRunner runner)
        {

            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Tab separated text file|*.txt|Comma separated text file - UTF8|*.csv|Comma separated text file - Unicode|*.csv"
            };

            string fileName = "";

            // Show save file dialog box
            var result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                fileName = dlg.FileName;
                return Task.Run(() =>
                {

                    try
                    {
                        runner.OutputMessage("Query Started");

                        var sw = Stopwatch.StartNew();

                        string sep = "\t";

                        string decimalSep = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator;

                        string isoDateFormat = string.Format("yyyy-MM-dd HH:mm:ss{0}000", decimalSep);

                        var enc = Encoding.UTF8;

                        switch (dlg.FilterIndex)
                        {
                            case 1: // tab separated
                                sep = "\t";
                                break;
                            case 2: // utf-8 csv
                                sep = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ListSeparator;
                                break;
                            case 3: //unicode csv
                                enc = Encoding.Unicode;
                                sep = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ListSeparator;
                                break;
                        }

                        var dq = runner.QueryText;

                        var reader = runner.ExecuteDataReaderQuery(dq);

                        try
                        {
                            if (reader != null)
                            {
                                sw.Stop();
                                var durationMs = sw.ElapsedMilliseconds;
                                //runner.ResultsTable = res;
                                runner.OutputMessage("Command Complete, writing output file");

                                var sbLine = new StringBuilder();
                                bool moreResults = true;
                                int iFileCnt = 1;

                                while (moreResults)
                                {
                                    int iMaxCol = reader.FieldCount - 1;
                                    int iRowCnt = 0;

                                    if (iFileCnt > 1) fileName = AddFileCntSuffix(fileName, iFileCnt);

                                    using (var textWriter = new System.IO.StreamWriter(fileName, false, enc))
                                    {
                                        using (var csvWriter = new CsvHelper.CsvWriter(textWriter))
                                        {
                                            // CSV Writer config

                                            csvWriter.Configuration.Delimiter = sep;

                                            // Datetime as ISOFormat

                                            csvWriter.Configuration.TypeConverterOptionsCache.AddOptions(typeof(DateTime), new CsvHelper.TypeConversion.TypeConverterOptions() { Formats = new string[] { isoDateFormat } });

                                            // write out clean column names

                                            foreach (var colName in reader.CleanColumnNames())
                                            {
                                                csvWriter.WriteField(colName);
                                            }

                                            csvWriter.NextRecord();

                                            while (reader.Read())
                                            {
                                                iRowCnt++;

                                                for (int iCol = 0; iCol < reader.FieldCount; iCol++)
                                                {
                                                    var fieldValue = reader[iCol];

                                                    csvWriter.WriteField(fieldValue);
                                                }

                                                csvWriter.NextRecord();                                              
                                                
                                                if (iRowCnt % 1000 == 0)
                                                {
                                                    runner.NewStatusBarMessage(string.Format("Written {0:n0} rows to the file output", iRowCnt));
                                                }
                                            
                                            }

                                        }

                                    }
                                    
                                    runner.OutputMessage(
                                            string.Format("Query Completed ({0:N0} row{1} returned)"
                                                        , iRowCnt
                                                        , iRowCnt == 1 ? "" : "s"), durationMs);

                                    runner.RowCount = iRowCnt;

                                    moreResults = reader.NextResult();

                                    iFileCnt++;
                                }

                                runner.SetResultsMessage("Query results written to file", OutputTargets.Grid);
                                //runner.QueryCompleted();
                                runner.ActivateOutput();
                            }
                        }
                        finally
                        {
                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        runner.ActivateOutput();
                        runner.OutputError(ex.Message);
#if DEBUG
                        runner.OutputError(ex.StackTrace);
#endif
                    }
                    finally
                    {
                        runner.QueryCompleted();

                    }

                });

            }
            // else dialog was cancelled so return an empty task.
            return Task.Run(() => { });
        }

        private string AddFileCntSuffix(string fileName, int iFileCnt)
        {
            FileInfo fi = new FileInfo(fileName);
            var newName = string.Format("{0}\\{1}_{2}{3}", fi.DirectoryName.TrimEnd('\\'), fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length), iFileCnt, fi.Extension);
            return newName;
        }
    }
}
