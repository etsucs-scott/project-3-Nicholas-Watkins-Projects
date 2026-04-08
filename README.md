### Building and running

While in the project directory, run:
```bash
dotnet build
dotnet run --project src/Minesweeper.Console
```

### Board size 

The board can come in 3 sizes:
```
8x8 - 10 mines
12x12 - 25 mines
16x16 - 40 mines
```
You choose the size at the start of the game.

### Input commands
```
r 0 0 - reveal row column 
f 0 0 - flag row column
q - quits the game
```

### Highscores

Highscores are stored in save.csv in the project directory.
You can open it in Microsoft Excel or LibreOffice Calc.
The format is:
board size, 
seconds it took to win the game, 
number of moves it took to win the game, 
the games seed, 
and the timestamp of when the game was complete.
i.e.
"size, seconds, moves, seed, timestamp"

### Board symbols
```
Hidden tiles -> #
Flag -> f
Mine (hit) -> b
Empty space -> .
Mines close by -> 1-8
```

### How to run unit tests

while in the project directory, run:
 ```bash
dotnet build
dotnet test
 ```