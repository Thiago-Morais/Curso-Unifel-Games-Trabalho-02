
// Trabalho #2 - Jogo da Velha
// Desenvolva um jogo da Velha.
// [ ] O jogo da velha é jogado por dois jogadores, onde um vai ser o X e o outro o O.
// [ ] Ao fazer as jogadas, o jogo deve verificar quem ganhou ou se deu velha.
// [ ] Durante o jogo deve ser possível ver o placar dos jogadores, inclusive a quantidade de vezes que a Velha aconteceu.
// [x] Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 
// [ ] O jogo se encerra ao informar o valor 0.
using static Util;
using static System.Console;

public class TicTacToe
{
    // Input, maybe extract into its own class
    public const string INPUT_EXIT = "0";
    HashSet<string> validInput = new HashSet<string>() { "x", "o", "0" };
    string? input;
    public string? Input { get => input; set => input = value; }

    Board currentBoard = new();
    Player player1 = new();
    Player player2 = new();
    List<Player> winnerHistory = new();
    GameStates currentState;
    enum GameStates
    {
        Welcome,
        GameStart,
        GameTurn,
    }

    public void Render()
    {
        string text;
        switch (currentState)
        {
            case GameStates.Welcome:
                Board board = new(new string[,]
                {
                    { "O", ".", "X" },
                    { "X", "X", "." },
                    { "O", "O", "O" }
                });
                text =
@$"JOGO DA VELHA

{board}

Nesse jogo você joga com dois jogadores
Alternando entre cada jogador em cada turno";
                Clear();
                WriteLine(text);
                break;
            case GameStates.GameTurn:
                text =
@$"
{currentBoard}
";
                currentBoard.TrySetElementOnBoard("a2", "X");
                WriteLine(text);
                text =
@$"
{currentBoard}
";
                currentBoard.TrySetElementOnBoard("1b", "O");
                WriteLine(text);
                text =
@$"
{currentBoard}
";
                WriteLine(text);
                WaitAnyInput();

                // Clear();
                // WriteLine(text);
                break;
            default: break;
        }
    }
    public void HandleInput()
    {
        switch (currentState)
        {
            case GameStates.Welcome:
                WaitAnyInput();
                // currentState = GameStates.GameStart;
                currentState = GameStates.GameTurn;
                break;
            case GameStates.GameTurn:
                break;
            default: break;
        }
    }
    void WaitAnyInput()
    {
        ConsoleKeyInfo consoleKeyInfo = WaitForAnyKey();
        input = consoleKeyInfo.Key.ToString();
    }

    bool IsInputValid(string? input) => input != null && validInput.Contains(input);
}
