extends Node


@onready var player = %Player

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


# ─── Private ───────────────────────────────────────────────────────────────────


func _ready():
	_initialize_room_arr()
	_randomize_room_contents(_room_grid_width, _room_grid_height)
	_add_room_tiles()
	_set_player_init_pos()
	_print_room_contents()


func _initialize_room_arr():
	_room_grid.resize(_room_grid_width)
	var _room_types: Array = []
	for i in range(_room_grid_width):
		_room_grid[i] = []
		for j in range(_room_grid_height):
			_room_grid[i].append(str(i) + " , " + str(j))


func _set_player_init_pos():
	var random_x = randi() % _room_grid_width
	var random_y = randi() % _room_grid_height
	while _room_grid[random_x][random_y] != "none":
		random_x = randi() % _room_grid_width
		random_y = randi() % _room_grid_height
	_current_room = Vector2(random_x, random_y)
	var pos = Vector2(room_grid_padding.x + random_x * room_size, room_grid_padding.y + random_y * room_size)
	player.position = pos
	print("Player initial position set to: " + str(player.position))

func _add_room_tiles():
	for i in range(_room_grid_width):
		for j in range(_room_grid_height):
			var room_scene_path = "res://entities/room.tscn"
			var sprite_resource = ""
			match _room_grid[i][j]:
				"wumpus":
					sprite_resource = "res://assets/sprites/environment/room-tile-wumpus.png"
				"smelly":
					sprite_resource = "res://assets/sprites/environment/room-tile-smelly.png"
				"arrow":
					sprite_resource = "res://assets/sprites/environment/room-tile-arrow.png"
				"none":
					sprite_resource = "res://assets/sprites/environment/room-tile.png"

			var room_scene = load(room_scene_path)
			var room_instance = room_scene.instantiate()
			var sprite = room_instance.get_node("Sprite2D")
			sprite.texture = load(sprite_resource)
			sprite.scale = Vector2(room_size / sprite.texture.get_size().x, room_size / sprite.texture.get_size().y)
			room_instance.position = Vector2(i * room_size + room_grid_padding.x, j * room_size + room_grid_padding.y)
			add_child(room_instance)


func _print_room_contents():
	for i in range(_room_grid_width):
		for j in range(_room_grid_height):
			print("ROOM: " + _room_grid[i][j])

func _randomize_room_contents(_width: int, _height: int):
	# Initialize arrays
	var _arrow_rooms: Array = []
	var _wumpus_rooms: Array = []
	var _smelly_rooms: Array = []

	# Randomize wumpus rooms
	var random_wumpus_x = randi() % _width
	var random_wumpus_y = randi() % _height
	_wumpus_rooms.append(Vector2(random_wumpus_x, random_wumpus_y))

	# Get smelly rooms
	for wumpus_room in _wumpus_rooms:
		var x = wumpus_room.x
		var y = wumpus_room.y
		for dx in range(-1, 2):
			for dy in range(-1, 2):
				if dx == 0 and dy == 0:
					continue
				var new_x = x + dx
				var new_y = y + dy
				if new_x >= 0 and new_x < _room_grid_width and new_y >= 0 and new_y < _room_grid_height:
					_smelly_rooms.append(Vector2(new_x, new_y))

	# Randomize arrow rooms (check if they are not wumpus or smelly or existing rooms)
	for i in range(4):
		var random_arrow_x = randi() % _width
		var random_arrow_y = randi() % _height
		var arrow_room = Vector2(random_arrow_x, random_arrow_y)
		while arrow_room in _wumpus_rooms or arrow_room in _smelly_rooms or arrow_room in _arrow_rooms:
			random_arrow_x = randi() % _width
			random_arrow_y = randi() % _height
			arrow_room = Vector2(random_arrow_x, random_arrow_y)
		_arrow_rooms.append(arrow_room)


	# Add wumpus rooms to the grid
	for wumpus_room in _wumpus_rooms:
		_room_grid[wumpus_room.x][wumpus_room.y] = "wumpus"

	# Add smelly rooms to the grid
	for smelly_room in _smelly_rooms:
		_room_grid[smelly_room.x][smelly_room.y] = "smelly"

	# Add arrow rooms to the grid
	for arrow_room in _arrow_rooms:
		_room_grid[arrow_room.x][arrow_room.y] = "arrow"

	# Fill remaining cells with "none"
	for i in range(_width):
		for j in range(_height):
			if _room_grid[i][j] not in ["wumpus", "smelly", "arrow"]:
				_room_grid[i][j] = "none"

	return _room_grid


func _get_current_room():
	return _room_grid[_current_room.x][_current_room.y]
