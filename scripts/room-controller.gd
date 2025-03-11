extends Node

var room_grid: Array = []
var room_grid_width: int = 5
var room_grid_height: int = 5

func _ready():
	_initialize_room_arr()
	_print_debug()

func _initialize_room_arr():
	for i: int in room_grid_width:
		room_grid.append([])
		for j in room_grid_height:
			room_grid[i].append(str(i) + " , " + str(j))

func _print_debug():
	for i in room_grid_width:
		for j in room_grid_height:
			print("ROOM: " + room_grid[i][j])
