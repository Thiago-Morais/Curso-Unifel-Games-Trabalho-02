# Curso-Unifel-Games-Trabalho-02

Segundo trabalho do curso `Desenvolvimento de Jogos_Games_Turma 03` da Unifel.

## Requisitos

![alt text](<Readme src/png/image.png>)

## State Flow Chart

```mermaid
stateDiagram-v2
    Welcome
    InputNames
    NewGame
    GameTurn
    InvalidInput
    WinnerScreen
    NonEmptySpace
    state is_there_a_winner <<choice>>
    is_there_a_winner_no: No Winner
    is_there_a_winner_yes: There is a winner

    [*] --> Welcome
    Welcome --> InputNames
    InputNames --> NewGame
    NewGame --> GameTurn: Player 1 / Loser Player
    GameTurn --> InvalidInput
    InvalidInput --> GameTurn
    GameTurn --> NonEmptySpace
    NonEmptySpace --> GameTurn
    GameTurn --> is_there_a_winner
    is_there_a_winner --> is_there_a_winner_no
    is_there_a_winner --> is_there_a_winner_yes
    is_there_a_winner_no --> GameTurn: Other Player
    is_there_a_winner_yes --> WinnerScreen
    WinnerScreen --> NewGame
```
