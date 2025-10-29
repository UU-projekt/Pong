public interface IGUIBuilder
{
    GameObject Build();
}

public abstract class GUIBuilder<TBuilder> : IGUIBuilder
    where TBuilder : GUIBuilder<TBuilder>
{
    public (int Width, int Height) Size { get; protected set; } = (0, 0);
    public (int X, int Y) Position { get; protected set; } = (0, 0);
    public abstract GameObject Build();
    protected readonly MenuBuilder _parent;

    public GUIBuilder(MenuBuilder parent)
    {
        _parent = parent;
    }

    public (int X, int Y) GetCalculatedPosition()
    {
        int calcX = _parent.Position.X + Position.X;
        int calcY = _parent.Position.Y + Position.Y;

        return (calcX, calcY);
    }

    public TBuilder SetSize(int? width, int? height)
    {
        Size = (width ?? Size.Width, height ?? Size.Height);
        return (TBuilder)this;
    }

    public TBuilder SetWidth(int width)
    {
        SetSize(width, null);
        return (TBuilder)this;
    }

    public TBuilder SetHeight(int height)
    {
        SetSize(null, height);
        return (TBuilder)this;
    }

    public TBuilder SetWidth(string width)
    {
        if (!width.EndsWith('%')) throw new ArgumentException("Must be percentage");
        string number = width[..^1];

        double percentage = double.Parse(number) / 100;

        Size = ((int)(_parent.Size.Width * percentage), Size.Height);
        return (TBuilder)this;
    }

    public TBuilder SetPosition(int? X, int? Y)
    {
        Position = (X ?? Position.X, Y ?? Position.Y);
        return (TBuilder)this;
    }

    public TBuilder SetOffset(int? offsetX, int? offsetY)
    {
        int newPosX = Position.X;
        if (offsetX is not null)
        {
            newPosX = (int)(_parent.Size.Width - offsetX);
        }

        int newPosY = Position.Y;
        if (offsetY is not null)
        {
            newPosY = (int)(_parent.Size.Height - offsetY);
        }

        Position = (newPosX, newPosY);
        return (TBuilder)this;
    }

    public TBuilder SetOffsetVertical(int offset)
    {
        SetOffset(null, offset);
        return (TBuilder)this;
    }

    public TBuilder SetOffsetHorizontal(int offset)
    {
        SetOffset(offset, null);
        return (TBuilder)this;
    }
}

public class TextBuilder : GUIBuilder<TextBuilder>
{
    public (ConsoleColor Background, ConsoleColor Foreground) Colours = (ConsoleColor.Black, ConsoleColor.White);
    public AlignType alignType = AlignType.LEFT;
    public string? TextContent;

    public TextBuilder(MenuBuilder parent) : base(parent)
    {

    }

    public TextBuilder SetColours(ConsoleColor? background, ConsoleColor? foreground)
    {
        Colours = (background ?? Colours.Background, foreground ?? Colours.Foreground);
        return this;
    }

    public TextBuilder SetForegroundColour(ConsoleColor foreground)
    {
        SetColours(null, foreground);
        return this;
    }

    public TextBuilder SetBackgroundColour(ConsoleColor background)
    {
        SetColours(background, null);
        return this;
    }

    public TextBuilder SetText(string text)
    {
        TextContent = text;
        return this;
    }

    public TextBuilder SetAlign(AlignType alignTypeYes)
    {
        alignType = alignTypeYes;
        return this;
    }

    public override GameObject Build()
    {
        var thisPosition = GetCalculatedPosition();
        return new GUIText(TextContent ?? "<null>", Size.Width, Size.Height, thisPosition.X, thisPosition.Y, alignType) { FGColour = Colours.Foreground, BGColour = Colours.Background };
    }
}

public class SelectMenuBuilder : GUIBuilder<SelectMenuBuilder>
{
    public string? Label;
    private List<(string Name, string Value)> _values = [];

    public SelectMenuBuilder(MenuBuilder parent) : base(parent)
    {

    }

    public SelectMenuBuilder SetLabel(string label)
    {
        Label = label;
        return this;
    }

    public SelectMenuBuilder AddItem(string Name, string Value)
    {
        _values.Add((Name, Value));
        return this;
    }

    public override GameObject Build()
    {
        var (X, Y) = GetCalculatedPosition();
        var SelectObject = new GUISelect(Label ?? "Select Menu", Size.Width, Size.Height, X, Y);
        _values.ForEach((Item) => SelectObject.AddItem(Item.Name, Item.Value));
        return SelectObject;
    }
}

public class MenuBuilder
{
    public (int Width, int Height) Size { get; private set; } = (0, 0);
    public (int X, int Y) Position { get; private set; } = (0, 0);
    private readonly List<IGUIBuilder> _items = [];
    public MenuBuilder(int width, int height)
    {
        Size = (width, height);
    }

    public void SetWidth(int width)
    {
        Size = (width, Size.Height);
    }

    public void SetWidth(string width)
    {
        if (!width.EndsWith('%')) throw new ArgumentException("Must be percentage");
        string number = width[..^1];

        double percentage = double.Parse(number) / 100;

        Size = ((int)(Console.BufferWidth * percentage), Size.Height);
    }

    public void SetHeight(int height)
    {
        Size = (Size.Width, height);
    }

    public void SetHeight(string height)
    {
        if (!height.EndsWith('%')) throw new ArgumentException("Must be percentage");
        string number = height[..^1];

        double percentage = double.Parse(number) / 100;

        Size = (Size.Width, (int)(Console.BufferHeight * percentage));
    }

    public void Center()
    {
        int posXCentered = Console.BufferWidth / 2 - Size.Width / 2;
        int posYCentered = Console.BufferHeight / 2 - Size.Height / 2;

        Position = (posXCentered, posYCentered);
    }

    private void AddGeneric<T>(Action<T> configure) where T : GUIBuilder<T>
    {
        T builder = (T)Activator.CreateInstance(typeof(T), this)!;
        configure(builder);
        _items.Add(builder);
    }
    public void AddText(Action<TextBuilder> configure) => AddGeneric(configure);
    public void AddSelectMenu(Action<SelectMenuBuilder> configure) => AddGeneric(configure);

    public List<GameObject> Build()
    {
        var bg = new GUIBox(Size.Width, Size.Height, Position.X, Position.Y);
        return [bg, .. _items.Select(b => b.Build())];
    }
}