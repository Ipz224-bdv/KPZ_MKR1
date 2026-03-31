namespace LightHTML_System
{
    public enum DisplayType
    {
        Block,
        Inline
    }

    public enum ClosingType
    {
        Paired, // Має відкриваючий і закриваючий теги (наприклад, <div></div>)
        Single  // Одиничний тег (наприклад, <img/>)
    }
}