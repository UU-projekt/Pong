

public class CPUController : IPaddleController
{
    private Ball? _ball;

    public void AttatchBall(Ball ball)
    {
        _ball = ball;
    }

    public int GetMove(Paddle paddle)
    {
        if (_ball == null) return 0;

        // Bara för test. Skulle vara omöjlig att möta då denna alltid är perfekt på prick
        return _ball.Position.Y - paddle.Position.Y;
    }
}