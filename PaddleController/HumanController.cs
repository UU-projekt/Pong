public class HumanController : IPaddleController
{
 private ConsoleKey upKey;
    private ConsoleKey downKey;

    public HumanController(ConsoleKey upKey, ConsoleKey downKey)
    {
        this.upKey = upKey;
        this.downKey = downKey;
    }

    public int GetMove()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == upKey)
                return -1;
            else if (key == downKey)
                return 1;
        }
        return 0;
    }
}
