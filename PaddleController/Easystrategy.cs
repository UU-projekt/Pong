
public class EasyStrategy : IDifficultyStrategy
{
    private Random random = new Random();

    public int CalculateMove(int ballY, int paddleCenter)
    {
        // Lätt: Missar ofta (50% chans att inte röra sig)
        if (random.Next(0, 2) == 0)
            return 0;

        // Bred tolerans för position
        if (ballY < paddleCenter - 2)
            return -1;
        else if (ballY > paddleCenter + 2)
            return 1;

        return 0;
    }
}
