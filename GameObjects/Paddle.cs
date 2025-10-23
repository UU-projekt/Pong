public class Paddle : GameObject
{
    private IPaddleController controller;
    public ConsoleColor paddleColour { get; set; }

    public Paddle(uint width, uint height, IPaddleController controller)
        : base(width, height)
    {
        this.controller = controller;
        paddleColour = ConsoleColor.Green;
    }

    public override void Update()
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
