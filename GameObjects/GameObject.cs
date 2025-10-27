public abstract class GameObject
{
    public (int X, int Y) Position { get; private set; } = (0, 0);
    public (int Width, int Height) Size { get; }

    protected GameObject(int width, int height)
    {
        Size = (width, height);
    }

    public abstract void Update(GameState state);
    public abstract void Draw();

    public void Translate(int deltaX, int deltaY)
    {
        if (deltaX < 0 && Math.Abs(deltaX) > Position.X)
            throw new ArgumentOutOfRangeException(nameof(deltaX), "Translation would underflow Position.X");
        if (deltaY < 0 && Math.Abs(deltaY) > Position.Y)
            throw new ArgumentOutOfRangeException(nameof(deltaY), "Translation would underflow Position.Y");

        Position = (Position.X + deltaX, Position.Y + deltaY);
    }


    public void TranslateClamped(int deltaX, int deltaY)
    {
        if (deltaX < 0 && Math.Abs(deltaX) > Position.X)
            deltaX = (int)Position.X * -1;

        if (deltaY < 0 && Math.Abs(deltaY) > Position.Y)
            deltaY = (int)Position.Y * -1;

        int newX = Position.X + deltaX;
        int newY = Position.Y + deltaY;

        int maxX = Console.BufferWidth - Size.Width;
        if (newX > maxX)
            newX = maxX;

        int maxY = Console.BufferHeight - Size.Height;
        if (newY > maxY)
            newY = maxY;

        Position = (newX, newY);
    }

    public void MoveTo(int posX, int posY)
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
}
