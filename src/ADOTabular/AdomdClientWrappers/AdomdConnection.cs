﻿extern alias ExcelAdomdClientReference;
using System;
using System.Data;

namespace ADOTabular.AdomdClientWrappers
{
    public class AdomdConnection:IDisposable
    {
        private readonly AdomdType _type;
        private Microsoft.AnalysisServices.AdomdClient.AdomdConnection _conn;
        private ExcelAdomdClientReference::Microsoft.AnalysisServices.AdomdClient.AdomdConnection _connExcel;
        private readonly Object rowsetLock = new Object();
        public AdomdConnection(Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection)
        {
            _type = AdomdType.AnalysisServices;
            _conn = connection;
        }
        public AdomdConnection(ExcelAdomdClientReference::Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection)
        {
            _type = AdomdType.Excel;
            _connExcel = connection;
        }
        
        public AdomdConnection(string connectionString, AdomdType type)
        {
            _type = type;
            if (_type == AdomdType.AnalysisServices)
            {
                _conn = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connectionString);
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    _connExcel = new ExcelAdomdClientReference::Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connectionString);
                };
                f();
            }
        }

        internal AdomdType Type
        {
            get { return _type; }
        }

        internal object UnderlyingConnection
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    return _conn;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<object> f = delegate
                    {
                        return _connExcel;
                    };
                    return f();
                }
            }
        }

        
        public void Open()
        {
            
            if (_type == AdomdType.AnalysisServices)
            {
                _conn.Open();
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                    {
                        _connExcel.Open();
                    };
                f();
            }
        }

        public void Open(string connectionString)
        {
            if (_type == AdomdType.AnalysisServices)
            {
                _conn.Open(connectionString);
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    _connExcel.Open(connectionString);
                };
                f();
            }
        }

        public void Close()
        {
            if (_type == AdomdType.AnalysisServices)
            {
                _conn.Close();
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    _connExcel.Close();
                };
                f();
            }
        }

        public void Close(bool endSession)
        {
            if (_type == AdomdType.AnalysisServices)
            {
                _conn.Close(endSession);
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    _connExcel.Close(endSession);
                };
                f();
            }
        }

        public void ChangeDatabase(string database)
        {
            if (database == null) return; 
            if (database.Trim().Length == 0) return;
            if (_type == AdomdType.AnalysisServices)
            {
                _conn.ChangeDatabase(database);
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    _connExcel.ChangeDatabase(database);
                };
                f();
            }
        }

        public string ConnectionString
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    return _conn.ConnectionString;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<string> f = delegate
                    {
                        return _connExcel.ConnectionString;
                    };
                    return f();
                }
            }
        }

        public string ClientVersion
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    return _conn.ClientVersion;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<string> f = delegate
                    {
                        return _connExcel.ClientVersion;
                    };
                    return f();
                }
            }
        }

        public AdomdCommand CreateCommand()
        {
            //if (_type == AdomdType.AnalysisServices)
            //{
            var cmd = new AdomdCommand
            {
                Connection = this
            };
            return cmd;
            /*}
            else
            {
                ExcelAdoMdConnections.ReturnDelegate<ExcelAdomdClientReference.Microsoft.AnalysisServices.AdomdClient.AdomdCommand> f = delegate
                {
                    return _connExcel.CreateCommand();
                };
                f();
            }
             */ 
        }

        public CubeCollection Cubes
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    return new CubeCollection(_conn.Cubes);
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<CubeCollection> f = delegate
                    {
                        return new CubeCollection(_connExcel.Cubes);
                    };
                    return f();
                }
            }
        }

        public ConnectionState State
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    if (_conn != null) return _conn.State;
                    return ConnectionState.Closed;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<ConnectionState> f = delegate
                    {
                        if (_connExcel != null) return _connExcel.State;
                        return ConnectionState.Closed;
                    };
                    return f();
                }
            }
        }

        public string SessionID
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    return _conn.SessionID;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<string> f = delegate
                    {
                        return _connExcel.SessionID;
                    };
                    return f();
                }
            }
            set
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    _conn.SessionID = value;
                }
                else
                {
                    ExcelAdoMdConnections.VoidDelegate f = delegate
                    {
                        _connExcel.SessionID = value;
                    };
                    f();
                }
            }
        }

        public string Database
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    return _conn.Database;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<string> f = delegate
                    {
                        return _connExcel.Database;
                    };
                    return f();
                }
            }
        }

        public string ServerVersion
        {
            get
            {
                if (_type == AdomdType.AnalysisServices)
                {
                    if (_conn.State != ConnectionState.Open)
                        _conn.Open();
                    return _conn.ServerVersion;
                }
                else
                {
                    ExcelAdoMdConnections.ReturnDelegate<string> f = delegate
                    {
                        return _connExcel.ServerVersion;
                    };
                    return f();
                }
            }
        }

        public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
        {
            if (_type == AdomdType.AnalysisServices)
            {
                global::Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection coll = new global::Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
                if (restrictions != null)
                {
                
                    foreach (AdomdClientWrappers.AdomdRestriction res in restrictions)
                    {
                        coll.Add(new global::Microsoft.AnalysisServices.AdomdClient.AdomdRestriction( res.Name, res.Value));
                    }
                }
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }
                lock (rowsetLock)
                {
                    return _conn.GetSchemaDataSet(schemaName, coll, throwOnInlineErrors);
                }
            }
            else
            {
                ExcelAdoMdConnections.ReturnDelegate<DataSet> f = delegate
                {
                    ExcelAdomdClientReference::Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection coll = new ExcelAdomdClientReference::Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
                    if (restrictions != null)
                    {
                        foreach (AdomdRestriction res in restrictions)
                        {
                            coll.Add(new ExcelAdomdClientReference::Microsoft.AnalysisServices.AdomdClient.AdomdRestriction(res.Name, res.Value));
                        }
                    }
                    if (_connExcel.State != ConnectionState.Open)
                    {
                        _connExcel.Open();
                    }
                    lock (rowsetLock)
                    {
                        return _connExcel.GetSchemaDataSet(schemaName, coll, throwOnInlineErrors);
                    }
                };
                return f();
            }
        }

        public void RefreshMetadata()
        {
            if (_type == AdomdType.AnalysisServices)
            {
                if (_conn != null)
                    _conn.RefreshMetadata();
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    if (_connExcel != null)
                        _connExcel.RefreshMetadata();
                };
                f();
                
            }
        }

        public void Dispose()
        {
            if (_type == AdomdType.AnalysisServices)
            {
                if (_conn != null)
                {
                    _conn.Dispose();
                    _conn = null;
                }
            }
            else
            {
                ExcelAdoMdConnections.VoidDelegate f = delegate
                {
                    if (_connExcel != null)
                    {
                        _connExcel.Dispose();
                        _connExcel = null;
                    }
                };
                f();
                
            }
        }
    }

    public enum AdomdType {
        AnalysisServices = 1,
        Excel = 2
    }
}
