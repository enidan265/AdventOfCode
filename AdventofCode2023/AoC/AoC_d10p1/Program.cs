
//(vertical, horizontal)

Dictionary<char, (int, int)[]> map = new Dictionary<char, (int, int)[]>();
map.Add('|', new (int, int)[] { (1, 0), (-1, 0) });
map.Add('-', new (int, int)[] { (0, 1), (0, -1) });
map.Add('L', new (int, int)[] { (-1, 1), (1, -1) });

//| is a vertical pipe connecting north and south.
//- is a horizontal pipe connecting east and west.
//L is a 90-degree bend connecting north and east.
//J is a 90-degree bend connecting north and west.
//7 is a 90-degree bend connecting south and west.
//F is a 90-degree bend connecting south and east.
//. is ground; there is no pipe in this tile.
//S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.



//{ '|', new (int, int)[] { (-1, 0), (1, 0) }},   // Vertical pipe
//{ '-', new (int, int)[] { (0, -1), (0, 1) }},   // Horizontal pipeß