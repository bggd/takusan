using System.Numerics;

public class Circle
{
    public Vector3 Position;
    public float Radius;

    public bool IsHit(Circle other)
    {
        float distanceX = Position.X - other.Position.X;
        float distanceY = Position.Y - other.Position.Y;
        float radius = Radius + other.Radius;

        if (distanceX * distanceX + distanceY * distanceY <= radius * radius)
        {
            return true;
        }
        return false;
    }
}
