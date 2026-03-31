namespace KPZ_MKR.Patterns
{
    public interface ICommand
    {
        void Execute(); // Виконати дію
        void Undo();    // Скасувати дію
    }
}