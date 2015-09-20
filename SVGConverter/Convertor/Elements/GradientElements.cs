using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;
using SVGConverter.Convertor.Attributes;

namespace SVGConverter.Convertor.Elements
{
    interface IGradient
    {
        GradientBrush GetGradientBrush();
    }

    class GradientStopElement
    {
        private readonly XElement _stopElement;

        public GradientStopElement(XElement stopElement)
        {
            _stopElement = stopElement;
        }

        public GradientStop GetGradientStop()
        {
            double offset = -1, opacity = -1;
            var color= Colors.RosyBrown;
            if (_stopElement.Attribute("offset") != null)
                offset = Double.Parse(_stopElement.Attribute("offset").Value);
            if (_stopElement.Attribute("stop-opacity") != null)
                opacity = Double.Parse(_stopElement.Attribute("stop-opacity").Value);
            if (_stopElement.Attribute("stop-color") != null)
                color = (Color)ColorConverter.ConvertFromString(_stopElement.Attribute("stop-color").Value);

            foreach (var xAttribute in _stopElement.Attributes())
            {
                var attributes = GetStyleAttributes(xAttribute);
                foreach (var attribute in attributes)
                {
                    if (Math.Abs(offset - (-1)) > 0.1 && attribute.Name.LocalName.Contains("offset"))
                    {
                        offset = Double.Parse(attribute.Value);
                    }

                    if (Math.Abs(opacity - (-1)) > 0.1 && attribute.Name.LocalName.Contains("stop-opacity"))
                    {
                        opacity = Double.Parse(attribute.Value);
                    }

                    if (color == Colors.RosyBrown && attribute.Name.LocalName.Contains("stop-color"))
                    {
                        color = (Color)ColorConverter.ConvertFromString(attribute.Value);
                    }
                }
            }

            color.A = (byte)Math.Round(opacity * 255);

            return new GradientStop { Color = color, Offset = offset };
        }

        private static List<XAttribute> GetStyleAttributes(XAttribute attribute)
        {
            var result = new List<XAttribute>();
            if (!attribute.Name.LocalName.Contains("style")) return result;
            try
            {
                var nameValuePairs = attribute.Value.Split(';');
                foreach (var nameValuePair in nameValuePairs)
                {
                    var trimmedNameValue = nameValuePair.Trim().Split(':');
                    if (trimmedNameValue.Length == 2)
                    {
                        var attributeName = trimmedNameValue[0].Trim();
                        var attributeValue = trimmedNameValue[1].Trim();
                        result.Add(new XAttribute(attributeName,attributeValue));
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }

    abstract class GradientElementBase : IGradient
    {
        private readonly ICollection<GradientStopElement> _gradientStops;

        protected GradientElementBase(ICollection<GradientStopElement> gradientStop)
        {
            _gradientStops = gradientStop;
        }

        protected abstract GradientBrush GetBaseBrush();

        public GradientBrush GetGradientBrush()
        {
            var brush = GetBaseBrush();
            foreach (var gradientStop in _gradientStops)
            {
                brush.GradientStops.Add(gradientStop.GetGradientStop());
            }
            return brush;
        }
    }

    class LinearGradientElement : GradientElementBase
    {
        private readonly LinearGradientAttributes _attributes;

        public LinearGradientElement(LinearGradientAttributes attributes, ICollection<GradientStopElement> gradientStop)
            : base(gradientStop)
        {
            _attributes = attributes;
        }

        protected override GradientBrush GetBaseBrush()
        {
            var brush = new LinearGradientBrush
            {
                StartPoint = new Point(_attributes.X1, _attributes.Y1),
                EndPoint = new Point(_attributes.X2, _attributes.Y2),
                SpreadMethod = _attributes.SpreadMethod
            };
            return brush;
        }

    }
}
