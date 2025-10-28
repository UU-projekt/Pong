
var gameState = new GameState()
{
    state = StateOption.PLAYING,
    LeftPlayerName = "Människa",
    RightPlayerName = "Dator"
};

var factory = new DefaultGameObjectFactory();

var scenes = new Dictionary<StateOption, Scene>
{
    { StateOption.PLAYING, new GameScene(factory) },
    { StateOption.GAME_COMPLETED, new GameOver() }
};

Scene? lastScene = null;
while (gameState.state != StateOption.SHOULD_EXIT)
{
    var scene = scenes[gameState.state] ?? throw new NotImplementedException($"No scene exists for GameState \"{gameState.state}\"");
    if (lastScene == null || lastScene != scene) scene.BeforeFirstRender();

    scene.render(gameState);
    lastScene = scene;
}