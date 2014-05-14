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
    public static class XamlRenderer
    {
        /// <summary>
        /// Render a Drawing 
        /// </summary>
        /// <remarks>
        /// Sorry about the hardcoded Width/Height and ScaleTransform... feel free to fix it :)
        /// </remarks>
        public static void Render(DnaDrawing drawing, ref StringBuilder sb, int scale)
        {
            sb = new StringBuilder();
            sb.Append(@"<Canvas
	xmlns=""http://schemas.microsoft.com/client/2007""
	xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
	Width=""800"" Height=""600""
	Background=""Black""
	x:Name=""canvas"">
    <Canvas.RenderTransform>
        <TransformGroup>
            <ScaleTransform ScaleX=""2"" ScaleY=""2"" />
        </TransformGroup>
    </Canvas.RenderTransform>");
            sb.Append("\r\n");
            foreach (DnaPolygon polygon in drawing.Polygons)
            {
                Render(polygon, sb, scale);
            }
            sb.Append(@"</Canvas>");
        }

        /// <summary>
        /// Render a polygon
        /// </summary>
        /// <remarks>
        /// &lt;Polygon Points="100,0 75,75 100,100 125,75" Stroke="Black" StrokeThickness="2" Fill="Yellow" /&gt;
        /// </remarks>
        private static void Render(DnaPolygon polygonToRender, StringBuilder c, int scale)
        {
            string polygonPoints = "";
            
            for (int j = 0; j < polygonToRender.Points.Count; j++)
            {
                polygonPoints += polygonToRender.Points[j].X.ToString() + "," + polygonToRender.Points[j].Y.ToString() + " ";
            }
            c.Append(@"<Polygon Points=""" + polygonPoints + @""" Fill="""+ GetColorString(polygonToRender.Brush) + "\" />\r\n");
        }
    
        /// <summary>
        /// Convert a DnaBrush to a System.Windows.Media.SolidColorBrush
        /// </summary>
        private static string GetColorString(DnaBrush b)
        {
            return "#" + b.Alpha.ToString("X").PadLeft(2, '0') + b.Red.ToString("X").PadLeft(2, '0') + b.Green.ToString("X").PadLeft(2, '0') + b.Blue.ToString("X").PadLeft(2, '0');
        }
    }
}
