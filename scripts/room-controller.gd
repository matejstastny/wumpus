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

func _physics_process(delta):
    if Input.is_action_just_pressed("go_left"):
        _move_player(1, 0)
    elif Input.is_action_just_pressed("go_right"):
        _move_player(-1, 0)
    elif Input.is_action_just_pressed("go_up"):
        _move_player(0, -1)
    elif Input.is_action_just_pressed("go_down"):
        _move_player(0, 1)

func _move_player(x, y):
    pass
