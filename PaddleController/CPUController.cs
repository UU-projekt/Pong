

public class CPUController : IPaddleController
{
    private Ball _ball;
    public CPUController(Ball ball) => _ball = ball;
    public int GetMove(Paddle paddle)
    {
        return (int)_ball.Position.Y - (int)paddle.Position.Y;
    }
}