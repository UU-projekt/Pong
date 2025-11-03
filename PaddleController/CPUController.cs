public enum AIDifficulty
{
    Easy,
    Medium,
    Hard
}

public static class Helper
{
    public static Dictionary<string, AIDifficulty> DifficultyString = new()
    {
        { "EASY", AIDifficulty.Easy },
        { "MEDIUM", AIDifficulty.Medium },
        { "HARD", AIDifficulty.Hard }
    };
}

public class CPUController : IPaddleController
{
    private Ball? _ball;
    private AIDifficulty _difficulty;
    private Random _random = new Random();

    public CPUController(AIDifficulty difficulty) => _difficulty = difficulty;
    public void AttatchBall(Ball ball) => _ball = ball;

    public (int, bool) GetMove(Paddle paddle)
    {
        if (_ball == null) return (0, false);

        int targetY = _ball.Position.Y;
        int paddleY = paddle.Position.Y;
        int movementYay = 0;
        bool shouldFire = false;

        switch (_difficulty)
        {
            case AIDifficulty.Easy:
                // Easy difficulty: 30% chance to move randomly, 10% chance to fire
                if (_random.NextDouble() < 0.3)
                {
                    movementYay = _random.Next(-1, 2); // -1, 0, or 1
                }
                else
                {
                    // If not moving randomly, move towards the ball
                    if (targetY < paddleY) movementYay = -1;
                    else if (targetY > paddleY) movementYay = 1;
                }
                if (paddle.CanFire())
                {
                    shouldFire = _random.NextDouble() < 0.01;
                }
                break;

            case AIDifficulty.Medium:
                // Medium difficulty: 10% chance to move randomly, 20% chance to fire
                if (_random.NextDouble() < 0.1)
                {
                    movementYay = _random.Next(-1, 2); // -1, 0, or 1
                }
                else
                {
                    // If not moving randomly, move towards the ball
                    if (targetY < paddleY) movementYay = -1;
                    else if (targetY > paddleY) movementYay = 1;
                }
                if (paddle.CanFire())
                {
                    shouldFire = _random.NextDouble() < 0.02;
                }
                break;

            case AIDifficulty.Hard:
                // Hard difficulty: always move towards the ball, 50% chance to fire
                if (targetY < paddleY) movementYay = -1;
                else if (targetY > paddleY) movementYay = 1;

                if (paddle.CanFire())
                {
                    shouldFire = _random.NextDouble() < 0.05;
                }
                break;
        }

        return (movementYay, shouldFire);
    }
}