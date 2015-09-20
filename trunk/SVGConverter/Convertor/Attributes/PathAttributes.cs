using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorToXamlConvertor.Convertor.Attributes
{
    class DataAttribute : SvgAttributeBase<Path>
    {
        public DataAttribute(string value)
            : base(value)
        {

        }

        protected override Path ApplyAttribute(Path ownerElement)
        {
            var geometry = Geometry.Parse(Value).Clone();
            ownerElement.Data = geometry;
            return ownerElement;
        }
    }

    class FiguresAttribute : DataAttribute
    {
        public FiguresAttribute(string value)
            : base(value)
        {
            
        }
    }
}
