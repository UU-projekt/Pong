public class Paddle : GameObject
{
    private IPaddleController controller;
    public ConsoleColor paddleColour { get; set; }

    public Paddle(int height, IPaddleController controller)
        : base(1, height)
    {
        this.controller = controller;
        paddleColour = ConsoleColor.Green;
    }

    public override void Update(GameState state)
    {
        int move = controller.GetMove(this);
        if (move != 0)
        {
            TranslateClamped(0, move);
        }
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
