using System.Numerics;

public class EntityBullet : Entity
{
    public Vector3 Direction;

    public override void OnUpdate(float delta)
    {
        Entity target = new Entity();
        target.Position = Position + Direction;
        var force = Seek(target);
        ApplyForce(force);
        UpdateSteeringBehaviour();
        Position += Velocity * delta;
        Model.Translate(ToGodotVector3(Velocity * delta));
    }
}
