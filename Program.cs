
var gameState = new GameState("Människa", "Dator");
gameState.State = StateOption.__DEV_GUITEST;

var factory = new DefaultGameObjectFactory();

var scenes = new Dictionary<StateOption, Scene>
{
    { StateOption.PLAYING, new GameScene(factory) },
    { StateOption.GAME_COMPLETED, new GameOver() },
    { StateOption.__DEV_GUITEST, new DEV_GUITEST() }
};

Scene? lastScene = null;
while (gameState.State != StateOption.SHOULD_EXIT)
{
    var scene = scenes[gameState.State] ?? throw new NotImplementedException($"No scene exists for GameState \"{gameState.State}\"");
    if (lastScene == null || lastScene != scene) scene.BeforeFirstRender(gameState);

    scene.render(gameState);
    lastScene = scene;
}