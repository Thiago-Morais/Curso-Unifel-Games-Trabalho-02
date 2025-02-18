// [x] Também deve ser possível mostrar o tabuleiro do jogo após cada jogada! 

using System.Numerics;
using System.Text.RegularExpressions;

public partial class Board
{
    string[,] data = new string[3, 3] { { ".", ".", "." }, { ".", ".", "." }, { ".", ".", "." } };
    public string[,] Data { get => data; set => data = value; }

    public Board() { }
    public Board(string[,] data)
    {
        this.data = data;
    }
    public bool TrySetElementOnBoard(string coordsText, string value)
    {
        if (TryParseCoords(coordsText, out Vector2Int coords))
        {
            data[coords.x, coords.y] = value;
            return true;
        }
        return false;
    }
    static bool TryParseCoords(string coordsText, out Vector2Int coordsInt)
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

}