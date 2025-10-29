
public class GUIMenu : GameObject
{
    public GUIMenu(int width, int height)
        : base(width, height)
    {
        int posX = Console.BufferWidth / 2 - width / 2;
        int posY = Console.BufferHeight / 2 - height / 2;
        MoveTo(posX, posY);
    }
    public override void Draw()
    {
        var bg = new GUIBox(Size.Width, Size.Height, Position.X, Position.Y);
        bg.Draw();


    }

    public override void Update(GameState state)
    {
        // N/A
    }
}
