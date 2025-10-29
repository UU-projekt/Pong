public class GameOver : Scene
{
    static int WAIT_BEFORE_TAKING_INPUTS = 1000;
    static string FOOTER_TEXT = "Press \"END\" to stop playing";
    public GameOver()
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
        MenuBuilder.AddText(header => header.SetText(" GAME OVER ").SetPosition(2, null).SetColours(ConsoleColor.White, ConsoleColor.Black));
        MenuBuilder.AddText(info => info.SetText(infoString).SetPosition(2, 1).SetHeight(3));
        MenuBuilder.AddText(footer =>
            footer
                .SetText(FOOTER_TEXT)
                .SetWidth("100%")
                .SetWidth(footer.Size.Width - 1)
                .SetAlign(AlignType.RIGHT)
                .SetOffsetVertical(1));

        MenuBuilder.Build().ForEach(item => item.Draw());

        Thread.Sleep(WAIT_BEFORE_TAKING_INPUTS);
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.End)
        {
            Console.WriteLine("thanks for playing! :D");
            state.State = StateOption.SHOULD_EXIT;
        }
        else
        {
            state.State = StateOption.PLAYING;
        }
    }
}