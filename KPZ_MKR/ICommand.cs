namespace LightHTML_System
{
    public interface ICommand
    {
        void Execute(); // Виконати дію
        void Undo();    // Скасувати дію
    }
}