extends Node

var room_grid: Array = []
var current_room: Vector2 = Vector2(0, 0)
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

func move_player(_x, _y) -> bool:
    var can_move_x = false
    var can_move_y = false
    if current_room.x + _x >= 0 and current_room.x + _x <= room_grid_width:
        current_room.x += _x
        can_move_x = true
    if current_room.y + _y >= 0 and current_room.y + _y <= room_grid_height:
        current_room.y += _y
        can_move_y = true
    print("Current Room data: " + str(_get_current_room()))
    print("Current Room pos: " + str(current_room.x) + ", " + str(current_room.y))
    print("Can Move: " + str(can_move_x))
    return can_move_x and can_move_y

func _get_current_room():
    return room_grid[current_room.x][current_room.y]
