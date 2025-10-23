
public class HumanController : IPaddleController
{
    public int GetMove(Paddle _paddle)
    {
        if (!Console.KeyAvailable) return 0;

        int move = 0;
        while (Console.KeyAvailable)
        {
            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    move = keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) ? -2 : -1;
                    break;
                case ConsoleKey.DownArrow:
                    move = keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) ? 2 : 1;
                    break;
            }

        }

        return move;
    }
}