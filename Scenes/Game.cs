public class GameScene : Scene
{
    private readonly IGameObjectFactory _factory;
    private Paddle _paddle1 = null!;
    private Paddle _paddle2 = null!;
    private Ball _ball = null!;
    private readonly GameObjectCollection<GameObject> gameItems = new GameObjectCollection<GameObject>();

    public GameScene(IGameObjectFactory factory)
    {
        _factory = factory;
        InitObjects();
    }

    public void InitObjects()
    {
        gameItems.Clear();
        _ball = _factory.CreateBall();
        _paddle1 = _factory.CreatePaddle(PlayerType.HUMAN);
        _paddle2 = _factory.CreatePaddle(PlayerType.COMPUTER);

        gameItems.Add(_paddle1);
        gameItems.Add(_paddle2);
        gameItems.Add(_ball);

        _ball.SetPaddles(_paddle1, _paddle2);

        if (_paddle2.controller is CPUController cpu)
        {
            cpu.AttatchBall(_ball);
        }
    }

    public override void BeforeFirstRender()
    {
        Console.CursorVisible = false;
        InitObjects();
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