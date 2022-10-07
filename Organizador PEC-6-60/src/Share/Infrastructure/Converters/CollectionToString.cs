using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Organizador_PEC_6_60.TipoInstrumento.Application;

namespace Organizador_PEC_6_60.Share.Infrastructure.Converters;

public class CollectionToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<TipoInstrumentoResponse>)
        {
            var instrumentos = (IEnumerable<TipoInstrumentoResponse>)value;
            return string.Join(", ", instrumentos.Select(item => item.Nombre));
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}