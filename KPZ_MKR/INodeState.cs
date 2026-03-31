namespace LightHTML_System
{
    public interface INodeState
    {
        // Метод отримує згенерований HTML і вирішує, що з ним робити
        string HandleRender(string originalHtml);
    }
}