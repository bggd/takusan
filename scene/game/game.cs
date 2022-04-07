using System.Collections.Generic;
using System.Numerics;
using Takusan.scene.game;

public class game : Godot.Spatial
{
    private double ElapsedTime;
    private Vector3 _input;
    private EntityPlayer _player;
    private Godot.PackedScene _modelCube;
    private List<Entity> _entities;

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

    public void AddEntityCube()
    {
        var model = _modelCube.Instance<Godot.Spatial>();
        var e = new EntityCube();
        e.Target = _player;
        e.Model = model;
        AddChild(e.Model);
        _entities.Add(e);
    }

    public override void _Ready()
    {
        ElapsedTime = 0.0;
        _entities = new List<Entity>();
        LoadModels();
        AddPlayer();
    }

    public override void _PhysicsProcess(float delta)
    {
        ElapsedTime += delta;

        HandleInput();
        _player.Input = _input;

        if (ElapsedTime > 3)
        {
            AddEntityCube();
            ElapsedTime -= 3f;
        }

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
