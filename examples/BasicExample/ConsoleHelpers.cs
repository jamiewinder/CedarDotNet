namespace BasicExample;

internal static class ConsoleHelpers
{
    public static void WriteTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("*** ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(title);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" ***");
        Console.ResetColor();
    }

    public static void WriteAnswer<T>(T answer)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("-> ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(answer);
        Console.ResetColor();
    }
}