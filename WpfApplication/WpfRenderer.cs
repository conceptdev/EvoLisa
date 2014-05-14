using System;
using GenArt.AST;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Based on Roger Alsing's Renderer.cs which is GDI-based.
    /// </summary>
    public static class WpfRenderer
    {
        /// <summary>
        /// Render a Drawing 
        /// </summary>
        public static void Render(DnaDrawing drawing, Canvas c, int scale)
        {
            c.Children.Clear();

            foreach (DnaPolygon polygon in drawing.Polygons)
                Render(polygon, c, scale);
        }

        /// <summary>
        /// Render a polygon
        /// </summary>
        private static void Render(DnaPolygon polygonToRender, Canvas c, int scale)
        {
            System.Windows.Shapes.Polygon polygon = new Polygon();
            Brush b = GetSolidColorBrush(polygonToRender.Brush); 
            polygon.StrokeThickness = 0; // no line
            polygon.Fill = b;

            for (int j = 0; j < polygonToRender.Points.Count; j++)
            {
                polygon.Points.Add(GetSysWinPoint(polygonToRender.Points[j]));
            }
            c.Children.Add(polygon);
        }
    
        /// <summary>
        /// Convert a DnaBrush to a System.Windows.Media.SolidColorBrush
        /// </summary>
        private static SolidColorBrush GetSolidColorBrush(DnaBrush b)
        {
            return new SolidColorBrush(Color.FromArgb(
                  Convert.ToByte(b.Alpha)
                , Convert.ToByte(b.Red)
                , Convert.ToByte(b.Green)
                , Convert.ToByte(b.Blue)));
        }

        /// <summary>
        /// Convert DnaPoint to System.Windows Point
        /// </summary>
        private static System.Windows.Point GetSysWinPoint(DnaPoint p)
        {
            return new Point(p.X, p.Y);
        }
    }
}
