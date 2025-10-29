public class GUISelect : GameObject
{
    public readonly AlignType align;
    public string Text;
    public ConsoleColor FGColour = Console.BackgroundColor;
    public ConsoleColor BGColour = Console.BackgroundColor;
    public GUISelect(string text, int width, int height, int posX, int posY, AlignType alignMode)
        : base(width, height)
    {
        align = alignMode;
        Text = text;
        MoveTo(posX, posY);
    }

    public override void Draw()
    {

    }

    public override void Update(GameState state)
    {
        // N/A
    }
}
