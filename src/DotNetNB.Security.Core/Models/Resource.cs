namespace DotNetNB.Security.Core.Models;

public class Resource
{
    public string Key { get; set; }

    public string Group { get; set; }

    public string Type { get; set; }

    public object Data { get; set; }
}