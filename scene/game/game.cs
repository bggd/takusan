using System;
using System.Numerics;
using System.Collections.Generic;
using Takusan.scene.game;

public class game : Godot.Spatial
{
    private Vector3 _input = new Vector3();
    private EntityPlayer _player;
    private Godot.PackedScene _modelCube;
    private List<Entity> _entities = new List<Entity>();

    public void LoadModels()
    {
        _modelCube = Godot.ResourceLoader.Load<Godot.PackedScene>("res://scene/model/cube.tscn");
    }

    public void AddPlayer()
    {
        var model = _modelCube.Instance<Godot.Spatial>();
        _player = new EntityPlayer();
        _player.Model = model;
        AddChild(_player.Model);
        _entities.Add(_player);
    }

    public override void _Ready()
    {
        LoadModels();
        AddPlayer();
    }

    public override void _PhysicsProcess(float delta)
    {
        HandleInput();
        _player.Input = _input;

        foreach (Entity e in _entities)
        {
            e.OnUpdate(delta);
        }
    }

    private void HandleInput()
    {
        _input = Vector3.Zero;

        if (Godot.Input.IsActionPressed("move_left"))
        {
            _input.X = -1.0f;
        }
        if (Godot.Input.IsActionPressed("move_right"))
        {
            _input.X = 1.0f;
        }
        if (Godot.Input.IsActionPressed("move_up"))
        {
            _input.Y = 1.0f;
        }
        if (Godot.Input.IsActionPressed("move_down"))
        {
            _input.Y = -1.0f;
        }

        if (_input.LengthSquared() > 0.0f)
        {
            _input = Vector3.Normalize(_input);
        }
    }
}
