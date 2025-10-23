
public class Ball : GameObject
{
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }

    public Ball(uint width, uint height, int velocityX, int velocityY)
        : base(width, height)
    {
        VelocityX = velocityX;
        VelocityY = velocityY;
    }

    public override void Update()
    {
        if (Position.Y == (Console.BufferHeight - 1) || Position.Y == 0) BounceVertical();
        TranslateClamped(VelocityX, VelocityY);
    }

    public void BounceVertical()
    {
        VelocityY = -VelocityY;
    }

    public void BounceHorizontal()
    {
        VelocityX = -VelocityX;
    }

    public override void Draw()
    {
        Console.SetCursorPosition((int)Position.X, (int)Position.Y);
        Console.Write("O");
    }
}
