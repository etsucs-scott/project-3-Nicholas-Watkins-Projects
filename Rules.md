Due: APRIL 8th


### CUSTOM TASK - TODO

Issues:
```
    - CSVTest.cs will take negative time,
    - Game.cs needs to be implemented and same goes for seed.cs,
    - Invaild input still an issue
    - Tiebreaker using moves has not been implemented in CSVTest.cs in the sort
    - Issue where some maps have a lack of bombs 
```
TODO:
```
    - Xunit tests for both console and core
    - UML diagram
    - Commenting and XML documentation
```


### Tasks

Architectural Rule (Required)
All game rules, board logic, persistence, and validation must live in Minesweeper.Core. The console project only handles input and rendering.


### Task 1 — Core Game Logic

Support three board sizes: 8x8, 12x12, 16x16.
Mine counts: 8x8 = 10, 12x12 = 25, 16x16 = 40.
Use a seed to generate mine placement deterministically.
Reveal logic must correctly compute adjacent mine counts.
Support flagging and unflagging tiles.
Win condition: all non-mine tiles revealed without triggering a mine.


### Task 2 — Files + Exception Handling

Load high scores from a file in data/ (create if missing).
Record new high scores back to the same file.
Handle missing/invalid files safely (no crashes; show meaningful errors).

High Score Rules (Required)
High score = fastest completion time in seconds.
Tie-breaker: fewer moves wins.
Keep top 5 scores per board size.
File format: CSV with header size,seconds,moves,seed,timestamp.


### Seed Rules (Required)

Prompt for a seed (integer). If the user leaves it blank, generate one from the current time.
Display the seed used for the current game.
Seed must fully determine mine placement for reproducible games.


### Input Format (Required)

Use 0-indexed coordinates: row col.
Commands: r row col (reveal), f row col (flag/unflag), q (quit).


### Board Symbols (Required)

Hidden: #
Flag: f
Bomb (hit): b
Empty revealed: .
Numbers: 1 through 8


### Edge Cases (Required)

Revealing a zero-adjacent tile cascades to neighbors (classic Minesweeper).
Flagged tiles cannot be revealed until unflagged.
Invalid input shows an error and re-prompts without crashing.


### Task 3 — Console UI

Provide a menu to choose board size and seed.
Render the board using console characters for hidden, revealed, flagged, and mined tiles.
Accept moves (reveal/flag) and refresh the board after each action.
Show win/lose messages and update high scores on win.
After win/loss, return to the main menu to allow another game.


### Task 4 — UML Diagram

Create a UML class diagram for the core game design.
Include classes, relationships, and key responsibilities.
Submit your .asta file or a PDF/PNG export.


### Task 5 — Unit Tests

Use xUnit for tests.
Write at least 10 unit tests.
Include tests for board generation, adjacency counts, cascade reveal, and win/loss rules.
Use the seed to make tests deterministic.


### Task 6 — README.md

Explain how to build and run the game.
Document board sizes, input commands, and seed usage.
Explain where high scores are stored and the CSV format.
List the board symbols.
Explain how to run the unit tests.
Add code comments for each class, each function, and any non-obvious or tricky code blocks.


