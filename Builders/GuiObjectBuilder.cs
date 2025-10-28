interface GUIBuilder
{
    public GameObject Build();
}

public class TextBuilder : GUIBuilder
{
    public (int Width, int Height) Size { get; private set; }
    public (int X, int Y) Position { get; private set; }
    private readonly MenuBuilder _parent;
    public AlignType alignType = AlignType.LEFT;
    public string? TextContent;

    public TextBuilder(MenuBuilder parent)
    {
        _parent = parent;
    }

    public (int X, int Y) GetCalculatedPosition()
    {
        int calcX = _parent.Position.X + Position.X;
        int calcY = _parent.Position.Y + Position.Y;

        return (calcX, calcY);
    }

    public TextBuilder SetWidth(int width)
    {
        Size = (width, Size.Height);
        return this;
    }

    public TextBuilder SetWidth(string width)
    {
        if (!width.EndsWith('%')) throw new ArgumentException("Must be percentage");
        string number = width[..^1];

        double percentage = double.Parse(number) / 100;

        Size = ((int)(_parent.Size.Width * percentage), Size.Height);
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

    public TextBuilder SetPosition(int? X, int? Y)
    {
        Position = (X ?? Position.X, Y ?? Position.Y);
        return this;
    }

    public TextBuilder SetOffset(int? offsetX, int? offsetY)
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
        return this;
    }

    public TextBuilder SetOffsetVertical(int offset)
    {
        SetOffset(null, offset);
        return this;
    }

    public TextBuilder SetOffsetHorizontal(int offset)
    {
        SetOffset(offset, null);
        return this;
    }

    public GameObject Build()
    {
        var thisPosition = GetCalculatedPosition();
        return new GUIText(TextContent ?? "<null>", Size.Width, Size.Height, thisPosition.X, thisPosition.Y, alignType);
    }
}

public class MenuBuilder
{
    public (int Width, int Height) Size { get; private set; }
    public (int X, int Y) Position { get; private set; }
    private readonly List<GUIBuilder> _items = new List<GUIBuilder>();
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

        Size = (Size.Width, (int)(Console.BufferWidth * percentage));
    }

    public void Center()
    {
        int posXCentered = Console.BufferWidth / 2 - Size.Width / 2;
        int posYCentered = Console.BufferHeight / 2 - Size.Height / 2;

        Position = (posXCentered, posYCentered);
    }

    public void AddText(Action<TextBuilder> configure)
    {
        var textBuilder = new TextBuilder(this);
        configure(textBuilder);
        _items.Add(textBuilder);
    }

    public List<GameObject> Build()
    {
        var bg = new GUIBox(Size.Width, Size.Height, Position.X, Position.Y);
        return [bg, .. _items.Select(b => b.Build())];
    }
}