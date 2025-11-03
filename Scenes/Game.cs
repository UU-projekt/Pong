public class GameScene : Scene
{
    private readonly IGameObjectFactory _gameObjectFactory;
    private readonly GenericFactory missileFactory;
    private Paddle _paddle1 = null!;
    private Paddle _paddle2 = null!;
    private Ball _ball = null!;
    private readonly GameObjectCollection<GameObject> gameItems = new GameObjectCollection<GameObject>();
    private readonly GameObjectCollection<Projectile> projectiles = new GameObjectCollection<Projectile>();
    private DateTime GameStarted;

    public GameScene(GenericFactory boomFactory)
    {
        _gameObjectFactory = new DefaultGameObjectFactory();
        missileFactory = boomFactory;
        InitObjects();
    }

    public void InitObjects()
    {
        gameItems.Clear();
        projectiles.Clear();
        GameStarted = DateTime.Now;
        _ball = _gameObjectFactory.CreateBall();
        _paddle1 = _gameObjectFactory.CreatePaddle(PlayerType.HUMAN);
        _paddle2 = _gameObjectFactory.CreatePaddle(PlayerType.COMPUTER);

        gameItems.Add(_paddle1);
        gameItems.Add(_paddle2);
        gameItems.Add(_ball);

        _ball.SetPaddles(_paddle1, _paddle2);
    }

    public override void BeforeFirstRender(GameState state)
    {
        Console.CursorVisible = false;
        InitObjects();

        _paddle1.OnFire = () =>
        {
            var missile = missileFactory.CreateMissile(_paddle1, _paddle2);
            projectiles.Add(missile);
        };

        _paddle2.OnFire = () =>
        {
            var missile = missileFactory.CreateMissile(_paddle2, _paddle1);
            projectiles.Add(missile);
        };

        var ctrl = new CPUController(state.difficulty);
        ctrl.AttatchBall(_ball);
        _paddle2.controller = ctrl;

        _ball.OnScored += direction =>
        {
            var diff = DateTime.Now - GameStarted;
            state.SetWinner(direction, _ball.Bounces, diff);
        };
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

        foreach (var missile in projectiles.Where(p => p.IsActive))
        {
            missile.Update(state);
            missile.Draw();
            Console.ResetColor();
        }

        // Pausa 100ms mellan varje frame så spelet inte går för fort
        Thread.Sleep(100);
    }
}