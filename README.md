# Curso-Unifel-Games-Trabalho-02

Segundo trabalho do curso `Desenvolvimento de Jogos_Games_Turma 03` da Unifel.

## Requisitos

![alt text](<Readme src/png/image.png>)

## State Flow Chart

```mermaid
stateDiagram-v2
    Welcome
    InputNamePlayer1
    InputNamePlayer2
    NewGame
    PlayerTurn
    InvalidInput
    TieScreen
    WinnerScreen
    NonEmptySpace
    state has_game_ended <<choice>>
    state is_there_a_winner <<choice>>

    [*] --> Welcome
    Welcome --> InputNamePlayer1
    InputNamePlayer1 --> InputNamePlayer2
    InputNamePlayer2 --> NewGame: Player 1 starts
    NewGame --> PlayerTurn
    PlayerTurn --> InvalidInput
    InvalidInput --> PlayerTurn
    PlayerTurn --> NonEmptySpace
    NonEmptySpace --> PlayerTurn
    PlayerTurn --> has_game_ended
    has_game_ended --> PlayerTurn : Game has not yet ended, change players
    has_game_ended --> is_there_a_winner : Game ended
    is_there_a_winner --> TieScreen : There is NO winner
    is_there_a_winner --> WinnerScreen : There IS a winner
    TieScreen --> NewGame: The player that played second starts
    WinnerScreen --> NewGame: Loser player starts
```
