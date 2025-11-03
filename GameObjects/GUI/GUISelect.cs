public struct SelectMenuItem
{
    public string Name;
    public string Value;
}

public class GUISelect : GameObject, IGuiInteractiveElement
{
    static readonly char UNFILLED_DOT = '○';
    static readonly char FILLED_DOT = '●';
    public List<SelectMenuItem> Items = [];
    public string Label;
    private int _pointer = 0;
    private int pointer
    {
        get { return _pointer; }
        set
        {
            _pointer = value;
            OnSelectionChanged?.Invoke(Items[_pointer].Value);
        }
    }
    public Action<string>? OnSelectionChanged;

    public GUISelect(string text, int width, int height, int posX, int posY)
        : base(width, height)
    {
        Label = text;
        MoveTo(posX, posY);
    }

    public void AddItem(SelectMenuItem item)
    {
        Items.Add(item);
    }

    public void SetSelected(string selected)
    {
        _pointer = Items.FindIndex(s => s.Value == selected);
    }

    public void AddItem(string name, string value) => AddItem(new SelectMenuItem { Name = name, Value = value });

    public void SetPointer(int pointer)
    {
        if (pointer > Items.Count) throw new ArgumentOutOfRangeException($"New pointer exceeds Item count ({Items.Count})");
        this.pointer = pointer;
    }

    public override void Draw()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.WriteLine(Label);
        int longestLine = 0;

        for (int i = 0; i < Items.Count; i++)
        {
            char dot = i == pointer ? FILLED_DOT : UNFILLED_DOT;
            int offset = 2 + i;
            Console.SetCursorPosition(Position.X, Position.Y + offset);
            string selectItemLine = $"{dot} {Items[i].Name}";
            Console.WriteLine(selectItemLine);
            longestLine = Math.Max(longestLine, selectItemLine.Length);
        }

        Console.SetCursorPosition(Position.X, Position.Y + 1);
        Console.WriteLine(new string('=', Size.Width is not 0 ? Size.Width : longestLine));
    }

    public void Next()
    {
        pointer = Math.Min(pointer + 1, Items.Count - 1);
    }

    public void Prev()
    {
        pointer = Math.Max(pointer - 1, 0);
    }

    public void Update(GameState state, ConsoleKeyInfo keydown)
    {
        switch (keydown.Key)
        {
            case ConsoleKey.UpArrow:
                Prev();
                break;
            case ConsoleKey.DownArrow:
                Next();
                break;
        }
    }

    public override void Update(GameState state)
    {
        // N/A
    }
}
