using System.Numerics;

public enum EntityType
{
    PlAYER,
    BULLET,
    ENEMY
}


public class Entity
{
    public EntityType Type;
    public Vector3 Position = new Vector3(0f);
    public Vector3 Velocity = new Vector3(0f);
    public Vector3 Acceleration = new Vector3(0f);
    public float MaxSpeed = 6.0f;
    public float MaxForce = 0.25f;
    public Entity Target = null;
    public Circle Circle = new Circle();
    public bool IsAlive = false;

    public Godot.Spatial Model { get; set; }

    public virtual void OnUpdate(float delta)
    {
    }

    public virtual void OnCollide(Entity other)
    {
    }

    public Godot.Vector3 ToGodotVector3(Vector3 v)
    {
        return new Godot.Vector3(v.X, v.Y, v.Z);
    }

    public Vector3 SetMag(Vector3 v, float len)
    {
        return Vector3.Normalize(v) * len;
    }

    public void SetPosition(Vector3 pos)
    {
        Model.Translate(ToGodotVector3(pos - Position));
        Position = pos;
        Circle.Position = pos;
    }

    public void MoveBy(Vector3 offset)
    {
        Circle c = new Circle();
        c.Position = Position + offset;
        c.Radius = Circle.Radius;
        foreach (var e in Game.Instance.GetEntities())
        {
            if (e == this) {  continue; }

            if (c.IsHit(e.Circle))
            {
                OnCollide(e);
            }
        }
        Position = Position + offset;
        Circle.Position = Position;
        Model.Translate(ToGodotVector3(offset));
    }

    public Vector3 Seek(Entity target)
    {
        Vector3 force = target.Position - Position;
        force = SetMag(force, MaxSpeed);
        force = force - Velocity;
        force = Vector3.Clamp(force, new Vector3(-MaxForce), new Vector3(MaxForce));
        return force;
    }

    public Vector3 Free(Entity target)
    {
        return Seek(target) * -1.0f;
    }

    public void ApplyForce(Vector3 force)
    {
        Acceleration += force;
    }

    public void UpdateSteeringBehaviour()
    {
        Velocity += Acceleration;
        Velocity = Vector3.Clamp(Velocity, new Vector3(-MaxSpeed), new Vector3(MaxSpeed));
        Acceleration = Vector3.Zero;
    }
}
