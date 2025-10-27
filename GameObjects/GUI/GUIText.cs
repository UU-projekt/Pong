public enum AlignType
{
    LEFT,
    CENTER,
    RIGHT
}

public class GUIText : GameObject
{
    public readonly AlignType align;
    public string Text;
    public GUIText(string text, int width, int height, int posX, int posY, AlignType alignMode)
        : base(width, height)
    {
        align = alignMode;
        Text = text;
        MoveTo(posX, posY);
    }

    public override void Draw()
    {
        switch (align)
        {
            case AlignType.LEFT:
                Console.SetCursorPosition((int)Position.X, (int)Position.Y);
                break;
            case AlignType.RIGHT:
                int rightBoundPosition = (int)Position.X + (int)Size.Width;
                Console.SetCursorPosition(rightBoundPosition - Text.Length, (int)Position.Y);
                break;
            case AlignType.CENTER:
                int textLenDiv = Text.Length / 2;
                int centerPosition = (int)Position.X + (int)(Size.Width / 2);
                Console.SetCursorPosition(centerPosition - textLenDiv, (int)Position.Y);
                break;
        }
        Console.WriteLine(Text);
    }

    public override void Update(GameState state)
    {
        // N/A
    }
}
