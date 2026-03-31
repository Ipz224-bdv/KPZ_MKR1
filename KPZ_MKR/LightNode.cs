namespace LightHTML_System
{
    public abstract class LightNode
    {
        public abstract string InnerHTML { get; }
        public abstract string OuterHTML { get; }

        // --- ПАТЕРН ШАБЛОННИЙ МЕТОД ---

        // Шаблонний метод, що задає алгоритм життєвого циклу
        public string Render()
        {
            OnCreated();
            OnStylesApplied();
            OnTextRendered();

            // Основна логіка отримання HTML (вже реалізована в дочірніх класах)
            string html = OuterHTML;

            OnRendered();

            return html;
        }

        // Віртуальні хуки (порожні за замовчуванням)
        protected virtual void OnCreated() { }
        protected virtual void OnStylesApplied() { }
        protected virtual void OnTextRendered() { }
        protected virtual void OnRendered() { }
    }
}