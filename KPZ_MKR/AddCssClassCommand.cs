namespace LightHTML_System
{
    public class AddCssClassCommand : ICommand
    {
        private readonly LightElementNode _node;
        private readonly string _cssClass;

        public AddCssClassCommand(LightElementNode node, string cssClass)
        {
            _node = node;
            _cssClass = cssClass;
        }

        public void Execute()
        {
            _node.AddCssClass(_cssClass);
        }

        public void Undo()
        {
            _node.RemoveCssClass(_cssClass);
        }
    }
}