using KPZ_MKR.Infrastructure;
using KPZ_MKR.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace KPZ_MKR.Core
{
    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        public DisplayType Display { get; }
        public ClosingType Closing { get; }

        // Інкапсульовані колекції
        private readonly List<string> _cssClasses;
        private readonly List<LightNode> _children;

        // --- ПАТЕРН СТАН (STATE) ---
        private INodeState _state;

        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            TagName = tagName;
            Display = display;
            Closing = closing;
            _cssClasses = new List<string>();
            _children = new List<LightNode>();

            // За замовчуванням всі елементи у нормальному стані
            _state = new NormalState();
        }

        // Метод для зміни стану "на льоту"
        public void SetState(INodeState state)
        {
            _state = state;
        }

        public void AddChild(LightNode node) => _children.Add(node);

        public void AddCssClass(string cssClass)
        {
            if (!_cssClasses.Contains(cssClass)) _cssClasses.Add(cssClass);
        }

        public void RemoveCssClass(string cssClass)
        {
            if (_cssClasses.Contains(cssClass)) _cssClasses.Remove(cssClass);
        }

        public int ChildrenCount => _children.Count;

        public override string InnerHTML
        {
            get
            {
                if (Closing == ClosingType.Single) return string.Empty;

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
                // 1. Стандартна генерація HTML-структури
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

                string standardHtml = sb.ToString();

                // 2. ДЕЛЕГУВАННЯ СТАНУ (Паттерн State)
                // Стан вирішує, чи модифікувати фінальний рядок (наприклад, додати підсвітку або приховати)
                return _state.HandleRender(standardHtml);
            }
        }

        // --- ПАТЕРН ІТЕРАТОР (ITERATOR) ---

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

                // Якщо це елемент, додаємо його дітей в чергу для подальшого опрацювання
                if (current is LightElementNode elementNode)
                {
                    foreach (var child in elementNode._children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }
        // --- РЕАЛІЗАЦІЯ ХУКІВ (ШАБЛОННИЙ МЕТОД) ---

        protected override void OnCreated()
        {
            Console.WriteLine($"[Hook - LightElementNode]: Створено вузол <{TagName}>");
        }

        protected override void OnStylesApplied()
        {
            if (_cssClasses.Count > 0)
            {
                Console.WriteLine($"[Hook - LightElementNode]: До <{TagName}> застосовано класи: {string.Join(", ", _cssClasses)}");
            }
        }

        protected override void OnRendered()
        {
            Console.WriteLine($"[Hook - LightElementNode]: Завершено рендеринг вузла <{TagName}>");
        }
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this); // Елемент приймає відвідувача

            // Відвідувач йде далі по всьому дереву дочірніх елементів
            foreach (var child in _children)
            {
                child.Accept(visitor);
            }
        }

    }
}