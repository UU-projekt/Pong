public class Paddle : GameObject
{
    private IPaddleController controller;

    public Paddle(uint width, uint height, IPaddleController controller) 
        : base(width, height)
    {
        this.controller = controller;
    }

    public void UpdateController(int ballY, int paddleY, int paddleHeight)
    {
        controller.Update(ballY, paddleY, paddleHeight);
    }

    public void Move()
    {
        int move = controller.GetMove();
        if (move != 0)
        {
            TranslateClamped(0, move);
        }
    }

    public void Draw()
    {
        for (uint i = 0; i < Size.Height; i++)
        {
            Console.SetCursorPosition(
                (int)Position.X, 
                (int)(Position.Y + i)
            );
            Console.Write("|");
        }
    }
}
