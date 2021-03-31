# csharpgame
A simple game for two players (on the same keyboard) written in csharp and windows forms

build and run in console with mono on unik like systems:
$ cd panacci
$ mcs -r:System.Drawing -r:System.Windows.Forms -out:panaci Program.cs 
$ ./panaci

build and run in your IDE (tested with Monodevelop on Linux, and MS Visual Studio on MS Windows):
open the panacci.sln file in your IDE

play the game:

left player: move with AWD, fire with QE
right player: move with numpad 468, fire with numpad 79
start a new game (when at least one player is dead): enter

