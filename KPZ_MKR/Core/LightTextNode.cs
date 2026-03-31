using KPZ_MKR.Patterns;

namespace KPZ_MKR.Core
{
    public class LightTextNode : LightNode
    {
        private readonly string _text;

        public LightTextNode(string text)
        {
            _text = text;
        }

        public override string InnerHTML => _text;
        public override string OuterHTML => _text;

        // --- РЕАЛІЗАЦІЯ ХУКА (ШАБЛОННИЙ МЕТОД) ---
        protected override void OnTextRendered()
        {
            Console.WriteLine($"[Hook - LightTextNode]: Відмальовано текст '{_text}'");
        }
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}