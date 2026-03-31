using KPZ_MKR.Patterns;

namespace KPZ_MKR.Core
{
    public abstract class LightNode
    {
        public abstract string InnerHTML { get; }
        public abstract string OuterHTML { get; }

        public string Render()
        {
            OnCreated();
            OnStylesApplied();
            OnTextRendered();

            string html = OuterHTML;

            OnRendered();

            return html;
        }

        protected virtual void OnCreated() { }
        protected virtual void OnStylesApplied() { }
        protected virtual void OnTextRendered() { }
        protected virtual void OnRendered() { }

        public abstract void Accept(INodeVisitor visitor);
    }
}