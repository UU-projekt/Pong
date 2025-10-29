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
    public ConsoleColor FGColour = Console.ForegroundColor;
    public ConsoleColor BGColour = Console.BackgroundColor;
    public GUIText(string text, int width, int height, int posX, int posY, AlignType alignMode)
        : base(width, height)
    {
        align = alignMode;
        Text = text;
        MoveTo(posX, posY);
    }

    private void DrawLine(string line, int offset)
    {
        switch (align)
        {
            case AlignType.LEFT:
                Console.SetCursorPosition(Position.X, Position.Y + offset);
                break;
            case AlignType.RIGHT:
                int rightBoundPosition = Position.X + Size.Width;
                Console.SetCursorPosition(rightBoundPosition - line.Length, Position.Y + offset);
                break;
            case AlignType.CENTER:
                int textLenDiv = line.Length / 2;
                int centerPosition = Position.X + (Size.Width / 2);
                Console.SetCursorPosition(centerPosition - textLenDiv, Position.Y + offset);
                break;
        }
        Console.WriteLine(line);
    }

    public override void Draw()
    {
        Console.ForegroundColor = FGColour;
        Console.BackgroundColor = BGColour;

        var lines = Text.Split(Environment.NewLine);
        for (int i = 0; i < lines.Length; i++)
        {
            DrawLine(lines[i], i);
        }

        Console.ResetColor();
    }

    public override void Update(GameState state)
    {
        // N/A
    }
}
