
public class EntityCube : Entity
{
    public override void OnCollide(Entity other)
    {
        if (other.Type == EntityType.BULLET)
        {
            IsAlive = false;
        }
    }
    public override void OnUpdate(float delta)
    {
        var force = Seek(Target);
        ApplyForce(force);
        UpdateSteeringBehaviour();
        MoveBy(Velocity * delta);
    }
}
