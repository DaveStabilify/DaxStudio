﻿using System;
using System.Data;

namespace ADOTabular
{
    //RRomano: Is it worth it to create the ADOTabularMeasure or reuse this in the ADOTabularColumn?
    public  class ADOTabularMeasure:IADOTabularObject
    {

        public ADOTabularMeasure(ADOTabularTable table, string internalReference, string name, string caption, string description,
                                bool isVisible, string expression)
        {
            Table = table;
            InternalReference = internalReference;
            Name = name ?? internalReference;
            Caption = caption ?? internalReference ?? name;
            Description = description;
            IsVisible = isVisible;
            Expression = expression;
        }

        public string InternalReference { get; private set; }

        public ADOTabularColumnType ColumnType { get; internal set; }

        public ADOTabularTable Table { get; private set; }

        public string Caption { get; private set; }

        public string Name { get; private set; }

        public string Contents { get; private set; }

        public string DisplayFolder { get; private set; }
        public virtual string DaxName {
            get
            {
                // for measures we exclude the table name
                return ColumnType == ADOTabularColumnType.Column  
                    ? string.Format("{0}[{1}]", Table.DaxName, Name)
                    : string.Format("[{0}]",Name);
            }
        }

        public string Description { get; set; }

        public bool IsVisible { get; private set; }
 
        public Type DataType { get; set; }

        public string DataTypeName { get { return DataType==null?"n/a":DataType.ToString().Replace("System.", ""); } }

        public string Expression { get; set; }

        public MetadataImages MetadataImage
        {
            get
            {
                return MetadataImages.Measure;
            }
        }
    }
}
