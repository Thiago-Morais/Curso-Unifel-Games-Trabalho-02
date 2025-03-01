﻿
// Trabalho #2 - Jogo da Velha
// Desenvolva um jogo da Velha.
// O jogo da velha é jogado por dois jogadores, onde um vai ser o X e o outro o O.
// Ao fazer as jogadas, o jogo deve verificar quem ganhou ou se deu velha.
// Durante o jogo deve ser possível ver o placar dos jogadores, inclusive a quantidade de vezes que a Velha aconteceu.
// Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 
// O jogo se encerra ao informar o valor 0.

using Curso_Unifel_Games_Trabalho_02;

internal class Program
{
    static void Main(string[] args)
    {
        TicTacToe ticTacToe = new();

        do
        {
            ticTacToe.PreProcessState();
            ticTacToe.Render();
            ticTacToe.HandleInput();
            ticTacToe.ProcessInputAndStateChange();
        }
        while (ticTacToe.Input != TicTacToe.INPUT_EXIT);

        Console.Clear();
        Console.WriteLine(
@$"Obrigado por jogar!

Placar final:
{ticTacToe.ScoreAsString(false)}");
        ConsoleUtil.WaitForAnyKey("\nAperte qualquer botão para sair.");
    }
}