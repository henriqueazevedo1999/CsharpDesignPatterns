using System.Text;

namespace Strategy.Dynamic;

public enum OutputFormat
{
    Markdown,
    Html
}

public interface IListStrategy
{
    void Start(StringBuilder sb);
    void End(StringBuilder sb);
    void AddListItem(StringBuilder sb, string item);
}

public class HtmlListStrategy : IListStrategy
{
    public void Start(StringBuilder sb)
    {
        sb.AppendLine("<ul>");
    }

    public void End(StringBuilder sb)
    {
        sb.AppendLine("</ul>");
    }

    public void AddListItem(StringBuilder sb, string item)
    {
        sb.AppendLine($"     <li>{item}</li>");
    }
}

public class MarkdownListStrategy : IListStrategy
{
    public void Start(StringBuilder sb)
    {
        
    }

    public void End(StringBuilder sb)
    {
        throw new NotImplementedException();
    }

    public void AddListItem(StringBuilder sb, string item)
    {
        sb.AppendLine($" * {item}");
    }
}

public class TextProcessor
{
    private StringBuilder _sb = new();
    private IListStrategy _listStrategy;

    public void SetOutputFormat(OutputFormat outputFormat)
    {
        _listStrategy = outputFormat switch
        {
            OutputFormat.Markdown => new MarkdownListStrategy(),
            OutputFormat.Html => new HtmlListStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(outputFormat), outputFormat, null),
        };
    }

    public void AppendList(IEnumerable<string> items)
    {
        _listStrategy.Start(_sb);
        foreach (var item in items)
            _listStrategy.AddListItem(item);

        _listStrategy.End(_sb);
    }

    public StringBuilder Clear()
    {
        return _sb.Clear();
    }

    public override string ToString()
    {
        return _sb.ToString();
    }
}
