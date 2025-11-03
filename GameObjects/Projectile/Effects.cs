public interface IProjectiveEffect
{
    void ApplyEffect(Paddle target);
}

public class FrozenController : IPaddleController
{
    public (int, bool) GetMove(Paddle _paddle)
    {
        return (0, false);
    }
}

public class FreezeEffect : IProjectiveEffect
{
    public void ApplyEffect(Paddle target)
    {
        var controller = target.controller;
        target.controller = new FrozenController();

        // (vårat konsolspel är multi-threaded 😎)
        new Thread(() =>
        {
            Thread.Sleep(1000);
            target.controller = controller;
        }).Start();
    }
}

public class ShrinkEffekt : IProjectiveEffect
{
    public void ApplyEffect(Paddle target)
    {
        target.Size = (1, Math.Max(target.Size.Height - 1, 2));
    }
}

public class Move : IProjectiveEffect
{
    public void ApplyEffect(Paddle target)
    {
        target.Translate(1, 0);
    }
}