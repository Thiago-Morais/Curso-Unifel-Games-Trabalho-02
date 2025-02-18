public static class Util
{
    public static ConsoleKeyInfo WaitForAnyKey(string message = "\nAperte qualquer botão para continuar.")
    {
        Console.Write(message);
        return Console.ReadKey();
    }
}
