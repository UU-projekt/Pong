public class FastPaddle : IPaddleType
{
    public int GetMoveSpeed()
    {
        return 2; // Rör sig 2 steg per frame
    }

    public string GetSymbol()
    {
        return "║"; // Dubbel linje för snabb paddel
    }
}
