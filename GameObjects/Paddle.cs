// KRAV #2
// 1: Strategy Pattern
// 2: Genom att def. ett interface (IPaddleController) som genom funktionen GetMove() returnerar hur en paddle rör sig
//    Vi kallar funktionen <IPaddleController>.GetMove(Paddle) för att veta hur paddlen ska röra på sig
// 3: Vi använder strategy pattern här för att enkelt låta oss använda samma Paddle class för både spelare och AI
//    allt vi behöver ändra är vilken IPaddleController som Paddle instantieras med
public class Paddle : GameObject
{
    public IPaddleController controller;
    public ConsoleColor paddleColour { get; set; }
    private DateTime lastFired = DateTime.Now;
    public delegate void OnFireDel();
    public OnFireDel? OnFire;
    private static TimeSpan FIRE_DELAY = TimeSpan.FromSeconds(3);

    public Paddle(int height, IPaddleController controller)
        : base(1, height)
    {
        this.controller = controller;
        paddleColour = ConsoleColor.Green;
    }

    public bool CanFire()
    {
        var diff = DateTime.Now - lastFired;
        return FIRE_DELAY < diff;
    }

    public void FireProjectile()
    {
        if (!CanFire()) return;
        if (OnFire is not null) OnFire();
        lastFired = DateTime.Now;
    }

    public override void Update(GameState state)
    {
        var actions = controller.GetMove(this);
        if (actions.movement != 0)
        {
            TranslateClamped(0, actions.movement);
        }
        if (actions.ShouldFire) FireProjectile();
    }

    public override void Draw()
    {
        Console.ForegroundColor = paddleColour;
        for (uint i = 0; i < Size.Height; i++)
        {
            Console.SetCursorPosition(
                (int)Position.X,
                (int)(Position.Y + i)
            );
            Console.Write("█");
        }
    }
}
