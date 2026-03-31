namespace KPZ_MKR.Patterns
{
    public interface INodeState
    {
        // Метод отримує згенерований HTML і вирішує, що з ним робити
        string HandleRender(string originalHtml);
    }
}