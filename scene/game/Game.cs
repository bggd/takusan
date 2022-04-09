using System.Collections.Generic;
using System.Numerics;
using Takusan.Scene.Game;

public class Game : Godot.Spatial
{
    private double ElapsedTime;
    private Vector3 _input;
    private EntityPlayer _player;
    private Godot.PackedScene _modelCube;
    private List<Entity> _entities;
    private Wave _wave;

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

    public void AddEntityCube(Vector3 position)
    {
        var model = _modelCube.Instance<Godot.Spatial>();
        var e = new EntityCube();
        e.Target = _player;
        e.Model = model;
        e.SetPosition(position);
        AddChild(e.Model);
        _entities.Add(e);
    }

    public override void _Ready()
    {
        ElapsedTime = 0.0;
        _entities = new List<Entity>();
        LoadModels();
        AddPlayer();
        _wave = new Wave();
        _wave.Setup(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        ElapsedTime += delta;

        HandleInput();
        _player.Input = _input;

        _wave.Update(delta);

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
