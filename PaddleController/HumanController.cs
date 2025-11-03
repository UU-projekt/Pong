
public class HumanController : IPaddleController
{
    public (int, bool) GetMove(Paddle _paddle)
    {
        if (!Console.KeyAvailable) return (0, false);

        int move = 0;
        bool shouldFire = false;
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
                case ConsoleKey.Spacebar:
                    shouldFire = true;
                    break;
            }

        }

        return (move, shouldFire);
    }
}