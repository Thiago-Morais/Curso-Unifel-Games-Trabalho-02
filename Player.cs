
// [ ] O jogo da velha é jogado por dois jogadores, onde um vai ser o X e o outro o O.
// [ ] Ao fazer as jogadas, o jogo deve verificar quem ganhou ou se deu velha.
// [ ] Durante o jogo deve ser possível ver o placar dos jogadores, inclusive a quantidade de vezes que a Velha aconteceu.

public class Player
{
    string symbol;
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