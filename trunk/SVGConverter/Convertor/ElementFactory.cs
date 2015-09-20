using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Linq;
using SVGConverter.Convertor.Attributes;
using SVGConverter.Convertor.Elements;
using VectorToXamlConvertor.Convertor.Attributes;
using VectorToXamlConvertor.Convertor.Elements;
using XAttribute = System.Xml.Linq.XAttribute;

namespace VectorToXamlConvertor.Convertor
{
    class ElementFactory
    {
        /// <summary>
        /// Returns the SvgElement object converted from given XElement
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ISvgElement GetSvgElement(XElement element)
        {
            switch (element.Name.LocalName)
            {
                case "path":
                    {
                        var pathElement = new PathElement(element);
                        AddAttributesToElement(pathElement, element);
                        return pathElement;
                    }
                case "text":
                    {
                        var textElement = new TextElement(element);
                        AddAttributesToElement(textElement, element);
                        return textElement;
                    }
                case "circle":
                case "ellipse":
                    {
                        var ellipseElemment = new EllipseElement(element);
                        AddAttributesToElement(ellipseElemment, element);
                        return ellipseElemment;
                    }
                case "rect":
                    {
                        var rectElement = new RectElement(element);
                        AddAttributesToElement(rectElement, element);
                        return rectElement;
                    }

                case "line":
                    {
                        var lineElement = new LineElement(element);
                        AddAttributesToElement(lineElement, element);
                        return lineElement;
                    }

                case "polyline":
                    {
                        var polyLineElement = new PolyLineElement(element, false);
                        AddAttributesToElement(polyLineElement, element);
                        return polyLineElement;
                    }

                case "polygon":
                    {
                        var polyLineElement = new PolyLineElement(element, true);
                        AddAttributesToElement(polyLineElement, element);
                        return polyLineElement;
                    }
                case "g":
                    {
                        var gElement = new GElement(element);
                        AddAttributesToElement(gElement, element);
                        return gElement;
                    }

                case "defs":
                    {
                        AddDefinitions(element);
                        return null;
                    }

                default:
                    return null;
            }
        }

        private static void AddDefinitions(XElement element)
        {
            foreach (var xElement in element.Elements())
            {
                var id = GetIdAttribute(xElement);
                if (String.IsNullOrEmpty(id))
                    continue;

                var svgElement = GetSvgElement(xElement);
                if (svgElement != null)
                {
                    SvgDefinitions.Instance.Elements.Add(id, svgElement);
                }

                //if (xElement.Name.LocalName.Contains("Gradient"))
                //{
                //    var gradient = GetGradient(xElement);
                //    if (gradient != null)
                //    {
                //        SvgDefinitions.Instance.Gradients.Add(id, gradient);
                //    }
                //}
            }
        }

        private static GradientBrush GetGradient(XElement element)
        {
            GradientBrush gradient = null;
            if (element.Name.LocalName.Contains("linearGradient"))
            {
                var spreadMethod = element.Attribute("spreadMethod") !=null?(GradientSpreadMethod)Enum.Parse(typeof (GradientSpreadMethod), element.Attribute("spreadMethod").Value):GradientSpreadMethod.Pad;
                var linearGradientAttributes = new LinearGradientAttributes
                {
                    SpreadMethod = spreadMethod,
                    X1 = Double.Parse(element.Attribute("x1").Value),
                    X2 = Double.Parse(element.Attribute("x2").Value),
                    Y1 = Double.Parse(element.Attribute("y1").Value),
                    Y2 = Double.Parse(element.Attribute("y2").Value)
                };
                var gradientStops = new List<GradientStopElement>();
                foreach (var xElement in element.Elements())
                {
                    if (element.Name.LocalName.Contains("stop"))
                    {
                        gradientStops.Add(new GradientStopElement(xElement));
                    }
                }

                var gradientElement = new LinearGradientElement(linearGradientAttributes, gradientStops);
                gradient = gradientElement.GetGradientBrush();
            }
            return gradient;
        }

        private static string GetIdAttribute(XElement element)
        {
            var id = string.Empty;
            foreach (var attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.Equals("id"))
                {
                    id = attribute.Value;
                    break;
                }
            }
            return id;
        }


        private static void AddAttributesToElement<TElem, TAttrib>(ISvgElement<TElem, TAttrib> svgElement, XElement xElement)
            where TElem : class, TAttrib
            where TAttrib : class
        {
            foreach (var attribute in xElement.Attributes())
            {
                try
                {
                    var svgAttribute = AttributeFactory.GetAttribute(attribute);
                    AddToElement(svgElement, svgAttribute);

                    //Add the style attributes if present
                    foreach (var styleAttribute in GetStyleAttributes(attribute))
                    {
                        AddToElement(svgElement, styleAttribute);
                    }
                }
                catch (Exception)
                {
                    // All attributes can't be applied
                }
            }
        }


        private static IEnumerable<object> GetStyleAttributes(XAttribute attribute)
        {
            var result = new List<object>();
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
                        result.Add(AttributeFactory.GetAttribute(attributeName, attributeValue));
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        private static void AddToElement<TElem, TAttrib>(ISvgElement<TElem, TAttrib> svgElement, object attributeToAdd)
            where TElem : class, TAttrib
            where TAttrib : class
        {
            if (attributeToAdd == null) return;
            if (attributeToAdd is ITransformationAttribute)
                svgElement.TransformationAttribute = (ITransformationAttribute)attributeToAdd;
            else if (attributeToAdd is IdAttribute)
                svgElement.IdAttribute = (IdAttribute)attributeToAdd;
            else if (attributeToAdd is ISvgAttribute<TAttrib>)
                svgElement.GenericAttributes.Add((ISvgAttribute<TAttrib>)attributeToAdd);
            else if (attributeToAdd is ISvgAttribute<TElem>)
                svgElement.ElementAttributes.Add((ISvgAttribute<TElem>)attributeToAdd);
        }
    }
}
