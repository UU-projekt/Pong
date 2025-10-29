public class DEV_GUITEST : Scene
{
    public DEV_GUITEST()
    {

    }

    public override void BeforeFirstRender(GameState _state)
    {
        Console.CursorVisible = true;
    }

    public override void render(GameState state)
    {
        string timeString = $"{state.LastGame.Duration.TotalSeconds:N2} seconds";

        string infoString = $@"
Winner: {state.LastGame.Winner}
Bounces: {state.LastGame.Bounces}
Duration: {timeString}
        ";

        var MenuBuilder = new MenuBuilder(Console.BufferWidth / 2, Console.BufferHeight / 2);
        MenuBuilder.Center();
        MenuBuilder.AddText(header => header.SetText(" GUI TEST ").SetPosition(2, null).SetColours(ConsoleColor.White, ConsoleColor.Black));
        MenuBuilder.AddSelectMenu(sm => sm.SetLabel("SELECT").AddItem("Test", "TEST").AddItem("Test 2", "TEST2").SetPosition(2, 2));

        MenuBuilder.Build().ForEach(item => item.Draw());

        Console.ReadKey();
    }
}