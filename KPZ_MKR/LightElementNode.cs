using System.Collections.Generic;
using System.Text;

namespace LightHTML_System
{
    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        public DisplayType Display { get; }
        public ClosingType Closing { get; }

        // Інкапсульовані колекції
        private readonly List<string> _cssClasses;
        private readonly List<LightNode> _children;

        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            TagName = tagName;
            Display = display;
            Closing = closing;
            _cssClasses = new List<string>();
            _children = new List<LightNode>();
        }

        public void AddChild(LightNode node)
        {
            _children.Add(node);
        }

        public void AddCssClass(string cssClass)
        {
            if (!_cssClasses.Contains(cssClass))
            {
                _cssClasses.Add(cssClass);
            }
        }

        public int ChildrenCount => _children.Count;

        public override string InnerHTML
        {
            get
            {
                if (Closing == ClosingType.Single)
                {
                    return string.Empty; // Одиничні теги не мають внутрішнього контенту
                }

                var sb = new StringBuilder();
                foreach (var child in _children)
                {
                    sb.Append(child.OuterHTML);
                }
                return sb.ToString();
            }
        }

        public override string OuterHTML
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"<{TagName}");

                if (_cssClasses.Count > 0)
                {
                    sb.Append($" class=\"{string.Join(" ", _cssClasses)}\"");
                }

                if (Closing == ClosingType.Single)
                {
                    sb.Append(" />");
                }
                else
                {
                    sb.Append(">");
                    sb.Append(InnerHTML);
                    sb.Append($"</{TagName}>");
                }

                return sb.ToString();
            }
        }
        // --- ПАТЕРН ІТЕРАТОР ---

        // 1. Обхід в глибину (Depth-First Search - DFS)
        public IEnumerable<LightNode> GetDepthFirstIterator()
        {
            // Повертаємо поточний вузол
            yield return this;

            // Рекурсивно обходимо всіх дітей
            foreach (var child in _children)
            {
                if (child is LightElementNode elementNode)
                {
                    foreach (var nestedChild in elementNode.GetDepthFirstIterator())
                    {
                        yield return nestedChild;
                    }
                }
                else
                {
                    yield return child; // Повертаємо текстовий вузол
                }
            }
        }

        // 2. Обхід в ширину (Breadth-First Search - BFS)
        public IEnumerable<LightNode> GetBreadthFirstIterator()
        {
            // Використовуємо чергу для обходу рівнями
            var queue = new Queue<LightNode>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;

                // Якщо це елемент, додаємо його дітей в чергу
                if (current is LightElementNode elementNode)
                {
                    foreach (var child in elementNode._children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }
    }
}