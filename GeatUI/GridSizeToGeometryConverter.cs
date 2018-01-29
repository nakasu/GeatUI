using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GeatUI
{
    /// <summary>
    /// グリッドの単位サイズから実際に描画するジオメトリデータへのコンバータ
    /// </summary>
    class GridSizeToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridSize = (int)value;

            var pf = new PathFigure
            {
                StartPoint = new Point(0, 0)
            };
            pf.Segments.Add(new LineSegment(new Point(0, gridSize), true));
            pf.Segments.Add(new LineSegment(new Point(gridSize, gridSize), true));
            var pg = new PathGeometry();
            pg.Figures.Add(pf);

            return pg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
