public class LeaderboardScene : Scene
{
    private string _selectValue = "";
    private string _diffValue = "ANY";
    private GUIMenu menu = null!;

    public override void BeforeFirstRender(GameState state)
    {
        Console.CursorVisible = true;
        ReRender(state);
    }

    public void ReRender(GameState state)
    {
        int width = Console.BufferWidth - 10;
        int height = Console.BufferHeight - 10;
        var MenuBuilder = new MenuBuilder(width, height);
        MenuBuilder.AddText(header => header.SetText(" LEADERBOARD ").SetPosition(2, null).SetColours(ConsoleColor.White, ConsoleColor.Black));
        MenuBuilder.AddSelectMenu(sm => sm
            .SetLabel("Sort By")
            .AddItem("Bounces", "BOUNCES")
            .AddItem("Duration", "DURATION")
            .SetSelectedValue(_selectValue)
            .SetPosition(2, 2)
            .BindValue(val =>
            {
                _selectValue = val;
                ReRender(state);
                return;
            })
        );

        MenuBuilder.AddSelectMenu(sm => sm
        .SetLabel("Difficulty")
        .AddItem("any", "ANY")
        .AddItem("easy", "EASY")
        .AddItem("medium", "MEDIUM")
        .AddItem("hard", "HARD")
        .SetSelectedValue(_diffValue)
        .SetPosition(20, 2)
        .BindValue(val =>
        {
            _diffValue = val;
            ReRender(state);
            return;
        })
        );

        MenuBuilder.AddText(footer =>
            footer
            .SetText("PRESS \"HOME\" to return to game")
            .SetWidth("100%")
            .SetWidth(footer.Size.Width - 1)
            .SetAlign(AlignType.RIGHT)
            .SetOffsetVertical(1)
        );

        // KRAV #6:
        // 1: LINQs metod-syntax
        // 2: vi använder linq genom funktionerna OrderByDescending() och Where() på List<GameInformation> state.leaderboard
        // 3: Vi använder detta för att låta användare filtrera vårt leaderboard. Kanske vill användaren bara se spel på "hard"
        //    eller vill se spel med många studsar. Genom att använda linq här och sedan kedja ihop flera olika "calls" är det
        //    väldigt enkelt att bara få fram den information som användaren vill ha
        IEnumerable<GameInformation> leaderboard;
        if (_selectValue == "BOUNCES")
        {
            leaderboard = state.leaderboard.OrderByDescending(game => game.Bounces);
        }
        else
        {
            leaderboard = state.leaderboard.OrderByDescending(game => game.Duration);
        }

        leaderboard = _diffValue != "ANY" ? leaderboard.Where(game => game.Difficulty.ToString().ToUpper() == _diffValue) : leaderboard;

        var asString = leaderboard.Take(height - 2).Select(entry => $"{entry.Winner}: {entry.Bounces} bounces. Diff: {entry.Difficulty}. Took {entry.Duration}");
        MenuBuilder.AddText(text => text.SetText(string.Join(Environment.NewLine, asString)).SetWidth("50%").SetPosition(width / 2, 1).SetAlign(AlignType.LEFT));

        menu = MenuBuilder.Build();
        menu.OnPress += (key) =>
        {
            if (key.Key == ConsoleKey.Home)
            {
                state.State = StateOption.MENU;
            }
        };
    }

    public override void render(GameState state)
    {

        menu.Draw();
        menu.Update(state);
        Thread.Sleep(100);
    }
}