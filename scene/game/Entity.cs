using System.Numerics;

namespace Takusan.scene.game
{
    internal class Entity
    {
        public Vector3 Position = new Vector3(0f);
        public Vector3 Velocity = new Vector3(0f);
        public Vector3 Acceleration = new Vector3(0f);
        public float MaxSpeed = 6.0f;
        public float MaxForce = 0.25f;

        public Godot.Spatial Model { get; set; }

        public virtual void OnUpdate(float delta)
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
            Position += Velocity;
            Acceleration = Vector3.Zero;
        }
    }
}
