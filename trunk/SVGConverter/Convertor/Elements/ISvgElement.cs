using System.Collections.Generic;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor.Attributes;
using VectorToXamlConvertor.Convertor.Attributes;
using VectorToXamlConvertor.Convertor.Elements;

namespace SVGConverter.Convertor.Elements
{
    interface ISvgElement
    {
        ICollection<Path> GeneratePaths();
    }

    /// <summary>
    /// Interface for an svg element
    /// </summary>
    /// <typeparam name="TElem">Type of object on which element specific attributes can be applied to</typeparam>
    /// <typeparam name="TAttrib">Type of object on which generic attributes can be applied to</typeparam>
    interface ISvgElement<TElem, TAttrib> : ISvgElement
        where TElem : class,TAttrib
        where TAttrib : class
    {
        ICollection<ISvgAttribute<TAttrib>> GenericAttributes { get; }

        ICollection<ISvgAttribute<TElem>> ElementAttributes { get; }

        ITransformationAttribute TransformationAttribute { get; set; }

        IdAttribute IdAttribute { get; set; }
        
        XElement Element { get; set; }

        TElem GetBaseObject(XElement element);
    }
}
