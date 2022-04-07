using System.Numerics;


namespace Takusan.scene.game
{
    internal class EntityPlayer : Entity
    {
        public Vector3 Input;
        public Vector3 Speed = new Vector3(4.0f, 4.0f, 0.0f);
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

            Position.X += offset.X;
            Position.Y += offset.Y;
            Model.Translate(ToGodotVector3(offset));
        }
    }
}
