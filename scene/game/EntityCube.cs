namespace Takusan.Scene.Game
{
    internal class EntityCube : Entity
    {
        public override void OnUpdate(float delta)
        {
            var force = Seek(Target);
            ApplyForce(force);
            UpdateSteeringBehaviour();
            Position += Velocity * delta;
            Model.Translate(ToGodotVector3(Velocity * delta));
        }
    }
}
