
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
    string? input;
    public string? Input { get => input; set => input = value; }

    Board currentBoard = new();
    Player player1 = new("X");
    Player player2 = new("O");
    int turnPlayerIndex = 1;
    List<Player> winnerHistory = new();
    GameStates currentState;
    enum GameStates
    {
        Welcome,
        InputNamePlayer1,
        InputNamePlayer2,
        NewGame,
        GameTurn,
        InvalidInput,
        WinnerScreen,
        NonEmptySpace,
    }
    public void Render()
    {
        string text = "";
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
                break;
            case GameStates.InputNamePlayer1:
            case GameStates.InputNamePlayer2:
                text = @$"Criação de personagem:";
                break;
            case GameStates.NewGame:
                text =
@$"Novo Jogo!

{player1.Name} vs {player2.Name}

Placar atual:
{player1.Name}: {player1.WinCount}
{player2.Name}: {player2.WinCount}
Velha: {GetTiesCount()}";
                break;
            case GameStates.GameTurn:
                text =
@$"
Vez do jogador {player1.Name}.

{currentBoard}

";
                break;
            case GameStates.WinnerScreen:
                Player winner = GetWinner();
                text =
@$"O jogador {winner.Name} ganhou!

{currentBoard}

Parabéns {winner.Name}!";
                break;
            default: break;
        }

        Clear();
        WriteLine(text);
    }
    int GetTiesCount() => winnerHistory.Count(x => x == null);
    public void HandleInput()
    {
        string text;
        switch (currentState)
        {
            case GameStates.GameTurn:
                WriteLine($"\nDigite as coordenadas da sua jogada com a, b ou c para coluna e 1, 2 ou 3 para linha.");
                input = ReadLine();
                return;
            case GameStates.InputNamePlayer1:
                WriteLine("\nDigite o nome do jogador 1:");
                input = ReadLine();
                return;
            case GameStates.InputNamePlayer2:
                WriteLine("\nDigite o nome do jogador 2:");
                input = ReadLine();
                return;
            default:
                WaitAnyInput();
                return;
        }
    }
    void WaitAnyInput()
    {
        ConsoleKeyInfo consoleKeyInfo = WaitForAnyKey();
        input = consoleKeyInfo.Key.ToString();
    }

    public void ProcessAfter()
    {
        switch (currentState)
        {
            case GameStates.Welcome:
                currentState = GameStates.InputNamePlayer1;
                return;
            case GameStates.InputNamePlayer1:
                if (input == "") player1.SetName("Jogador 1");
                else player1.SetName(input);
                currentState = GameStates.InputNamePlayer2;
                return;
            case GameStates.InputNamePlayer2:
                if (input == "") player2.SetName("Jogador 2");
                else player2.SetName(input);
                currentState = GameStates.NewGame;
                return;
            case GameStates.NewGame:
                currentState = GameStates.GameTurn;
                return;
            case GameStates.GameTurn:
                if (!Board.TryParseCoords(input, out Vector2Int coords))
                {
                    currentState = GameStates.InvalidInput;
                    return;
                }
                if (currentBoard.GetSymbolAt(coords) != Board.FREE_SPACE)
                {
                    currentState = GameStates.NonEmptySpace;
                    return;
                }
                currentBoard.SetElementOnBoard(coords, GetTurnPlayer().Symbol);
                // Verify if there is a winner
                if (currentBoard.Winner != null)
                {
                    currentState = GameStates.WinnerScreen;
                }
                return;
            case GameStates.WinnerScreen:
                // Change current player
                Player winner = GetWinner();
                winner?.Win();
                winnerHistory.Add(winner);

                SwitchTurnPlayer();
                currentState = GameStates.GameTurn;
                return;
            default:
                return;
        }
    }
    Player GetWinner()
    {
        if (currentBoard.Winner == player1.Symbol)
            return player1;
        else if (currentBoard.Winner == player2.Symbol)
            return player2;
        return null;
    }

    public Player GetTurnPlayer() => turnPlayerIndex == 1 ? player1 : player2;
    public void SwitchTurnPlayer()
    {
        if (turnPlayerIndex == 1)
            turnPlayerIndex = 2;
        else
            turnPlayerIndex = 1;
    }
}
