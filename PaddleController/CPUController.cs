public class CPUController : IPaddleController
{
    private int ballY;
    private int paddleY;
    private int paddleHeight;
    private DifficultyLevel difficulty;
    private Random random = new Random();

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    // KRAV 2: Strategy Pattern
    public CPUController(DifficultyLevel difficulty)
    {
        this.difficulty = difficulty;
    }

    public void Update(int ballY, int paddleY, int paddleHeight)
    {
        this.ballY = ballY;
        this.paddleY = paddleY;
        this.paddleHeight = paddleHeight;
    }

    public int GetMove()
    {
        int paddleCenter = paddleY + paddleHeight / 2;

        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                if (random.Next(0, 2) == 0)
                    return 0;
                if (ballY < paddleCenter - 2)
                    return -1;
                else if (ballY > paddleCenter + 2)
                    return 1;
                break;

            case DifficultyLevel.Medium:
                if (random.Next(0, 4) == 0)
                    return 0;
                if (ballY < paddleCenter - 1)
                    return -1;
                else if (ballY > paddleCenter + 1)
                    return 1;
                break;

            case DifficultyLevel.Hard:
                if (ballY < paddleCenter)
                    return -1;
                else if (ballY > paddleCenter)
                    return 1;
                break;
        }

        return 0;
    }
}
