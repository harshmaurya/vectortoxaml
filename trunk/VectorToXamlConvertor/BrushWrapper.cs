using System.Windows.Media;

namespace VectorToXamlConvertor
{
    public class BrushWrapper
    {
        private readonly string _name;
        private readonly Brush _brush;

        public BrushWrapper(string name, Brush brush)
        {
            _name = name;
            _brush = brush;
        }

        public Brush Brush
        {
            get { return _brush; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}