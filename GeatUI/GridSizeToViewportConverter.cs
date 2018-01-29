using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GeatUI
{
    /// <summary>
    /// グリッドの単位サイズから描画に用いるViewportへのコンバータ
    /// </summary>
    class GridSizeToViewportConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridSize = (int)value;

            return new Rect { X = 0, Y = 0, Width = gridSize, Height = gridSize };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
