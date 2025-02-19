namespace Curso_Unifel_Games_Trabalho_02
{
    public class Player
    {
        readonly string symbol;
        string name;
        int winCount;

        public string Symbol => symbol;
        public string Name => name;
        public int WinCount => winCount;

        public Player(string symbol, string name = null, int winCount = 0)
        {
            this.symbol = symbol;
            this.name = name;
            this.winCount = winCount;
        }
        public void IncreaseScore() => winCount++;
        public void SetName(string name) => this.name = name;
    }
}