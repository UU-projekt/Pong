
public class GUIMenu : GameObject
{

    public List<GameObject> gameObjects = new();
    private int pointer = 0;
    private GameObject? selectedObject;
    public event Action<ConsoleKeyInfo>? OnPress;

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

        foreach (var gameObject in gameObjects)
        {
            if (gameObject == selectedObject) Console.ForegroundColor = ConsoleColor.Cyan;
            gameObject.Draw();
            Console.ResetColor();
        }
    }

    public void PreviousItem()
    {
        pointer = Math.Max(pointer - 1, 0);
    }

    public void NextItem()
    {
        pointer = Math.Min(pointer + 1, gameObjects.Count - 1);
    }

    public override void Update(GameState state)
    {
        var key = Console.ReadKey();

        if (key.Modifiers.HasFlag(ConsoleModifiers.Alt))
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    PreviousItem();
                    break;
                case ConsoleKey.DownArrow:
                    NextItem();
                    break;
            }

            var go = gameObjects[pointer];
            if (go is IGuiInteractiveElement) selectedObject = go;
            return;
        }

        OnPress?.Invoke(key);

        var gameObject = gameObjects[pointer];
        if (gameObject is IGuiInteractiveElement) ((IGuiInteractiveElement)gameObject).Update(state, key);
    }
}
