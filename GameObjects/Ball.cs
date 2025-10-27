
public class Ball : GameObject
{
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    private Paddle _paddle1;
    private Paddle _paddle2;

    public Ball(int velocityX, int velocityY)
        : base(1, 1)
    {
        VelocityX = velocityX;
        VelocityY = velocityY;
    }

    public void SetPaddles(Paddle paddle1, Paddle paddle2)
    {
        _paddle1 = paddle1;
        _paddle2 = paddle2;
    }

    private void CheckPaddleBounce(Paddle paddle)
    {
        int nextX = (int)Position.X + VelocityX;
        int nextY = (int)Position.Y + VelocityY;

        int paddleLeft = (int)paddle.Position.X;
        int paddleRight = paddleLeft + (int)paddle.Size.Width - 1;
        int paddleTop = (int)paddle.Position.Y;
        int paddleBottom = paddleTop + (int)paddle.Size.Height - 1;

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
        if (collidesLeft || collidesRight)
        {
            state.state = StateOption.GAME_COMPLETED;
            state.Winner = collidesLeft ? state.RightPlayerName : state.LeftPlayerName;
        }

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
