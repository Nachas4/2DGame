# Adventurer Game

## Before running
You need to copy some files to the directory `\2DGame\bin\Debug\net6.0-windows\Content` from `\2DGame\Content` like so:
- Create `Maps` folder and copy the 10 map files in there
- Copy the files `enemy.sf` and `player.sf`

Now you can debug the program in Visual Studio!

# Possible errors

If you run into the error `"The command "dotnet tool restore" exited with code 1."` then go to `2DGame-master\2DGame\.config\dotnet-tools.json`: and do  right click -> properties -> check the `Unblock` labelled checkbox
