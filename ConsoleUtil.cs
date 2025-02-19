namespace Curso_Unifel_Games_Trabalho_02
{
    public static class ConsoleUtil
    {
        public static ConsoleKeyInfo WaitForAnyKey(string message = "\nAperte qualquer botão para continuar.")
        {
            Console.Write(message);
            return Console.ReadKey();
        }
    }
}