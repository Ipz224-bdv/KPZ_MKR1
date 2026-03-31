namespace KPZ_MKR.Patterns
{
    // 1. Нормальний стан: просто повертає HTML як є
    public class NormalState : INodeState
    {
        public string HandleRender(string originalHtml)
        {
            return originalHtml;
        }
    }

    // 2. Прихований стан: замість HTML повертає коментар або порожній рядок
    public class HiddenState : INodeState
    {
        public string HandleRender(string originalHtml)
        {
            // Замість рендерингу всього дерева, ми просто кажемо, що елемент приховано
            return "";
        }
    }
}