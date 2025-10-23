
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

    public void Move()
    {
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

    public void Draw()
    {
        Console.SetCursorPosition((int)Position.X, (int)Position.Y);
        Console.Write("O");
    }
}
