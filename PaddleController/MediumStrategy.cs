public class MediumStrategy : IDifficultyStrategy
{
    private Random random = new Random();

    public int CalculateMove(int ballY, int paddleCenter)
    {
        // Medel: Missar ibland (25% chans att inte röra sig)
        if (random.Next(0, 4) == 0)
            return 0;

        // Normal tolerans för position
        if (ballY < paddleCenter - 1)
            return -1;
        else if (ballY > paddleCenter + 1)
            return 1;

        return 0;
    }
}
