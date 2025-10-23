

public class CPUController : IPaddleController
{
    private Ball _ball;
    public CPUController(Ball ball) => _ball = ball;
    public int GetMove(Paddle paddle)
    {
        // Bara för test. Skulle vara omöjlig att möta då denna alltid är perfekt på prick
        return (int)_ball.Position.Y - (int)paddle.Position.Y;
    }
}