﻿using DaxStudio.Interfaces;
using DaxStudio.Interfaces.Attributes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DaxStudio.UI.Validation
{
    public class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty OptionsProperty =
             DependencyProperty.Register("Options", typeof(IGlobalOptions),
             typeof(Wrapper), null);

        public IGlobalOptions Options
        {
            get { return (IGlobalOptions)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public static readonly DependencyProperty PropertyNameProperty =
             DependencyProperty.Register("PropertyName", typeof(string),
             typeof(Wrapper));

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
    }

    public class HotkeyValidationRule : System.Windows.Controls.ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string hotkey = value.ToString();

            var props = this.Wrapper.Options.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                var att = prop.GetCustomAttributes(typeof(HotkeyAttribute),true);
                if (att.Length == 0) continue;
                if (prop.Name == this.Wrapper.PropertyName) continue;
                if ((string)prop.GetValue(this.Wrapper.Options) == hotkey) return new ValidationResult(false, "Duplicate Hotkey");
            }
                


            return ValidationResult.ValidResult;
        }

        public Wrapper Wrapper { get; set; }
    }
}
