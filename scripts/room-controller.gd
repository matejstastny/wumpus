extends Node

@onready var player: Node2D = %Player

var ROOM_TEXTURE_SIZE: int = 120
var ROOM_GRID_PADDING: Vector2 = Vector2(100, 80)
var ROOM_GRID_WIDTH: int = 9
var ROOM_GRID_HEIGHT: int = 5

var _room_grid: Array = []
var _current_room: Vector2 = Vector2(0, 0)


# ─── Public ────────────────────────────────────────────────────────────────────

func move_player(delta_x: int, delta_y: int) -> bool:
    var new_pos := _current_room + Vector2(delta_x, delta_y)
    var can_move := new_pos.x >= 0 and new_pos.x < ROOM_GRID_WIDTH and new_pos.y >= 0 and new_pos.y < ROOM_GRID_HEIGHT

    if can_move:
        _current_room = new_pos

    print("Current Room Position:", _current_room)
    print("New position:", new_pos)
    print("Can Move:", can_move)

    return can_move


# ─── Private ───────────────────────────────────────────────────────────────────


func _ready() -> void:
    _initialize_room_grid()
    _randomize_room_contents()
    _add_room_tiles()
    _set_player_initial_position()
    _print_room_contents()


func _initialize_room_grid() -> void:
    _room_grid.resize(ROOM_GRID_WIDTH)
    for i in range(ROOM_GRID_WIDTH):
        _room_grid[i] = []
        for j in range(ROOM_GRID_HEIGHT):
            _room_grid[i].append("none")


func _get_current_room():
    return _room_grid[_current_room.x][_current_room.y]


func _set_player_initial_position() -> void:
    var random_pos := _get_random_empty_position()
    _current_room = random_pos
    player.position = ROOM_GRID_PADDING + random_pos * ROOM_TEXTURE_SIZE
    print("Player initial position set to:", player.position)


func _get_random_empty_position() -> Vector2:
    var pos: Vector2
    while true:
        pos = Vector2(randi() % ROOM_GRID_WIDTH, randi() % ROOM_GRID_HEIGHT)
        if _room_grid[pos.x][pos.y] == "none":
            break
    return pos


func _add_room_tiles():
    var room_scene = load("res://entities/room.tscn")
    var sprite_resources = {
        "wumpus": "res://assets/sprites/environment/room-tile-wumpus.png",
        "smelly": "res://assets/sprites/environment/room-tile-smelly.png",
        "arrow": "res://assets/sprites/environment/room-tile-arrow.png",
        "none": "res://assets/sprites/environment/room-tile.png"
    }

    for i in range(ROOM_GRID_WIDTH):
        for j in range(ROOM_GRID_HEIGHT):
            var room_instance = room_scene.instantiate()
            var sprite = room_instance.get_node("Sprite2D")
            var room_type = _room_grid[i][j]
            sprite.texture = load(sprite_resources[room_type])
            sprite.scale = Vector2(ROOM_TEXTURE_SIZE / sprite.texture.get_size().x, ROOM_TEXTURE_SIZE / sprite.texture.get_size().y)
            room_instance.position = Vector2(i * ROOM_TEXTURE_SIZE + ROOM_GRID_PADDING.x, j * ROOM_TEXTURE_SIZE + ROOM_GRID_PADDING.y)
            add_child(room_instance)


func _print_room_contents() -> void:
    for row in _room_grid:
        print(" ".join(row))


func _randomize_room_contents():
    var _arrow_rooms: Array = []
    var _wumpus_rooms: Array = [Vector2(randi() % ROOM_GRID_WIDTH, randi() % ROOM_GRID_HEIGHT)]
    var _smelly_rooms: Array = []

    for wumpus_room in _wumpus_rooms:
        for dx in range(-1, 2):
            for dy in range(-1, 2):
                if dx != 0 or dy != 0:
                    var new_pos = wumpus_room + Vector2(dx, dy)
                    if new_pos.x >= 0 and new_pos.x < ROOM_GRID_WIDTH and new_pos.y >= 0 and new_pos.y < ROOM_GRID_HEIGHT:
                        _smelly_rooms.append(new_pos)

    while _arrow_rooms.size() < 4:
        var arrow_room = Vector2(randi() % ROOM_GRID_WIDTH, randi() % ROOM_GRID_HEIGHT)
        if arrow_room not in _wumpus_rooms and arrow_room not in _smelly_rooms and arrow_room not in _arrow_rooms:
            _arrow_rooms.append(arrow_room)

    for wumpus_room in _wumpus_rooms:
        _room_grid[wumpus_room.x][wumpus_room.y] = "wumpus"
    for smelly_room in _smelly_rooms:
        _room_grid[smelly_room.x][smelly_room.y] = "smelly"
    for arrow_room in _arrow_rooms:
        _room_grid[arrow_room.x][arrow_room.y] = "arrow"

    for i in range(ROOM_GRID_WIDTH):
        for j in range(ROOM_GRID_HEIGHT):
            if _room_grid[i][j] == "none":
                _room_grid[i][j] = "none"
