public class GameOver : Scene
{
    public GameOver()
    {

    }

    public override void BeforeFirstRender()
    {
        Console.CursorVisible = true;
    }

    public override void render(GameState state)
    {
        var boxWidth = Console.BufferWidth / 2;
        var boxHeight = Console.BufferHeight / 2;

        int boxPosX = boxWidth / 2;
        int boxPosY = boxHeight / 2;

        var Box = new GUIBox(boxWidth, boxHeight, boxPosX, boxPosY);
        var HeaderText = new GUIText("GAME OVER", boxWidth - 2, 1, boxPosX + 3, boxPosY, AlignType.LEFT);

        int infoTextPosX = boxPosX;
        int infoTextPosy = boxPosY + boxHeight - 1;
        var InfoText = new GUIText("Press \"END\" to stop playing", boxWidth - 2, 1, infoTextPosX, infoTextPosy, AlignType.RIGHT);

        Box.Draw();
        HeaderText.Draw();
        InfoText.Draw();

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