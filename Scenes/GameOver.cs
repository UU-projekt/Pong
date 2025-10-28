public class GameOver : Scene
{
    static int WAIT_BEFORE_TAKING_INPUTS = 200;
    static string FOOTER_TEXT = "Press \"END\" to stop playing";
    public GameOver()
    {

    }

    public override void BeforeFirstRender()
    {
        Console.CursorVisible = true;
    }

    public override void render(GameState state)
    {
        var MenuBuilder = new MenuBuilder(Console.BufferWidth / 2, Console.BufferHeight / 2);
        MenuBuilder.Center();
        MenuBuilder.AddText(header => header.SetText("GAME OVER").SetPosition(2, null));
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
            state.state = StateOption.SHOULD_EXIT;
        }
        else
        {
            state.state = StateOption.PLAYING;
        }
    }
}