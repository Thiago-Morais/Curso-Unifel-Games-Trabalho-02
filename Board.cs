// [x] Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 

using System.Text.RegularExpressions;

public partial class Board
{
    public const string FREE_SPACE = ".";
    string[,] data = new string[3, 3] { { FREE_SPACE, FREE_SPACE, FREE_SPACE }, { FREE_SPACE, FREE_SPACE, FREE_SPACE }, { FREE_SPACE, FREE_SPACE, FREE_SPACE } };
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
        data[coords.x, coords.y] = value;
        ProcessWinner();
    }
    void ProcessWinner()
    {
        throw new NotImplementedException();
    }
    public string GetSymbolAt(Vector2Int coords)
    {
        return data[coords.x, coords.y];
    }

    public override string ToString()
    {
        return
@$"╔ a ╦ b ╦ c ╗
1 {data[0, 0]} ║ {data[1, 0]} ║ {data[2, 0]} ║
╠═══╬═══╬═══╣
2 {data[0, 1]} ║ {data[1, 1]} ║ {data[2, 1]} ║
╠═══╬═══╬═══╣
3 {data[0, 2]} ║ {data[1, 2]} ║ {data[2, 2]} ║
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

}