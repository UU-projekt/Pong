public class Menu : Scene
{
    private string _selectValue = "EASY";
    private GUIMenu menu = null!;

    public override void BeforeFirstRender(GameState state)
    {
        Console.CursorVisible = true;
        Console.Clear();
        ReRender(state);
    }

    public void ReRender(GameState state)
    {
        int width = Console.BufferWidth / 2;
        int height = Console.BufferHeight / 2;
        var MenuBuilder = new MenuBuilder(width, height);
        MenuBuilder.AddText(header => header.SetText(" START GAME ").SetPosition(2, null).SetColours(ConsoleColor.White, ConsoleColor.Black));
        MenuBuilder.AddSelectMenu(sm => sm
            .SetLabel("Difficulty")
            .AddItem("Easy", "EASY")
            .AddItem("Medium", "MEDIUM")
            .AddItem("Hard", "HARD")
            .SetSelectedValue(_selectValue)
            .SetPosition(2, 2)
            .BindValue(val =>
            {
                _selectValue = val;
            })
        );
        MenuBuilder.AddText(footer =>
            footer
            .SetText("press \"ENTER\" to start game and \"END\" to end game")
            .SetWidth("100%")
            .SetWidth(footer.Size.Width - 1)
            .SetAlign(AlignType.RIGHT)
            .SetOffsetVertical(1)
        );

        menu = MenuBuilder.Build();
        menu.OnPress += (key) =>
        {
            if (key.Key == ConsoleKey.End)
            {
                state.State = StateOption.SHOULD_EXIT;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                // START GAME
                state.StartGame(Helper.DifficultyString[_selectValue]);
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