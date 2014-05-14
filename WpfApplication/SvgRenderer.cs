using System;
using GenArt.AST;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Text;

namespace WpfApplication
{
    /// <summary>
    /// Based on Roger Alsing's Renderer.cs which is GDI-based.
    /// </summary>
    public static class SvgRenderer 
    {
        /// <summary>
        /// Unknown bounds (should not be required)
        /// </summary>
        [Obsolete("original method, before identifying that a black background was required")]
        public static void Render(DnaDrawing drawing, ref StringBuilder sb, int scale)
        {
            SvgRenderer.Render(drawing, ref sb, scale, new System.Drawing.Rectangle(0, 0, 0, 0));
        }
        /// <summary>
        /// Render a Drawing 
        /// </summary>
        /// <remarks>
        /// Sorry about the hardcoded Width/Height and ScaleTransform... feel free to fix it :)
        /// </remarks>
        /// <param name="bounds">previously calculated extent of image, so we can draw a black background behind everything</param>
        public static void Render(DnaDrawing drawing, ref StringBuilder sb, int scale, System.Drawing.Rectangle bounds)
        {
            sb = new StringBuilder();
            sb.Append(@"<?xml version=""1.0"" standalone=""no""?>
<!DOCTYPE svg PUBLIC ""-//W3C//DTD SVG 1.1//EN"" 
""http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"">

<svg width=""100%"" height=""100%"" version=""1.1"" xmlns=""http://www.w3.org/2000/svg"">");
            sb.Append("\r\n");

            sb.Append(
                String.Format(@"<polygon points=""0,0 {0},0 {1},{2} 0,{3}"" style=""fill:Black;fill-opacity:1"" />", bounds.Width, bounds.Width, bounds.Height, bounds.Height)
            ); // black background - excuse my lack of SVG knowledge - didn't seem to work in the <svg> tag

            foreach (DnaPolygon polygon in drawing.Polygons)
            {
                Render(polygon, sb, scale);
            }
            sb.Append(@"</svg>");
        }

        /// <summary>
        /// Render a polygon
        /// </summary>
        /// <remarks>
        /// &lt;polygon points="100,0 75,75 100,100 125,75" style="fill:Black;fill-opacity:1" /&gt;
        /// </remarks>
        private static void Render(DnaPolygon polygonToRender, StringBuilder c, int scale)
        {
            string polygonPoints = "";

            for (int j = 0; j < polygonToRender.Points.Count; j++)
            {
                polygonPoints += polygonToRender.Points[j].X.ToString() + "," + polygonToRender.Points[j].Y.ToString() + " ";
            }
            c.Append(@"<polygon points=""" + polygonPoints + @""" style=""fill:" + GetColorString(polygonToRender.Brush) + ";fill-opacity:" + GetOpacityString (polygonToRender.Brush)+ "\" />\r\n");
        }

        /// <summary>
        /// Convert a DnaBrush to a System.Windows.Media.SolidColorBrush
        /// </summary>
        private static string GetColorString(DnaBrush b)
        {
            return "#" + b.Red.ToString("X").PadLeft(2, '0') + b.Green.ToString("X").PadLeft(2, '0') + b.Blue.ToString("X").PadLeft(2, '0');
        }
        /// <summary>
        /// Convert a DnaBrush to a System.Windows.Media.SolidColorBrush
        /// </summary>
        private static string GetOpacityString(DnaBrush b)
        {
            return (b.Alpha / 256.0).ToString();
        }
    }
}