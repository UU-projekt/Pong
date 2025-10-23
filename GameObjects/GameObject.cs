public abstract class GameObject
{
    public (uint X, uint Y) Position { get; private set; } = (0, 0);
    public (uint Width, uint Height) Size { get; }

    protected GameObject(uint width, uint height)
    {
        Size = (width, height);
    }

    public abstract void Update();
    public abstract void Draw();

    public void Translate(int deltaX, int deltaY)
    {
        if (deltaX < 0 && Math.Abs(deltaX) > Position.X)
            throw new ArgumentOutOfRangeException(nameof(deltaX), "Translation would underflow Position.X");
        if (deltaY < 0 && Math.Abs(deltaY) > Position.Y)
            throw new ArgumentOutOfRangeException(nameof(deltaY), "Translation would underflow Position.Y");

        Position = ((uint)(Position.X + deltaX), (uint)(Position.Y + deltaY));
    }


    public void TranslateClamped(int deltaX, int deltaY)
    {
        if (deltaX < 0 && Math.Abs(deltaX) > Position.X)
            deltaX = (int)Position.X * -1;

        if (deltaY < 0 && Math.Abs(deltaY) > Position.Y)
            deltaY = (int)Position.Y * -1;

        uint newX = (uint)(Position.X + deltaX);
        uint newY = (uint)(Position.Y + deltaY);

        uint maxX = (uint)(Console.BufferWidth - Size.Width);
        if (newX > maxX)
            newX = maxX;

        uint maxY = (uint)(Console.BufferHeight - Size.Height);
        if (newY > maxY)
            newY = maxY;

        Position = (newX, newY);
    }

    public void MoveTo(uint posX, uint posY)
    {
        Position = (posX, posY);
    }

    public (int deltaX, int deltaY) DistanceVectorTo(GameObject other)
    {
        int deltaX = (int)Position.X - (int)other.Position.X;
        int deltaY = (int)Position.Y - (int)other.Position.Y;
        return (deltaX, deltaY);
    }

    public double DistanceTo(GameObject other)
    {
        var VecDistance = DistanceVectorTo(other);
        var deltaXPow2 = VecDistance.deltaX * VecDistance.deltaX;
        var deltaYPow2 = VecDistance.deltaY * VecDistance.deltaY;
        var combined = deltaXPow2 + deltaYPow2;
        return Math.Sqrt(combined);
    }

    public bool CollidesWith(GameObject other)
    {
        bool xOverlap = Position.X < other.Position.X + other.Size.Width &&
                Position.X + Size.Width > other.Position.X;
        bool yOverlap = Position.Y < other.Position.Y + other.Size.Height &&
                        Position.Y + Size.Height > other.Position.Y;
        return xOverlap && yOverlap;
    }
}
