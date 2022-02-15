namespace DotNetNB.Security.Core.Models;


public class Permission : Permission<object>
{


}

public class Permission<T> where T : class
{
    public string Key { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public string Group { get; set; }

    public IEnumerable<Resource> Resources { get; set; }

    public T Data { get; set; }
}