
public class GUIBox : GameObject
{
    public GUIBox(int width, int height, int posX, int posY)
        : base(width, height)
    {
        MoveTo(posX, posY);
    }
    public override void Draw()
    {
        for (int i = 0; i <= Size.Height; i++)
        {
            int yPosition = (int)Position.Y + i;
            Console.SetCursorPosition((int)Position.X, yPosition);

            string fill = new String(' ', (int)Size.Width - 2);
            string line = $"▌{fill}▐";

            if (i == 0)
            {
                string fillTop = new string('▀', (int)Size.Width - 2);
                line = $"▛{fillTop}▜";
            }
            if (i == Size.Height)
            {
                string fillTop = new string('▄', (int)Size.Width - 2);
                line = $"▙{fillTop}▟";
            }

            Console.WriteLine(line);
        }
    }

    public override void Update(GameState state)
    {
        // N/A
    }
}
