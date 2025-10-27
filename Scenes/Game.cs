public class GameScene : Scene
{
    private Paddle _paddle1;
    private Paddle _paddle2;
    private Ball _ball;
    private GameObjectCollection<GameObject> gameItems = new GameObjectCollection<GameObject>();
    public GameScene()
    {
        var ball = new Ball(1, 1);

        // CONTROLLERS
        var cpuController = new CPUController(ball);
        var humanController = new HumanController();

        // PADDLES
        // (vi bör fixa builders för dessa)
        var paddle1 = new Paddle(5, humanController);
        paddle1.paddleColour = ConsoleColor.White;

        var paddle2 = new Paddle(5, cpuController);
        paddle2.paddleColour = ConsoleColor.DarkRed;

        gameItems.Add(paddle1);
        gameItems.Add(paddle2);
        gameItems.Add(ball);

        _paddle1 = paddle1;
        _paddle2 = paddle2;
        _ball = ball;
        ball.SetPaddles(paddle1, paddle2);
    }

    public override void BeforeFirstRender()
    {
        Console.CursorVisible = false;
        _paddle1.MoveTo(0, 0);
        _paddle2.MoveTo(Console.BufferWidth - 1, 0);
        _ball.MoveTo(Console.BufferWidth / 2, Console.BufferHeight / 2);
    }

    public override void render(GameState state)
    {
        Console.Clear();

        foreach (var gameObject in gameItems)
        {
            gameObject.Update(state);
            gameObject.Draw();
            Console.ResetColor();
        }

        // Pausa 100ms mellan varje frame så spelet inte går för fort
        Thread.Sleep(100);
    }
}