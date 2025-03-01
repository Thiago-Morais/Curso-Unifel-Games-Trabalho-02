namespace Curso_Unifel_Games_Trabalho_02
{
    // [x] Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 

    using System.Text.RegularExpressions;

    public partial class Board
    {
        public const string FREE_SPACE = ".";
        readonly string[,] data = new string[3, 3]
        {
            { FREE_SPACE, FREE_SPACE, FREE_SPACE },
            { FREE_SPACE, FREE_SPACE, FREE_SPACE },
            { FREE_SPACE, FREE_SPACE, FREE_SPACE }
        };
        public static readonly (int, int)[][] WIN_CONDITIONS =
        [
            // Rows
            [(0, 0), (0, 1), (0, 2)],
            [(1, 0), (1, 1), (1, 2)],
            [(2, 0), (2, 1), (2, 2)],
            // Column
            [(0, 0), (1, 0), (2, 0)],
            [(0, 1), (1, 1), (2, 1)],
            [(0, 2), (1, 2), (2, 2)],
            // Diagonals
            [(0, 0), (1, 1), (2, 2)],
            [(0, 2), (1, 1), (2, 0)],
        ];
        string winnerSymbol;
        public string[,] Data => data;
        public string WinnerSymbol => winnerSymbol;

        public Board() { }
        public Board(string[,] data)
        {
            this.data = data;
        }
        public void SetElementOnBoard(Vector2Int coords, string value)
        {
            data[coords.y, coords.x] = value;
            winnerSymbol = CalculateWinnerSymbol();
        }
        string CalculateWinnerSymbol()
        {
            // Go over every win condition and return if there is a winner
            for (int i = 0; i < WIN_CONDITIONS.Length; i++)
            {
                (int x, int y) firstPossibileCoord = WIN_CONDITIONS[i][0];
                string possibleWinnerSymbol = data[firstPossibileCoord.y, firstPossibileCoord.x];
                if (possibleWinnerSymbol == FREE_SPACE)
                    continue;

                // Compare with the other 2 possible coordinates
                for (int j = 1; j < WIN_CONDITIONS[i].Length; j++)
                {
                    (int x, int y) nextCoord = WIN_CONDITIONS[i][j];
                    if (possibleWinnerSymbol != data[nextCoord.y, nextCoord.x])
                        break;

                    bool isLastPossibleCoord = j == WIN_CONDITIONS[i].Length - 1;
                    if (isLastPossibleCoord)
                        return possibleWinnerSymbol;
                }
            }
            return null;
        }
        public bool HasGameEnded() => winnerSymbol != null || !IsThereAnyFreeSpace();
        bool IsThereAnyFreeSpace()
        {
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    if (data[j, i] == FREE_SPACE)
                        return true;
            return false;
        }

        public override string ToString()
        {
            return
@$"╔ a ╦ b ╦ c ╗
1 {data[0, 0]} ║ {data[0, 1]} ║ {data[0, 2]} ║
╠═══╬═══╬═══╣
2 {data[1, 0]} ║ {data[1, 1]} ║ {data[1, 2]} ║
╠═══╬═══╬═══╣
3 {data[2, 0]} ║ {data[2, 1]} ║ {data[2, 2]} ║
╚═══╩═══╩═══╝";
        }

        public static bool TryParseCoords(string coordsText, out Vector2Int coordsInt)
        {
            Match matchColumn = ColumnRegex().Match(coordsText);
            Match matchRow = RowRegex().Match(coordsText);
            if (matchColumn.Success && matchRow.Success)
            {
                coordsInt = new Vector2Int(matchColumn.Value[0] - 'a', matchRow.Value[0] - '1');
                return true;
            }
            else
            {
                coordsInt = default;
                return false;
            }
        }
        [GeneratedRegex(@"[a-c]")] private static partial Regex ColumnRegex();
        [GeneratedRegex(@"[1-3]")] private static partial Regex RowRegex();

        public bool IsSpaceFreeAt(Vector2Int coords) => GetSymbolAt(coords) == FREE_SPACE;
        public string GetSymbolAt(Vector2Int coords) => data[coords.y, coords.x];
        public static string FormatCoords(Vector2Int coords) => $"({(char)(coords.x + 'a')}, {(char)(coords.y + '1')})";
    }
}