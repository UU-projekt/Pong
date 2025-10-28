
public class Ball : GameObject
{
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    private Paddle _paddle1;
    private Paddle _paddle2;
    public int Bounces { get; private set; }

    public event Action<Direction>? OnScored;

    public Ball(int velocityX, int velocityY)
        : base(1, 1)
    {
        VelocityX = velocityX;
        VelocityY = velocityY;
        Bounces = 0;
    }

    public void SetPaddles(Paddle paddle1, Paddle paddle2)
    {
        _paddle1 = paddle1;
        _paddle2 = paddle2;
    }

    private void CheckPaddleBounce(Paddle paddle)
    {
        int nextX = Position.X + VelocityX;
        int nextY = Position.Y + VelocityY;

        int paddleLeft = paddle.Position.X;
        int paddleRight = paddleLeft + paddle.Size.Width - 1;
        int paddleTop = paddle.Position.Y;
        int paddleBottom = paddleTop + paddle.Size.Height - 1;

        bool crossesHorizontally = false;

        if (VelocityX < 0)
        {
            crossesHorizontally = nextX <= paddleRight && Position.X >= paddleRight;
        }
        else if (VelocityX > 0)
        {
            crossesHorizontally = nextX >= paddleLeft && Position.X <= paddleLeft;
        }

        bool overlapsVertically = nextY >= paddleTop && nextY <= paddleBottom;

        if (crossesHorizontally && overlapsVertically)
        {
            Bounces += 1;
            BounceHorizontal();
        }
    }

    public override void Update(GameState state)
    {
        // Hantera studs mot paddles
        if (_paddle1 != null) CheckPaddleBounce(_paddle1);
        if (_paddle2 != null) CheckPaddleBounce(_paddle2);

        // Hantera studs mot top och botten
        if (Position.Y == (Console.BufferHeight - 1) || Position.Y == 0) BounceVertical();

        bool collidesLeft = Position.X == 0;
        bool collidesRight = Position.X == Console.BufferWidth - 1;

        if (collidesLeft) OnScored?.Invoke(Direction.RIGHT);
        else if (collidesRight) OnScored?.Invoke(Direction.LEFT);

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
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write("O");
    }
}
