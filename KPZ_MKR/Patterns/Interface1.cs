using KPZ_MKR.Core;

namespace KPZ_MKR.Patterns
{
    public interface INodeVisitor
    {
        void Visit(LightTextNode textNode);
        void Visit(LightElementNode elementNode);
    }
}