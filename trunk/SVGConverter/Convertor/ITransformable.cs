using System.Windows.Media;

namespace SVGConverter.Convertor
{
    interface ITransformable
    {
        void ApplyTransformation(Transform transform);
    }
}
