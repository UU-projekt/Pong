
var gameState = new GameState("Människa", "Dator");
gameState.State = StateOption.MENU;

GenericFactory factory = new NormalMissileFactory();
if (new Random().NextDouble() > 0.9)
{
    factory = new DefectiveMissileFactory();
}

var scenes = new Dictionary<StateOption, Scene>
{
    { StateOption.PLAYING, new GameScene(factory) },
    { StateOption.GAME_COMPLETED, new GameOver() },
    { StateOption.LEADERBOARD, new LeaderboardScene() },
    { StateOption.MENU, new Menu() }
};

Scene? lastScene = null;
while (gameState.State != StateOption.SHOULD_EXIT)
{
    var scene = scenes[gameState.State] ?? throw new NotImplementedException($"No scene exists for GameState \"{gameState.State}\"");
    if (lastScene == null || lastScene != scene) scene.BeforeFirstRender(gameState);

    scene.render(gameState);
    lastScene = scene;
}