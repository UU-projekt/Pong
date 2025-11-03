public enum PlayerType
{
    HUMAN,
    COMPUTER
}

public interface IGameObjectFactory
{
    Ball CreateBall();
    Paddle CreatePaddle(PlayerType type);
}

public class DefaultGameObjectFactory : IGameObjectFactory
{
    static int DEFAULT_PADDLE_HEIGHT = 5;
    static Dictionary<PlayerType, ConsoleColor> DEFAULT_PADDLE_COLOURS = new()
    {
        { PlayerType.HUMAN, ConsoleColor.White },
        { PlayerType.COMPUTER, ConsoleColor.Red }
    };

    public Ball CreateBall()
    {
        Ball ball = new Ball(1, 1);

        int midX = Console.BufferWidth / 2;
        int midY = Console.BufferHeight / 2;

        // Positionera i mitten
        ball.MoveTo(midX, midY);

        return ball;
    }

    public Paddle CreatePaddle(PlayerType type)
    {
        IPaddleController controller = type == PlayerType.HUMAN ? new HumanController() : new CPUController(AIDifficulty.Easy);
        ConsoleColor paddleColour = DEFAULT_PADDLE_COLOURS[type];
        Paddle paddle = new Paddle(DEFAULT_PADDLE_HEIGHT, controller) { paddleColour = paddleColour };

        int posX = type == PlayerType.HUMAN ? 0 : Console.BufferWidth - 1;
        paddle.MoveTo(posX, 1);

        return paddle;
    }
}