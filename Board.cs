namespace Curso_Unifel_Games_Trabalho_02
{
    // [x] Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 

    using System.Text.RegularExpressions;

    public partial class Board
    {
        public const string FREE_SPACE = ".";
        readonly string[,] data = new string[3, 3] { { FREE_SPACE, FREE_SPACE, FREE_SPACE }, { FREE_SPACE, FREE_SPACE, FREE_SPACE }, { FREE_SPACE, FREE_SPACE, FREE_SPACE } };
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
        string winner;
        public string[,] Data => data;
        public string Winner => winner;

        public Board() { }
        public Board(string[,] data)
        {
            this.data = data;
        }
        public void SetElementOnBoard(Vector2Int coords, string value)
        {
            data[coords.y, coords.x] = value;
            winner = CalculateWinnerSymbol();
        }
        string CalculateWinnerSymbol()
        {
            // Go over every win condition and return if there is a winner
            for (int i = 0; i < WIN_CONDITIONS.Length; i++)
            {
                (int x, int y) firstPossibility = WIN_CONDITIONS[i][0];
                string possibleWinner = data[firstPossibility.y, firstPossibility.x];
                if (possibleWinner == FREE_SPACE)
                    continue;

                for (int j = 1; j < WIN_CONDITIONS[i].Length; j++)
                {
                    (int x, int y) nextPossibility = WIN_CONDITIONS[i][j];
                    if (possibleWinner != data[nextPossibility.y, nextPossibility.x])
                        break;

                    bool isLastInnerCondition = j == WIN_CONDITIONS[i].Length - 1;
                    if (isLastInnerCondition)
                        return possibleWinner;
                }
            }
            return null;
        }
        public bool HasGameEnded()
        {
            if (winner != null) return true;

            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    if (data[j, i] == FREE_SPACE)
                        return false;
            return true;
        }
        public string GetSymbolAt(Vector2Int coords)
        {
            return data[coords.y, coords.x];
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
        public static string FormatCoords(Vector2Int coords) => $"({(char)(coords.x + 'a')}, {(char)(coords.y + '1')})";
    }
}