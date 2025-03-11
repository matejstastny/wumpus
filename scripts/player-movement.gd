extends Node

@onready var room_manager = %RoomManager
@onready var sprite = $Sprite2D

func _ready():
    pass

func _physics_process(_delta):
    if Input.is_action_just_pressed("move_left"):
        _move(-1, 0)
    elif Input.is_action_just_pressed("move_right"):
        _move(1, 0)
    elif Input.is_action_just_pressed("move_up"):
        _move(0, -1)
    elif Input.is_action_just_pressed("move_down"):
        _move(0, 1)

func _move(_x, _y):
    if room_manager.move_player(_x, _y):
        sprite.position = Vector2(sprite.position.x + _x * 16, sprite.position.y + _y * 16)
