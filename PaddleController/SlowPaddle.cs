public class SlowPaddle : IPaddleType
{
    public int GetMoveSpeed()
    {
        return 1; // Rör sig 1 steg per frame
    }

    public string GetSymbol()
    {
        return "|"; // Enkel linje för långsam paddel
    }
}
