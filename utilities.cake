public class ChainedArgument : IProcessArgument
{
    private readonly List<IProcessArgument> _arguments;

    public ChainedArgument()
    {
        _arguments = new List<IProcessArgument>();
    }

    public ChainedArgument Append(IProcessArgument argument)
    {
        _arguments.Add(argument);
        return this;
    }

    public string Render()
    {
        return string.Join(",", _arguments.Select(x => x.Render()));
    }

    public string RenderSafe()
    {
        return string.Join(",", _arguments.Select(x => x.RenderSafe()));
    }
}

public class SingleQuotedArgument : IProcessArgument
{
    private readonly IProcessArgument _argument;

    public SingleQuotedArgument(string text)
        : this(new TextArgument(text))
    {
    }

    public SingleQuotedArgument(IProcessArgument argument)
    {
        _argument = argument;
    }

    public string Render()
    {
        return string.Concat("'", _argument.Render(), "'");
    }

    public string RenderSafe()
    {
        return string.Concat("'", _argument.RenderSafe(), "'");
    }
}

public class KeyValueArgument : IProcessArgument
{
    private readonly string _key;
    private readonly IProcessArgument _value;

    public KeyValueArgument(string key, IProcessArgument value)
    {
        _key = key;
        _value = value;
    }

    public string Render()
    {
        return string.Concat(_key, "=", _value.Render());
    }

    public string RenderSafe()
    {
        return string.Concat(_key, "=", _value.RenderSafe());
    }
}