using System.Collections.Generic;
using System.Numerics;

public class Game : Godot.Spatial
{
    public static Game Instance { get; private set; }

    private double ElapsedTime;
    private Vector3 _input;
    private EntityPlayer _player;
    private Godot.PackedScene _modelCube;
    private List<Entity> _entities;
    private List<Entity> _addEntityQueue;
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
        _addEntityQueue.Add(_player);
    }

    public void AddEntityBullet(Vector3 position, Vector3 direction)
    {
        var model = _modelCube.Instance<Godot.Spatial>();
        var e = new EntityBullet();
        e.Model = model;
        e.SetPosition(position);
        e.Direction = direction;
        AddChild(e.Model);
        _addEntityQueue.Add(e);
    }

    public void AddEntityCube(Vector3 position)
    {
        var model = _modelCube.Instance<Godot.Spatial>();
        var e = new EntityCube();
        e.Target = _player;
        e.Model = model;
        e.SetPosition(position);
        AddChild(e.Model);
        _addEntityQueue.Add(e);
    }

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        ElapsedTime = 0.0;
        _entities = new List<Entity>();
        _addEntityQueue = new List<Entity>();
        LoadModels();
        AddPlayer();
        _wave = new Wave();
        _wave.Setup();
    }

    public override void _PhysicsProcess(float delta)
    {
        ElapsedTime += delta;

        HandleInput();
        _player.Input = _input;
        var cam = GetNode<Godot.Camera>("Camera");
        Godot.Vector3 worldPos = cam.ProjectPosition(GetViewport().GetMousePosition(), 0f);
        _player.MousePosition = new Vector3(worldPos.x, worldPos.y, 0f);

        _wave.Update(delta);

        foreach (var e in _addEntityQueue)
        {
            _entities.Add(e);
        }
        _addEntityQueue.Clear();

        foreach (Entity e in _entities)
        {
            e.OnUpdate(delta);
        }

        foreach (var e in _addEntityQueue)
        {
            _entities.Add(e);
        }
        _addEntityQueue.Clear();
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
