using KPZ_MKR.Core;
using System.Text;

namespace KPZ_MKR.Patterns
{
    // 1. Відвідувач: Конвертер HTML у звичайний текст (Plain Text)
    public class TextExtractionVisitor : INodeVisitor
    {
        private readonly StringBuilder _textBuilder = new StringBuilder();

        public void Visit(LightTextNode textNode)
        {
            // Збираємо лише текст
            _textBuilder.Append(textNode.OuterHTML).Append(" ");
        }

        public void Visit(LightElementNode elementNode)
        {
            // Теги ігноруємо, нам потрібен лише текст всередині
        }

        public string GetPlainText() => _textBuilder.ToString().Trim();
    }

    // 2. Відвідувач: Рахує кількість конкретних тегів (наприклад, скільки <li> на сторінці)
    public class TagCounterVisitor : INodeVisitor
    {
        private readonly string _targetTag;
        public int Count { get; private set; }

        public TagCounterVisitor(string targetTag)
        {
            _targetTag = targetTag;
        }

        public void Visit(LightTextNode textNode) { }

        public void Visit(LightElementNode elementNode)
        {
            if (elementNode.TagName == _targetTag)
            {
                Count++;
            }
        }
    }
}