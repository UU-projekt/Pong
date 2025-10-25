
public class HardStrategy : IDifficultyStrategy
{
    public int CalculateMove(int ballY, int paddleCenter)
    {
        // Svår: Perfekt följning av bollen
        if (ballY < paddleCenter)
            return -1;
        else if (ballY > paddleCenter)
            return 1;

        return 0;
    }
}
