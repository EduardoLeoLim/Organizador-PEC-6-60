using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Organizador_PEC_6_60.Instrumento.Application;

namespace Organizador_PEC_6_60.Resources.Converters
{
    public class CollectionToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<InstrumentoResponse>)
            {
                var instrumentos = (IEnumerable<InstrumentoResponse>)value;
                return string.Join(", ", instrumentos.Select(item => item.Nombre));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}