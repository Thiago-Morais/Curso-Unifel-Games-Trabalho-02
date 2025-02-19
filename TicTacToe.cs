// Trabalho #2 - Jogo da Velha
// Desenvolva um jogo da Velha.
//  O jogo da velha é jogado por dois jogadores, onde um vai ser o X e o outro o O.
// [x] Ao fazer as jogadas, o jogo deve verificar quem ganhou ou se deu velha.
// [/] Durante o jogo deve ser possível ver o placar dos jogadores, inclusive a quantidade de vezes que a Velha aconteceu.
// [x] Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 
// [x] O jogo se encerra ao informar o valor 0.
namespace Curso_Unifel_Games_Trabalho_02
{
    using static ConsoleUtil;
    using static System.Console;

    public class TicTacToe
    {
        // Input, maybe extract into its own class
        public const string INPUT_EXIT = "0";
        private const string DEFAULT_PLAYER_1_NAME = "Jogador 1";
        private const string DEFAULT_PLAYER_2_NAME = "Jogador 2";
        string input;
        public string Input { get => input; set => input = value; }

        Board currentBoard = new();
        readonly Player player1 = new("X");
        readonly Player player2 = new("O");
        Player firstTurnPlayer;
        Player currentTurnPlayer;
        Player currentWinner;
        int tieCount;
        GameStates currentState;
        Vector2Int cacheLastCoordsInput;

        enum GameStates
        {
            Welcome,
            InputNamePlayer1,
            InputNamePlayer2,
            NewGame,
            GameTurn,
            InvalidInput,
            TieScreen,
            WinnerScreen,
            NonEmptySpace,
        }
        public TicTacToe()
        {
            firstTurnPlayer = player1;
        }

        public void PreProcessState()
        {
            switch (currentState)
            {
                case GameStates.NewGame:
                    currentTurnPlayer = firstTurnPlayer;
                    currentBoard = new();
                    // Test tie
                    // currentBoard = new(new string[,]{
                    //     { "X", "O", "X" },
                    //     { "X", "O", "." },
                    //     { "O", "X", "O" }});
                    // Test win
                    // currentBoard = new(new string[,]{
                    //     { "X", "O", "X" },
                    //     { "X", "O", "O" },
                    //     { ".", "X", "O" }});
                    // currentBoard = new(new string[,]{
                    //     { "O", "O", "." },
                    //     { ".", "X", "." },
                    //     { "X", ".", "O" }});
                    return;
                case GameStates.TieScreen:
                    firstTurnPlayer = GetOtherPlayer(firstTurnPlayer);
                    tieCount++;
                    return;
                case GameStates.WinnerScreen:
                    currentWinner = GetWinner();
                    currentWinner?.Win();
                    firstTurnPlayer = GetOtherPlayer(currentWinner);
                    return;
                default:
                    return;
            }
        }
        public void Render()
        {
            string overrideConsoleText = "";
            switch (currentState)
            {
                case GameStates.Welcome:
                    Board exampleBoard = new(new string[,]
                    {
                    { "O", ".", "X" },
                    { "X", "X", "." },
                    { "O", "O", "O" }
                    });
                    overrideConsoleText =
@$"JOGO DA VELHA

{exampleBoard}

Nesse jogo você joga com dois jogadores
Alternando entre cada jogador em cada turno";
                    break;
                case GameStates.InputNamePlayer1:
                case GameStates.InputNamePlayer2:
                    overrideConsoleText = @$"Criação de personagem:";
                    break;
                case GameStates.NewGame:
                    overrideConsoleText =
@$"Novo Jogo!

{player1.Name} vs {player2.Name}

{ScoreAsString()}";
                    break;
                case GameStates.GameTurn:
                    overrideConsoleText =
@$"
Vez do jogador {currentTurnPlayer.Name}.

{currentBoard}

";
                    break;
                case GameStates.InvalidInput:
                    overrideConsoleText =
@$"Entrada inválida.

certifique de que suas coordenadas contenham a, b ou c para coluna e 1, 2 ou 3 para linha.";
                    break;
                case GameStates.NonEmptySpace:
                    overrideConsoleText =
@$"Este espaço {Board.FormatCoords(cacheLastCoordsInput)} já está ocupado.

{currentBoard}
";
                    break;
                case GameStates.TieScreen:
                    overrideConsoleText =
@$"Deu velha.

{currentBoard}

{ScoreAsString()}";
                    break;
                case GameStates.WinnerScreen:
                    Player winner = GetWinner();
                    overrideConsoleText =
@$"O jogador {winner.Name} ganhou!

{currentBoard}

Parabéns {winner.Name}!

{ScoreAsString()}";
                    break;
                default: break;
            }

            Clear();
            WriteLine(overrideConsoleText);
        }
        string ScoreAsString()
        {
            return
@$"Placar atual:
{player1.Name}: {player1.WinCount}
{player2.Name}: {player2.WinCount}
Velha: {tieCount}";
        }
        public void HandleInput()
        {
            switch (currentState)
            {
                case GameStates.InputNamePlayer1:
                    WriteLine("\nDigite o nome do jogador 1:");
                    input = ReadLine();
                    return;
                case GameStates.InputNamePlayer2:
                    WriteLine("\nDigite o nome do jogador 2:");
                    input = ReadLine();
                    return;
                case GameStates.GameTurn:
                    WriteLine($"\nDigite as coordenadas da sua jogada com a, b ou c para coluna e 1, 2 ou 3 para linha.");
                    input = ReadLine();
                    return;
                default:
                    WaitAnyInput();
                    return;
            }
        }
        void WaitAnyInput()
        {
            ConsoleKeyInfo consoleKeyInfo = WaitForAnyKey("\nAperte qualquer botão para continuar, ou 0 para sair.");
            input = consoleKeyInfo.Key.ToString();
            if (input.Contains(INPUT_EXIT))
                input = INPUT_EXIT;
        }

        public void ProcessInputAndStateChange()
        {
            switch (currentState)
            {
                case GameStates.Welcome:
                    currentState = GameStates.InputNamePlayer1;
                    return;
                case GameStates.InputNamePlayer1:
                    if (input != "") player1.SetName(input);
                    else player1.SetName(DEFAULT_PLAYER_1_NAME);
                    currentState = GameStates.InputNamePlayer2;
                    return;
                case GameStates.InputNamePlayer2:
                    if (input != "") player2.SetName(input);
                    else player2.SetName(DEFAULT_PLAYER_2_NAME);
                    currentState = GameStates.NewGame;
                    return;
                case GameStates.NewGame:
                    currentState = GameStates.GameTurn;
                    return;
                case GameStates.GameTurn:
                    if (!Board.TryParseCoords(input!, out Vector2Int coords))
                    {
                        currentState = GameStates.InvalidInput;
                        return;
                    }

                    cacheLastCoordsInput = coords;
                    if (!currentBoard.IsSpaceFreeAt(coords))
                    {
                        currentState = GameStates.NonEmptySpace;
                        return;
                    }

                    currentBoard.SetElementOnBoard(coords, currentTurnPlayer.Symbol);
                    if (currentBoard.HasGameEnded())
                    {
                        if (currentBoard.Winner == null)
                        {
                            currentState = GameStates.TieScreen;
                            return;
                        }
                        else
                        {
                            currentState = GameStates.WinnerScreen;
                            return;
                        }
                    }
                    else
                    {
                        SwitchTurnPlayer();
                        currentState = GameStates.GameTurn;
                        return;
                    }
                case GameStates.InvalidInput:
                case GameStates.NonEmptySpace:
                    currentState = GameStates.GameTurn;
                    return;
                case GameStates.TieScreen:
                    currentState = GameStates.NewGame;
                    return;
                case GameStates.WinnerScreen:
                    currentState = GameStates.NewGame;
                    return;
                default:
                    return;
            }
        }
        public void SwitchTurnPlayer() => currentTurnPlayer = currentTurnPlayer == player1 ? player2 : player1;
        Player GetWinner()
        {
            if (currentBoard.Winner == player1.Symbol)
                return player1;
            else if (currentBoard.Winner == player2.Symbol)
                return player2;
            return null;
        }
        Player GetOtherPlayer(Player player) => player == player1 ? player2 : player1;
    }
}