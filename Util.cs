public static class Util
{
    public static ConsoleKeyInfo WaitForAnyKey()
    {
        Console.Write("\nAperte qualquer botão para continuar.");
        return Console.ReadKey();
    }
}
