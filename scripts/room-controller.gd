extends Node


var room_size: int = 120
var room_grid_padding: Vector2 = Vector2(100, 80)

var _room_grid: Array = []
var _current_room: Vector2 = Vector2(0, 0)
var _room_grid_width: int = 9
var _room_grid_height: int = 5


# ─── Public ────────────────────────────────────────────────────────────────────


func move_player(_x: int, _y: int) -> bool:
    var new_x = _current_room.x + _x
    var new_y = _current_room.y + _y
    var can_move_x = new_x >= 0 and new_x < _room_grid_width
    var can_move_y = new_y >= 0 and new_y < _room_grid_height

    if can_move_x:
        _current_room.x = new_x
    if can_move_y:
        _current_room.y = new_y

    print("Current Room data: " + str(_get_current_room()))
    print("Current Room pos: " + str(_current_room))
    print("Can Move: " + str(can_move_x and can_move_y))

    return can_move_x and can_move_y


func get_init_player_pos():
    return Vector2(room_grid_padding.x, room_grid_padding.y)


# ─── Private ───────────────────────────────────────────────────────────────────


func _ready():
    _initialize_room_arr()
    _add_room_tiles()
    _print_room_contents()


func _initialize_room_arr():
    _room_grid.resize(_room_grid_width)
    for i in range(_room_grid_width):
        _room_grid[i] = []
        for j in range(_room_grid_height):
            _room_grid[i].append(str(i) + " , " + str(j))


func _add_room_tiles():
    for i in range(_room_grid_width):
        for j in range(_room_grid_height):
            var room_scene = load("res://entities/room.tscn")
            var room_instance = room_scene.instantiate()
            var sprite = room_instance.get_node("Sprite2D")
            sprite.scale = Vector2(room_size / sprite.texture.get_size().x, room_size / sprite.texture.get_size().y)
            room_instance.position = Vector2(i * room_size + room_grid_padding.x, j * room_size + room_grid_padding.y)
            add_child(room_instance)


func _print_room_contents():
    for i in range(_room_grid_width):
        for j in range(_room_grid_height):
            print("ROOM: " + _room_grid[i][j])


func _get_current_room():
    return _room_grid[_current_room.x][_current_room.y]
