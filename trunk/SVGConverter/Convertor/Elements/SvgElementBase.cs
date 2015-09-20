using System.Collections.Generic;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor;
using SVGConverter.Convertor.Attributes;
using SVGConverter.Convertor.Elements;
using VectorToXamlConvertor.Convertor.Attributes;

namespace VectorToXamlConvertor.Convertor.Elements
{
    abstract class SvgElementBase<TElemType, TAttrib> : ISvgElement<TElemType, TAttrib>
        where TElemType : class, TAttrib
        where TAttrib : class
    {
        protected SvgElementBase(XElement element)
        {
            Element = element;
            GenericAttributes = new List<ISvgAttribute<TAttrib>>();
            ElementAttributes = new List<ISvgAttribute<TElemType>>();
        }

        public ICollection<ISvgAttribute<TAttrib>> GenericAttributes { get; set; }
        public ICollection<ISvgAttribute<TElemType>> ElementAttributes { get; private set; }
        public ITransformationAttribute TransformationAttribute { get; set; }
        public IdAttribute IdAttribute { get; set; }


        public XElement Element { get; set; }

        public abstract TElemType GetBaseObject(XElement element);

        protected abstract ICollection<Path> ConvertObjectToPaths(TElemType objectToConvert);

        public ICollection<Path> GeneratePaths()
        {
            //Apply attributes
            var baseObject = GetBaseObject(Element);
            foreach (var attribute in GenericAttributes)
                baseObject = attribute.TryApplyAttribute<TElemType>(baseObject);
            foreach (var attribute in ElementAttributes)
                baseObject = attribute.TryApplyAttribute<TElemType>(baseObject);

            var pathCollection =  ConvertObjectToPaths(baseObject);
            
            //Apply transformations
            if (!(this is ITransformable) || TransformationAttribute == null) return pathCollection;
            var transform = TransformationHelper.GetTransform(TransformationAttribute.TransformParameters);
            if (transform != null)
            {
                ((ITransformable) this).ApplyTransformation(transform);
            }
            return pathCollection;
        }
    }
}
