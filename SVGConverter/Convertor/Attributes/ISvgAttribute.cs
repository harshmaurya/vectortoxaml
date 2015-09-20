namespace VectorToXamlConvertor.Convertor.Attributes
{
    /// <summary>
    /// Interface for an svg attribute
    /// </summary>
    /// <typeparam name="TAttrib">Type of object on which the attribute can be applied to</typeparam>
    interface ISvgAttribute<in TAttrib> where TAttrib : class
    {
        /// <summary>
        /// Value of the attribute
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TElemType"></typeparam>
        /// <param name="ownerElement"></param>
        /// <returns></returns>
        TElemType TryApplyAttribute<TElemType>(TAttrib ownerElement) where TElemType : TAttrib;
    }
}
