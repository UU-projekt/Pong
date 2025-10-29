public class DEV_GUITEST : Scene
{
    public GUIMenu menu;
    private string _selectValue = "";
    public DEV_GUITEST()
    {

    }

    public override void BeforeFirstRender(GameState _state)
    {
        Console.CursorVisible = true;
        var MenuBuilder = new MenuBuilder(Console.BufferWidth / 2, Console.BufferHeight / 2);
        MenuBuilder.AddText(header => header.SetText(" GUI TEST ").SetPosition(2, null).SetColours(ConsoleColor.White, ConsoleColor.Black));
        MenuBuilder.AddSelectMenu(sm => sm
        .SetLabel("SELECT")
        .AddItem("Test", "TEST1")
        .AddItem("Test 2", "TEST2")
        .SetPosition(2, 2)
        .BindValue(val => _selectValue = val)
        );

        menu = MenuBuilder.Build();
    }

    public override void render(GameState state)
    {
        menu.Draw();
        menu.Update(state);
        Thread.Sleep(100);
    }
}