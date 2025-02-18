namespace Curso_Unifel_Games_Trabalho_02
{
    public class Player
    {
        readonly string symbol;
        string name;
        int score;

        public string Symbol => symbol;
        public string Name => name;
        public int WinCount => score;

        public Player(string symbol, string name = null, int score = 0)
        {
            this.symbol = symbol;
            this.name = name;
            this.score = score;
        }
        public void Win() => score++;
        public void SetName(string name) => this.name = name;
    }
}