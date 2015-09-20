using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor.Elements;

namespace VectorToXamlConvertor.Convertor.Elements
{
    class TextElement : SvgGeometryElement<SvgTextAdaptor, Shape>
    {
        public TextElement(XElement element)
            : base(element)
        {
        }

        public override SvgTextAdaptor GetBaseObject(XElement element)
        {
            return new SvgTextAdaptor { Text = element.Value };
        }
        
        protected override Geometry GetGeometry(SvgTextAdaptor objectToConvert)
        {
            var text = new FormattedText(objectToConvert.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, objectToConvert.Font,
                objectToConvert.FontSize, objectToConvert.Fill);
            var textGeometry = text.BuildGeometry(new Point(0, 0));
            return textGeometry;
        }
    }


    /// <summary>
    /// This clas is just an intermediate to covert the text into wpf equivalent
    /// </summary>
    class SvgTextAdaptor : Shape
    {
        private Typeface _font;
        private double _fontSize;

        protected override Geometry DefiningGeometry
        {
            get { throw new NotImplementedException(); }
        }

        public new Brush Fill
        {
            get
            {
                return base.Fill ?? new SolidColorBrush(Colors.Black);
            }
            set { base.Fill = value; }
        }

        public new Brush Stroke
        {
            get
            {
                return base.Stroke ?? new SolidColorBrush(Colors.White);
            }
            set { base.Stroke = value; }
        }


        public string Text { get; set; }

        public Typeface Font
        {
            get
            {
                return _font ?? new Typeface("Verdana");
            }
            set { _font = value; }
        }

        public double FontSize
        {
            get
            {
                return _fontSize < 1 ? 12 : _fontSize;
            }
            set { _fontSize = value; }
        }
    }
}
