extends Node

@onready var room_manager = %RoomManager
@onready var sprite = $Sprite2D

func _ready():
    sprite.z_index = 1 # Make sure the player is on top of the room tiles

func _physics_process(_delta):
    var direction = Vector2.ZERO
    if Input.is_action_just_pressed("move_left"):
        direction.x = -1
    elif Input.is_action_just_pressed("move_right"):
        direction.x = 1
    elif Input.is_action_just_pressed("move_up"):
        direction.y = -1
    elif Input.is_action_just_pressed("move_down"):
        direction.y = 1

    if direction != Vector2.ZERO:
        _move(direction)

func _move(direction):
    if room_manager.move_player(direction.x, direction.y): # Move only if the room allows it (don't if out of bounds of the room)
        sprite.position += direction * room_manager.ROOM_TEXTURE_SIZE
