using System.Numerics;

public class EntityPlayer : Entity
{
    public Vector3 Input;
    public Vector3 MousePosition;
    public Vector3 Speed = new Vector3(4.0f, 4.0f, 0.0f);
    private Takusan.Scene.Game.Trigger _trigger = new Takusan.Scene.Game.Trigger();

    public EntityPlayer()
    {
        _trigger.CoolDown(0.1, () => { return true; }, () =>
        {
            Vector3 direction = MousePosition - Position;
            direction = Vector3.Normalize(direction);
            Game.Instance.AddEntityBullet(Position, direction);
        }
        );
    }

    public override void OnCollide(Entity other)
    {
        if (other.Type == EntityType.ENEMY)
        {
            Godot.GD.Print("collide!");
        }
    }

    public override void OnUpdate(float delta)
    {
        Vector3 offset = Vector3.Zero;

        if (Vector3.Abs(Input).X > 0.0f)
        {
            offset.X = Speed.X * Input.X * delta;
        }
        if (Vector3.Abs(Input).Y > 0.0f)
        {
            offset.Y = Speed.Y * Input.Y * delta;
        }

        MoveBy(offset);

        _trigger.Update(delta);
    }
}
