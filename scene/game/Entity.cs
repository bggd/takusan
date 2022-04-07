using System.Numerics;

namespace Takusan.scene.game
{
    internal class Entity
    {
        public Vector3 Position;
        public Godot.Spatial Model { get; set; }
        public virtual void OnUpdate(float delta)
        {
        }

        public Godot.Vector3 ToGodotVector3(Vector3 v)
        {
            return new Godot.Vector3(v.X, v.Y, v.Z);
        }
    }
}
