namespace SVGConverter.Convertor.Attributes
{
    interface ITransformationAttribute
    {
        string TransformParameters { get; }
    }

    class TransformationAttribute : ITransformationAttribute
    {
        public TransformationAttribute(string transformationParams)
        {
            TransformParameters = transformationParams;
        }

        public string TransformParameters { get; private set; }
    }
}
